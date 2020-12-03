using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests.Utils
{
    [TestClass]
    public class Day_4_Test : AbstractTestRunner
    {
        [TestMethod]
        public void GetValidPassportsTest()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.GetValidPassports(File.ReadAllLines(Path.Combine("Day_4", "inputTest.txt")));
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetPassportsTest()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.GetPassportsFromFile(File.ReadAllLines(Path.Combine("Day_4", "inputTest.txt")));
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void ExtractInformationsTest()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.ExtractInformations("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd");
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("gry", result["ecl"]);
            Assert.AreEqual("860033327", result["pid"]);
            Assert.AreEqual("2020", result["eyr"]);
            Assert.AreEqual("#fffffd", result["hcl"]);
        }

        [TestMethod]
        public void GetValidPassportsSecondInvalidTest()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.ExtendedChecks(runner.GetValidPassports(File.ReadAllLines(Path.Combine("Day_4", "inputTestSecondInvalid.txt"))));
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetValidPassportsSecondValidTest()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.ExtendedChecks(runner.GetValidPassports(File.ReadAllLines(Path.Combine("Day_4", "inputTestSecondValid.txt"))));
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void BirthdateValidation()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.CheckYear("2002", 1920, 2002);
            Assert.IsTrue(result);
            result = runner.CheckYear("1920", 1920, 2002);
            Assert.IsTrue(result);
            result = runner.CheckYear("2003", 1920, 2002);
            Assert.IsFalse(result);
            result = runner.CheckYear("20", 1920, 2002);
            Assert.IsFalse(result);
            result = runner.CheckYear("2010", 2010, 2020);
            Assert.IsTrue(result);
            result = runner.CheckYear("2020", 2010, 2020);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PidValidation()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.CheckPid("000000001");
            Assert.IsTrue(result);
            result = runner.CheckHairColor("0123456789");
            Assert.IsFalse(result);
            result = runner.CheckHairColor("200000001");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EyeColorValidation()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.CheckEyeColor("brn");
            Assert.IsTrue(result);
            result = runner.CheckHairColor("wat");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HairColorValidation()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.CheckHairColor("#123abc");
            Assert.IsTrue(result);
            result = runner.CheckHairColor("#123abcd");
            Assert.IsFalse(result);
            result = runner.CheckHairColor("#123abz");
            Assert.IsFalse(result);
            result = runner.CheckHairColor("123abc");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HeightValidation()
        {
            var runner = GetRunner<AdventOfCode.Day_04.Runner>();
            var result = runner.CheckHeight("59in");
            Assert.IsTrue(result);
            result = runner.CheckHeight("76in");
            Assert.IsTrue(result);
            result = runner.CheckHeight("70in");
            Assert.IsTrue(result);
            result = runner.CheckHeight("150cm");
            Assert.IsTrue(result);
            result = runner.CheckHeight("170cm");
            Assert.IsTrue(result);
            result = runner.CheckHeight("193cm");
            Assert.IsTrue(result);

            result = runner.CheckHeight("149cm");
            Assert.IsFalse(result);
            result = runner.CheckHeight("194cm");
            Assert.IsFalse(result);
            result = runner.CheckHeight("77in");
            Assert.IsFalse(result);
            result = runner.CheckHeight("58in");
            Assert.IsFalse(result);
            result = runner.CheckHeight("190");
            Assert.IsFalse(result);
        }
    }
}