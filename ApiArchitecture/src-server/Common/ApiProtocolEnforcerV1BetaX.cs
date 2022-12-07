using log4RP;
using Perigee;
using Perigee.GlobalErrorHandling;
using Perigee.RequiredREFSInterfaces;
using RP.DomainModel.Common;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Routing;

namespace Jda.WfmEssApi.Common
{
  public class ApiProtocolEnforcerV1BetaX : IApiProtocolEnforcerV1BetaX
  {
    private static readonly Logger _Logger = new Logger(typeof(ApiProtocolEnforcerV1BetaX));
    public delegate ApiOperationResult DelegateAction();

    public HttpRequestMessage Request { get; protected set; }

    public UrlHelper UrlHelper { get; protected set; }

    /// <summary>
    /// Needs to be called by all Controllers that use the ProtocolEnforcer before any operations.
    /// Currently unable to add Request prior to Controller's method being called (once done we can refactor this)
    /// </summary>
    /// <param name="request"></param>
    public void SetRequest(HttpRequestMessage request)
    {
      Request = request;
    }

    public void SetUrlHelper(UrlHelper urlHelper)
    {
      UrlHelper = urlHelper;
    }

    public void CheckForUrlHelperNotSet()
    {
      var protocolUrlHelperisNull = UrlHelper == null;
      if (protocolUrlHelperisNull)
      {
        throw new CustomApplicationException<ProtocolEnforcerErrorCodes>(ProtocolEnforcerErrorCodes.UrlHelperNotSet);
      }
    }

    public HttpResponseMessage Create(ISingleResourceCommandOperationCoordinator commandOperationCoordinator)
    {
      CheckForUrlHelperNotSet();
      ResourceHelper.UrlHelper = UrlHelper;

      var resultOfOperation = commandOperationCoordinator.Execute();
      EnsureCreateCommandDidNotReturnUpdatedOrDeletedStatusCodes(resultOfOperation);
      var response = CreateResponseMessage(resultOfOperation);
      return response;
    }

    public virtual HttpResponseMessage Read(ISingleResourceQueryOperationCoordinator queryOperationCoordinator)
    {
      CheckForUrlHelperNotSet();
      ResourceHelper.UrlHelper = UrlHelper;

      var resultOfOperation = queryOperationCoordinator.Read();
      ValidateFetchSingleMethod(resultOfOperation);
      var resourceNotNull = resultOfOperation.Result != null;
      if (resourceNotNull)
      {
        EnsureResourceImplementsIApiGettableResource(resultOfOperation);
        EnsureResourcesIMetaOrIMetaGettable(resultOfOperation);
      }

      var response = CreateResponseMessage(resultOfOperation);
      return response;
    }

    public virtual HttpResponseMessage Read(ICollectionResourceQueryOperationCoordinator queryOperationCoordinator)
    {
      CheckForUrlHelperNotSet();
      ResourceHelper.UrlHelper = UrlHelper;

      var resultOfOperation = queryOperationCoordinator.Read();
      ValidateFetchManyMethod(resultOfOperation);
      EnsureResourceImplementsIApiGettableResource(resultOfOperation);
      EnsureResourcesIMetaOrIMetaGettable(resultOfOperation);
      var response = CreateResponseMessage(resultOfOperation);
      return response;
    }

    public virtual HttpResponseMessage Read(ISingleResourceApiQuery query)
    {
      var uow = CreateUnitOfWork();
      var resultOfOperation = query.Fetch(uow);
      ValidateFetchSingleMethod(resultOfOperation);
      var response = CreateResponseMessage(resultOfOperation);
      return response;
    }

    public HttpResponseMessage Read(ICollectionResourceApiQuery query)
    {
      var uow = CreateUnitOfWork();
      var resultOfOperation = query.Fetch(uow);
      ValidateFetchManyMethod(resultOfOperation);
      var response = CreateResponseMessage(resultOfOperation);
      return response;
    }

    public HttpResponseMessage Update(ISingleResourceCommandOperationCoordinator commandOperationCoordinator)
    {
      CheckForUrlHelperNotSet();
      ResourceHelper.UrlHelper = UrlHelper;

      var resultOfOperation = commandOperationCoordinator.Execute();
      EnsureUpdateCommandDidNotReturnCreatedOrDeletedStatusCodes(resultOfOperation);
      var response = CreateResponseMessage(resultOfOperation);
      return response;
    }

