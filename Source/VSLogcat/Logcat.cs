using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ly.VSLogcat
{
    public class Logcat
    {
        EnvDTE.OutputWindow OutputWnd;
        EnvDTE.OutputWindowPane DebugPane;
        Dictionary<string, EnvDTE.OutputWindowPane> OutputPanes;
        EnvDTE.OutputWindowPane CurrentPane;
        public static Logcat Current;

        Regex regex;

        internal Logcat()
        {
            var frame = VSLogcatPackage.Current.DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            Debug.Assert(frame != null);
            OutputWnd = frame.Object as EnvDTE.OutputWindow;
            DebugPane = OutputWnd.OutputWindowPanes.Item(GuidList.guidVSDebugOutputWndString);
            
            regex = new Regex(@"^\[(?<name>[a-zA-Z0-9\u4e00-\u9fa5_\-]+)\].*", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            OutputPanes = new Dictionary<string, EnvDTE.OutputWindowPane>(16);
            
            AddLogPane(LogType.VERBOSE);
            AddLogPane(LogType.DEBUG);
            AddLogPane(LogType.INFO);
            AddLogPane(LogType.WARN);
            AddLogPane(LogType.ERROR);

            Current = this;
        }

        private void AddLogPane(string name)
        {
            if (OutputPanes.ContainsKey(name)) return;
            var wnd = OutputWnd.OutputWindowPanes.Add(name);

            OutputPanes.Add(name, wnd);
        }

        public void Log(string type, string message)
        {
            if (OutputPanes.ContainsKey(type))
            {
                OutputPanes[type].OutputString(message);
            }
            System.Diagnostics.Debug.Print(type + " ------> " + message);
        }

        public string GetLogType(string text)
        {
            var match = regex.Match(text);

            if (match.Success)
            {
                return match.Groups["name"].Value.ToUpper();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    class LogType
    {
        public const string VERBOSE = "VERBOSE";
        public const string DEBUG = "DEBUG";
        public const string INFO = "INFO";
        public const string WARN = "WARN";
        public const string ERROR = "ERROR";

        public static bool Contains(string type)
        {
            return !string.IsNullOrEmpty(type) && (type == VERBOSE || type == DEBUG || type == INFO || type == WARN || type == ERROR);
        }
    }
}
