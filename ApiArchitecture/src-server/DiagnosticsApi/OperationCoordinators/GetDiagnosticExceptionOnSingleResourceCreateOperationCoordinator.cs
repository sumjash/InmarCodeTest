using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators
{
  public class GetDiagnosticExceptionOnSingleResourceCreateOperationCoordinator :
    OperationsCoordinatorCreateSingle<DiagnosticEntity, DiagnosticResource, DiagnosticsMapper>
  {
    public override IApiOperation<DiagnosticEntity> GenerateOperation()
    {
      var operation = DiagnosticApiOperationFactory.CreateDiagnosticExceptionOnSingleResourceCreate();
      return operation;
    }
  }
}