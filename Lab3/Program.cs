using System;
using System.IO;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                try
                {
                var fs = new FileStream(args[0], FileMode.Open);
                var fileSize = (int)fs.Length;
                var data = new byte[fileSize];
                fs.Read(data, 0, fileSize);
                var pngChecker = PNGChecker(data);
                var bmpChecker = BMPChecker(data);

                if (pngChecker)
                {
                    Console.WriteLine("This is a PNG File!");
                    ReadResolutionPNG(fs);
                }
                else if (bmpChecker)
                {
                    Console.WriteLine("This is a BMP File!");
                    ReadResolutionBMP(fs);
                }
                else
                {
                    Console.WriteLine("Invalid file! Use BMP or PNG file!");
                }

                fs.Close();

                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("The file was not found!");
                }
            }
        }

        static bool PNGChecker(byte[] data) //This methods checks if the file is a PNG file.
        {
            var pngFile = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }; //First 8 bytes of an PNG file, always the same.

            for (int i = 0; i < 8; i++)
            {
                if (data[i] != pngFile[i])
                {
                    return false;
                }
            }
            return true;
        }

        static bool BMPChecker(byte[] data) //This method checks if the file is a BMP file.
        {
            var bmpFile = new byte[] { 0x42, 0x4D }; //First two bytes of a BMP file, always the same.

            for (int i = 0; i < 2; i++)
            {
                if (data[i] != bmpFile[i])
                {
                    return false;
                }
            }
            return true;
        }

        static void ReadResolutionPNG(FileStream fs) //This method reads BMP files resolution.
        {
            var data = new byte[8];
            var width = "";
            var height = "";

            fs.Seek(16, SeekOrigin.Begin); //Starts to read from position 16.
            fs.Read(data, 0, 8); //Reads from position 16 and 8 positions forward (to 24).

            for (int i = 0; i < 4; i++)
            {
                width += data[i].ToString("X2"); //Reads from data position 16 to 20 and saves the hexa from those positions to width, to later convert them to ints.
                height += data[i + 4].ToString("X2"); //Reads from data position 21 to 24 and saves the hexa from those positions to height, to later convert them to ints.
            }

            int x = Convert.ToInt32(width, 16); //Converts hexa width to int and saves it to x.
            int y = Convert.ToInt32(height, 16); //Converts hexa height to int and saves it to y.

            Console.WriteLine($"The Resolution is: {x}x{y} pixels");
        }

        static void ReadResolutionBMP(FileStream fs) //This method reads BMP files resolution.
        {
            var data = new byte[8];
            var width = "";
            var height = "";

            fs.Seek(18, SeekOrigin.Begin); //Starts to read from position 18.
            fs.Read(data, 0, 8); //Reads from position 18 and 8 positions forward (to 25).

            for (int i = 3; i > -1; i--)
            {
                width += data[i].ToString("X2"); //Reads from data position 21 to 18 and saves the hexa from those positions to width, to later convert them to ints.
                height += data[i + 4].ToString("X2"); //Reads from data position 25 to 22 and saves the hexa from those positions to height, to later convert them to ints.
            }

            int x = Convert.ToInt32(width, 16); //Converts hexa width to int and saves it to x.
            int y = Convert.ToInt32(height, 16); //Converts hexa height to int and saves it to y.

            Console.WriteLine($"The Resolution is: {x}x{y} pixels");
        }
    }
}