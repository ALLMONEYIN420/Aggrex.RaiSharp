namespace Aggrex.ConsensusProtocol.View
{
    public class ConsenseViewChangedEventArg
    {
        public ReasonForChange ReasonForChange { get; set; }
        public ConsensusView NewConsensView { get; set; }
    }
}