using System.Collections.ObjectModel;
using Jda.WfmEssApi.Areas.HelpPage.ModelDescriptions;

namespace Jda.WfmEssApi.Areas.HelpPage.ModelDescriptions
{
    public class ComplexTypeModelDescription : ModelDescription
    {
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
        }

        public Collection<ParameterDescription> Properties { get; private set; }
    }
}