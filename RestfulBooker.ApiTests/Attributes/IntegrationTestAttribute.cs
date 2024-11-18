using NUnit.Framework;
using System;

namespace RestfulBooker.ApiTests.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class IntegrationTestAttribute : CategoryAttribute
    {
        public IntegrationTestAttribute() : base("Integration")
        {
        }
    }
}
