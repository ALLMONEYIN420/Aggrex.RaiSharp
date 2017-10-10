using System.IO;

namespace Aggrex.Common
{
    public static class StreamHelper
    {
        public static void Write(IStreamable data, Stream stream)
        {
           data.WriteToStream(new BinaryWriter(stream)); 
        }
    }
}