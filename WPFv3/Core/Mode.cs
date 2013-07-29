using System;
using System.Collections.Generic;
using System.Linq;

namespace Procon.Core
{
    public class Mode
    {
        public Mode(int i, string s)
        {
            index = i;
            name = s;
        }
        public int index { get; private set; }
        public string name { get; private set; }

        public static Mode getMode(int index)
        {
            for (int i = 0; i < C.NUMBER_OF_MODES; i++)
            {
                if (index == C.MODES[i].index)
                    return C.MODES[i];
            }

            throw new Exception("Mode not found");
        }
    }
}
