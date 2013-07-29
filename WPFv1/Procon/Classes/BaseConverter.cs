using System;
using System.Collections.Generic;
using System.Linq;

namespace Procon.Classes
{
    class BaseConverter
    {

        private static string b86table = Constants.DEFAULT_SYMBOLS_SET;
        #region Public Static Methods

        //Convert number in string representation from base:from to base:to.
        //Return result as a string
        public static string Convert(int from, int to, string numberToConvert)
        {
            /**
            char[] b86table_ = new char[] {
                '0','1',  
                '2','3','4','5','6','7','8','9',    
                'A','B','C','D','E','F','G','H','I','J','K','L','M',
                'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',    
                'a','b','c','d','e','f','g','h','i','j','k','l','m'
                ,'n','o','p','q','r','s','t','u','v','w','x','y','z',  
                '#', '_'}; // base 64
             **/

            

            //Return error if input is empty
            if (string.IsNullOrEmpty(numberToConvert))
            {
                throw new Exception("Error: Nothing in Input String");
            }
            //only do base 2 to base 64 (digit represented by characters 0-9;a-z;A-Z;#_)"
            if (from < 2 || from > 86 || to < 2 || to > 86)
            {
                throw new Exception("Base requested outside range : 2 to 86");
            }

            if (from < 27)
            {
                numberToConvert = numberToConvert.ToUpper();
            }

            //convert string to an array of integer 
            //digits representing number in base:from
            int il = numberToConvert.Length;
            int[] fs = new int[il];
            int k = 0;
            for (int i = numberToConvert.Length - 1; i >= 0; i--)
            {
                fs[k] = -1;
                for (int p = 0; p < b86table.Length; ++p)
                {
                    if (b86table[p] == numberToConvert[i])
                    {
                        fs[k] = p;
                        break;
                    }
                }

                if (fs[k] == -1)
                {
                    string s = string.Empty;
                    for (int p = 0; p < from; ++p)
                        s += b86table[p];
                    throw new Exception(
                        string.Format("Error: Input string must only contain any of {0}."
                        , s));
                }

                //check the input for digits that exceed the allowable for base:from
                if (fs[k] >= from)
                {
                    throw new Exception(
                        string.Format("Error: Not a valid number for this input base: {0}"
                        , b86table[fs[k]]));
                }
                k++;
            }

            //find how many digits the output needs

            //int ol = il * (from / to + 1);
            // by m.gcombes
            int ol = System.Convert.ToInt32(il * (Math.Log(from) / Math.Log(to)) + 1);

            int[] ts = new int[ol + 10]; //assign accumulation array
            int[] cums = new int[ol + 10]; //assign the result array
            ts[0] = 1; //initialize array with number 1

            //evaluate the output
            for (int i = 0; i < il; i++) //for each input digit
            {
                for (int j = 0; j < ol; j++) //add the input digit
                // times (base:to from^i) to the output cumulator
                {
                    cums[j] += ts[j] * fs[i];
                    int temp = cums[j];
                    int rem = 0;
                    int ip = j;
                    do // fix up any remainders in base:to
                    {
                        rem = temp / to;
                        cums[ip] = temp - rem * to; ip++;
                        cums[ip] += rem;
                        temp = cums[ip];
                    }
                    while (temp >= to);
                }

                //calculate the next power from^i) in base:to format
                for (int j = 0; j < ol; j++)
                {
                    ts[j] = ts[j] * from;
                }
                for (int j = 0; j < ol; j++) //check for any remainders
                {
                    int temp = ts[j];
                    int rem = 0;
                    int ip = j;
                    do  //fix up any remainders
                    {
                        rem = temp / to;
                        ts[ip] = temp - rem * to; ip++;
                        ts[ip] += rem;
                        temp = ts[ip];
                    }
                    while (temp >= to);
                }
            }

            //convert the output to string format 
            //(digits 0,to-1 converted to 0-Z characters)
            string sout = string.Empty; //initialize output string
            bool first = false; //leading zero flag
            for (int i = ol; i >= 0; i--)
            {
                if (cums[i] != 0) { first = true; }
                if (!first) { continue; }
                sout += b86table[cums[i]];
            }

            //input was zero, return 0
            if (string.IsNullOrEmpty(sout)) { sout = "0"; }

            //return the converted string
            return sout;
        }

        #endregion Public Static Methods

        /// <summary>
        /// This method return an array of byte represent a base86 number (string) used in this problem only
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] getBytes(string input){
            string output = Convert(86, 16, input);
            if (output.Length % 2 == 1)
                output = 0 + output;
            byte[] result = new byte[output.Length / 2];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte) (16*Symbol.getIndex(output[i*2]) + Symbol.getIndex(output[i*2 + 1]));
            }
            return result ;
        }

        /// <summary>
        /// This method return a string of base86 used in this problem through an array of bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string getString(byte[] bytes)
        {
            string base86 = "";
            string base16 = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                base16 += Symbol.getCharacter(bytes[i] / 16);
                base16 += Symbol.getCharacter(bytes[i] % 16);
            }

            base86 = Convert(16, 86, base16);

            return base86;
        }
    }
}
