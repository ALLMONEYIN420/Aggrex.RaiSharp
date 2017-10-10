namespace Aggrex.Network.Requests
{
    public enum MessageType
    {
        None = 0x00,
        Introduction = 0x01,
        GetPeerAddresses = 0x02,
        PeerAddressesPayload = 0x03,

        Operation = 0x04,
        Transaction = 0x05
    }
}