using Adfc.VisualStudio.Extension.VS2017.ProjectSystem;
using Adfc.VisualStudio.Extension.VS2017.Utils;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Linq;

namespace Adfc.VisualStudio.Extension.VS2017.NuGet
{
    public class NuGetRestoreSolutionEvents : IVsSolutionEvents, IVsSolutionEvents5
    {
        private static readonly SemanticVersion MinimumVersion = new SemanticVersion(0, 2, 1, "preview", string.Empty);
        private static readonly string MinimumVersionString = "0.2.1-preview";

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterCloseSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }

        public void OnBeforeOpenProject(ref Guid guidProjectID, ref Guid guidProjectType, string pszFileName)
        {
            if (guidProjectType == AdfcProject.ProjectGuid)
            {
                var msbuildProject = ProjectCollection.GlobalProjectCollection.LoadProject(pszFileName);

                UpgradeAdfcBuildNugetVersionIfNeeded(msbuildProject);

                var projectInstance = msbuildProject.CreateProjectInstance();
                projectInstance.Build("Restore", null);
            }
        }

        private void UpgradeAdfcBuildNugetVersionIfNeeded(Project msbuildProject)
        {
            var items = msbuildProject.GetItems("PackageReference");
            var adfcBuildItem = items?.FirstOrDefault(i => string.Equals("Adfc.Msbuild", i.EvaluatedInclude));
            if (adfcBuildItem != null)
            {
                var version = adfcBuildItem.GetMetadata("Version");
                if (SemanticVersion.TryParse(version.EvaluatedValue, out var semver))
                {
                    bool needsUpdate = semver < MinimumVersion;
                    if (needsUpdate)
                    {
                        adfcBuildItem.SetMetadataValue("Version", MinimumVersionString);
                        msbuildProject.Save();
                    }
                }
            }
        }
    }
}
