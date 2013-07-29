using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Procon
{
    public class Smaz
    {
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int smaz_compress(StringBuilder inString, int inlen, StringBuilder outString, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int smaz_decompress(StringBuilder inString, int inlen, StringBuilder outString, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp1(StringBuilder Buf1, StringBuilder Buf2, int Size);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void memcpy1(StringBuilder Des, StringBuilder Src, int Size);



        public static byte[] Compress(String input) {

            StringBuilder inputTemp = new StringBuilder(input);
            StringBuilder outputTemp = new StringBuilder(4000);

            //inputTemp.Clear();
            //inputTemp.Append(input);

            int outlen = smaz_compress(inputTemp, inputTemp.Capacity, outputTemp, outputTemp.Capacity);

            byte[] result = new byte[outputTemp.Length];
            for (int i = 0; i < outputTemp.Length; i++) {
                result[i] = Convert.ToByte(outputTemp[i]);
            }
           
            return result;
        }
        public static string Uncompress(byte[] bytes) {
            StringBuilder inputTemp = new StringBuilder(20000);
            StringBuilder outputTemp = new StringBuilder(20000);
            string inputstring = "";
            string result = "";
            for (int i = 0; i < bytes.Length; i++) {
                inputstring += System.Convert.ToChar(bytes[i]);
            }
            inputTemp.Clear();
            inputTemp.Append(inputstring);
            int outlen = smaz_decompress(inputTemp, inputTemp.Capacity, outputTemp, outputTemp.Capacity);
            for (int i = 0; i < outputTemp.Length; i++) {
                result += outputTemp[i].ToString();
            }

            return result;

        }
    }
}
