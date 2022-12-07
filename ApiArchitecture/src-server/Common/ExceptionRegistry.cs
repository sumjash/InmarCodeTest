using System;
using System.Collections.Generic;
using System.Net;
using RP.DomainModel.Common;
using RPCore;


namespace Jda.WfmEssApi.Common
{
  public class ExceptionRegistry
  {
    public const string ExceptionRegistryCodeKey = "ExceptionRegistryCodes.Key";
    public const string ExceptionRegistryArgumentKey = "ExceptionRegistryArguments.Key";
    protected static object Mutex = new object();

    protected static Dictionary<Enum, Func<object>> ExceptionCodeMapper
    {
      get
      {
        lock (Mutex)
        {
          var mapper = Local.Data[ExceptionRegistryCodeKey] as Dictionary<Enum, Func<object>>;
          if (mapper == null)
          {
            mapper = new Dictionary<Enum, Func<object>>();
            ExceptionCodeMapper = mapper;
            RegisterApiExceptions();
          }
          return mapper;
        }
      }
      set { Local.Data[ExceptionRegistryCodeKey] = value; }
    }

    protected static Dictionary<Enum, Func<object>> ExceptionArgumentMapper
    {
      get
      {
        lock (Mutex)
        {
          var mapper = Local.Data[ExceptionRegistryArgumentKey] as Dictionary<Enum, Func<object>>;
          if (mapper == null)
          {
            mapper = new Dictionary<Enum, Func<object>>();
            ExceptionArgumentMapper = mapper;
            RegisterApiExceptionArguments();
          }
          return mapper;
        }
      }
      set { Local.Data[ExceptionRegistryArgumentKey] = value; }
    }

    public static IApiException GetApiException(DomainOperationException domainException)
    {
      var code = (Enum)domainException.OperationException;
      if (!ExceptionCodeMapper.ContainsKey(code))
      {
        return new ApiException(Jda.WfmEssApi.ApiExceptionCode.Default.Unsupported, HttpStatusCode.InternalServerError);
      }

      var mapperRegistration = ExceptionCodeMapper[code];
      return mapperRegistration() as ApiException;
    }

    public static IApiExceptionArgument GetApiExceptionArgument(Enum argumentKey)
    {
      if (!ExceptionArgumentMapper.ContainsKey(argumentKey))
      {
        return new ApiExceptionArgument
        {
          ApiExceptionArugmentCode = Jda.WfmEssApi.ApiExceptionArgumentKey.Default.Unsupported
        };
      }

      var mapperRegistration = ExceptionArgumentMapper[argumentKey];
      return mapperRegistration() as IApiExceptionArgument;
    }

    protected static void RegisterApiExceptions()
    {
      var registrations = new ExceptionRegistrations();
      registrations.RegisterApiExceptions();
    }

    protected static void RegisterApiExceptionArguments()
    {
      var registrations = new ExceptionRegistrations();
      registrations.RegisterApiExceptionArguments();
    }

    public static void RegisterExceptionCodeMapping(Enum domainVal, Enum apiVal,
      HttpStatusCode status = HttpStatusCode.Conflict)
    {
      var code = domainVal;

      ExceptionCodeMapper[code] = () => new ApiException(apiVal, status);
    }

    public static void RegisterArgumentKeyMapping(Enum domainVal, Enum apiVal)
    {
      var code = domainVal;

      ExceptionArgumentMapper[code] = () => new ApiExceptionArgument
      {
        ApiExceptionArugmentCode = apiVal
      };
    }
  }
}