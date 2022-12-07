using Jda.WfmEssApi.Areas.HelpPage.ModelDescriptions;

namespace Jda.WfmEssApi.Areas.HelpPage.ModelDescriptions
{
    public class KeyValuePairModelDescription : ModelDescription
    {
        public ModelDescription KeyModelDescription { get; set; }

        public ModelDescription ValueModelDescription { get; set; }
    }
}