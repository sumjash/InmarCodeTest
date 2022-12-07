namespace Jda.WfmEssApi.Common
{
  public interface ISingleResourceCommandOperationCoordinator : IApiOperationCoordinator
  {
    ApiOperationResult Execute();
  }
}