using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Services;
using Jda.WfmEssApi.Common;
using Jda.WfmEssApi.ErrorLogging.ErrorResponses;
using log4RP;
using Newtonsoft.Json;
using Perigee;
using Perigee.ErrorLogging;
using Perigee.GlobalErrorHandling;
using RP.DomainModel.Common;
using RP.DomainModel.Common.NHibernateAbstracts;
using RP.DomainModel.Common.UnitOfWork.Diagnostics;
using RPCore;
using RPCore.Contracts;

namespace Jda.WfmEssApi.ErrorLogging
{
  public class WfmEssApiAspNetErrorLoggingFilter : AspNetErrorLoggingFilter
  {
    private static readonly Logger _Logger = new Logger(typeof(WfmEssApiAspNetErrorLoggingFilter));
    private static readonly string _JsonReaderErrorCode = "JsonReader.DeserializationFailed";
    private static readonly string _DevMessageNoArgumentsText = "No error arguments available.";
    private static readonly string _WebServicesPageFileExtension = ".asmx";
    private static readonly Type _DomainOperationExceptionType = typeof(DomainOperationException);
    private static readonly Type _CustomApplicationExceptionType = typeof(CustomApplicationException);

    public override void OnException(HttpActionExecutedContext actionExecutedContext)
    {
      var oldAsmxWebServiceCall = HttpContextManager.Current.Request.AppRelativeCurrentExecutionFilePath != null &&
        HttpContextManager.Current.Request.AppRelativeCurrentExecutionFilePath.EndsWith(
          _WebServicesPageFileExtension, StringComparison.OrdinalIgnoreCase);
      if (oldAsmxWebServiceCall)
      {
        base.OnException(actionExecutedContext);
        return;
      }

      HandleException(actionExecutedContext);
    }

    protected static void HandleException(HttpActionExecutedContext actionExecutedContext)
    {
      var exceptionThatOccurred = actionExecutedContext.Exception;
      if (IsJsonReaderException(exceptionThatOccurred))
      {
        HandleApiJsonReaderException(actionExecutedContext);
        return;
      }
      else if (ImplementsDomainOperationException(exceptionThatOccurred))
      {
        HandleDomainOperationException(actionExecutedContext);
        return;
      }
      else if (ImplementsCustomApplicationException(exceptionThatOccurred))
      {
        HandleCustomApplicationException(actionExecutedContext);
        return;
      }
      HandleEveryOtherException(actionExecutedContext);
    }

    private static bool ImplementsDomainOperationException(Exception exceptionThrown)
    {
      var exceptionType = exceptionThrown.GetType();
      var returnVal = (Check.ImplementsInterface(_DomainOperationExceptionType, exceptionType));
      return returnVal;
    }

    private static bool ImplementsCustomApplicationException(Exception exceptionThrown)
    {
      var exceptionType = exceptionThrown.GetType();
      var returnValue = (Check.ImplementsInterface(_CustomApplicationExceptionType, exceptionType));
      return returnValue;
    }

    private static bool IsJsonReaderException(Exception exceptionThrown)
    {
      var returnValue = (exceptionThrown is JsonReaderException)
        || (exceptionThrown is FormatException) ||
        (exceptionThrown is JsonSerializationException);
      return returnValue;
    }
    private static void HandleCustomApplicationException(HttpActionExecutedContext actionExecutedContext)
    {
      var exceptionThatOccurred = actionExecutedContext.Exception as CustomApplicationException;
      var errorResponseIfFoundOrNull =
         GetFirstMatchingErrorResponse(actionExecutedContext, exceptionThatOccurred.ApplicationException);
      GenerateAndLogApiErrorContainer(actionExecutedContext, exceptionThatOccurred, errorResponseIfFoundOrNull);
    }

    private static void HandleDomainOperationException(HttpActionExecutedContext actionExecutedContext)
    {
      var exceptionThatOccurred = actionExecutedContext.Exception as DomainOperationException;
      var errorResponseIfFoundOrNull = GetFirstMatchingErrorResponse(actionExecutedContext, exceptionThatOccurred.OperationException);
      var noMappedErrorResponse = errorResponseIfFoundOrNull == null;
      if (noMappedErrorResponse && IsDataStorageException(exceptionThatOccurred))
      {
        var castException = exceptionThatOccurred as DomainOperationException<DataStorageErrorCodes>;
        errorResponseIfFoundOrNull = GetErrorResponseForDataStorageException(castException);
      }
      GenerateAndLogApiErrorContainer(actionExecutedContext, exceptionThatOccurred, errorResponseIfFoundOrNull);
    }

