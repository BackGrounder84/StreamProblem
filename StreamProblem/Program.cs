using System.IO.Compression;
using System.Text;

namespace StreamProblem
{
    internal static class Program
    {
        //https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/partial-byte-reads-in-streams
        //Um die Änderung zu sehen Framework Version zwischen 5 <-> 6 ändern

        public static void Main(string[] args)
        {
            var pdfData = File.ReadAllBytes("OcrInvoice.pdf");
            PrintData(pdfData, "original.txt");


            Console.ReadLine();
            Console.Clear();

            var zipFileData = File.ReadAllBytes("Data.zip");
            var dataArrayOld = ProcessZipNet5(zipFileData);

            PrintData(dataArrayOld, "net5.txt");

            Console.ReadLine();
            Console.Clear();

            var dataArrayNew = ProcessZipNet6(zipFileData);

            PrintData(dataArrayNew, "net6.txt");

            Console.ReadLine();
        }

        private static void PrintData(byte[] pdfData, string fileName)
        {
            var sb = new StringBuilder();
            foreach (var b in pdfData)
            {
                Console.Write(b);
            }
            File.WriteAllText(fileName, sb.ToString());
        }

        private static byte[] ProcessZipNet5(byte[] fileData)
        {
            using var stream = new MemoryStream(fileData, false);
            using var zipArchive = new ZipArchive(stream);


            var entry = zipArchive.Entries.First();
            var data = new byte[entry.Length];
            using var entryStream = entry.Open();
            entryStream.Read(data, 0, data.Length);

            return data;

        }

        private static byte[] ProcessZipNet6(byte[] fileData)
        {
            using var stream = new MemoryStream(fileData, false);
            using var zipArchive = new ZipArchive(stream);


            var entry = zipArchive.Entries.First();
            Span<byte> buffer = new byte[entry.Length];

            int totalRead = 0;
            using var pdfStream = entry.Open();

            while (totalRead < buffer.Length)
            {
                int bytesRead = pdfStream.Read(buffer.Slice(totalRead));

                if (bytesRead == 0)
                {
                    break;
                }

                totalRead += bytesRead;
            }

            return buffer.ToArray();

        }
    }
}