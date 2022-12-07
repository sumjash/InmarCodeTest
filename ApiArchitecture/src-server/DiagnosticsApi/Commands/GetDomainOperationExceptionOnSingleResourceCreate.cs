using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using RP.DomainModel.Services.Diagnostics;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.Commands
{
  public class GetDomainOperationExceptionOnSingleResourceCreate : IApiOperation<DiagnosticEntity>
  {
    public DiagnosticEntity Run(IUnitOfWork unitOfWork)
    {
      DiagnosticService.ThrowDomainOperationException(
        DoeDiagnosticErrors.DoeFromSingleResourceCreate);
      return null;
    }
  }
}