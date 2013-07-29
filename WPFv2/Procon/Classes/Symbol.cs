using System;

namespace Procon
{
    /// <summary>
    /// This class convert a single symbol to a single number
    /// The symbols set is loaded from resources by constructor
    /// </summary>
    public class Symbol
    {

        /// <summary>
        /// Static method getIndex by using the DEFAULT_SYMBOLS_SET 
        /// Return the index if the input character is available, otherwise return -1
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int getIndex(char c)
        {
            var index = 0;
            var c1 = new char[1];
            c1[0] = c;
            try
            {
                index = Constants.DEFAULT_SYMBOLS_SET.IndexOfAny(c1);
            }
            catch (Exception)
            {
                index = -1;
            }

            if (index == -1)
                throw new Exception("Input symbol is not in the symbols set: '" + c + "'");
            return index;
        }


        /// <summary>
        /// The static method getCharacter. Using the DEFAULT_SYMBOLS_SET
        /// </summary>
        /// <param name="i">Integer</param>
        /// <returns>Corresponding character</returns>
        public static char getCharacter(int i)
        {
            if (i < 0)
            {
                throw new Exception("i<0");
            }
            if (i > Constants.DEFAULT_SYMBOLS_SET.Length - 1)
            {
                throw new Exception("i>85");
            }
            return Constants.DEFAULT_SYMBOLS_SET[i];
        }
    }
}
