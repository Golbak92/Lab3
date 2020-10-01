using System;
using System.IO;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var fs = new FileStream(@"C:\Users\KristianGolba\Pictures\Saved Pictures\sample_1280×853.bmp", FileMode.Open);
            var fileSize = (int)fs.Length;
            var data = new byte[fileSize];
            fs.Read(data, 0, fileSize);
            fs.Close();

            var pngChecker = PNGChecker(data) ? "This is a PNG File!" : "This is not PNG File!";
            var bmpChecker = BMPChecker(data) ? "This is a BMP File!" : "This is not BMP File!";

            Console.Write(pngChecker);
            Console.Write(bmpChecker);


        }

        static bool PNGChecker(byte[] data)
        {
            var pngFile = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

            for (int i = 0; i < 8; i++)
            {
                if (data[i] != pngFile[i])
                {
                    return false;
                }
            }
            return true;
        }

        static bool BMPChecker(byte[] data)
        {
            var bmpFile = new byte[] { 0x42, 0x4D };

            for (int i = 0; i < 2; i++)
            {
                if (data[i] != bmpFile[i])
                {
                    return false;
                }
            }
            return true;
        }

        
    }
}