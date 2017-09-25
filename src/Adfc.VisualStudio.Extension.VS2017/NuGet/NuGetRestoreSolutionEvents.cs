using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using Adfc.VisualStudio.Extension.VS2017.ProjectSystem;

namespace Adfc.VisualStudio.Extension.VS2017.NuGet
{
    public class NuGetRestoreSolutionEvents : IVsSolutionEvents, IVsSolutionEvents5
    {
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
                var msbuildProject = Microsoft.Build.Evaluation.ProjectCollection.GlobalProjectCollection.LoadProject(pszFileName);
                var projectInstance = msbuildProject.CreateProjectInstance();
                projectInstance.Build("Restore", null);
            }
        }
    }
}
