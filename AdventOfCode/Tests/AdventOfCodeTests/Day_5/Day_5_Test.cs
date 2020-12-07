using AdventOfCode.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeTests.Utils
{
    [TestClass]
    public class Day_5_Test
    {
        [TestMethod]
        public void GetRowTest()
        {
            var runner = new AdventOfCode.Day_05.Runner();
            var result = runner.GetRow("FBFBBFFRLR");
            Assert.AreEqual(44, result);
            result = runner.GetRow("BFFFBBFRRR");
            Assert.AreEqual(70, result);
            result = runner.GetRow("FFFBBBFRRR");
            Assert.AreEqual(14, result);
        }

        [TestMethod]
        public void GetIdTest()
        {
            var runner = new AdventOfCode.Day_05.Runner();
            var result = runner.GetId("BFFFBBFRRR");
            Assert.AreEqual(567, result);
            result = runner.GetId("FFFBBBFRRR");
            Assert.AreEqual(119, result);
            result = runner.GetId("BBFFBBFRLL");
            Assert.AreEqual(820, result);
        }

        [TestMethod]
        public void GetIdsTest()
        {
            var runner = new AdventOfCode.Day_05.Runner();
            var result = runner.GetMissingId(File.ReadAllLines(Path.Combine("Day_5", "inputTest.txt")).ToList());
        }

        [TestMethod]
        public void GetColumnTest()
        {
            var runner = new AdventOfCode.Day_05.Runner();
            var result = runner.GetColumn("FBFBBFFRLR");
            Assert.AreEqual(5, result);
            result = runner.GetColumn("BFFFBBFRRR");
            Assert.AreEqual(7, result);
            result = runner.GetColumn("FFFBBBFRRR");
            Assert.AreEqual(7, result);
            result = runner.GetColumn("BBFFBBFRLL");
            Assert.AreEqual(4, result);
        }
    }
}