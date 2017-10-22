namespace Aggrex.ConsensusProtocol.Transactions
{
    public enum TransactionType
    {
        TransferTransaction = 0x00,
        DeployContractTransaction = 0x01,
        InvokeContractTransaction = 0x02
    }
}