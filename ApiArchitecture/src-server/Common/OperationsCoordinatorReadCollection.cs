using Perigee;

namespace Jda.WfmEssApi.Common
{
  public abstract class OperationsCoordinatorReadCollection<TEntity, TResource, TMapper> :
    OperationsCoordinatorBase<TEntity, TResource, TMapper>, ICollectionResourceQueryOperationCoordinator
    where TResource : IApiCollectionResource
    where TMapper : IMapper<TEntity, TResource>, new()
  {
    public bool IsCommitNecessary => false;
    public ApiOperationResult Read()
    {
      return Operate(IsCommitNecessary);
    }
    protected override ApiOperationStatusCode GenerateStatusCode(TResource resourceWithMetaData)
    {
      var statusCode = resourceWithMetaData.HasRecords() ? ApiOperationStatusCode.Found : ApiOperationStatusCode.EmptyCollection;
      return statusCode;
    }
  }
}