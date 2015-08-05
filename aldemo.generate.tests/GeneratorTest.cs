using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aldemo.generate.tests
{
    [TestClass]
    public class GeneratorTest
    {
        [TestMethod]
        public void Generate()
        {
            Generator.Run(10, 10);
        }
        [TestMethod]
        public void Cleanup()
        {
            Generator.Clean();
        }
    }
}
