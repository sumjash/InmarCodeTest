using RP.DomainModel.Common;

namespace Jda.WfmEssApi.Common.Resources.Site
{
  public class SiteNameMapper : IMapper<RP.DomainModel.Hierarchy.Site, SiteNameResource>
  {
    public SiteNameResource Map(RP.DomainModel.Hierarchy.Site entity)
    {
      var domainEntityDoesNotExist = !DomainModelHelper.ValidateEntityExists(entity);
      if (domainEntityDoesNotExist)
      {
        return null;
      }

      var resource = new SiteNameResource
      {
        Id = entity.ID.ToString(),
        Name = entity.Name
      };
      return resource;
    }
  }
}