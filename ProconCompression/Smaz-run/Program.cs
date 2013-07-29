using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Smaz_run
{
    class Program
    {
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int smaz_compress(StringBuilder input, int inlen, StringBuilder output, int outlen);
        [DllImport("Smaz.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int smaz_decompress(StringBuilder input, int inlen, StringBuilder output, int outlen);

        static void Main(string[] args)
        {
            string test = "The_following_ASCII_table_with_hex,_octal,_html,_binary_and_decimal_chart_conversion_contains_both_the_ASCII_control_characters,_ASCII_printable_characters_and\n";
            byte[] temp = Compress(test);
            Console.WriteLine(test);
            Console.WriteLine(Uncompress(temp));
            Console.Read();

        }

        public static byte[] Compress(String input) {
            /**
            //check if input have any space
            string temp = input.Replace(" ", "_");
            if (temp != input)
            {
                throw new Exception("Input string contains space");
            }
            temp = input.Replace("_", " ");
            **/

            StringBuilder inputTemp = new StringBuilder(input);
            StringBuilder outputTemp = new StringBuilder(20000);

            int numOf2ByteChar = 0;
            int outlen = smaz_compress(inputTemp, inputTemp.Length, outputTemp, outputTemp.Capacity);
            String a = outputTemp.ToString();
            List<byte> result = new List<byte>();
            List<byte> twoByteCharPos = new List<byte>();
            result.Add(Convert.ToByte(0));


            for (int i = 0; i < outlen; i++) {
                int charCode = Convert.ToInt16(outputTemp[i]);
                if (charCode > 255) {
                    numOf2ByteChar++;
                    //throw new Exception(Convert.ToInt16(outputTemp[i]).ToString());
                    result.Add(Convert.ToByte(charCode / 256));
                    result.Add(Convert.ToByte(charCode % 256));
                    twoByteCharPos.Add(Convert.ToByte(result.Count - 2));
                } else {
                    result.Add(Convert.ToByte(outputTemp[i]));
                }
            }
            result[0] = Convert.ToByte(numOf2ByteChar);
            result.AddRange(twoByteCharPos);

            //throw new Exception(" ");
            return result.ToArray();
        }



        public static string Uncompress(byte[] bytes) {
            StringBuilder inputTemp = new StringBuilder(200000);
            StringBuilder outputTemp = new StringBuilder(20000);
            string inputstring = "";
            string result = "";
            #region Bytes Array to String
            List<int> temp = new List<int>();

            for (int i = 0; i < bytes.Length; i++) {
                temp.Add(System.Convert.ToInt16(bytes[i]));
            }
            int numOf2ByteChar = temp[0];
            for (int i = 0; i < numOf2ByteChar; i++) {
                int pos = temp[temp.Count - 1 - i];
                temp[temp.Count - 1 - i] = -1;
                temp[pos] = temp[pos] * 256 + temp[pos + 1];
                temp[pos + 1] = -1;
            }

            for (int i = 0; i < temp.Count; i++) {
                if (temp[i] == -1) {
                    temp.RemoveAt(i);
                    i--;
                }
            }
            temp.RemoveAt(0);
            for (int i = 0; i < temp.Count; i++) {
                inputstring += Convert.ToChar(temp[i]);
            }
            #endregion

            inputTemp.Clear();
            inputTemp.Append(inputstring);
            int outlen = smaz_decompress(inputTemp, inputTemp.Length, outputTemp, outputTemp.Capacity);
            for (int i = 0; i < outlen; i++) {
                result += outputTemp[i].ToString();
            }
            //throw new Exception("");
            return result;

        }
    }
}
