using Microsoft.VisualStudio.ProjectSystem;
using Microsoft.VisualStudio.ProjectSystem.Properties;
using System.ComponentModel.Composition;

namespace Adfc.VisualStudio.Extension.VS2017.ProjectSystem
{
    internal partial class ProjectProperties : StronglyTypedPropertyAccess
    {
        [ImportingConstructor]
        protected ProjectProperties(ConfiguredProject configuredProject)
            : base(configuredProject)
        {
        }

        protected ProjectProperties(ConfiguredProject configuredProject, IProjectPropertiesContext projectPropertiesContext)
            : base(configuredProject, projectPropertiesContext)
        {
        }

        protected ProjectProperties(ConfiguredProject configuredProject, UnconfiguredProject unconfiguredProject)
            : base(configuredProject, unconfiguredProject)
        {
        }

        protected ProjectProperties(ConfiguredProject configuredProject, string file, string itemType, string itemName)
            : base(configuredProject, file, itemType, itemName)
        {
        }
    }
}