using System;

namespace Api.Attributes
{
    public class TelemetryAttribute : Attribute
    {
        public string Method { get; set; }
        public string ClassName { get; set; }

        public TelemetryAttribute(string className, string method)
        {
            Method = method;
            ClassName = className;
         

        }
    }
}