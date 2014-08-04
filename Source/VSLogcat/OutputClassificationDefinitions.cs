// Copyright (c) 2011 Blue Onion Software, All rights reserved
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using ly.VSLogcat;

namespace BlueOnionSoftware
{
    public static class OutputClassificationDefinitions
    {
        private const string logcat = "Logcat ";

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(LogType.VERBOSE)]
        public static ClassificationTypeDefinition VerboseDefinition { get; set; }

        [Name(LogType.VERBOSE)]
        [UserVisible(true)]
        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = LogType.VERBOSE)]
        public sealed class VERBOSEFormat : ClassificationFormatDefinition
        {
            public VERBOSEFormat()
            {
                DisplayName = logcat + LogType.VERBOSE;
                ForegroundColor = Colors.Black;
            }
        }

        [Export]
        [Name(LogType.DEBUG)]
        public static ClassificationTypeDefinition DebugDefinition { get; set; }

        [Name(LogType.DEBUG)]
        [UserVisible(true)]
        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = LogType.DEBUG)]
        public sealed class DEBUGFormat : ClassificationFormatDefinition
        {
            public DEBUGFormat()
            {
                DisplayName = logcat + LogType.DEBUG;
                ForegroundColor = Colors.Blue;
            }
        }

        [Export]
        [Name(LogType.INFO)]
        public static ClassificationTypeDefinition InfoDefinition { get; set; }

        [Name(LogType.INFO)]
        [UserVisible(true)]
        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = LogType.INFO)]
        public sealed class InfoFormat : ClassificationFormatDefinition
        {
            public InfoFormat()
            {
                DisplayName = logcat + LogType.INFO;
                ForegroundColor = Colors.Green;
            }
        }

        [Export]
        [Name(LogType.WARN)]
        public static ClassificationTypeDefinition WARNDefinition { get; set; }

        [Name(LogType.WARN)]
        [UserVisible(true)]
        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = LogType.WARN)]
        public sealed class WARNFormat : ClassificationFormatDefinition
        {
            public WARNFormat()
            {
                DisplayName = logcat + LogType.WARN;
                ForegroundColor = Colors.Orange;
            }
        }

        [Export]
        [Name(LogType.ERROR)]
        public static ClassificationTypeDefinition ERRORDefinition { get; set; }

        [Name(LogType.ERROR)]
        [UserVisible(true)]
        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = LogType.ERROR)]
        public sealed class ERRORFormat : ClassificationFormatDefinition
        {
            public ERRORFormat()
            {
                DisplayName = logcat + LogType.ERROR;
                ForegroundColor = Colors.Red;
            }
        }
    }
}