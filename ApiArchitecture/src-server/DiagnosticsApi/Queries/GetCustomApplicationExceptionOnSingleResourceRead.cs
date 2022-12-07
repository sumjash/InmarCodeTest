using Perigee.GlobalErrorHandling;
using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.Queries
{
  public class GetCustomApplicationExceptionOnSingleResourceRead : IApiOperation<DiagnosticEntity>
  {
    public DiagnosticEntity Run(IUnitOfWork unitOfWork)
    {
      throw new CustomApplicationException<DiagnosticError>(DiagnosticError.DiagnosticErrorFromSingleResourceRead);
    }
  }
}