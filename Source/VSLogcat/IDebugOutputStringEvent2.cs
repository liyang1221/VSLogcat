using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;

namespace ly.VSLogcat
{
    public class DebugOutputEventCallback : IDebugEventCallback2
    {
        string[] sp = { "\r\n" };
        public int Event(IDebugEngine2 pEngine, IDebugProcess2 pProcess, IDebugProgram2 pProgram, IDebugThread2 pThread, IDebugEvent2 pEvent, ref Guid riidEvent, uint dwAttrib)
        {
            if (riidEvent == typeof(IDebugOutputStringEvent2).GUID)
            {
                IDebugOutputStringEvent2 ev = pEvent as IDebugOutputStringEvent2;
                if (ev != null)
                {
                    string message;
                    if (ErrorHandler.Succeeded(ev.GetString(out message)))
                    {
                        var lines = message.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                        
                        foreach(var line in lines)
                        {
                            HandleMessage(line);
                        }
                    }
                }
            }

            return VSConstants.S_OK;
        }

        void HandleMessage(string message)
        {
            var classificationName = ly.VSLogcat.Logcat.Current.GetLogType(message);

            var type = Logcat.Current.GetLogType(message);
            if (LogType.Contains(type))
            {
                message = message.Substring(message.IndexOf(']') + 1);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    Logcat.Current.Log(type, string.Format("{0}\r\n", message));
                }
            }
        }
    }
}