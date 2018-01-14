namespace Aggrex.Network.Messages.Publish
{
    public enum BlockType
    {
       Invalid = 0x0,
       NotABlock = 0x1,
       Send = 0x2,
       Receive = 0x3,
       Open = 0x4,
       Change = 0x5
    }
}