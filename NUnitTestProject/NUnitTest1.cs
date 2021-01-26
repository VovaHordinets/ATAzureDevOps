using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace NUnitTestProject
{
    [SetUpFixture]
    public class StartUpClass
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestContext.Progress.WriteLine("Hello World!");
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            TestContext.Progress.WriteLine("Cruel World!");
        }
    }
        [TestFixture]
        public class Tests
        {
        private static IEnumerable<double[]> GetDataFromCSV()
        {
            using (var reader = new StreamReader(@"\codes\C#\Lab1Solution\data.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    double x = double.Parse(values[0]);
                    double expectedResult = double.Parse(values[1]);
                    yield return new[] { x, expectedResult };
                }
            }
        }
        private MyFunction.Function _myFunction;
        [SetUp]
        public void Setup()
        {
            Console.WriteLine($"Test Started - {DateTime.Now.ToString()}");
            _myFunction = new MyFunction.Function();
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine($"Test Ended - {DateTime.Now.ToString()}");
        }

        [Test]
        [TestCase(23,4),
        TestCase(11, 2),
        TestCase(16, 3),]
        public void TestWithDataDriven(double x, double expectedResult)
        {
            var result = _myFunction.MyFunctionResult(x);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        [Test]
        [TestCaseSource("GetDataFromCSV")]
        public void TestWithDataDrivenFromCSV(double x, double expectedResult)
        {
            var result = _myFunction.MyFunctionResult(x);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        [Test]
        public void TestWithRandomDataDriven([Random(8, 2000, 100)]
        double x)
        {
          var result = _myFunction.MyFunctionResult(x);
            Assert.That(result, Is.Not.NaN);
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        [TestCase(-40)]
        public void TestWithNegativeDataDriven(double x)
        {
            Assert.That(() => _myFunction.MyFunctionResult(x), Throws.Exception.TypeOf<ArithmeticException> ());
        }
        [Test]
        [TestCase(23, 4)]
        [Timeout(900), Retry(5)]
        public void TestWithDataDriven_WhenTimeExecutionLessThan900(double x, double expectedResult)
        {
            var result = _myFunction.MyFunctionResult(x);
            Thread.Sleep(901);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}