using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Aggrex.Common;
using Aggrex.Framework;

namespace Aggrex.ConsensusProtocol
{
    public class ActiveNodeSet : IActiveNodeSet
    {
        private readonly ConcurrentDictionary<string, int> _confirmations;
        private Filter<string> _activeNodeBloomFilter;
        public ActiveNodeSet()
        {
            _activeNodeBloomFilter = new Filter<string>(4);
            _confirmations = new ConcurrentDictionary<string, int>();
        }
        public void Add(string id)
        {
            _activeNodeBloomFilter.Add(id);

            if (!_confirmations.ContainsKey(id))
            {
                _confirmations[id] = 1;
            }
            else
            {
                _confirmations[id]++;
            }
        }

        public bool Contains(string id)
        {
            return _activeNodeBloomFilter.Contains(id);
        }

        public void Clear()
        {
            _activeNodeBloomFilter = new Filter<string>(4);
            _confirmations.Clear();
        }
    }
}