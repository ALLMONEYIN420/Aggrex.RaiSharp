namespace Aggrex.Framework
{
    public interface IActiveNodeSet
    {
        void Add(string id);
        bool Contains(string id);
        void Clear();
    }
}