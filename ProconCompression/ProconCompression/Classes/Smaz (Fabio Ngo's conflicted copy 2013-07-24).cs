using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Procon {
    public class Smaz {
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Compress(StringBuilder inString, int inlen, StringBuilder outString, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int smaz_decompress(StringBuilder bytes, int bytelen, StringBuilder outString, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int smaz_decompress(char[] bytes, int bytelen, char[] outString, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Uncompress(StringBuilder outString, int outlen);


        public static byte[] Compress(String input) {
            
            StringBuilder inputTemp = new StringBuilder(input);
            StringBuilder outputTemp = new StringBuilder(20000);
            StringBuilder c = new StringBuilder(20000);
            int outlen = Compress(inputTemp, inputTemp.Length, outputTemp, outputTemp.Capacity);
            
            FileStream fs = new FileStream("a.bin", FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[fs.Length];
            int numBytesToRead = (int)fs.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0) {
                // Read may return anything from 0 to numBytesToRead. 
                int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached. 
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            
            fs.Close();
            //throw new Exception("");
            //File.Delete("a.bin");
            return bytes;
        }



        
        public static string Uncompress(byte[] bytes) {
            StringBuilder outputTemp = new StringBuilder(20000);
            
            
            
            FileStream fs = new FileStream("b.bin", FileMode.Create, FileAccess.Write);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            /*fs = new FileStream("b.bin", FileMode.Open, FileAccess.Read);
            inputTemp = new StringBuilder(20000);
            int i = 0;
            List<byte> b = new List<byte>();
            while (true) {
                int n = fs.ReadByte();
                if(n==-1) break;
                s.Add(Convert.ToChar(n));
                inputTemp.Append(s);
                
                
                //throw new Exception("");
            }
            
            //throw new Exception("");
            //outputTemp = new StringBuilder(s);
            /*inputTemp = new StringBuilder();
            inputTemp.Length = bytes.Length;
            for (int i = 0; i < bytes.Length; i++) {
                inputTemp[i] = Convert.ToChar(bytes[i]);
            }
            //inputTemp = new StringBuilder(line);*/
            //int outlen = smaz_decompress(inputTemp, inputTemp.Length, outputTemp, outputTemp.Capacity);
            //int outlen = smaz_decompress(s.ToArray(), s.Count, d, 20000);
            int outlen = Uncompress(outputTemp, outputTemp.Capacity);
            return outputTemp.ToString();

        }
    }
}
