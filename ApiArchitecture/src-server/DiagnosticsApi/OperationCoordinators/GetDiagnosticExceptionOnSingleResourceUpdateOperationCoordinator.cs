using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators
{
  public class GetDiagnosticExceptionOnSingleResourceUpdateOperationCoordinator : 
    OperationsCoordinatorUpdateSingle<DiagnosticEntity, DiagnosticResource, DiagnosticsMapper>
  {
    public override IApiOperation<DiagnosticEntity> GenerateOperation()
    {
      var operation = DiagnosticApiOperationFactory.CreateDiagnosticExceptionOnSingleResourceUpdate();
      return operation;
    }
  }
}