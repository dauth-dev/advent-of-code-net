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
    public class Day_6_Test
    {
        [TestMethod]
        public void GetFirstTest()
        {
            var runner = new AdventOfCode.Day_06.Runner();
            var groups = runner.GetGroups(File.ReadAllLines(Path.Combine("Day_6", "inputTest.txt")).ToList());
            Assert.AreEqual(5, groups.Count);
            var clearedData = runner.GetClearedData(groups);
            Assert.AreEqual(3, clearedData[1]);
            Assert.AreEqual(3, clearedData[2]);
            Assert.AreEqual(3, clearedData[3]);
            Assert.AreEqual(1, clearedData[4]);
            Assert.AreEqual(1, clearedData[5]);
            var result = runner.GetResult(clearedData);
            Assert.AreEqual(11, result);
        }

        [TestMethod]
        public void GetSecondTest()
        {
            var runner = new AdventOfCode.Day_06.Runner();
            var groups = runner.GetSecondGroups(File.ReadAllLines(Path.Combine("Day_6", "inputTest.txt")).ToList());
            Assert.AreEqual(5, groups.Count);
            var clearedData = runner.GetClearedData(groups);
            Assert.AreEqual(3, clearedData[1]);
            Assert.AreEqual(0, clearedData[2]);
            Assert.AreEqual(1, clearedData[3]);
            Assert.AreEqual(1, clearedData[4]);
            Assert.AreEqual(1, clearedData[5]);
            var result = runner.GetResult(clearedData);
            Assert.AreEqual(6, result);
        }
    }
}