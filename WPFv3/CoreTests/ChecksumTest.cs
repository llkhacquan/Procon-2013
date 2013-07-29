using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Procon.Core;

namespace CoreTests
{
    [TestClass]
    public class ChecksumTest
    {
        [TestMethod]
        public void TestCalculateChecksum()
        {
            Assert.AreEqual(6, Checksum.calculateChecksum("123"));
            Assert.AreEqual(5, Checksum.calculateChecksum("1234567890"));
            Assert.AreEqual(0, Checksum.calculateChecksum("1234567890987654321"));

        }

        [TestMethod]
        public void TestAddChecksum()
        {
            Assert.AreEqual("1236", Checksum.addCheckSum("123"));
            Assert.AreEqual("1234567890123456789012340678900", Checksum.addCheckSum("12345678901234567890123467890"));
        }
        
        [TestMethod]
        public void TestCheckChecksumUnit()
        {
            Assert.AreEqual(true, Checksum.checkChecksumUnit("12345678905"));
            Assert.AreEqual(true, Checksum.checkChecksumUnit("12345678"));
            Assert.AreEqual(false, Checksum.checkChecksumUnit("12345679"));
        }

        [TestMethod]
        public void TestCheckChecksum()
        {
            Assert.AreEqual(-1, Checksum.checkChecksum("12345678"));
            Assert.AreEqual(-1, Checksum.checkChecksum("123456789012345678901234012345678"));

            Assert.AreEqual(7, Checksum.checkChecksum("12345670"));
            Assert.AreEqual(24, Checksum.checkChecksum("123456789012345678901234112345678"));
            Assert.AreEqual(34, Checksum.checkChecksum("12345678901234567890123401234567890"));
        }
    }
}
