using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jda.WfmEssApi.Areas.HelpPage
{
    public static class ComplexTypeModelDescriptionExtensions
    {
        public static string SelectTypeOverrideOrReturnEmpty(string fullName)
        {
            switch (fullName)
            {
                case "RPCore.Time.BusinessDate":
                    return "BusinessDate";
                case "RPCore.Time.CalendarDate":
                    return "CalendarDate";
                case "RPCore.Time.SiteLocalTimestamp":
                    return "SiteLocalTimestamp";
                case "RPCore.FreeText":
                    return "FreeText";
                default:
                    return "";
            }
        }
    }
}