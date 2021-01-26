using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Practice1MsTest
{
    [TestClass]
    public class MSUnitTest1
    {
        private MyFunction.Function _myFunction;
        private static IEnumerable<object[]> GetDataFromCSV()
        {
            using (var reader = new StreamReader(@"\codes\C#\Lab1Solution\data.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    double x = double.Parse(values[0]);
                    double expectedResult = double.Parse(values[1]);
                    yield return new object[] { x, expectedResult };
                }
            }
        }
        [TestInitialize]
        public void Setup()
        {
            _myFunction = new MyFunction.Function();
        }

        [TestMethod]
        [DataRow(23, 4)]
        public void TestWithDataDriven(double x, double expectedResult)
        {
            var result = _myFunction.MyFunctionResult(x);
            Assert.AreEqual(result, expectedResult);
        }
        [TestMethod]
        [DynamicData("GetDataFromCSV", DynamicDataSourceType.Method)]
        public void TestWithDataDrivenFromCSV(double x, double expectedResult)
        {
            var result = _myFunction.MyFunctionResult(x);
            Assert.AreEqual(result, expectedResult);
        }
    }
}
