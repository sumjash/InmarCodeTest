using Perigee;

namespace Jda.WfmEssApi.Common
{
  public abstract class OperationsCoordinatorUpdateSingle<TEntity, TResource, TMapper> : 
    OperationsCoordinatorBase<TEntity, TResource, TMapper>,
    ISingleResourceCommandOperationCoordinator
    where TMapper : IMapper<TEntity, TResource>, new()
  {
    public bool IsCommitNecessary => true;
    public ApiOperationResult Execute()
    {
      return Operate(IsCommitNecessary);
    }
    protected override ApiOperationStatusCode GenerateStatusCode(TResource resourceWithMetaData)
    {
      var statusCode = resourceWithMetaData != null ? ApiOperationStatusCode.Updated : ApiOperationStatusCode.NotFound;
      return statusCode;
    }
  }
}