using Microsoft.VisualStudio.ProjectSystem;
using System.ComponentModel.Composition;

namespace Adfc.VisualStudio.Extension.VS2017.ProjectSystem
{
    [Export]
    [AppliesTo(AdfcProject.UniqueCapability)]
    internal class AdfcProjectBuildConfiguration
    {
        [Import]
        internal ConfiguredProject ConfiguredProject { get; private set; }

        [Import]
        internal ProjectProperties Properties { get; private set; }
    }
}
