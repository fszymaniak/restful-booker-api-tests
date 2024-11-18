using NUnit.Framework;
using System;

namespace RestfulBooker.ApiTests.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class RegressionTestAttribute : CategoryAttribute
    {
        public RegressionTestAttribute() : base("Regression")
        {
        }
    }
}
