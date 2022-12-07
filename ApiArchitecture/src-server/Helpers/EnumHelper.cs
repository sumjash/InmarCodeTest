using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Jda.WfmEssApi.Helpers
{
  public class EnumHelper
  {
    public static string ConvertEnumToString(Enum enumToBeConverted)
    {
      var collectionType = enumToBeConverted.GetType().Name;
      var exceptionType = enumToBeConverted.ToString();

      return String.Format("{0}.{1}", collectionType, exceptionType);
    }

    public static T ParseStringToEnum<T>(string value)
    {
      if (value.Contains("."))
      {
        var enumData = value.Split('.');
        return (T)Enum.Parse(typeof(T), enumData[1], true);
      }
      return (T)Enum.Parse(typeof(T), value, true);
    }

    public static T ToEnum<T>(string enumCode)
    {
      if (enumCode.Length == 1)
      {
        return (T)Enum.ToObject(typeof(T), Convert.ToChar(enumCode, CultureInfo.InvariantCulture));
      }
      else
      {
        return (T)Enum.ToObject(typeof(T), Convert.ToInt32(enumCode,CultureInfo.InvariantCulture));
      }
    }
  }
}