using NUnit.Framework;
using System;

namespace RestfulBooker.ApiTests.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class SmokeTestAttribute : CategoryAttribute
    {
        public SmokeTestAttribute() : base("Smoke")
        {
        }
    }
}
