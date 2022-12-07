using Perigee;

namespace Jda.WfmEssApi.Common
{
  public abstract class OperationsCoordinatorDeleteSingle<TEntity, TResource, TMapper> : 
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
      var statusCode = resourceWithMetaData != null ? ApiOperationStatusCode.Deleted : ApiOperationStatusCode.NotFound;
      return statusCode;
    }
  }
}