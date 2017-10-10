using System.IO;
using Aggrex.Common;

namespace Aggrex.Network.ObjectReader
{
    /// <summary>
    /// Reads an object from a binary reader.
    /// </summary>
    public interface IObjectReader
    {
        T ReadObject<T>(BinaryReader reader) where T : IStreamable, new();
    }
}