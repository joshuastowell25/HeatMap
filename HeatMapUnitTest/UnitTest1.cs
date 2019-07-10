using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemDisplayApp;
using System.Collections.Generic;
using SystemTools;

namespace HeatMapUnitTest
{
    [TestClass]
    public class UnitTests_DateBuckets
    {

        [TestMethod]
        public void TestMethod1()
        {
            HeatMapForm f = new HeatMapForm();
            List<dateBucket> buckets = f.getDateBuckets(DateTime.Parse("2000-01-01"), DateTime.Parse("2018-12-07"), bucketGranularity.YEARLY);
            Console.WriteLine(buckets.ToString());
            Assert.AreEqual(buckets.Count, 19);
            Assert.AreEqual(buckets[0].start, DateTime.Parse("2000-01-01"));
            Assert.AreEqual(buckets[0].end, DateTime.Parse("2000-12-31"));

            Assert.AreEqual(buckets[18].start, DateTime.Parse("2018-01-01"));
            Assert.AreEqual(buckets[18].end, DateTime.Parse("2018-12-07"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<inc> data = ToolClass.getData(@"D:\google_drive\Python Projects\^GSPC.csv");
            Assert.AreEqual(data[0].date, DateTime.Parse("2018-11-12"));
            Assert.AreEqual(data[0].closingPrice, 2726.219971);
        }

    }

    [TestClass]
    public class UnitTests_SystemParser
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
