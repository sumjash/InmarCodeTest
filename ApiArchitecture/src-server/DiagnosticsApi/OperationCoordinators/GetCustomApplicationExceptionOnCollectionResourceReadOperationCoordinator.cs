using System.Collections.Generic;
using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.OperationCoordinators
{
  public class GetCustomApplicationExceptionOnCollectionResourceReadOperationCoordinator :
    OperationsCoordinatorReadCollection<IEnumerable<DiagnosticEntity>, DiagnosticCollectionResource, DiagnosticsMapper>
  {
    public override IApiOperation<IEnumerable<DiagnosticEntity>> GenerateOperation()
    {
      var operation = DiagnosticApiOperationFactory.CreateCustomApplicationExceptionOnCollectionResourceRead();
      return operation;
    }
  }
}