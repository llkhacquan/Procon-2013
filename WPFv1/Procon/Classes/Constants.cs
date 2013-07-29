using System;
using System.Collections.Generic;
using System.Linq;

namespace Procon.Classes {
    public class Constants {
        public const String DEFAULT_SYMBOLS_SET =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_!\"#$%&'()*+,-./:;<=>?@`";
        public const int INSERT = 1;
        public const int OVERWRITE = 5;
        public const int DELETE = 3;
        public const int NEW = 0;
        public const int SIZE_OF_OFFSET = 4; // In number of dices
        public const int SIZE_OF_LENGTH = 3; // In number of dices
        public const int SIZE_OF_MODE = 1; // In number of dices
        public const int MAX_PACKET_LENGTH = 100; // In number of symbols
        public const int MAX_DICES_NUMBER = 150; //The maximum number of dices
        public const int NUMBER_OF_DICES_IN_A_ROW = 25;
        public const int NUMBER_OF_DICES_IN_A_COLUMN = 25;
        public const int DICE_SIZE = 24; // In pixel
        public const int NUMBER_OF_DICE_S_FACE = 10;
        public const int LENGHT_OF_A_LINE = 10;
    }
}
