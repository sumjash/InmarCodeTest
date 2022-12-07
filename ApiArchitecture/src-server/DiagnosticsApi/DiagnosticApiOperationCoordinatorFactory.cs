using RP.DomainModel.Common.UnitOfWork.Diagnostics;
using Jda.WfmEssApi.Common;
using Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators;

namespace Jda.WfmEssApi.DiagnosticsApi
{
  public class DiagnosticApiOperationCoordinatorFactory
  {
    public virtual GetCustomApplicationExceptionOnSingResourceCreateOperationCoordinator
      CreateGetCustomApplicationExceptionOnSingleResourceCreateOperation()
    {
      return new GetCustomApplicationExceptionOnSingResourceCreateOperationCoordinator();
    }

    public virtual GetCustomApplicationExceptionOnSingleResourceDeleteOperationCoordinator
      CreateGetCustomApplicationExceptionOnSingleResourceDeleteOperation()
    {
      return new GetCustomApplicationExceptionOnSingleResourceDeleteOperationCoordinator();
    }

    public virtual GetCustomApplicationExceptionOnSingleResourceUpdateOperationCoordinator
      CreateGetCustomApplicationExceptionOnSingleResourceUpdateOperation()
    {
      return new GetCustomApplicationExceptionOnSingleResourceUpdateOperationCoordinator();
    }

    public virtual GetDomainExceptionOnSingleResourceUpdateOperationCoordinator
      CreateGetDomainExceptionOnSingleResourceUpdateOperation()
    {
      return new GetDomainExceptionOnSingleResourceUpdateOperationCoordinator();
    }

    public virtual GetDomainOperationExceptionOnSingleResourceCreateOperationCoordinator
      CreateGetDomainOperationExceptionOnSingleResourceCreateOperation()
    {
      return new GetDomainOperationExceptionOnSingleResourceCreateOperationCoordinator();
    }

    public virtual GetDomainOperationExceptionOnSingleResourceDeleteOperationCoordinator
      CreateGetDomainOperationExceptionOnSingleResourceDelete()
    {
      return new  GetDomainOperationExceptionOnSingleResourceDeleteOperationCoordinator();
    }

    public virtual GetDiagnosticExceptionOnSingleResourceCreateOperationCoordinator
      CreateGetDiagnosticExceptionOnSingleResourceCreateOperation()
    {
      return new GetDiagnosticExceptionOnSingleResourceCreateOperationCoordinator();
    }

    public virtual GetDiagnosticExceptionOnSingleResourceDeleteOperationCoordinator
      CreateGetDiagnosticExceptionOnSingleResourceDeleteOperation()
    {
      return new GetDiagnosticExceptionOnSingleResourceDeleteOperationCoordinator();
    }

    public virtual GetDiagnosticExceptionOnSingleResourceUpdateOperationCoordinator
      CreateGetDiagnosticExceptionOnSingleResourceUpdateOperation()
    {
      return new GetDiagnosticExceptionOnSingleResourceUpdateOperationCoordinator();
    }

    public virtual GetCustomApplicationExceptionOnCollectionResourceReadOperationCoordinator
      CreateGetCustomApplicationExceptionOnCollectionResourceReadOperation()
    {
      return new GetCustomApplicationExceptionOnCollectionResourceReadOperationCoordinator();
    }

    public virtual GetCustomApplicationExceptionOnSingleResourceReadOperationCoordinator
      CreateGetCustomApplicationExceptionOnSingleResourceReadOperation()
    {
      return new GetCustomApplicationExceptionOnSingleResourceReadOperationCoordinator();
    }

    public virtual GetDomainOperationExceptionOnCollectionResourceReadOperationCoordinator
      CreateGetDomainOperationExceptionOnCollectionResourceReadOperation()
    {
      return new GetDomainOperationExceptionOnCollectionResourceReadOperationCoordinator();
    }

    public virtual GetDomainOperationExceptionOnSingleResourceReadOperationCoordinator
      CreateGetDomainOperationExceptionOnSingleResourceReadOperation()
    {
      return new GetDomainOperationExceptionOnSingleResourceReadOperationCoordinator();
    }

    public virtual GetDiagnosticExceptionOnCollectionResourceReadOperationCoordinator
      CreateGetDiagnosticExceptionOnCollectionResourceReadOperation()
    {
      return new GetDiagnosticExceptionOnCollectionResourceReadOperationCoordinator();
    }

    public virtual GetDiagnosticExceptionOnSingleResourceReadOperationCoordinator
      CreateGetDiagnosticExceptionOnSingleResourceReadOperation()
    {
      return new GetDiagnosticExceptionOnSingleResourceReadOperationCoordinator();
    }

    public ApiThrowUnitOfWorkErrorByParameterOperationsCoordinator CreateApiThrowUnitOfWorkErrorByParameter()
    {
      return new ApiThrowUnitOfWorkErrorByParameterOperationsCoordinator();
    }
  }
}