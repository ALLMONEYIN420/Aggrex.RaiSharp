using System.Collections;
using System.Collections.Generic;

namespace Aggrex.VirtualMachine
{
    public class InvocationContext
    {
        private Stack<ExecutionContext> _executionContexts;

        public byte[] Parameters { get; set; }
    }
}