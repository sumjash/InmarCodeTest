using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using RP.DomainModel.Services.Diagnostics;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.Queries
{
  public class GetDomainOperationExceptionOnSingleResourceRead : IApiOperation<DiagnosticEntity>
  {
    public DiagnosticEntity Run(IUnitOfWork unitOfWork)
    {
      DiagnosticService.ThrowDomainOperationException(DoeDiagnosticErrors.DoeFromSingleResourceRead);
      return null;
    }
  }
}