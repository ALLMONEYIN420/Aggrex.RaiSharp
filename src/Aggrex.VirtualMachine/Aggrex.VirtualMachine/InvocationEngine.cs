namespace Aggrex.VirtualMachine
{
    public class InvocationEngine : IInvocationEngine
    {
        private IScriptContainer _scriptContainer;
        private IScriptStorageContainer _scriptStorageContainer;
        private IExecutionEngine _executionEngine;

        private InvocationContext _invocationContext;

        public InvocationEngine(IScriptContainer scriptContainer, IScriptStorageContainer scriptStorageContainer, IExecutionEngine executionEngine)
        {
            _scriptContainer = scriptContainer;
            _scriptStorageContainer = scriptStorageContainer;
            _executionEngine = executionEngine;
        }

        public void Execute(byte[] scriptHash, params byte[] parameters)
        {
            var script  = _scriptContainer.GetScriptByHash(scriptHash);
            var scriptStorageContext = _scriptStorageContainer.GetScriptStorageContext(scriptHash);
            ExecutionContext context = new ExecutionContext(parameters, script, scriptStorageContext);
            _executionEngine.Execute(context);
        }
    }
}