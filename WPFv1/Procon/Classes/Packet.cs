using System;

namespace Procon.Classes
{
    public class Packet
    {
        /// <summary>
        /// The position of the first symbol in the packet
        /// </summary>
        public int offset { get; private set; }
        /// <summary>
        /// Number of symbols encoded in the packet
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// Mode of packet
        /// </summary>
        public int mode { get; set; }
        /// <summary>
        /// String stores symbols of the packet
        /// </summary>
        public String message { get; set; }

        /// <summary>
        /// String stores code of the sentence
        /// </summary>
        public String codeOfMessage { get; private set; }
        /// <summary>
        /// String stores code of the full packet
        /// </summary>
        public String codeOfPacket { get; private set; }

        /// <summary>
        /// Constructor with a String of Full Code
        /// </summary>
        /// <param name="inputString"></param>
        public Packet(string inCode)
        {
            codeOfPacket = inCode;
            message = "";
            offset = int.Parse(inCode.Substring(0, Constants.SIZE_OF_OFFSET));
            length = int.Parse(inCode.Substring(Constants.SIZE_OF_OFFSET, Constants.SIZE_OF_LENGTH));
            if (length > Constants.MAX_PACKET_LENGTH)
                throw new Exception(String.Format("Length of packet is too big. MAX = {0} Current = {1}", Constants.MAX_PACKET_LENGTH, length));
            mode = int.Parse(inCode.Substring(Constants.SIZE_OF_OFFSET + Constants.SIZE_OF_LENGTH, Constants.SIZE_OF_MODE));

            if (mode == Constants.DELETE) {
                message = "";
                return;
            }
            // Convert to Data
            string codeOfData = inCode.Substring(Constants.SIZE_OF_OFFSET + Constants.SIZE_OF_LENGTH + Constants.SIZE_OF_MODE);

            if (2 * length != codeOfData.Length)
            {
                throw new Exception("Different lengths in Code input to packet");
            }
            for (var i = 0; i < length; i++)
            {
                message = message + Symbol.getCharacter(int.Parse(codeOfData.Substring(i * 2, 2)));
            }
        }
        /// <summary>
        /// Constructor with a String of Data, offset and mode
        /// </summary>
        /// <param name="inOffset"></param>
        /// <param name="inMessage"></param>
        public Packet(int inOffset, int inMode, String inMessage)
        {
            offset = inOffset;
            mode = inMode;
            length = inMessage.Length;
            if (length > Constants.MAX_PACKET_LENGTH)
                throw new Exception(String.Format("Length of packet is too big. MAX = {0} Current = {1}", Constants.MAX_PACKET_LENGTH, length));
            message = inMessage;
            codeOfMessage = "";
            for (var i = 0; i < inMessage.Length; i++)
            {
                codeOfMessage += getInt(Symbol.getIndex(inMessage[i]), 2);
            }

            codeOfPacket =  Packet.getInt(offset, Constants.SIZE_OF_OFFSET);
            codeOfPacket += Packet.getInt(length, Constants.SIZE_OF_LENGTH);
            codeOfPacket += Packet.getInt(mode, Constants.SIZE_OF_MODE);
            codeOfPacket += codeOfMessage;
        }
        /// <summary>
        /// Use only for DELETE mode
        /// </summary>
        /// <param name="i"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Packet(int inOffset, int inMode, int inLength) {
            offset = inOffset;
            mode = inMode;
            length = inLength;
            if (length > Constants.MAX_PACKET_LENGTH)
                throw new Exception(String.Format("Length of packet is too big. MAX = {0} Current = {1}", Constants.MAX_PACKET_LENGTH, length));
            message = "";
            codeOfMessage = "";
            codeOfPacket = Packet.getInt(offset, Constants.SIZE_OF_OFFSET);
            codeOfPacket += Packet.getInt(length, Constants.SIZE_OF_LENGTH);
            codeOfPacket += Packet.getInt(mode, Constants.SIZE_OF_MODE);
            codeOfPacket += codeOfMessage;
        }
        /// <summary>
        /// Return a string present a positive integer n with fixed length by fill up 0 before the number
        /// If length is too small, skip.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string getInt(int n, int length)
        {
            string result = n.ToString();
            if (n == 0)
            {
                for (int i = 0; i < length - 1; i++)
                {
                    result += "0";
                }
                return result;  
            }

            
            int numberOfNeederZero = length - (int)Math.Log10(n) - 1;
            for (var i = 0; i < numberOfNeederZero; i++)
            {
                result = "0" + result;
            }

            return result;
        }

        public int CompareTo(Packet p){
            return this.codeOfPacket.CompareTo(p.codeOfPacket);
        }
    }
}
