using Jda.WfmEssApi.Helpers;
using Newtonsoft.Json;
using System.Net.Http;

namespace Jda.WfmEssApi
{
  /// <summary>
  /// A hyperlink.
  /// </summary>
  public class Link
  {
    /// <summary>
    /// The relation of this link to its bearer.
    /// </summary>
    public string Rel { get; set; }

    public string Href { get; set; }
    /// <summary>
    /// The HTTP method to use when following this link.
    /// </summary>
    [JsonIgnore]
    private string _Method;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Method
    {
      get
      {
        var isGet = _Method == HttpMethod.Get.ToString();
        return (isGet) ? null : _Method;
      }
      set => _Method = value;
    }

    public Link()
    {
    }

    public Link(Relation rel, HttpVerb method, string uri, params object[] objects)
    {
      Href = string.Format(uri, objects);
      Rel = rel.ToString().ToLower();
      Method = method.ToString();
    }

    public Link(string relationship, HttpVerb method, string uri, params object[] objects)
    {
      Href = string.Format(uri, objects);
      Rel = relationship.ToLower();
      Method = method.ToString();
    }
  }
}
