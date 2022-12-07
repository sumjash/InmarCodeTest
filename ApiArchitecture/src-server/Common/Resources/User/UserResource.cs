using Newtonsoft.Json;
using Perigee;
using RPCore;

namespace Jda.WfmEssApi.Common.Resources.User
{
  public class UserResource : IEntityResource
  {
    /// <summary>
    /// The user's internal Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The user's last name
    /// </summary>
    public FreeText LastName { get; set; }

    /// <summary>
    /// The user's first name
    /// </summary>
    public FreeText FirstName { get; set; }

    /// <summary>
    /// The user's nick name
    /// </summary>
    public FreeText NickName { get; set; }

    /// <summary>
    /// The user's name formatted by lastname separated by a comma
    /// followed by the user's nickname or first name if nickname is not
    /// present
    /// </summary>
    internal FreeText FormattedName { get; set; }

    [JsonIgnore]
    public string IdAsString { get; }
  }
}