    public HttpResponseMessage Delete(ISingleResourceCommandOperationCoordinator commandOperationCoordinator)
    {
      CheckForUrlHelperNotSet();
      ResourceHelper.UrlHelper = UrlHelper;

      var resultOfOperation = commandOperationCoordinator.Execute();
      EnsureDeleteCommandDidNotReturnCreatedOrUpdatedStatusCodes(resultOfOperation);
      var response = CreateResponseMessage(resultOfOperation);
      return response;
    }

    public HttpResponseMessage CreateMalformedRequestResponse(Type requestResourceType, string parameterName = null)
    {
      var genericMessage =
        "The request is malformed and can not be understood. Please correct the request and try again.";
      var messageUsingParameterNameProvided =
        string.Format("The request has a malformed parameter: {0}. Please correct the request and try again.",
          parameterName);
      var parameterNameProvided = parameterName != null;
      var userMessage = parameterNameProvided ? messageUsingParameterNameProvided : genericMessage;

      var errorCode = GetMalformedRequestErrorCode();
      var errorContainer = new ApiErrorContainerV2(userMessage, errorCode);
      return Request.CreateResponse(HttpStatusCode.BadRequest, errorContainer);
    }

    public HttpResponseMessage CreateResponseForWhenUriIdDoesNotMatchResourceId(
      string idOnResource, string idInUrl, string parameterName)
    {
      var errorContainer = new GenericUriIDDoesNotMatchResourceIDErrorContainer(parameterName, idOnResource, idInUrl);

      return Request.CreateResponse(HttpStatusCode.BadRequest, errorContainer);
    }

    public HttpResponseMessage CreateResponseForWhenUriIdDoesNotMatchResourceId(long idOnResource, long idInUrl, string parameterName)
    {
      var resourceIDAsString = idOnResource.ToString(CultureInfo.InvariantCulture);
      var urlIDAsString = idInUrl.ToString(CultureInfo.InvariantCulture);
      return CreateResponseForWhenUriIdDoesNotMatchResourceId(resourceIDAsString, urlIDAsString, parameterName);
    }

    public HttpResponseMessage CreateResponseForWhenUriIdDoesNotMatchResourceId(int idOnResource, int idInUrl, string parameterName)
    {
      var resourceIDAsString = idOnResource.ToString(CultureInfo.InvariantCulture);
      var urlIDAsString = idInUrl.ToString(CultureInfo.InvariantCulture);
      return CreateResponseForWhenUriIdDoesNotMatchResourceId(resourceIDAsString, urlIDAsString, parameterName);
    }

    protected static void ValidateFetchSingleMethod(ApiOperationResult resultOfOperation)
    {
      EnsureQueryDidNotDoLogicalWrite(resultOfOperation);
      EnsureQueryWasNotQueued(resultOfOperation);
      EnsureOperationThatReturnsSingleResourceDoesNotReturnEmptyCollection(resultOfOperation);
    }

    protected static void ValidateFetchManyMethod(ApiOperationResult resultOfOperation)
    {
      EnsureQueryDidNotDoLogicalWrite(resultOfOperation);
      EnsureOperationThatReturnsManyResourcesDoesNotReturnNotFound(resultOfOperation);
      EnsureQueryWasNotQueued(resultOfOperation);
      EnsureOperationThatReturnsManyResourcesDoesNotReturnNull(resultOfOperation);
    }

    protected static void EnsureQueryDidNotDoLogicalWrite(ApiOperationResult resultOfOperation)
    {
      EnsureQueryDidNotDoLogicalCreate(resultOfOperation);
      EnsureQueryDidNotDoLogicalUpdate(resultOfOperation);
      EnsureQueryDidNotDoLogicalDelete(resultOfOperation);
    }

    protected static void EnsureQueryDidNotDoLogicalCreate(ApiOperationResult resultOfOperation)
    {
      var isCreatedFound = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Created;

      if (isCreatedFound)
      {
        throw new Exception("A query to return a resource or a collection should not create any resource.");
      }
    }

    protected static void EnsureQueryDidNotDoLogicalUpdate(ApiOperationResult resultOfOperation)
    {
      var isUpdateFound = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Updated;

      if (isUpdateFound)
      {
        throw new Exception("A query to return a resource or a collection should not update any resource.");
      }
    }

    protected static void EnsureQueryDidNotDoLogicalDelete(ApiOperationResult resultOfOperation)
    {
      var isDeleteFound = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Deleted;
      if (isDeleteFound)
      {
        throw new Exception("A query to return a resource or a collection should not delete any resource.");
      }
    }


    protected static void EnsureOperationThatReturnsManyResourcesDoesNotReturnNotFound(ApiOperationResult resultOfOperation)
    {
      var isNotFound = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.NotFound;
      if (isNotFound)
      {
        throw new Exception("A query to a collection of resources should not return not found."
          + " Instead it should return empty collection");
      }
    }

