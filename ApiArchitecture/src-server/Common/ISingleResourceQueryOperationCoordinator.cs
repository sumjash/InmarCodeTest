namespace Jda.WfmEssApi.Common
{
  public interface ISingleResourceQueryOperationCoordinator : IApiOperationCoordinator
  {
    ApiOperationResult Read();
  }
}
