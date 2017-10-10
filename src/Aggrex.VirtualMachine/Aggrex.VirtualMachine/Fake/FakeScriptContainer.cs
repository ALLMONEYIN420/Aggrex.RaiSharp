using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Aggrex.VirtualMachine.Fake
{
    public class FakeScriptContainer : IScriptContainer
    {
        IDictionary<UInt32, byte[]> _store = new ConcurrentDictionary<uint, byte[]>();

        public byte[] GetScriptByHash(byte[] scriptHash)
        {
            return null;
        }
    }
}