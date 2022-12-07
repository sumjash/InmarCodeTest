using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators
{
  public class GetDiagnosticExceptionOnSingleResourceReadOperationCoordinator :
    OperationsCoordinatorReadSingle<DiagnosticEntity, DiagnosticResource, DiagnosticsMapper>
  {
    public override IApiOperation<DiagnosticEntity> GenerateOperation()
    {
      var operation = DiagnosticApiOperationFactory.CreateDiagnosticExceptionOnSingleResourceRead();
      return operation;
    }
  }
}