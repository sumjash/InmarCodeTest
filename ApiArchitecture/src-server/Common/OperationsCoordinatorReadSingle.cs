using Perigee;

namespace Jda.WfmEssApi.Common
{
  public abstract class OperationsCoordinatorReadSingle<TEntity, TResource, TMapper> :
    OperationsCoordinatorBase<TEntity, TResource, TMapper>,
    ISingleResourceQueryOperationCoordinator 
    where TMapper : IMapper<TEntity, TResource>, new()
  {
    public bool IsCommitNecessary => false;

    public ApiOperationResult Read()
    {
      return Operate(IsCommitNecessary);
    }

    protected override ApiOperationStatusCode GenerateStatusCode(TResource resourceWithMetaData)
    {
      var statusCode = resourceWithMetaData != null ? ApiOperationStatusCode.Found : ApiOperationStatusCode.NotFound;
      return statusCode;
    }
  }
}