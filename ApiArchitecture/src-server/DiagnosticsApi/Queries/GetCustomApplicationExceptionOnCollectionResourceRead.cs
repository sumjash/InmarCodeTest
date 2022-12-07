using System.Collections.Generic;
using Perigee.GlobalErrorHandling;
using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.Queries
{
  public class GetCustomApplicationExceptionOnCollectionResourceRead : IApiOperation<IEnumerable<DiagnosticEntity>>
  {
    public IEnumerable<DiagnosticEntity> Run(IUnitOfWork unitOfWork)
    {
      throw new CustomApplicationException<DiagnosticError>(
        DiagnosticError.DiagnosticErrorFromCollectionResourceRead);
    }
  }
}