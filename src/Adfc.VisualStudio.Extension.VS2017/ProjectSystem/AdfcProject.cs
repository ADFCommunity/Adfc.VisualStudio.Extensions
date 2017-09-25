using Microsoft.VisualStudio.ProjectSystem;
using Microsoft.VisualStudio.ProjectSystem.VS;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Composition;

namespace Adfc.VisualStudio.Extension.VS2017.ProjectSystem
{
    [Export]
    [AppliesTo(UniqueCapability)]
    [ProjectTypeRegistration(ProjectGuidString,
      "ADF Community Project",
      "Data Factory (*.adfcproj);*.adfcproj",
      "adfcproj",
      "Data Factory",
      PossibleProjectExtensions = "adfcproj")]
    internal class AdfcProject
    {
        internal const string UniqueCapability = "AdfcProject";

        internal const string ProjectGuidString = "1cb5cf53-4394-4cd2-9deb-9cb709f77ed2";

        internal static readonly Guid ProjectGuid = Guid.Parse(ProjectGuidString);

        [ImportingConstructor]
        public AdfcProject(UnconfiguredProject unconfiguredProject)
        {
            ProjectHierarchies = new OrderPrecedenceImportCollection<IVsHierarchy>(projectCapabilityCheckProvider: unconfiguredProject);
        }

        [Import]
        internal UnconfiguredProject UnconfiguredProject { get; private set; }

        [Import]
        internal IActiveConfiguredProjectSubscriptionService SubscriptionService { get; private set; }

        [Import]
        internal IProjectThreadingService ProjectThreadingService { get; private set; }

        [Import]
        internal ActiveConfiguredProject<ConfiguredProject> ActiveConfiguredProject { get; private set; }

        [Import]
        internal ActiveConfiguredProject<AdfcProjectBuildConfiguration> MyActiveConfiguredProject { get; private set; }

        [ImportMany(ExportContractNames.VsTypes.IVsProject, typeof(IVsProject))]
        internal OrderPrecedenceImportCollection<IVsHierarchy> ProjectHierarchies { get; private set; }

        internal IVsHierarchy ProjectHierarchy
        {
            get { return this.ProjectHierarchies.Single().Value; }
        }
    }
}
