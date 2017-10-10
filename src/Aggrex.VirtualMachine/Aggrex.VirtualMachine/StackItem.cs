namespace Aggrex.VirtualMachine
{
    public struct StackItem
    {
        public StackItemType StackItemType { get; set; }

        public byte[] Item;

        public StackItem(StackItemType type, byte[] item)
        {
            StackItemType = type;
            Item = item;
        }
    }

    public enum StackItemType
    {
        BigInteger = 0x00,
        UInt160 = 0x01,
        UInt256 = 0x02,
        ByteArray = 0x03
    }
}