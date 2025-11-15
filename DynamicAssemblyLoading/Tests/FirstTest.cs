using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Tests
{
    [TestClass]
    public class TestDynamincLoading
    {
        [TestMethod]
        public void DynamicLoading_Test1()
        {
            var assembly = Assembly.Load("DynamicAssemblyLoading");
            Assert.AreEqual(
                assembly.FullName, 
                "DynamicAssemblyLoading, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        }
    }
}
