using System.Collections.Generic;
using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using RP.DomainModel.Services.Diagnostics;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.Queries
{
  public class GetDomainOperationExceptionOnCollectionResourceRead : IApiOperation<IEnumerable<DiagnosticEntity>>
  {
    public IEnumerable<DiagnosticEntity> Run(IUnitOfWork unitOfWork)
    {
      DiagnosticService.ThrowDomainOperationException(
        DoeDiagnosticErrors.DoeFromCollectionResourceRead);
      return null;
    }
  }
}