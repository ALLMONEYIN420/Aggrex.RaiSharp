using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;

namespace Aggrex.VirtualMachine
{

    public static class BinaryReaderExtensionMethods
    {
        public static OpCode ReadNextInstruction(this BinaryReader reader)
        {
            return (OpCode) reader.ReadByte();
        }
    }


    public class ExecutionContext
    {
        private IScriptStorageContext _scriptStorageContext;

        public BinaryReader ProgramReader
        {
            get { return _programReader; }
        }

        private BinaryReader _programReader;

        private byte[] _program;

        public int InstructionPointer => (int)_programReader.BaseStream.Position;
        public bool HasMoreInstructions => InstructionPointer < _programReader.BaseStream.Length;

        public ExecutionContext(byte[] parameters, byte[] script, IScriptStorageContext scriptStorageContext)
        {
            _scriptStorageContext = scriptStorageContext;
            _program = parameters.Concat(script).ToArray(); 
            _programReader = new BinaryReader(new MemoryStream(_program));
        }



        private StackItem _result;

        public StackItem Result
        {
            get
            {
                if (HasMoreInstructions)
                {
                    throw new Exception("Program did not execute to completion.");
                }

                return _result;
            }
            set
            {
                if (HasMoreInstructions)
                {
                    throw new Exception("Program did not execute to completion.");
                }

                _result = value;
            }
        }
    }
}