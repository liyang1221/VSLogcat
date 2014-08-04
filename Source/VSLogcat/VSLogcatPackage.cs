using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

namespace ly.VSLogcat
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionExists)]
    [Guid(GuidList.guidVSLogcatPkgString)]
    public sealed class VSLogcatPackage : Package
    {
        public VSLogcatPackage()
        {
            Current = this;
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        public static VSLogcatPackage Current;
        public EnvDTE.DTE DTE
        {
            get
            {
                return GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            }
        }

        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();
            new Logcat();
            IVsDebugger debugger = (IVsDebugger)GetService(typeof(IVsDebugger));
            if (debugger != null)
            {
                ErrorHandler.ThrowOnFailure(debugger.AdviseDebugEventCallback(new DebugOutputEventCallback()));
            }
        }
    }
}
