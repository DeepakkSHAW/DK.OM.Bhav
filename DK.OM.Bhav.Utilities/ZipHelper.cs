using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace DK.OM.Bhav.Utilities
{
    /// <summary>
    /// Zip and Unzip in memory using System.IO.Compression.
    /// </summary>
    /// <remarks>
    /// Include System.IO.Compression in your project.
    /// </remarks>
    /// <source>
    /// https://sandervandevelde.wordpress.com/2017/12/20/zip-and-unzip-a-string-of-data-in-memory/
    /// </source>
    public static class ZipHelper
    {
        /// <summary>
        /// Zips a string into a zipped byte array.
        /// </summary>
        /// <param name="textToZip">The text to be zipped.</param>
        /// <returns>byte[] representing a zipped stream</returns>
        public static byte[] Zip(string textToZip)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var demoFile = zipArchive.CreateEntry("zipped.txt");

                    using (var entryStream = demoFile.Open())
                    {
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            streamWriter.Write(textToZip);
                        }
                    }
                }
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Unzip a zipped byte array into a string.
        /// </summary>
        /// <param name="zippedBuffer">The byte array to be unzipped</param>
        /// <returns>string representing the original stream</returns>
        public static string Unzip(byte[] zippedBuffer)
        {
            using (var zippedStream = new MemoryStream(zippedBuffer))
            {
                using (var archive = new ZipArchive(zippedStream))
                {
                    var entry = archive.Entries.FirstOrDefault();

                    if (entry != null)
                    {
                        using (var unzippedEntryStream = entry.Open())
                        {
                            using (var ms = new MemoryStream())
                            {
                                unzippedEntryStream.CopyTo(ms);
                                var unzippedArray = ms.ToArray();

                                return Encoding.Default.GetString(unzippedArray);
                            }
                        }
                    }
                    return null;
                }
            }
        }
    }
}


//Test Main//
/*
class Program
{
    static void Main(string[] args)
    {
        //// 1. Read file from disk and text. So text is our input

        var fileName = @"C:\dev\filewithtexttocompress.txt";
        var textBuffer = File.ReadAllText(fileName);

        //// 2. ZIP IN MEMORY. Create zipped byte array from string

        var zippedBuffer = ZipHelper.Zip(textBuffer);

        //// 3. write byte array to zip file

        var zipName = @"C:\dev\filewithtexttocompress.zip";
        File.WriteAllBytes(zipName, zippedBuffer);

        //// 4. read zip from file into byte array

        var rawFileStream = File.OpenRead(zipName);
        byte[] zippedtoTextBuffer = new byte[rawFileStream.Length];
        rawFileStream.Read(zippedtoTextBuffer, 0, (int)rawFileStream.Length);

        //// 5. UNZIP IN MEMORY. Create text from zipped byte array

        var text = ZipHelper.Unzip(zippedtoTextBuffer);

        //// 6. Write unzipped file

        File.WriteAllText(@"C:\dev\unzipped.txt", text);
    }
}
*/
