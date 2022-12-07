namespace Jda.WfmEssApi.DiagnosticsApi
{
  public enum DiagnosticError
  {
    DiagnosticErrorThrownFromMethod,
    DiagnosticErrorFromClassicCreate,
    DiagnosticErrorFromClassicRead,
    DiagnosticErrorFromClassicUpdate,
    DiagnosticErrorFromClassicDelete,
    DiagnosticErrorFromSingleResourceCreate,
    DiagnosticErrorFromSingleResourceRead,
    DiagnosticErrorFromSingleResourceUpdate,
    DiagnosticErrorFromSingleResourceDelete,
    DiagnosticErrorFromCollectionResourceRead,
    DiagnosticErrorFromAsmxMethod
  }
}