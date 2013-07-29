using System;
using System.Collections.Generic;
using System.Linq;

namespace Procon {
    public class Constants {
        public const String DEFAULT_SYMBOLS_SET =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_!\"#$%&'()*+,-./:;<=>?@`";
        public const int MODE_NORMAL = 0;
        public const int MODE_SMAZ = 2;
        public const int MODE_BASE_86 = 1;
        public const int MODE_ZLIB = 3;


        public const int MODE_INSERT = 9;
        public const int MODE_OVERWRITE = 8;
        public const int MODE_DELETE = 7;
        public const int NEW = 4;

        public const int SIZE_OF_OFFSET = 4; // In number of dices
        public const int SIZE_OF_LENGTH = 4; // In number of dices
        public const int SIZE_OF_MODE = 1; // In number of dices
        public const int SIZE_OF_CHECKSUM = 2;

        public const int MAX_LENGTH_OF_MESSAGE = 4000;
        public const int MAX_PACKET_LENGTH = 4000; // In number of symbols
        public const int MAX_DICES_NUMBER = 150; //The maximum number of dices

        public const int NUMBER_OF_DICES_IN_A_ROW = 25;
        public const int NUMBER_OF_DICES_IN_A_COLUMN = 25;
        public const int DICE_SIZE = 24; // In pixel
    }
}
