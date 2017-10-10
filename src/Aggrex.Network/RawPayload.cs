using System.Net;

namespace Aggrex.Network
{
    /// <summary>
    /// Represents the raw message that comes in.
    /// </summary>
    public class RawPayload
    {
        public byte[] Message { get; set; }
        public IPEndPoint Sender { get; set; }
    }
}