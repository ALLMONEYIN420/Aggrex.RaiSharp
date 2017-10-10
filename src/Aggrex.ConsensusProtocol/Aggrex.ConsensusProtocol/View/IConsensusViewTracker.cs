using System;

namespace Aggrex.ConsensusProtocol.View
{
    public interface IConsensusViewTracker
    {
        event EventHandler<ConsenseViewChangedEventArg> ConsensusViewChanged;
    }
}