namespace Aggrex.Network.Requests
{
    public enum MessageType
    {
        None = 0x00,
        Introduction = 0x01,

        RequestPeerAddresses = 0x02,
        PeerAddressesPayload = 0x03,

        RequestActiveNodeSet = 0x04,
        ActiveNodeSetPayload = 0x05,



        Operation = 0x50,
        Transaction = 0x51
    }
}