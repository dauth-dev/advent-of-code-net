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
    public class Day_7_Test
    {
        [TestMethod]
        public void GetBagTest()
        {
            var runner = new AdventOfCode.Day_07.Runner();
            var bag = runner.GetBag("light green bags contain 2 pale cyan bags");
            Assert.AreEqual("light green", bag.name);
            Assert.AreEqual("pale cyan", bag.bags[0].Name);
            Assert.AreEqual(2, bag.bags[0].Count);
            Assert.AreEqual(1, bag.bags.Count);

            bag = runner.GetBag("posh crimson bags contain 3 clear crimson bags, 3 mirrored tan bags, 5 muted coral bags, 4 striped yellow bags");
            Assert.AreEqual("posh crimson", bag.name);
            Assert.AreEqual("clear crimson", bag.bags[0].Name);
            Assert.AreEqual(3, bag.bags[0].Count);
            Assert.AreEqual("mirrored tan", bag.bags[1].Name);
            Assert.AreEqual(3, bag.bags[1].Count);
            Assert.AreEqual("muted coral", bag.bags[2].Name);
            Assert.AreEqual(5, bag.bags[2].Count);
            Assert.AreEqual("striped yellow", bag.bags[3].Name);
            Assert.AreEqual(4, bag.bags[3].Count);
            Assert.AreEqual(4, bag.bags.Count);
        }

        [TestMethod]
        public void GetBagMatrixTest()
        {
            var runner = new AdventOfCode.Day_07.Runner();
            var input = runner.GetBagMatrix(File.ReadAllLines(Path.Combine("Day_7", "inputTest.txt")));
            Assert.AreEqual(11, input.Count);
        }

        [TestMethod]
        public void GetFirstResultTest()
        {
            var runner = new AdventOfCode.Day_07.Runner();
            var input = runner.GetBagMatrix(File.ReadAllLines(Path.Combine("Day_7", "inputTest.txt")));
            var result = runner.GetResult(input, "shiny gold");
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void GetSecondResultTest()
        {
            var runner = new AdventOfCode.Day_07.Runner();
            var input = runner.GetBagMatrix(File.ReadAllLines(Path.Combine("Day_7", "inputTest.txt")));
            var result = runner.GetSecondResult(input, "shiny gold");
            Assert.AreEqual(32, result);
            input = runner.GetBagMatrix(File.ReadAllLines(Path.Combine("Day_7", "inputSecond.txt")));
            result = runner.GetSecondResult(input, "shiny gold");
            Assert.AreEqual(126, result);
        }
    }
}