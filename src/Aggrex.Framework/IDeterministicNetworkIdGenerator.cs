using System;
using System.Collections.Generic;
using System.Text;

namespace Aggrex.Network
{
    public interface IDeterministicNetworkIdGenerator
    {
        string GenerateNetworkId { get; }
    }
}
