using System;
using RP.DomainModel.Common;
using RP.DomainModel.Diagnostic;
using Jda.WfmEssApi.Common;

namespace Jda.WfmEssApi.DiagnosticsApi.Commands
{
    public class GetDiagnosticExceptionOnSingleResourceUpdate : IApiOperation<DiagnosticEntity>
    {
      public DiagnosticEntity Run(IUnitOfWork unitOfWork)
      {
      throw new Exception("An exception was thrown from a Single Resource Update Command.");
      }
  }
}