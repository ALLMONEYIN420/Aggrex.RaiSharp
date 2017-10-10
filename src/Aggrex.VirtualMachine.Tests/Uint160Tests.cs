using System;
using System.Linq;
using Aggrex.Common.BitSharp;
using Xunit;

namespace Aggrex.VirtualMachine.Tests
{
    public class Uint160Tests
    {
        [Fact]
        public void ToStringTest()
        {
            byte[] byteRepresentation = { 0xFA , 0xFF, 0x00, 0x1A, 0x22, 0x33, 0x34, 0x0F, 0xAA, 0x01, 0xBB, 0xCC, 0xDD, 0xEE, 0xAB, 0xCD, 0xEF, 0xAF, 0x34, 0x77 };
            UInt160 num1 = new UInt160(byteRepresentation);
            Assert.Equal(num1.ToString().ToLower(), "7734afefcdabeeddccbb01aa0f3433221a00fffa");
        }
    }
}
