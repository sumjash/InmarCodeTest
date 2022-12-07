using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.Common
{
  public class GenericNotFoundErrorContainer: EssApiErrorContainer
  {
    protected const string ErrorCodeTemplate = "NoResourceWithGivenIdentifierExists";
    protected const string UserMessageTemplate = "The resource with the given identifier was not found.";

    public GenericNotFoundErrorContainer() 
      : base(null, GetUserMessage(), GetErrorCode())
    {}

    protected static string GetUserMessage()
    {
      return string.Format(UserMessageTemplate);
    }

    protected static string GetErrorCode()
    {
      return string.Format(ErrorCodeTemplate);
    }
  }
}