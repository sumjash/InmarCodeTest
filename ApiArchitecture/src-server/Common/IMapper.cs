using Perigee;

namespace Jda.WfmEssApi.Common
{
  public interface IMapper<in TEntity, out TResource>
  {
    TResource Map(TEntity entity);
  }
}
