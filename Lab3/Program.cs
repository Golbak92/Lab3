using System;
using System.IO;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var fs = new FileStream(@"C:\Users\KristianGolba\Pictures\Saved Pictures\bWyA9.png", FileMode.Open);
            var fileSize = (int)fs.Length;
            var data = new byte[fileSize];
            fs.Read(data, 0, fileSize);
            fs.Close();

            PNGChecker(data);
        }

        static bool PNGChecker(byte[] data)
        {
            var pngFile = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

            for (int i = 0; i < 8; i++)
            {
                if (data[i] == pngFile.Length)
                {
                    Console.WriteLine("This is a PNG file");
                }
            }
            return true;
        }
    }
}