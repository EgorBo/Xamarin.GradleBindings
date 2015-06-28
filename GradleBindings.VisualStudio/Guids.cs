// Guids.cs
// MUST match guids.h
using System;

namespace EgorBo.GradleBindings_VisualStudio
{
    static class GuidList
    {
        public const string guidGradleBindings_VisualStudioPkgString = "94487fc6-72c6-448b-a460-5e52f7ea2b92";
        public const string guidGradleBindings_VisualStudioCmdSetString = "82484a7f-647f-41c1-9f41-29b193185a8d";

        public static readonly Guid guidGradleBindings_VisualStudioCmdSet = new Guid(guidGradleBindings_VisualStudioCmdSetString);
    };
}