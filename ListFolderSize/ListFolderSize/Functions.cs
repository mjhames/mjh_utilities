using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFolderSize
{
    internal class Functions
    {
        public static void ListListDirectoriesWithSizeAndCountInfo(DirectoryInfo rootPathDI, bool showAllFolders)
        {

            ulong totalSize = 0;
            uint totalFileCount = 0;

            Console.WriteLine($"Directory\tSize\tFile Count");
            foreach (var dir in rootPathDI.GetDirectories())
            {
                var sizeAndCount = DirSizeAndCount(dir);
                totalSize += sizeAndCount.Size;
                totalFileCount += sizeAndCount.FileCount;
                Console.WriteLine($"\"{dir.Name}\"\t{ToStringByteFormat(sizeAndCount.Size)}\t{sizeAndCount.FileCount} files");
            }
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"Totals for \"...\\{rootPathDI.Name}\"\t{ToStringByteFormat(totalSize)}\t{totalFileCount} files");

        }

        public static DirInfo DirSizeAndCount(DirectoryInfo d)
        {
            var retVal = new DirInfo();
            try
            {
                // Add file sizes.
                FileInfo[] fis = d.GetFiles();
                retVal.FileCount = (uint)fis.Count();
                foreach (FileInfo fi in fis)
                {
                    retVal.Size += (ulong)fi.Length;
                }

                DirectoryInfo[] dis = d.GetDirectories();


                // Add subdirectory sizes.

                foreach (DirectoryInfo di in dis)
                {
                    retVal.Add(DirSizeAndCount(di));
                }



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
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };

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
