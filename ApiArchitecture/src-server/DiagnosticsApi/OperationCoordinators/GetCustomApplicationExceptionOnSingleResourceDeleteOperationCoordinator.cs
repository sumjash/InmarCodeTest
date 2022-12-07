using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators
{
  public class GetCustomApplicationExceptionOnSingleResourceDeleteOperationCoordinator :
    OperationsCoordinatorDeleteSingle<DiagnosticEntity, DiagnosticResource, DiagnosticsMapper>
  {
    public override IApiOperation<DiagnosticEntity> GenerateOperation()
    {
      var operation = DiagnosticApiOperationFactory.CreateCustomApplicationExceptionOnSingleResourceDelete();
      return operation;
    }
  }
}