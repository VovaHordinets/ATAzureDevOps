using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Practice1xUnit
{
    public class xUnitTest1
    {

        private MyFunction.Function _myFunction;

        public xUnitTest1()
        {
            _myFunction = new MyFunction.Function();
        }

        public static IEnumerable<object[]> GetDataFromCSV()
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

        [Theory]
        [InlineData(23,4)]
        public void TestWithDataDriven(double x, double expectedResult)
        {
            var result = _myFunction.MyFunctionResult(x);
            Assert.Equal(result, expectedResult);
        }
        [Theory]
        [MemberData(nameof(GetDataFromCSV))]
        public void TestWithDataDrivenFromCSV(double x, double expectedResult)
        {
            var result = _myFunction.MyFunctionResult(x);
            Assert.Equal(result, expectedResult);
        }
    }
}
