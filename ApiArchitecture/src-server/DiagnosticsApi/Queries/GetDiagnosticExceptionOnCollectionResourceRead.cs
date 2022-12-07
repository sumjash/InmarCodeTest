using System;
using System.Collections.Generic;
using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.Queries
{
  public class GetDiagnosticExceptionOnCollectionResourceRead : IApiOperation<IEnumerable<DiagnosticEntity>>
  {
    public IEnumerable<DiagnosticEntity> Run(IUnitOfWork unitOfWork)
    { 
        throw new Exception("An exception was thrown from a Collection Resource Read Query.");
    }
  }
}