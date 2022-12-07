
namespace Jda.WfmEssApi.Common
{
  /// <summary>
  /// Metadata for this resource
  /// </summary>
  public interface IMeta
  {
    /// <summary>
    /// Links for potential actions and related resources.
    /// </summary>
    LinkCollection Links { get; set; }
  }
}
