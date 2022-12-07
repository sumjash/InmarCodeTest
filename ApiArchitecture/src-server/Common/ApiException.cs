using System;
using System.Collections.Generic;
using System.Net;

namespace Jda.WfmEssApi.Common
{
  public class ApiException : Exception, IApiException
  {
    public Enum ApiExceptionCode { get; set; }
    public HttpStatusCode HttpStatus { get; set; }
    public IList<ApiExceptionArgument> Arguments { get; }
    protected string CollectionType => ApiExceptionCode.GetType().Name;
    protected string ExceptionType => ApiExceptionCode.ToString();

    public ApiException(Enum apiExceptionCode, HttpStatusCode httpStatusCode)
    {
      ApiExceptionCode = apiExceptionCode;
      HttpStatus = httpStatusCode;
    }

    public ApiException(Enum apiExceptionCode, HttpStatusCode httpStatusCode, IList<ApiExceptionArgument> arguments)
    {
      ApiExceptionCode = apiExceptionCode;
      HttpStatus = httpStatusCode;
      Arguments = arguments;
    }

    public virtual HttpStatusCode GetHttpStatusCode()
    {
      return HttpStatus;
    }

    public virtual string GetUserMessage()
    {
      return "Unsupported Domain Operation.  Reason is " + GetErrorCode();
    }

    public virtual string GetErrorCode()
    {
      return $"{CollectionType}.{ExceptionType}";
    }
  }
}
