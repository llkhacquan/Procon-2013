using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBaseConversion {
    class Base10Base256Switch {
        public const int MAX_LENGTH_NUMBER = 16;
        public const ulong MAX_NUMBER = (ulong)1e16-1;

        /// <summary>
        /// number optimization so that it is the shortest List
        /// </summary>
        /// <param name="input"></param>
        public static void OptimizeNumber(List<UInt64> input) {
            //printList(input);
            while (true) {
                if (input[0] == 0 && input.Count > 1) {
                    input.RemoveAt(0);
                } else {
                    return;
                }
            }
        }

        #region Addition Method
        public static List<UInt64> plus(List<UInt64> input, ulong smallplusNum) {
            input.Reverse();
            input = plus(input, smallplusNum, 0);
            input.Reverse();
            return input;
        }

        public static List<UInt64> plus(List<UInt64> input, ulong plusNum, int place) {
            List<UInt64> temp = input;
            temp[place] += plusNum;
            if (temp[place] > (ulong)MAX_NUMBER) {
                if (place + 1 == input.Count) {
                    temp.Add(ulong.Parse("0"));
                }
                plus(temp, 1, place + 1);
                temp[place] = temp[place] % (MAX_NUMBER+1);
                return temp;
            } else {
                return temp;
            }

        }
        #endregion

        #region Substraction Methods
        public static void minus(List<UInt64> input, ulong smallMinusNum) {
            input.Reverse();
            minus(input, smallMinusNum, 0);
            input.Reverse();
            OptimizeNumber(input);
        }
        public static void minus(List<UInt64> input, ulong minusNum, int place) {

            if (input[place] < minusNum) {
                input[place] = MAX_NUMBER+1 - (minusNum - input[place]);
                minus(input, 1, place + 1);
            } else {
                input[place] -= minusNum;
            }
            return;

        }
        #endregion

        #region Multiplication Methods
        public static void multiply(List<UInt64> input, int mulNum) {
            
            ulong carry = 0;
            
            for (int i = 0; i < input.Count; i++) {
                carry = multiply(input, mulNum, carry, i);
                if (i == input.Count - 1 && carry != 0) {

                    input.Add(carry);
                }
               
            }
            
        }


        public static ulong multiply(List<UInt64> input, int mulNum, ulong carryIn, int place) {
            ulong carryout = 0;
            input[place] = input[place] * (ulong)mulNum + carryIn;
            carryout = input[place] / (MAX_NUMBER+1);
            input[place] = input[place] % (MAX_NUMBER+1);
            //throw new Exception("asdas");
            return carryout;
        }
        #endregion

        #region Devision
        ///<summary>
        ///return[1] is result and return [2] is remainder
        ///</summary>
        public static List<UInt64> Divide2(UInt64 num1_1, UInt64 num1_2, ulong num2) {
            //Console.WriteLine(num1_1.ToString()+"\t"+num1_2.ToString());
            List<UInt64> result = new List<UInt64>(3);
            ulong r2;
            ulong remainder;
            ulong temp1;

            //Console.WriteLine((num1_1/num2));
            result.Add(num1_1 / num2);

            r2 = num1_1 % num2;
            //Console.WriteLine(r2);
            temp1 = r2 * (MAX_NUMBER + 1) + num1_2;
            //Console.WriteLine(temp1);
            remainder = temp1 % num2;
            temp1 = temp1 / num2;
            result.Add(temp1);
            result.Add(remainder);
            //printList(result);
            return result;
        }
        /// <summary>
        /// return the remainder; and the result of division is num1 itself
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static List<UInt64> Divide(List<UInt64> num1, ulong num2) {
            //printList(num1);
            UInt64 remainder = 0;
            List<UInt64> result = new List<ulong>();
            List<UInt64> temp;
            for (int i = num1.Count - 1; i >= 0; i--) {
                //Console.WriteLine(i);
                temp = Divide2(remainder, num1[i], 256);
                
                remainder = temp[2];
                result.Add(temp[1]);
            }
            result.Reverse();
            
            //num1 = result;
            //printList(result);
            //Console.Read();
            
            OptimizeNumber(result);

            result.Add(remainder);
            return result;
        }
        #endregion

        #region Number and String Swapping
        public  static string numberToString(List<UInt64> input) {
            string result = "";
            for (int i = input.Count - 1; i >= 0; i--) {
                result += Convert.ToString(input[i]);
            }
            return result;
        }
        public static List<UInt64> StringToNumber(String input) {
            
            List<UInt64> result = new List<ulong>();
            int j = 0;
            int k = input.Length;
            for (int i=k; i >= 0; i--) {
                j++;
                if ((j) % MAX_LENGTH_NUMBER == 0) {
                    j = 0;
                    input = input.Insert(i -1, " ");
                    i++;
                }

            }
            
            string[] temp2 = input.Split(' ');
            for (int i = 0; i < temp2.Length; i++) {
                if (temp2[i] != "") {
                    result.Add(UInt64.Parse(temp2[i]));
                }
            }
            
            
            return result;
        }
        #endregion

        #region Base Conversion
        public static byte[] Base10ToBase256(String input) {
            Console.WriteLine(input);
            List<byte> result = new List<byte>();
            List<UInt64> inputNumber = StringToNumber(input);
            UInt64 remainder=0;
            while (true) {
                inputNumber = Divide(inputNumber, 256);
                
                remainder = inputNumber[inputNumber.Count - 1];
                inputNumber.RemoveAt(inputNumber.Count - 1);

                //Console.WriteLine(inputNumber[0]);
                result.Add(Convert.ToByte(remainder));
                if (inputNumber.Count == 1 && inputNumber[0] == 0) break;
                

            }
            
            result.Reverse();
            return result.ToArray();


        }

        public static string ByteToBase10(byte[] inputString) {
            
            List<UInt64> temp = new List<ulong>();
            temp.Add(0);
            
            for (int i = 0; i < inputString.Length; i++) {

                temp = plus(temp, System.Convert.ToUInt64(inputString[i]) * (UInt64)Math.Pow(256, inputString.Length - 1 - i));
            }
            return numberToString(temp);
        }
        #endregion
        public static void printBytes(byte[] input) {
            for (int i = 0; i < input.Length; i++) {
                Console.Write(input[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        public static void printList(List<UInt64> input) {
            for (int i = 0; i < input.Count; i++) {
                Console.Write(input[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
        }

    }
}
