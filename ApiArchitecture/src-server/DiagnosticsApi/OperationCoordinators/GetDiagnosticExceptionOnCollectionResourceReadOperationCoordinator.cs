using System.Collections.Generic;
using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators
{
  public class GetDiagnosticExceptionOnCollectionResourceReadOperationCoordinator :
    OperationsCoordinatorReadCollection<IEnumerable<DiagnosticEntity>, DiagnosticCollectionResource, DiagnosticsMapper>
  {
    public override IApiOperation<IEnumerable<DiagnosticEntity>> GenerateOperation()
    {
      var operation = DiagnosticApiOperationFactory.CreateDiagnosticExceptionOnCollectionResourceRead();
      return operation;
    }
  }
}