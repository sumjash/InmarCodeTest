using System.Net;

namespace Jda.WfmEssApi.Common
{
  public interface IApiException
  {
    HttpStatusCode GetHttpStatusCode();
    string GetUserMessage();
    string GetErrorCode();
  }
}