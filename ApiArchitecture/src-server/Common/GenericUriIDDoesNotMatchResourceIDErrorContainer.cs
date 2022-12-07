using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.Common // JMCD: Might be good to move this and similar "containers" to their own namespace
{
  public class GenericUriIDDoesNotMatchResourceIDErrorContainer : EssApiErrorContainer
  {
    public GenericUriIDDoesNotMatchResourceIDErrorContainer(string parameterName,
      string idOnResource, string idInUrl)
      : base(null, string.Empty, string.Empty)
    {
      var userMessage = GetUserMessage(parameterName, idOnResource, idInUrl);
      var errorCode = GetErrorCode();
      base.AddApiError(userMessage, errorCode);
    }

    protected string GetUserMessage(string parameterName, string idOnResource, string idInUrl)
    {
      const string msgTemplate = "The parameter {0} value of {1} does not match the identifier of {2} provided in the URL.";
      return string.Format(msgTemplate, parameterName, idOnResource, idInUrl);
    }

    protected string GetErrorCode()
    {
      const string errorCodeTemplate = "IDParameterDoesNotMatchResourceID";
      return errorCodeTemplate;
    }
  }
}