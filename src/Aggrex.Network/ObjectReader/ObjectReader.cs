using System.IO;
using Aggrex.Common;

namespace Aggrex.Network.ObjectReader
{
    public class ObjectReader : IObjectReader
    {
        public T ReadObject<T>(BinaryReader reader) where T : IStreamable, new()
        {
            T request = new T();
            request.ReadFromStream(reader);
            return request;
        }
    }
}