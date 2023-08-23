using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFolderSize
{
    internal class Functions
    {
        private static readonly string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        private const string HEADER_ROW = "Directory\tSize\tFile Count";

        public static void ListDirectoriesWithSizeAndCountInfo(DirectoryInfo rootPathDI, bool showAllFolders, TextWriter? textWriter = null)
        {
            textWriter ??= Console.Out;

            ulong totalSize = 0;
            uint totalFileCount = 0;

            textWriter.WriteLine(HEADER_ROW);
            foreach (var dir in rootPathDI.GetDirectories())
            {
                var sizeAndCount = DirSizeAndCount(dir);
                totalSize += sizeAndCount.Size;
                totalFileCount += sizeAndCount.FileCount;
                textWriter.WriteLine($"\"{dir.Name}\"\t{ToStringByteFormat(sizeAndCount.Size)}\t{sizeAndCount.FileCount} files");
            }
            textWriter.WriteLine(new string('-', 80));
            textWriter.WriteLine($"Totals for \"...\\{rootPathDI.Name}\"\t{ToStringByteFormat(totalSize)}\t{totalFileCount} files");
        }

        public static DirInfo DirSizeAndCount(DirectoryInfo directoryInfo)
        {
            var retVal = new DirInfo();
            try
            {
                // Add file sizes.
                var files = directoryInfo.GetFiles();
                retVal.FileCount = (uint)files.Count();
                retVal.Size = (ulong)files.Sum(f =>f.Length);

                directoryInfo.GetDirectories().ToList().ForEach(d => retVal.Add(DirSizeAndCount(d)));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"DirSize Error: {ex.Message}\r\n");
            }
            return retVal;
        }

        public static string ToStringByteFormat(ulong numberOfBytes)
        {
            return ToStringByteFormat((double)numberOfBytes);
        }

        public static string ToStringByteFormat(double numberOfBytes)
        {
            var order = 0;
            while (numberOfBytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                numberOfBytes /= 1024;
            }
            return $"{numberOfBytes:0.##} {sizes[order]}";
        }
        public class DirInfo
        {
            public ulong Size;
            public uint FileCount;
            public void Add(DirInfo newDI)
            {
                Size += newDI.Size;
                FileCount += newDI.FileCount;
            }

        }
    }
}
