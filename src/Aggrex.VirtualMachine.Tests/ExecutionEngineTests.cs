using System;
using System.Linq;
using Aggrex.Common.BitSharp;
using Xunit;

namespace Aggrex.VirtualMachine.Tests
{
    public class ExecutionEngineTests
    {
        [Fact]
        public void SimpleTest()
        {
            ExecutionEngine engine = new ExecutionEngine();
            byte[] parameters = new byte[0];
            byte[] script = new byte[] {(byte) OpCode.PUSH1, (byte)OpCode.PUSH2, (byte)OpCode.ADD };
            ExecutionContext context = new ExecutionContext(parameters, script, null);
            engine.Execute(context);
            var result = context.Result;
        }
    }
}
