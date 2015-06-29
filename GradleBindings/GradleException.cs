using System;

namespace GradleBindings
{
    public class GradleException : Exception
    {
        public GradleException(string log)
            : base(log)
        {
        }
    }
}