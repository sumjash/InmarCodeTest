namespace Jda.WfmEssApi.Common
{
  /// <summary>
  /// Used to signify to the ProtocolEnforcer that only a single "resource" (a thing with a URI of its own) should be fetched. 
  ///   Must be a read-only operation (in API Contract terms).
  /// This also allows us to overload the "Read(...)" method to more easily make the decision 
  ///   to use FetchSingle, FetchMany, etc. during design/run-time execution.
  /// </summary>
  public interface ISingleResourceApiQuery : IApiQuery
  {

  }
}
