using System;
using System.ComponentModel.Design;
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
            try
            {
                var discoverer = new NatDiscoverer();
                var cts = new CancellationTokenSource(5000);
                var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);

                var existingMapping = await device.GetSpecificMappingAsync(Protocol.Udp, 7075);
                if (existingMapping != null)
                {
                    await device.DeletePortMapAsync(existingMapping);
                }

                await device.CreatePortMapAsync(new Mapping(Protocol.Udp, 7075, 7075, "RaiBlocks"));
            }
            catch (Exception ex)
            {
                // If there isn't a Nat Device, we don't need to use UPnP 
            }
        }
    }
}