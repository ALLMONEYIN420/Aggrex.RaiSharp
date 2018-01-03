using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aggrex.Common;
using Aggrex.Configuration;
using Aggrex.Network.HandShakes;
using Aggrex.Network.Messages;
using Aggrex.Network.Messages.KeepAlive;
using Aggrex.Network.Packets;
using Aggrex.Network.Requests;
using Autofac;

namespace Aggrex.Network
{
    public class RemoteNode : IRemoteNode
    {
        public delegate RemoteNode Factory(IPEndPoint ipEndPoint);
        private IPacketBroadcaster _packetBroadcaster;
        public IPEndPoint IpEndPoint { get; private set; }

        public RemoteNode(IPEndPoint ipEndPoint, IPacketBroadcaster packetBroadcaster)
        {
            IpEndPoint = ipEndPoint;
            _packetBroadcaster = packetBroadcaster;
        }
    }
}