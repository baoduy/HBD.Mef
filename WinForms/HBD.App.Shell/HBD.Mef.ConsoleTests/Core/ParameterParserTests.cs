using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Console.Core.Tests
{
    [TestClass()]
    public class ParameterParserTests
    {
        [TestMethod()]
        public void Parse_Parameters_Test()
        {
            var p = ParameterParser.Parse(new[] { "-JobName1","param1","param2", "param3", "-JobName2", "param1", "param2", "" });

            Assert.IsTrue(p.Count == 2);
            Assert.IsTrue(p["JobName1"].Parameters.Count == 3);
            Assert.IsTrue(p["JobName2"].Parameters.Count == 2);
        }
    }
}