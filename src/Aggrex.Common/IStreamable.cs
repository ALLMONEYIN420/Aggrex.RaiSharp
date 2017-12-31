using System.IO;

namespace Aggrex.Common
{
    /// <summary>
    /// Allows writing and reading from a stream.
    /// </summary>
    public interface IStreamable
    {
        void WriteToStream(BinaryWriter writer);
        bool ReadFromStream(BinaryReader reader);
    }
}