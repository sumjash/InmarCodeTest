using System;
using System.Collections.Generic;

namespace Jda.WfmEssApi.Common
{
  public abstract class EnumRegistry<TInside, TOutside> : EnumRegistry where TInside : struct where TOutside : struct
  {
    protected readonly Dictionary<TInside, TOutside> DomainToApiEnumMapper;
    protected readonly Dictionary<TOutside, TInside> ApiToDomainEnumMapper;

    protected EnumRegistry()
    {
      DomainToApiEnumMapper = new Dictionary<TInside, TOutside>();
      ApiToDomainEnumMapper = new Dictionary<TOutside, TInside>();
      RegisterApiEnums();
    }

    public TOutside MapDomainEnumToApiEnum(TInside domainEnum)
    {
      if (!DomainToApiEnumMapper.ContainsKey(domainEnum))
      {
        throw new ArgumentException(@"Domain Enum has no matching API Enum mapped.", nameof(domainEnum));
      }
      return DomainToApiEnumMapper[domainEnum];
    }

    public TOutside? MapDomainEnumToApiEnum(TInside? domainEnum)
    {
      if (domainEnum == null)
      {
        return null;
      }
      return MapDomainEnumToApiEnum((TInside)domainEnum);
    }

    public TInside MapApiEnumToDomainEnum(TOutside apiEnum)
    {
      if (!ApiToDomainEnumMapper.ContainsKey(apiEnum))
      {
        throw new ArgumentException(@"API Enum has no matching Domain Enum mapped.", nameof(apiEnum));
      }
      return ApiToDomainEnumMapper[apiEnum];
    }

    public TInside? MapApiEnumToDomainEnum(TOutside? apiEnum)
    {
      if (apiEnum == null)
      {
        return null;
      }
      return MapApiEnumToDomainEnum((TOutside)apiEnum);
    }

    protected abstract void RegisterApiEnums();

    protected void RegisterEnumMapping(TInside domainEnum, TOutside apiEnum)
    {
      DomainToApiEnumMapper[domainEnum] = apiEnum;
      ApiToDomainEnumMapper[apiEnum] = domainEnum;
    }
  }

  public class EnumRegistry{}
}