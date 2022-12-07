using System;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi
{
  public class EssApiException : IEssApiException
  {
    public Enum ApiExceptionCode { get; set; }
    public string ApiExceptionMessage { get; set; }
    public ApiOperationStatusCode OperationStatusCode { get; set; }

    public EssApiException(Enum apiExceptionCode, ApiOperationStatusCode operationStatusCode, string apiExceptionMessage = null)
    {
      ApiExceptionCode = apiExceptionCode;
      OperationStatusCode = operationStatusCode;
      ApiExceptionMessage = apiExceptionMessage;
    }

    protected string CollectionType => ApiExceptionCode.GetType().Name;

    protected string ExceptionType => ApiExceptionCode.ToString();

    public virtual ApiOperationStatusCode GetApiStatusCode()
    {
      return OperationStatusCode;
    }

    public virtual string GetUserMessage()
    {
      return ApiExceptionMessage;
    }

    public virtual string GetErrorCode()
    { // JMCD: This probably shouldn't be FooResource.ErrorCode. This likely needs to be controlled by the thing handling the exception
      return $"{CollectionType}.{ExceptionType}";
    }
  }
}
