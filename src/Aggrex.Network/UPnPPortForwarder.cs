using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Open.Nat;

namespace Aggrex.Network
{
    public class UPnPPortForwarder : IUPnPPortForwarder
    {
        private SemaphoreSlim _lock = new SemaphoreSlim(1,1);

        public async Task ForwardPortIfNatFound()
        {
            return;
            //try
            //{
            //    var discoverer = new NatDiscoverer();
            //    var cts = new CancellationTokenSource(5000);
            //    var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);

            //    await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, NetworkSettings.DefaultServerPrivatePort, NetworkSettings.DefaultServerExternalPort, "Aggrex Port Mapping"));
            //}
            //catch (NatDeviceNotFoundException ex)
            //{
            //    // If there isn't a Nat Device, we don't need to use UPnP 
            //}
        }
    }
}