    protected static void EnsureQueryWasNotQueued(ApiOperationResult resultOfOperation)
    {
      var opWasQueued = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Queued;
      if (opWasQueued)
      {
        throw new Exception("A query to return a resource or a collection should not should not be queued.");
      }
    }

    protected static void EnsureOperationThatReturnsSingleResourceDoesNotReturnEmptyCollection(
      ApiOperationResult resultOfOperation)
    {
      var isEmptyCollection = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.EmptyCollection;
      if (isEmptyCollection)
      {
        throw new Exception("A query to return a single resource should not return an empty collection.");
      }
    }

    protected static void EnsureOperationThatReturnsManyResourcesDoesNotReturnNull(ApiOperationResult resultOfOperation)
    {
      var resultingResource = resultOfOperation.Result;
      var operationDidNotException = resultOfOperation.OperationStatusCode != ApiOperationStatusCode.Exception;
      var actionReturnedNullInsteadOfAnEmptyCollectionResource = operationDidNotException
        && resultingResource == null;
      if (actionReturnedNullInsteadOfAnEmptyCollectionResource)
      {
        throw new Exception
          ("A query or command to access a Resource Collection returned null when it should return an "
            + "empty Collection Resource instead.");
      }
    }

    protected static void EnsureCreateCommandDidNotReturnUpdatedOrDeletedStatusCodes(ApiOperationResult resultOfOperation)
    {
      var operationUpdatedStatusCode = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Updated;
      var operationDeletedStatusCode = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Deleted;
      if (operationUpdatedStatusCode || operationDeletedStatusCode)
      {
        throw new Exception(
          "A Create command returned other status code when it should return Created status code instead.");
      }
    }

    protected static void EnsureDeleteCommandDidNotReturnCreatedOrUpdatedStatusCodes(ApiOperationResult resultOfOperation)
    {
      var operationCreatedStatusCode = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Created;
      var operationUpdatedStatusCode = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Updated;
      if (operationCreatedStatusCode || operationUpdatedStatusCode)
      {
        throw new Exception(
          "A Delete command returned other status code when it should return Deleted status code instead.");
      }
    }

    protected static void EnsureUpdateCommandDidNotReturnCreatedOrDeletedStatusCodes(ApiOperationResult resultOfOperation)
    {
      var operationCreatedStatusCode = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Created;
      var operationUpdatedStatusCode = resultOfOperation.OperationStatusCode == ApiOperationStatusCode.Deleted;
      if (operationCreatedStatusCode || operationUpdatedStatusCode)
      {
        throw new Exception(
          "A Update command returned other status code when it should return Updated status code instead.");
      }
    }

    protected static void EnsureOperationThatShouldReturnResultDidNotReturnNull(ApiOperationResult result)
    {
      var resultingResourceIsNull = result.Result == null;
      if (resultingResourceIsNull)
      {
        throw new Exception("Null was returned from an operation when it should not have been.");
      }
    }

    /// <summary>
    /// Ensure a 'gettable' resource is actually implementing the IApiGettableResource or IApiGettableResourceV1
    /// IApiGettableResourceV1 is deprecated and should NO LONGER be used.
    /// </summary>
    /// <param name="result"></param>
#pragma warning disable CS0618
    protected static void EnsureResourceImplementsIApiGettableResource(ApiOperationResult result)
    {
      var implementsIApiGettableResource = result.Result is IApiGettableResource;
      var implementsIApiGettableResourceV1 = result.Result is IApiGettableResourceV1;
      if (!(implementsIApiGettableResource || !implementsIApiGettableResourceV1))
      {
        throw new CustomApplicationException<ProtocolEnforcerErrorCodes>(ProtocolEnforcerErrorCodes
          .ReturnedResponseDoesNotImplementIApiGettableResource);
      }
    }
#pragma warning restore CS0618
    protected static void EnsureResourcesIMetaOrIMetaGettable(ApiOperationResult result)
    {
      var res = result.Result;
      if (res is IApiGettableResource resource)
      {
        var meta = resource.Meta;
        if (meta.Self == null)
        {
          throw new CustomApplicationException<ProtocolEnforcerErrorCodes>(ProtocolEnforcerErrorCodes
            .ReturnedResponseDoesNotContainSelfLink);
        }
        if (meta.Links == null)
        {
          throw new CustomApplicationException<ProtocolEnforcerErrorCodes>(ProtocolEnforcerErrorCodes
            .ReturnedResponseShouldContainLinkCollection);
        }
      }
      else
      {
        throw new CustomApplicationException<ProtocolEnforcerErrorCodes>(ProtocolEnforcerErrorCodes
          .ReturnedResponseDoesNotImplementIApiGettableResource);
      }
    }