    private static void GenerateAndLogApiErrorContainer(HttpActionExecutedContext actionExecutedContext,
      Exception exceptionThatOccurred, IErrorResponse errorResponseIfFoundOrNull)
    {
      var noMatchingErrorResponse = errorResponseIfFoundOrNull == null;
      if (noMatchingErrorResponse)
      {
        errorResponseIfFoundOrNull = new UnmappedException();
      }
      var errorCode = Helpers.EnumHelper.ConvertEnumToString(errorResponseIfFoundOrNull.ErrorCode);
      var statusCode = errorResponseIfFoundOrNull.StatusCode;
      var message = errorResponseIfFoundOrNull.Scenario;
      var container = CreateErrorContainer(exceptionThatOccurred, statusCode, errorCode, message);
      var response = actionExecutedContext.Request.CreateResponse(statusCode, container);
      actionExecutedContext.Response = response;
      LogException(container, exceptionThatOccurred, statusCode);
    }

   private static IErrorResponse GetErrorResponseForDataStorageException(
      DomainOperationException<DataStorageErrorCodes> exception)
    {
      var useNotFoundErrorResponse = exception.OperationException.ToString() == DataStorageErrorCodes.EntityNotFound.ToString();
      if (useNotFoundErrorResponse)
      {
        return new DataStorageEntityNotFound();
      }
      return new DataStorageAllOthers();
    }

    private static bool IsDataStorageException(Exception exceptionThrown)
    {
      var isDataStorageException = exceptionThrown is DomainOperationException<DataStorageErrorCodes>;
      return isDataStorageException;
    }

    private static IErrorResponse GetFirstMatchingErrorResponse
      (HttpActionExecutedContext actionExecutedContext, object errorCode)
    {
      var errorResponses = GetErrorResponsesIfFoundOrNull(actionExecutedContext);
      var couldNotGetErrorResponses = errorResponses == null;
      var errorResponseIfFoundOrNull = (couldNotGetErrorResponses)
        ? null
        : (from responseType in errorResponses
           where responseType.SourceErrorCode.Equals(errorCode)
           select responseType).FirstOrDefault();
      return errorResponseIfFoundOrNull;
    }

    private static IEnumerable<IErrorResponse> GetErrorResponsesIfFoundOrNull(
      HttpActionExecutedContext actionExecutedContext)
    {
      var attribute = GetErrorResponseAttribute(actionExecutedContext)?.FirstOrDefault();
      var errorResponses = (IEnumerable<IErrorResponse>)attribute?.ErrorResponses;
      return errorResponses;
    }

    private static IEnumerable<ErrorResponsesAttribute> GetErrorResponseAttribute(HttpActionExecutedContext actionExecutedContext)
    {
      ReflectedHttpActionDescriptor reflectedHttpActionDescriptor;
      IEnumerable<ErrorResponsesAttribute> thisErrorResponses;

      var wrapper = actionExecutedContext.ActionContext.ActionDescriptor as IDecorator<HttpActionDescriptor>;
      var isUsingDecoratorInterface = wrapper != null;
      if (isUsingDecoratorInterface)
      {
        reflectedHttpActionDescriptor = wrapper.Inner as ReflectedHttpActionDescriptor;
      }
      else
      {
        reflectedHttpActionDescriptor = actionExecutedContext.ActionContext.ActionDescriptor as ReflectedHttpActionDescriptor;
      }

      var descriptorIsFound = reflectedHttpActionDescriptor != null;
      if (descriptorIsFound)
      {
        thisErrorResponses = reflectedHttpActionDescriptor?.GetCustomAttributes<ErrorResponsesAttribute>();
      }
      else
      {
        var justActionDescriptor = actionExecutedContext.ActionContext.ActionDescriptor;
        thisErrorResponses = justActionDescriptor?.GetCustomAttributes<ErrorResponsesAttribute>();
      }
      return thisErrorResponses;
    }

    protected static void HandleEveryOtherException(HttpActionExecutedContext actionExecutedContext)
    {
      var exceptionThatOccurred = actionExecutedContext.Exception;
      var errorResponse = GenerateSystemExceptionErrorResponse(exceptionThatOccurred);

      var statusCode = errorResponse.StatusCode;
      var errorCode = Helpers.EnumHelper.ConvertEnumToString(errorResponse.ErrorCode);
      var message = errorResponse.Scenario;
      var container = CreateErrorContainer(exceptionThatOccurred, statusCode, errorCode, message);
      var response = actionExecutedContext.Request.CreateResponse(statusCode, container);
      actionExecutedContext.Response = response;
      LogException(container, exceptionThatOccurred, statusCode);
    }

    private static ApiErrorContainerV2 CreateErrorContainer(Exception exceptionThrown, HttpStatusCode statusCode,
     string errorCode, string message)
    {
      var devMessage = GenerateDevMessage(exceptionThrown);
      var container = new ApiErrorContainerV2(message, errorCode, devMessage);
      return container;
    }
    private static string GenerateDevMessage(Exception exceptionThatOccurred)
    {
      var errorArguments = _DevMessageNoArgumentsText;
      var isExceptionThatOccurredOneOfOurExceptions = IsOneOfOurExceptions(exceptionThatOccurred);
      if (isExceptionThatOccurredOneOfOurExceptions)
      {
        var exceptionAsSerializable = (ISerializableException)exceptionThatOccurred;
        var exceptionThatOccurredHasArguments = exceptionAsSerializable.Arguments.Count != 0;
        if (exceptionThatOccurredHasArguments)
        {
          var serializedErrorArguments = "Error Arguments: " + SerializeDevMessage(exceptionAsSerializable.Arguments);
          errorArguments = serializedErrorArguments;
        }
      }
      var devMessage = errorArguments;
      return devMessage;
    }

