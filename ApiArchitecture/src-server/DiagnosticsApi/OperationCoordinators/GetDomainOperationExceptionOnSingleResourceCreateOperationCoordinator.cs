using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators
{
  public class GetDomainOperationExceptionOnSingleResourceCreateOperationCoordinator :
    OperationsCoordinatorCreateSingle<DiagnosticEntity, DiagnosticResource, DiagnosticsMapper>
  {
    public override IApiOperation<DiagnosticEntity> GenerateOperation()
    {
      var operation = DiagnosticApiOperationFactory.CreateDomainOperationExceptionOnSingleResourceCreate();
      return operation;
    }
  }
}