using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Aggrex.Common;
using Aggrex.Common.BitSharp;

namespace Aggrex.VirtualMachine
{
    public class EvaluationStack
    {
        private Stack<StackItem> _stack;

        public EvaluationStack()
        {
            _stack = new Stack<StackItem>();
        }

        public int Count => _stack.Count;

        public StackItem Peek()
        {
            return _stack.Peek();
        }
        public void Push(StackItem item)
        {
            _stack.Push(item);
        }

        public void Push(UInt160 data)
        {
            _stack.Push(new StackItem(StackItemType.UInt160, data.ToByteArray()));
        }

        public void Push(UInt256 data)
        {
            _stack.Push(new StackItem(StackItemType.UInt256, data.ToByteArray()));
        }

        public void Push(byte[] data)
        {
            _stack.Push(new StackItem(StackItemType.ByteArray, data));
        }

        public void Push(BigInteger data)
        {
            _stack.Push(new StackItem(StackItemType.BigInteger, data.ToByteArray()));
        }

        public StackItem Pop()
        {
            return _stack.Pop();
        }
    }
}