    private static string SerializeDevMessage(IDictionary<string, object> devMessage)
    {
      var delimeter = ", ";
      var array = new List<string>();
      foreach (var kvp in devMessage)
      {
        array.Add(SafeToString(kvp.Key) + " = " + SafeToString(kvp.Value));
      }
      return string.Join(delimeter, array);
    }

    private static string SafeToString(object x)
    {
      if (x == null)
      {
        return "(null)";
      }

      try
      {
        return x.ToString();
      }
      catch (Exception)
      {
        return x.GetType().Name + " ToString() threw an exception";
      }
    }


    protected static void HandleApiJsonReaderException(HttpActionExecutedContext actionExecutedContext)
    {
      var exceptionThatOccurred = actionExecutedContext.Exception;
      var statusCode = HttpStatusCode.BadRequest;
      var message = exceptionThatOccurred.Message;
      var container = CreateErrorContainer(exceptionThatOccurred, statusCode, _JsonReaderErrorCode, message);
      var response = actionExecutedContext.Request.CreateResponse(statusCode, container);
      actionExecutedContext.Response = response;
      LogException(container, exceptionThatOccurred, statusCode);
    }

    private static IErrorResponse GenerateSystemExceptionErrorResponse(Exception exception)
    {
      if (IsUnsupportedMediaTypeException(exception))
      {
        return new UnsupportedMediaTypeResponse()
        {
          Scenario = exception.Message
        };
      }

      if (IsUnauthorizedAccessException(exception))
      {
        return new UnauthorizedExceptionResponse();
      }

      if (IsOneOfOurExceptions(exception))
      {
        var errorResponse = new UnmappedException();
        return errorResponse;
      }

      var defaultErrorResponse = new OopsErrorResponse();
      return defaultErrorResponse;
    }

    private static bool IsUnsupportedMediaTypeException(Exception exceptionThrown)
    {
      var exceptionIsUnsupportedMediaType = exceptionThrown is UnsupportedMediaTypeException;
      return exceptionIsUnsupportedMediaType;
    }

    private static bool IsUnauthorizedAccessException(Exception exceptionThrown)
    {
      var exceptionIsUnauthorizedAccess = exceptionThrown is UnauthorizedAccessException;
      return exceptionIsUnauthorizedAccess;
    }

    private static bool IsOneOfOurExceptions(Exception exceptionThrown)
    {
      var isOneOfOurExceptions = exceptionThrown is ISerializableException;
      return isOneOfOurExceptions;
    }
    protected static HttpStatusCode GetStatusCodeForDataStorageTypeException(
      DomainOperationException<DataStorageErrorCodes> dataStorageException)
    {
      var exceptionIsEntityNotFound =
        dataStorageException.OperationExceptionType == DataStorageErrorCodes.EntityNotFound.ToString();
      return exceptionIsEntityNotFound ? HttpStatusCode.NotFound : HttpStatusCode.BadRequest;
    }

    protected static void LogException(ApiErrorContainerV2 container, Exception exceptionThatOccurred, HttpStatusCode statusCode)
    {
      var logLevel = DetermineLoggingLevel(statusCode);
      switch (logLevel)
      {
        case LoggingLevel.Error:
          _Logger.Error(container, exceptionThatOccurred);
          break;
        case LoggingLevel.Warning:
          _Logger.Warn(container, exceptionThatOccurred);
          break;
        default:
          _Logger.Error(container, exceptionThatOccurred);
          break;
      }
    }
    private static LoggingLevel DetermineLoggingLevel(HttpStatusCode statusCode)
    {
      var statusCodeAsInt = Convert.ToInt32(statusCode);
      var statusCodeIsIn4Xx = statusCodeAsInt >= 400 && statusCodeAsInt < 500;
      if (statusCodeIsIn4Xx)
      {
        return LoggingLevel.Warning;
      }
      return LoggingLevel.Error;
    }


    protected static void ThrowDiagnosticExceptionsIfNeeded(HttpActionExecutedContext actionExecutedContext)
    {
      DiagnosticExceptionType requestedExceptionType;
      var diagnosticString = actionExecutedContext.Request.GetQueryNameValuePairs()
        .SingleOrDefault(kv => kv.Key.Equals("runDiagnostic")).Value;
      var nonDefaultExceptionRequested = Enum.TryParse(diagnosticString, out requestedExceptionType);

      var exceptionRequestedInFilter = requestedExceptionType.Equals(DiagnosticExceptionType.ExceptionInFilter)
        && nonDefaultExceptionRequested;

      if (exceptionRequestedInFilter)
      {
        throw new Exception("Exception Thrown From Diagnostic Error Filter");
      }
    }

    private enum LoggingLevel
    {
      Warning,
      Error,
      Unknown
    }

  }
}