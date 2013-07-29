using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Procon.Core;

namespace CoreTests
{
    [TestClass]
    public class SymbolTests
    {
        [TestMethod]
        public void TestGetIndex()
        {
            // TEST valid input
            Assert.AreEqual(2, Symbol.getIndex('2'));
            Assert.AreEqual(10, Symbol.getIndex('A'));
            Assert.AreEqual(85, Symbol.getIndex('`'));
            // TEST invalid input
        }

        [TestMethod]
        public void TestGetCharacter()
        {
            // TEST valid input
            Assert.AreEqual('A', Symbol.getCharacter(10));
            Assert.AreEqual('0', Symbol.getCharacter(00));
            Assert.AreEqual('`', Symbol.getCharacter(85));


            // TEST invalid input
            try
            {
                Symbol.getCharacter(-1);
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }

            try
            {
                Symbol.getCharacter(86);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
