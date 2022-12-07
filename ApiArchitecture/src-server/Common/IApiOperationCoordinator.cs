using System.Net.Http;

namespace Jda.WfmEssApi.Common
{
  /// <summary>
  /// Base Interface for any "OperationCoordinator": Command (write) or Query (read). 
  /// Likely not used in code except to communicate intent and possibly for future meta-analysis
  /// </summary>
  public interface IApiOperationCoordinator
  {
    bool IsCommitNecessary { get; }
  }
}
