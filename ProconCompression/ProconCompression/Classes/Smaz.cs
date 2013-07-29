using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Procon
{
    public class Smaz
    {
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Compress(StringBuilder inString, int inlen, StringBuilder outString, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int smaz_decompress(StringBuilder bytes, int bytelen, StringBuilder outString, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int smaz_decompress(char[] bytes, int bytelen, char[] outString, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Uncompress(StringBuilder outString, int outlen);


        private const int MAX_LENGTH = 2000000;

        public static byte[] Compress(String input)
        {
            
            if (input.Replace(" ", "_") != input)
                throw new Exception("string has space!!!");

            input = input.Replace("_", " ");
            


            StringBuilder inputTemp = new StringBuilder(input);
            StringBuilder outputTemp = new StringBuilder(MAX_LENGTH);
            int outlen = Compress(inputTemp, inputTemp.Length, outputTemp, outputTemp.Capacity);
            if (outlen == MAX_LENGTH + 1)
                throw new Exception("Output capacity is not big enough");

            FileStream fs = new FileStream("a.bin", FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[fs.Length];
            int numBytesToRead = (int)fs.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead. 
                int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached. 
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }

            fs.Close();

            File.Delete("a.bin");
            return bytes;
        }




        public static string Uncompress(byte[] bytes)
        {
            StringBuilder outputTemp = new StringBuilder(MAX_LENGTH);
            List<char> s = new List<char>();
            FileStream fs = new FileStream("b.bin", FileMode.Create, FileAccess.Write);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            int outlen = Uncompress(outputTemp, outputTemp.Capacity);

            File.Delete("b.bin");

            return outputTemp.ToString().Replace(" ", "_");
            //return outputTemp.ToString();

        }
    }
}
