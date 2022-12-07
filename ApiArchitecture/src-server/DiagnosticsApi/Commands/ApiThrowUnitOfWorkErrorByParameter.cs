using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.Commands
{
  public class ApiThrowUnitOfWorkErrorByParameter : IApiOperation<DiagnosticEntity>
  {
    public ApiThrowUnitOfWorkErrorByParameter()
    {
    }

    public DiagnosticEntity Run(IUnitOfWork unitOfWork)
    {
      return new DiagnosticEntity();
    }
  }
}