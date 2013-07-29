using System;
using System.Collections.Generic;
using System.Linq;

namespace Procon.Core
{
    public class Checksum
    {

        /// <summary>
        /// return a string from adding checksum information to input string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string addCheckSum(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length / C.CHECKSUM_SPACE; i++)
            {
                string temp = input.Substring(i * C.CHECKSUM_SPACE, C.CHECKSUM_SPACE);
                result = result + temp + calculateChecksum(temp);
            }
            if (input.Length > ((int)(input.Length / C.CHECKSUM_SPACE)) * C.CHECKSUM_SPACE)
            {
                string temp = input.Substring(((int)(input.Length / C.CHECKSUM_SPACE)) * C.CHECKSUM_SPACE);
                result = result + temp + calculateChecksum(temp);
            }
            return result;
        }


        public static int calculateChecksum(string i)
        {
            int result = 0;
            string temp = i;
            while (temp.Length > 0)
            {
                result += int.Parse(temp.Substring(temp.Length - 1))*(temp.Length + 1);
                temp = temp.Substring(0, temp.Length - 1);
            }
            return result % 10;
        }
        
        /// <summary>
        /// Check an input string.
        /// Return -1 if everything is ok. And if it is removeChecksum = true, removeChecksum information.
        /// If there is something wrong, return the index of wrong checksum number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int checkChecksum(string input, bool removeChecksum = false)
        {
            string[] temp = splitSentence(input, C.CHECKSUM_SPACE + 1);

            for (int i = 0; i< temp.Length; i++)
            {
                if (!checkChecksumUnit(temp[i]))
                    return  i * (C.CHECKSUM_SPACE + 1) + temp[i].Length - 1;
            }

            if (removeChecksum)
            {
                for (int i = 0; i < temp.Length; i++)
                    temp[i] = temp[i].Remove(temp[i].Length - 1);
            }

            return -1;
        }

        public static bool checkChecksumUnit(string i)
        {
            if (i.Length < 2) return false;
            int checksum = Checksum.calculateChecksum(i.Substring(0, i.Length - 1));
            int acctual = int.Parse( i.Substring(i.Length - 1, 1));
            return (checksum == acctual);
        }

        private static string[] splitSentence(string input, int length)
        {
            int noOfParts = input.Length / length + 1;
            string[] result = new String[noOfParts];
            for (int i = 0; i < noOfParts; i++)
            {
                if (i == noOfParts - 1)
                    result[i] = input.Substring(i * length);
                else
                    result[i] = input.Substring(i * length, length);
            }

            return result;
        }
    }
}
