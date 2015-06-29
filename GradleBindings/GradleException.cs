using System;
using System.Text;

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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Gradle log:\n========\n");
            result.Append(Message);
            result.Append("\n\nOriginal script:\n========\n");
            result.Append(OriginalScript);
            result.Append("\n\nStack-Trace:\n========\n");
            result.Append(StackTrace);

            return result.ToString();
        }
    }
}