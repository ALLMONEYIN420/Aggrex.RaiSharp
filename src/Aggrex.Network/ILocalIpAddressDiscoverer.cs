namespace Aggrex.Network
{
    /// <summary>
    /// This should return an IPAddress for current matchine. The IP Address should be found
    /// in a deterministic way.
    /// </summary>
    public interface ILocalIpAddressDiscoverer
    {
        string GetLocalIpAddress();
    }
}