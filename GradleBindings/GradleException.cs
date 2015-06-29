using System;

namespace GradleBindings
{
    public class GradleException : Exception
    {
        public string OriginalScript { get; set; }

        public GradleException(string log, string originalScript)
            : base(log)
        {
            OriginalScript = originalScript;
        }
    }
}