using System;
using Perigee;
using Perigee.GlobalErrorHandling;
using Perigee.RequiredREFSInterfaces;
using RP.DomainModel.Common;

namespace Jda.WfmEssApi.Common
{
  /// <summary>
  /// TEntity, Entity or Aggregate from Domain
  /// TResource, EntityResource
  /// TMapper, Mapper 
  /// TOperation, Command Or Query
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  /// <typeparam name="TResource"></typeparam>
  /// <typeparam name="TMapper"></typeparam>
  public abstract class OperationsCoordinatorBase<TEntity, TResource, TMapper>
    where TMapper : IMapper<TEntity, TResource>, new()
  {
    protected IApiOperation<TEntity> _operation;
    private readonly TMapper _mapper;
    protected IUnitOfWork Uow;
    protected OperationsCoordinatorBase()
    {
      _mapper = (TMapper)Activator.CreateInstance(typeof(TMapper));
    }

    /// <summary>
    /// Perform addtional queries to assist building metadata
    /// and performing primary queries
    /// </summary>
    public virtual void PerformLookups() { } //intentional no-op unless overridden

    /// <summary>
    /// Generate your Command or Query Here returning same.
    /// </summary>
    /// <returns></returns>
    public abstract IApiOperation<TEntity> GenerateOperation();
    protected abstract ApiOperationStatusCode GenerateStatusCode(TResource resourceWithMetaData);

    protected virtual ApiOperationResult Operate(bool commitIsNecessary)
    {
      ApiOperationResult resultOfOperation;
      try
      {
        Uow = CreateUnitOfWork();
        PerformLookups();
        _operation = GenerateOperation();
        VerifyClassIsReadyForOperation();
        var result = _operation.Run(Uow);

        if (commitIsNecessary)
        {
          Uow.Commit();
        }

        var resource = _mapper.Map(result);
        var resourceWithMetaData = BuildMetaData(resource);
        var statusCode = GenerateStatusCode(resourceWithMetaData);
        resultOfOperation = GenerateApiOperationResult(resourceWithMetaData, statusCode);
      }
      finally
      {
        UnitOfWorkFactory.Dispose();
      }

      return resultOfOperation;
    }

    private static IUnitOfWork CreateUnitOfWork()
    {
      var unitOfWork =  SessionHelper.CreateUnitOfWorkForEssApi();

      var isUnAuthorizedRequest = SessionHelper.IsUnauthorizedRequest();

      if (isUnAuthorizedRequest)
      {
        throw SessionHelper.GetUnauthorizedAccessException();
      }

      return unitOfWork;
    }

    private void VerifyClassIsReadyForOperation()
    {
      var operationIsEmpty = _operation == null;
      if (operationIsEmpty)
      {
        throw new CustomApplicationException<OperationCoordinatorErrorCodes>(OperationCoordinatorErrorCodes
          .OperationNotProvided);
      }
      var mapperIsNotInstanced = _mapper == null;
      if (mapperIsNotInstanced)
      {
        throw new CustomApplicationException<OperationCoordinatorErrorCodes>(OperationCoordinatorErrorCodes
          .MapperNotProvided);
      }
    }

    //TODO: make this abstract to force every OC to generate MetaData for its resource.
    public virtual TResource BuildMetaData(TResource resource)
    {
      return resource;
    }

    internal ApiOperationResult GenerateApiOperationResult(object resource, ApiOperationStatusCode statusCode)
    {
      return new ApiOperationResult
      {
        OperationStatusCode = statusCode,
        Result = resource
      };
    }
  }

  public enum OperationCoordinatorErrorCodes
  {
    OperationNotProvided,
    MapperNotProvided
  }
}