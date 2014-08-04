// Guids.cs
// MUST match guids.h
using System;

namespace ly.VSLogcat
{
    static class GuidList
    {
        public const string guidVSLogcatPkgString = "631fa957-53f9-47f0-80c0-aae0cb2a0170";
        public const string guidVSLogcatCmdSetString = "2718a4a0-d884-4bee-9f15-7e41c5c2ae7a";
        public const string guidVSDebugOutputWndString = "{FC076020-078A-11D1-A7DF-00A0C9110051}";

        public static readonly Guid guidVSLogcatCmdSet = new Guid(guidVSLogcatCmdSetString);
    };
}