using System.Collections.Generic;
using System.Linq;

namespace Jda.WfmEssApi.Helpers
{
  public static class LinkHelper
  {
    public static Link Lookup(this List<Link> links, HttpVerb verb, string relation)
    {
      return links.FirstOrDefault(x => x.Method == verb.ToString() && x.Rel == relation);
    }

    public static Link Lookup(this List<Link> links, HttpVerb verb, Relation relation)
    {
      return links.FirstOrDefault(x => x.Method == verb.ToString() && x.Rel == relation.ToString().ToLower());
    }
  }

  public enum HttpVerb
  {
    GET,
    POST,
    PUT,
    DELETE
  }

  public enum Relation
  {
    /// <summary>
    /// Denotes that the URI points to the entity itself
    /// </summary>
    Self,
    Add,
    Edit,
    Delete,
    ParentOrganizationalUnit, 
    Next,
    Previous,
    Accept,
    Decline,
    Dispute,
    Reason,
    Site,
    Job,
    Cancel,
    ReplaceJobPriorities,
    ReadJobPriorities
  }
}