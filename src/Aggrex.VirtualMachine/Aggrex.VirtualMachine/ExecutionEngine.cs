using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Aggrex.VirtualMachine
{
    internal class ExecutionEngine : IExecutionEngine
    {
        private Stack<ExecutionContext> _contextStack;
        private EvaluationStack _evaluationStack;
        private EvaluationStack _altStack;

        public ExecutionEngine()
        {
            _contextStack = new Stack<ExecutionContext>();
            _evaluationStack = new EvaluationStack();
            _altStack = new EvaluationStack();
        }

        public void Execute(ExecutionContext initialContext)
        {
            _contextStack.Push(initialContext);

            while (_contextStack.Any())
            {
                ExecuteContext(_contextStack.Pop());
            }
        }

        private void ExecuteContext(ExecutionContext context)
        {
            while (context.HasMoreInstructions)
            {
                OpCode opCode = context.ProgramReader.ReadNextInstruction();

                switch (opCode)
                {
                    case OpCode.PUSH0:
                        _evaluationStack.Push(new BigInteger(0));
                        break;
                    case OpCode.PUSHDATA1:
                        _evaluationStack.Push(new BigInteger(1));
                        break;
                    case OpCode.PUSHDATA2:
                        _evaluationStack.Push(new BigInteger(2));
                        break;
                    //case OpCode.PUSHDATA4:
                    //    _evaluationStack.Push(context.ProgramReader.ReadBytes(context.ProgramReader.ReadInt32()));
                    //    break;
                    //case OpCode.PUSHM1:
                    //case OpCode.PUSH1:
                    //case OpCode.PUSH2:
                    //case OpCode.PUSH3:
                    //case OpCode.PUSH4:
                    //case OpCode.PUSH5:
                    //case OpCode.PUSH6:
                    //case OpCode.PUSH7:
                    //case OpCode.PUSH8:
                    //case OpCode.PUSH9:
                    //case OpCode.PUSH10:
                    //case OpCode.PUSH11:
                    //case OpCode.PUSH12:
                    //case OpCode.PUSH13:
                    //case OpCode.PUSH14:
                    //case OpCode.PUSH15:
                    //case OpCode.PUSH16:
                    //    _evaluationStack.Push((int)opCode - (int)OpCode.PUSH1 + 1);
                    //    break;

                    //    Control
                    //case OpCode.NOP:
                    //    break;
                    //case OpCode.JMP:
                    //case OpCode.JMPIF:
                    //case OpCode.JMPIFNOT:
                    //    TODO FIX
                    //    {
                    //        int offset = context.ProgramReader.ReadInt16();
                    //        offset = context.InstructionPointer + offset - 3;
                    //        if (offset < 0 || offset > context.Script.Length)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        bool fValue = true;
                    //        if (opCode > OpCode.JMP)
                    //        {
                    //            fValue = _evaluationStack.Pop().GetBoolean();
                    //            if (opCode == OpCode.JMPIFNOT)
                    //                fValue = !fValue;
                    //        }
                    //        if (fValue)
                    //            context.InstructionPointer = offset;
                    //    }
                    //    break;
                    //case OpCode.CALL:
                    //    TODO FIX
                    //    InvocationStack.Push(context.Clone());
                    //    context.InstructionPointer += 2;
                    //    ExecuteOp(OpCode.JMP, CurrentContext);
                    //    break;
                    //case OpCode.RET:
                    //    TODO FIX
                    //    InvocationStack.Pop().Dispose();
                    //    if (InvocationStack.Count == 0)
                    //        State |= VMState.HALT;
                    //    break;
                    //case OpCode.APPCALL:
                    //case OpCode.TAILCALL:
                    //    TODO FIX
                    //    {
                    //        if (table == null)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        byte[] script_hash = context.OpReader.ReadBytes(20);
                    //        byte[] script = table.GetScript(script_hash);
                    //        if (script == null)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        if (opCode == OpCode.TAILCALL)
                    //            InvocationStack.Pop().Dispose();
                    //        LoadScript(script);
                    //    }
                    //    break;
                    //case OpCode.SYSCALL:
                    //    TODO FIX
                    //    if (!service.Invoke(Encoding.ASCII.GetString(context.OpReader.ReadVarBytes(252)), this))
                    //        State |= VMState.FAULT;
                    //    break;

                    //    Stack ops
                    //case OpCode.DUPFROMALTSTACK:
                    //    _evaluationStack.Push(_altStack.Peek());
                    //    break;
                    //case OpCode.TOALTSTACK:
                    //    _altStack.Push(_evaluationStack.Pop());
                    //    break;
                    //case OpCode.FROMALTSTACK:
                    //    _evaluationStack.Push(_altStack.Pop());
                    //    break;
                    //case OpCode.XDROP:
                    //    {
                    //        TODO FIX
                    //        BigInteger n = new BigInteger(_evaluationStack.Pop().Item);
                    //        if (n < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        for (int i = 0; i < n; i++)
                    //        {
                    //            _evaluationStack.Pop(n);
                    //        }
                    //        break;
                    //    }

                    //case OpCode.XSWAP:
                    //    {
                    //        TODO FIX
                    //        BigInteger n = new BigInteger(_evaluationStack.Pop().Item);
                    //        if (n < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        if (n == 0) break;
                    //        StackItem xn = _evaluationStack.Peek(n);
                    //        _evaluationStack.Set(n, _evaluationStack.Peek());
                    //        _evaluationStack.Set(0, xn);
                    //    }
                    //    break;
                    //case OpCode.XTUCK:
                    //    {
                    //        int n = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (n <= 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        _evaluationStack.Insert(n, _evaluationStack.Peek());
                    //    }
                    //    break;
                    //case OpCode.DEPTH:
                    //    _evaluationStack.Push(_evaluationStack.Count);
                    //    break;
                    //case OpCode.DROP:
                    //    _evaluationStack.Pop();
                    //    break;
                    //case OpCode.DUP:
                    //    _evaluationStack.Push(_evaluationStack.Peek());
                    //    break;
                    //case OpCode.NIP:
                    //    {
                    //        StackItem x2 = _evaluationStack.Pop();
                    //        _evaluationStack.Pop();
                    //        _evaluationStack.Push(x2);
                    //    }
                    //    break;
                    //case OpCode.OVER:
                    //    {
                    //        StackItem x2 = _evaluationStack.Pop();
                    //        StackItem x1 = _evaluationStack.Peek();
                    //        _evaluationStack.Push(x2);
                    //        _evaluationStack.Push(x1);
                    //    }
                    //    break;
                    //case OpCode.PICK:
                    //    {
                    //        int n = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (n < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        _evaluationStack.Push(_evaluationStack.Peek(n));
                    //    }
                    //    break;
                    //case OpCode.ROLL:
                    //    {
                    //        int n = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (n < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        if (n == 0) break;
                    //        _evaluationStack.Push(_evaluationStack.Remove(n));
                    //    }
                    //    break;
                    //case OpCode.ROT:
                    //    {
                    //        StackItem x3 = _evaluationStack.Pop();
                    //        StackItem x2 = _evaluationStack.Pop();
                    //        StackItem x1 = _evaluationStack.Pop();
                    //        _evaluationStack.Push(x2);
                    //        _evaluationStack.Push(x3);
                    //        _evaluationStack.Push(x1);
                    //    }
                    //    break;
                    //case OpCode.SWAP:
                    //    {
                    //        StackItem x2 = _evaluationStack.Pop();
                    //        StackItem x1 = _evaluationStack.Pop();
                    //        _evaluationStack.Push(x2);
                    //        _evaluationStack.Push(x1);
                    //    }
                    //    break;
                    //case OpCode.TUCK:
                    //    {
                    //        StackItem x2 = _evaluationStack.Pop();
                    //        StackItem x1 = _evaluationStack.Pop();
                    //        _evaluationStack.Push(x2);
                    //        _evaluationStack.Push(x1);
                    //        _evaluationStack.Push(x2);
                    //    }
                    //    break;
                    //case OpCode.CAT:
                    //    {
                    //        byte[] x2 = _evaluationStack.Pop().GetByteArray();
                    //        byte[] x1 = _evaluationStack.Pop().GetByteArray();
                    //        _evaluationStack.Push(x1.Concat(x2).ToArray());
                    //    }
                    //    break;
                    //case OpCode.SUBSTR:
                    //    {
                    //        int count = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (count < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        int index = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (index < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        byte[] x = _evaluationStack.Pop().GetByteArray();
                    //        _evaluationStack.Push(x.Skip(index).Take(count).ToArray());
                    //    }
                    //    break;
                    //case OpCode.LEFT:
                    //    {
                    //        int count = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (count < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        byte[] x = _evaluationStack.Pop().GetByteArray();
                    //        _evaluationStack.Push(x.Take(count).ToArray());
                    //    }
                    //    break;
                    //case OpCode.RIGHT:
                    //    {
                    //        int count = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (count < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        byte[] x = _evaluationStack.Pop().GetByteArray();
                    //        if (x.Length < count)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        _evaluationStack.Push(x.Skip(x.Length - count).ToArray());
                    //    }
                    //    break;
                    //case OpCode.SIZE:
                    //    {
                    //        byte[] x = _evaluationStack.Pop().GetByteArray();
                    //        _evaluationStack.Push(x.Length);
                    //    }
                    //    break;

                    //    Bitwise logic
                    //case OpCode.INVERT:
                    //    {
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(~x);
                    //    }
                    //    break;
                    //case OpCode.AND:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 & x2);
                    //    }
                    //    break;
                    //case OpCode.OR:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 | x2);
                    //    }
                    //    break;
                    //case OpCode.XOR:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 ^ x2);
                    //    }
                    //    break;
                    //case OpCode.EQUAL:
                    //    {
                    //        StackItem x2 = _evaluationStack.Pop();
                    //        StackItem x1 = _evaluationStack.Pop();
                    //        _evaluationStack.Push(x1.Equals(x2));
                    //    }
                    //    break;

                    //    Numeric
                    //case OpCode.INC:
                    //    {
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x + 1);
                    //    }
                    //    break;
                    //case OpCode.DEC:
                    //    {
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x - 1);
                    //    }
                    //    break;
                    //case OpCode.SIGN:
                    //    {
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x.Sign);
                    //    }
                    //    break;
                    //case OpCode.NEGATE:
                    //    {
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(-x);
                    //    }
                    //    break;
                    //case OpCode.ABS:
                    //    {
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(BigInteger.Abs(x));
                    //    }
                    //    break;
                    //case OpCode.NOT:
                    //    {
                    //        bool x = _evaluationStack.Pop().GetBoolean();
                    //        _evaluationStack.Push(!x);
                    //    }
                    //    break;
                    //case OpCode.NZ:
                    //    {
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x != BigInteger.Zero);
                    //    }
                    //    break;
                    //case OpCode.ADD:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 + x2);
                    //    }
                    //    break;
                    //case OpCode.SUB:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 - x2);
                    //    }
                    //    break;
                    //case OpCode.MUL:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 * x2);
                    //    }
                    //    break;
                    //case OpCode.DIV:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 / x2);
                    //    }
                    //    break;
                    //case OpCode.MOD:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 % x2);
                    //    }
                    //    break;
                    //case OpCode.SHL:
                    //    {
                    //        int n = (int)_evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x << n);
                    //    }
                    //    break;
                    //case OpCode.SHR:
                    //    {
                    //        int n = (int)_evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x >> n);
                    //    }
                    //    break;
                    //case OpCode.BOOLAND:
                    //    {
                    //        bool x2 = _evaluationStack.Pop().GetBoolean();
                    //        bool x1 = _evaluationStack.Pop().GetBoolean();
                    //        _evaluationStack.Push(x1 && x2);
                    //    }
                    //    break;
                    //case OpCode.BOOLOR:
                    //    {
                    //        bool x2 = _evaluationStack.Pop().GetBoolean();
                    //        bool x1 = _evaluationStack.Pop().GetBoolean();
                    //        _evaluationStack.Push(x1 || x2);
                    //    }
                    //    break;
                    //case OpCode.NUMEQUAL:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 == x2);
                    //    }
                    //    break;
                    //case OpCode.NUMNOTEQUAL:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 != x2);
                    //    }
                    //    break;
                    //case OpCode.LT:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 < x2);
                    //    }
                    //    break;
                    //case OpCode.GT:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 > x2);
                    //    }
                    //    break;
                    //case OpCode.LTE:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 <= x2);
                    //    }
                    //    break;
                    //case OpCode.GTE:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(x1 >= x2);
                    //    }
                    //    break;
                    //case OpCode.MIN:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(BigInteger.Min(x1, x2));
                    //    }
                    //    break;
                    //case OpCode.MAX:
                    //    {
                    //        BigInteger x2 = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x1 = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(BigInteger.Max(x1, x2));
                    //    }
                    //    break;
                    //case OpCode.WITHIN:
                    //    {
                    //        BigInteger b = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger a = _evaluationStack.Pop().GetBigInteger();
                    //        BigInteger x = _evaluationStack.Pop().GetBigInteger();
                    //        _evaluationStack.Push(a <= x && x < b);
                    //    }
                    //    break;

                    //    Crypto
                    //case OpCode.SHA1:
                    //    using (SHA1 sha = SHA1.Create())
                    //    {
                    //        byte[] x = _evaluationStack.Pop().GetByteArray();
                    //        _evaluationStack.Push(sha.ComputeHash(x));
                    //    }
                    //    break;
                    //case OpCode.SHA256:
                    //    using (SHA256 sha = SHA256.Create())
                    //    {
                    //        byte[] x = _evaluationStack.Pop().GetByteArray();
                    //        _evaluationStack.Push(sha.ComputeHash(x));
                    //    }
                    //    break;
                    //case OpCode.HASH160:
                    //    {
                    //        byte[] x = _evaluationStack.Pop().GetByteArray();
                    //        _evaluationStack.Push(Crypto.Hash160(x));
                    //    }
                    //    break;
                    //case OpCode.HASH256:
                    //    {
                    //        byte[] x = _evaluationStack.Pop().GetByteArray();
                    //        _evaluationStack.Push(Crypto.Hash256(x));
                    //    }
                    //    break;
                    //case OpCode.CHECKSIG:
                    //    {
                    //        byte[] pubkey = _evaluationStack.Pop().GetByteArray();
                    //        byte[] signature = _evaluationStack.Pop().GetByteArray();
                    //        try
                    //        {
                    //            _evaluationStack.Push(Crypto.VerifySignature(ScriptContainer.GetMessage(), signature, pubkey));
                    //        }
                    //        catch (ArgumentException)
                    //        {
                    //            _evaluationStack.Push(false);
                    //        }
                    //    }
                    //    break;
                    //case OpCode.CHECKMULTISIG:
                    //    {
                    //        int n = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (n < 1)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        byte[][] pubkeys = new byte[n][];
                    //        for (int i = 0; i < n; i++)
                    //            pubkeys[i] = _evaluationStack.Pop().GetByteArray();
                    //        int m = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (m < 1 || m > n)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        byte[][] signatures = new byte[m][];
                    //        for (int i = 0; i < m; i++)
                    //            signatures[i] = _evaluationStack.Pop().GetByteArray();
                    //        byte[] message = ScriptContainer.GetMessage();
                    //        bool fSuccess = true;
                    //        try
                    //        {
                    //            for (int i = 0, j = 0; fSuccess && i < m && j < n;)
                    //            {
                    //                if (Crypto.VerifySignature(message, signatures[i], pubkeys[j]))
                    //                    i++;
                    //                j++;
                    //                if (m - i > n - j)
                    //                    fSuccess = false;
                    //            }
                    //        }
                    //        catch (ArgumentException)
                    //        {
                    //            fSuccess = false;
                    //        }
                    //        _evaluationStack.Push(fSuccess);
                    //    }
                    //    break;

                    //    Array
                    //case OpCode.ARRAYSIZE:
                    //    {
                    //        StackItem item = _evaluationStack.Pop();
                    //        if (!item.IsArray)
                    //            _evaluationStack.Push(item.GetByteArray().Length);
                    //        else
                    //            _evaluationStack.Push(item.GetArray().Length);
                    //    }
                    //    break;
                    //case OpCode.PACK:
                    //    {
                    //        int size = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (size < 0 || size > _evaluationStack.Count)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        StackItem[] items = new StackItem[size];
                    //        for (int i = 0; i < size; i++)
                    //            items[i] = _evaluationStack.Pop();
                    //        _evaluationStack.Push(items);
                    //    }
                    //    break;
                    //case OpCode.UNPACK:
                    //    {
                    //        StackItem item = _evaluationStack.Pop();
                    //        if (!item.IsArray)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        StackItem[] items = item.GetArray();
                    //        for (int i = items.Length - 1; i >= 0; i--)
                    //            _evaluationStack.Push(items[i]);
                    //        _evaluationStack.Push(items.Length);
                    //    }
                    //    break;
                    //case OpCode.PICKITEM:
                    //    {
                    //        int index = (int)_evaluationStack.Pop().GetBigInteger();
                    //        if (index < 0)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        StackItem item = _evaluationStack.Pop();
                    //        if (!item.IsArray)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        StackItem[] items = item.GetArray();
                    //        if (index >= items.Length)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        _evaluationStack.Push(items[index]);
                    //    }
                    //    break;
                    //case OpCode.SETITEM:
                    //    {
                    //        StackItem newItem = _evaluationStack.Pop();
                    //        if (newItem.IsStruct)
                    //        {
                    //            newItem = (newItem as Types.Struct).Clone();
                    //        }
                    //        int index = (int)_evaluationStack.Pop().GetBigInteger();
                    //        StackItem arrItem = _evaluationStack.Pop();
                    //        if (!arrItem.IsArray)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        StackItem[] items = arrItem.GetArray();
                    //        if (index < 0 || index >= items.Length)
                    //        {
                    //            State |= VMState.FAULT;
                    //            return;
                    //        }
                    //        items[index] = newItem;
                    //    }
                    //    break;
                    //case OpCode.NEWARRAY:
                    //    {
                    //        int count = (int)_evaluationStack.Pop().GetBigInteger();
                    //        StackItem[] items = new StackItem[count];
                    //        for (var i = 0; i < count; i++)
                    //        {
                    //            items[i] = false;
                    //        }
                    //        _evaluationStack.Push(new Types.Array(items));
                    //    }
                    //    break;
                    //case OpCode.NEWSTRUCT:
                    //    {
                    //        int count = (int)_evaluationStack.Pop().GetBigInteger();
                    //        StackItem[] items = new StackItem[count];
                    //        for (var i = 0; i < count; i++)
                    //        {
                    //            items[i] = false;
                    //        }
                    //        _evaluationStack.Push(new VM.Types.Struct(items));
                    //    }
                    //    break;
                    //default:
                    //    State |= VMState.FAULT;
                    //    return;
                }
            }

            context.Result = _evaluationStack.Peek();
        }
    }
}