    protected HttpResponseMessage CreateResponseMessage(ApiOperationResult result)
    {
      var methodStatus = result.OperationStatusCode;
      switch (methodStatus)
      {
        case ApiOperationStatusCode.EmptyCollection:
          {
            return CreateEmptyCollectionResponse(result);
          }
        case ApiOperationStatusCode.Deleted:
          {
            return CreateDeletedResponse(result);
          }
        case ApiOperationStatusCode.Updated:
          {
            return CreateUpdatedResponse(result);
          }
        case ApiOperationStatusCode.Found:
          {
            return CreateFoundResponse(result);
          }
        case ApiOperationStatusCode.NotFound:
          {
            return CreateNotFoundResponse(result);
          }
        case ApiOperationStatusCode.Created:
          {
            return CreateCreatedResponse(result);
          }
        case ApiOperationStatusCode.Exception:
          {
            return CreateExceptionResponse(result);
          }
        case ApiOperationStatusCode.BadRequest:
          {
            return CreateBadRequestResponse(result);
          }
        default:
          {
            throw new NotImplementedException("No case coded for: " + methodStatus);
          }
      }
    }

    protected HttpResponseMessage CreateEmptyCollectionResponse(ApiOperationResult result)
    {
      return CreateOkResponse(result);
    }

    protected HttpResponseMessage CreateCreatedResponse(ApiOperationResult result)
    {
      EnsureOperationThatShouldReturnResultDidNotReturnNull(result);
      return CreateCreatedSingleResponse(result);
    }

    protected HttpResponseMessage CreateFoundResponse(ApiOperationResult result)
    {
      EnsureOperationThatShouldReturnResultDidNotReturnNull(result);
      return CreateOkResponse(result);
    }

    protected HttpResponseMessage CreateUpdatedResponse(ApiOperationResult result)
    {
      EnsureOperationThatShouldReturnResultDidNotReturnNull(result);
      return CreateOkResponse(result);
    }

    protected HttpResponseMessage CreateDeletedResponse(ApiOperationResult result)
    {
      EnsureOperationThatShouldReturnResultDidNotReturnNull(result);
      return CreateOkResponse(result);
    }

    protected HttpResponseMessage CreateBadRequestResponse(ApiOperationResult result)
    {
      return Request.CreateResponse(HttpStatusCode.BadRequest, result.Result);
    }

    protected HttpResponseMessage CreateExceptionResponse(ApiOperationResult result)
    {
      var errorContainer = result.Result as EssApiErrorContainer;
      var errorContainerCastSucceeded = errorContainer != null;
      if (errorContainerCastSucceeded)
      {
        var exception = errorContainer.Exception;
        LogApiErrorResponse(exception, errorContainer);
      }
      return Request.CreateResponse(HttpStatusCode.Conflict, errorContainer);
    }

    protected HttpResponseMessage CreateCreatedSingleResponse(ApiOperationResult result)
    {
      var resource = result.Result;
      var response = CreateResponse(HttpStatusCode.Created, resource);
      var resultAsIApiResource = resource as IApiGettableResource;
      var castingToResourceTypeSucceeded = resultAsIApiResource != null;
      if (castingToResourceTypeSucceeded)
      {
        var uri = resultAsIApiResource.Meta.Self;
        response.Headers.Location = new Uri(uri, UriKind.Relative);
      }
      return response;
    }

    protected HttpResponseMessage CreateOkResponse(ApiOperationResult result)
    {
      return CreateResponse(HttpStatusCode.OK, result.Result);
    }

    protected HttpResponseMessage CreateResponse(HttpStatusCode statusCode, object result)
    {
      var response = Request.CreateResponse(statusCode, result);
      return response;
    }

    protected HttpResponseMessage CreateNotFoundResponse(ApiOperationResult result)
    {
      var apiErrorContainer = new GenericNotFoundErrorContainer();
      return CreateResponse(HttpStatusCode.NotFound, apiErrorContainer);
    }

    protected static void LogApiErrorResponse(Exception ex, ApiErrorContainerV2 container)
    {
      var message = container.ToString();
      _Logger.Warn(ex.ToString());
      _Logger.Warn(message);
    }

    private static IUnitOfWork CreateUnitOfWork()
    {
      return SessionHelper.CreateUnitOfWorkForEssApi();
    }

    protected static string GetMalformedRequestErrorCode() => "MalformedRequest";
  }
}
