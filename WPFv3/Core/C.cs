using System;
using System.Collections.Generic;
using System.Linq;

namespace Procon.Core
{
    public class C {

        static C()
        {
            MODES = new Mode[3];
            MODES[0] = new Mode(0, NORMAL);
           
            MODES[1] = new Mode(1, ZLIB);
            MODES[2] = new Mode(2, SMAZ);

            NUMBER_OF_MODES = MODES.Length;

            NUMBER_OF_DICES_PER_ROW = NUMBER_OF_BLOCKS_PER_ROW * BLOCK_OF_DICES_WIDTH;
            CHECKSUM_SPACE = NUMBER_OF_DICES_PER_ROW - 1;
        }



        public const String DEFAULT_SYMBOLS_SET =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_!\"#$%&'()*+,-./:;<=>?@`";
        public const String RECEVIED_CODE_RANGE = "0123456789";
        public static readonly Mode[] MODES;
        public static readonly int NUMBER_OF_MODES;

        // Modes' names
        public const string NORMAL = "Normal";
        public const string SMAZ = "Smaz";
        public const string ZLIB = "Zlib";

        // Size of each field in packet headed
        public const int SIZE_OF_OFFSET = 4; // In number of dices
        //public const int SIZE_OF_LENGTH = 0; // In number of dices
        public const int SIZE_OF_MODE = 1; // In number of dices

        // Default constants for Procon 2013 problem
        public const int MAX_LENGTH_OF_MESSAGE = 4000;
        public const int MAX_PACKET_LENGTH = 4000; // In number of symbols
        public const int MAX_DICES_NUMBER = 150; //The maximum number of dices

        // Information to draw dices
        public const int BLOCK_OF_DICES_WIDTH = 5; // There are 5 dices in a row of a block of dices
        public const int BLOCK_OF_DICES_HEIGHT = 5; // There are 5 dices in a column of a block of dices
        public const int NUMBER_OF_BLOCKS_PER_ROW = 4; // There are 5 blocks in a row of dice wrap panel
        public static readonly int NUMBER_OF_DICES_PER_ROW;
        public const int DICE_SIZE = 24; // In pixel
        public const int SPACE_SIZE = 5;

        public const int LENGTH_OF_A_LINE_OF_CODE = 30;
        
        public const char SPACE = ' ';


        public static readonly int CHECKSUM_SPACE; // For a number of dices, a dice as checksum with be added
    }
}
