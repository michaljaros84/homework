using Company.Schedule.Services.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Company.Schedule.Tests
{
    [TestClass]
    public class DateCalculatorTests
    {
        [TestMethod]
        public void CalculateDateHappyPathTest()
        {
            var underTest = new DateCalculator();

            var res = underTest.GetScheduledDates(new DateTime(2021, 6, 1), new int[] { 1, 4, 7 });

            Assert.AreEqual(new DateTime(2021, 6, 2) ,res[0]);
            Assert.AreEqual(new DateTime(2021, 6, 5), res[1]);
            Assert.AreEqual(new DateTime(2021, 6, 8), res[2]);
        }
    }
}
