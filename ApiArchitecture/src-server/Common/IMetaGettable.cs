
namespace Jda.WfmEssApi.Common
{
  /// <summary>
  /// Metadata for this resource.
  /// </summary>
  public interface IMetaGettable
  {
    /// <summary>
    /// The canonical link to this representation.
    /// </summary>
    string Self { get; set; }
    /// <summary>
    /// Links for potential actions and related resources.
    /// </summary>
    LinkCollection Links { get; set; }
  }
}
