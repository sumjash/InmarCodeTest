namespace Jda.WfmEssApi.Common.Resources.User
{
  public class UserMapper : IMapper<RP.DomainModel.HumanResources.User, UserResource>
  {
    public UserResource Map(RP.DomainModel.HumanResources.User entity)
    {
      var resource = new UserResource
      {
        Id = entity.ID.ToString(),
        LastName = entity.Name.LastName,
        FirstName = entity.Name.FirstName,
        NickName = entity.Name.NickName,
        FormattedName = entity.Name.GetFormattedName()
      };
      return resource;
    }
  }
}