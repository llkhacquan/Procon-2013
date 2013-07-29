using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;


namespace TestBaseConversion {
    class Program {
        public static void printBytes(byte[] input){
            for(int i=0;i<input.Length;i++){
                Console.Write(input[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args) {
            /*String a = "12";
            List<UInt64> b = Base10Base256Switch.StringToNumber(a);
            Base10Base256Switch.printList(b);
            //throw new Exception("asd");
            Base10Base256Switch.multiply(b,256);
            
            Base10Base256Switch.printList(b);
            */
            String StringEnd = "abcdefg";

            byte[] ByteArrayEnd = Smaz.Compress(StringEnd);
            printBytes(ByteArrayEnd);
            string medium = Base10Base256Switch.ByteToBase10(ByteArrayEnd);

            Console.WriteLine(medium);
            
            byte[] bytesOutput = Base10Base256Switch.Base10ToBase256(medium);
            printBytes(bytesOutput);
            String output = Smaz.Uncompress(bytesOutput);
            Console.WriteLine(output);
             
            Console.Read();
            
            
           
        }
    }
}
