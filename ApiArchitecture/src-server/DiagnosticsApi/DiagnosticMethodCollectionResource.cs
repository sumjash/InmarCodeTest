using System.Collections.Generic;

namespace Jda.WfmEssApi.DiagnosticsApi
{
  public class DiagnosticMethodCollectionResource
  {
    public List<string> ControllerMethods { get; protected set; }

    public DiagnosticMethodCollectionResource()
    {
      ControllerMethods = new List<string>
      {
        "exceptionInController",
        "exceptionInClassicCreate",
        "exceptionInClassicRead",
        "exceptionInClassicUpdate",
        "exceptionInClassicDelete",
        "exceptionOnSingleResourceCreate",
        "exceptionOnSingleResourceRead",
        "exceptionOnSingleResourceUpdate",
        "exceptionOnSingleResourceDelete",
        "exceptionOnCollectionResourceRead",
        "cae/exceptionWithinMethod",
        "cae/exceptionInClassicCreate",
        "cae/exceptionInClassicRead",
        "cae/exceptionInClassicUpdate",
        "cae/exceptionInClassicDelete",
        "cae/exceptionOnSingleResourceCreate",
        "cae/exceptionOnSingleResourceRead",
        "cae/exceptionOnSingleResourceUpdate",
        "cae/exceptionOnSingleResourceDelete",
        "cae/exceptionOnCollectionResourceRead",
        "doe/domainOperationExceptionInControllerMethod",
        "doe/domainOperationExceptionFromCreate",
        "doe/domainOperationExceptionFromRead",
        "doe/domainOperationExceptionFromUpdate",
        "doe/domainOperationExceptionFromDelete",
        "doe/domainOperationExceptionOnSingleResourceCreate",
        "doe/domainOperationExceptionOnSingleResourceRead",
        "doe/domainOperationExceptionOnSingleResourceUpdate",
        "doe/domainOperationExceptionOnSingleResourceDelete",
        "doe/domainOperationExceptionOnCollectionResourceRead",
        "throwUnitOfWorkError",
        "doe/domainOperationExceptionDuringCommit",
        "exceptionDuringCommit",
        "formatExceptionDuringSerialization",
        "nHiberbernateSqlException"
      };
    }
  }
}