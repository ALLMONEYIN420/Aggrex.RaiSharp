namespace Aggrex.Network.Requests
{
    public enum MessageType
    {
        Invalid = 0x0,
        NotAType = 0x1,
        Keepalive = 0x2,
        Publish = 0x3,
        ConfirmReq = 0x4,
        ConfirmAck = 0x5,
        BulkPull = 0x6,
        BulkPush = 0x7,
        FrontierRe = 0x8,
    }
}