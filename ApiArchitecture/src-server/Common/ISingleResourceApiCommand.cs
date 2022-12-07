namespace Jda.WfmEssApi.Common
{
  /// <summary>
  /// Like ISingleResourceApiQuery, but must be a potential-write operation (in API contract terms).
  /// Will potentially create one or more resources, that is, things with their own URIs.
  /// </summary>
  public interface ISingleResourceApiCommand : IApiCommand
  {

  }
}
