using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace V2RayShell
{
    class FileManager
    {
        public static void UncompressFile(string fileName, byte[] content)
        {
            using (var ms = new MemoryStream(content))
            using (FileStream decompressedFileStream = File.Create(fileName))
            using (GZipStream decompressionStream = new GZipStream(ms, CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(decompressedFileStream);
            }
        }

        public static string UncompressString(byte[] content)
        {
            using (var msCompress = new MemoryStream(content))
            using (var msDecompress = new MemoryStream())
            using (GZipStream decompressionStream = new GZipStream(msCompress, CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(msDecompress);
                return Encoding.UTF8.GetString(msDecompress.ToArray());
            }
        }
    }
}
