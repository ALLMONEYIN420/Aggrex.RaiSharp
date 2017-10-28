using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Aggrex.Common;
using Aggrex.Framework;
using Aggrex.Network;

namespace Aggrex.ConsensusProtocol
{
    public class ActiveNodeSet : IActiveNodeSet
    {
        private readonly ConcurrentDictionary<string, int> _confirmations;
        public IDictionary<string,int> Confirmations => _confirmations;

        private string _networkId;

        private Filter<string> _activeNodeBloomFilter;

        public ActiveNodeSet(IDeterministicNetworkIdGenerator deterministicNetworkIdGenerator)
        {
            _activeNodeBloomFilter = new Filter<string>(4);
            _confirmations = new ConcurrentDictionary<string, int>();

            _networkId = deterministicNetworkIdGenerator.GenerateNetworkId;
        }

        public void Add(string id)
        {
            if (string.Equals(_networkId, id))
            {
                return;
            }

            _activeNodeBloomFilter.Add(id);

            if (!_confirmations.ContainsKey(id))
            {
                _confirmations[id] = 1;
            }
            else
            {
                _confirmations[id]++;
            }

            Console.WriteLine("------------------------");

            foreach (var keyValuePair in _confirmations)
            {
               Console.WriteLine($"{keyValuePair.Key} - {keyValuePair.Value}"); 
            }

            Console.WriteLine("------------------------");
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