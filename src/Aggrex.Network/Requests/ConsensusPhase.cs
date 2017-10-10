namespace Aggrex.Network.Requests
{
    public enum ConsensusPhase
    {
        Request = 0x00,
        PrePrepare = 0x01,
        Prepare = 0x02,
        Commit = 0x03,
        Reply = 0x04
    }
}