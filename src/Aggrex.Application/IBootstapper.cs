namespace Aggrex.Application
{
    public interface IBootstapper
    {
        void Startup();

        void Shutdown();
    }
}