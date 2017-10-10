using Aggrex.Network.Requests;

namespace Aggrex.ConsensusProtocol.ConsensePhases
{
    public interface IConsensusPhaseRoutine
    {
        ConsensusPhase ConsensusPhase { get; }
        void StartExecutionLoop();
    }
}