using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
// Copyright (c) 2012 Blue Onion Software, All rights reserved
using System;
using System.ComponentModel.Composition;

#pragma warning disable 649

namespace BlueOnionSoftware
{
    [ContentType("Output")]
    [Export(typeof(IClassifierProvider))]
    public class OutputClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry;
        [Import]
        internal IContentTypeRegistryService ContentTypeRegistryService { get; set; }
        [Import]
        internal SVsServiceProvider ServiceProvider;

        public static OutputClassifier OutputClassifier { get; private set; }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            try
            {
                if (OutputClassifier == null)
                {
                    OutputClassifier = new OutputClassifier(ClassificationRegistry, ServiceProvider);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            return OutputClassifier;
        }
    }
}