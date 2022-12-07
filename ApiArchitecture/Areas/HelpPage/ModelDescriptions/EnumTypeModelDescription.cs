using System.Collections.ObjectModel;
using Jda.WfmEssApi.Areas.HelpPage.ModelDescriptions;

namespace Jda.WfmEssApi.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}