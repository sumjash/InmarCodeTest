namespace Jda.WfmEssApi.Common
{
  public interface IApiExceptionArgument
  {
    string GetArgumentCode();
    object Value { get; set; }
  }
}