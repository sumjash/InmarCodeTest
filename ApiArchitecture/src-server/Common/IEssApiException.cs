using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi
{
  public interface IEssApiException
  {
    ApiOperationStatusCode GetApiStatusCode();
    string GetUserMessage();
    string GetErrorCode();
  }
}