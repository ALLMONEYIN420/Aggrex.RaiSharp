using System.Collections.Generic;

namespace Aggrex.Framework
{
    public interface IActiveNodeSet
    {
        IDictionary<string, int> Confirmations { get; }
        void Add(string id);
        bool Contains(string id);
        void Clear();
    }
}