// Copyright (c) 2012 Blue Onion Software, All rights reserved
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using EnvDTE;

namespace BlueOnionSoftware
{
    public class OutputClassifier : IClassifier
    {
        private readonly IClassificationTypeRegistryService _classificationTypeRegistry;

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public OutputClassifier(IClassificationTypeRegistryService registry, IServiceProvider serviceProvider)
        {
            try
            {
                _classificationTypeRegistry = registry;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            try
            {
                var spans = new List<ClassificationSpan>();
                var snapshot = span.Snapshot;
                
                if (snapshot == null || snapshot.Length == 0)
                {
                    return spans;
                }

                var start = span.Start.GetContainingLine().LineNumber;
                var end = (span.End - 1).GetContainingLine().LineNumber;
                for (var i = start; i <= end; i++)
                {
                    var line = snapshot.GetLineFromLineNumber(i);
                    var text = line.GetText();
                    //var snapshotSpan = new SnapshotSpan(line.Start, line.Length);
                    //var text = line.Snapshot.GetText(snapshotSpan);
                    if (string.IsNullOrEmpty(text) == false)
                    {
                        var classificationName = ly.VSLogcat.Logcat.Current.GetLogType(text);

                        IClassificationType type;
                        //if (!string.IsNullOrEmpty(classificationName))
                        if(ly.VSLogcat.LogType.Contains(classificationName))
                        {
                            //snapshot.TextBuffer.Delete(snapshotSpan);

                            //int index = text.IndexOf(']') + 1;
                            //SnapshotSpan newSpan = new SnapshotSpan(line.Start + index, line.Length - index);

                            //if (newSpan.Length > 0)
                            {
                                type = _classificationTypeRegistry.GetClassificationType(classificationName);

                                spans.Add(new ClassificationSpan(line.Extent, type));
                            }
                            //ly.VSLogcat.Logcat.Current.Log(classificationName, text);
                        }
                        //else
                        //{
                        //    type = _classificationTypeRegistry.GetClassificationType(ly.VSLogcat.LogType.ERROR);
                            
                        //}
                        
                    }
                }
                return spans;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return null;
            }
        }
    }
}
