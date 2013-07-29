using System;
using System.Collections.Generic;
using System.Linq;
using Ionic.Zlib;


namespace Procon.Core
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
        public int length { get; private set; }
        /// <summary>
        /// Mode of packet
        /// </summary>
        public Mode mode { get; private set; }
        /// <summary>
        /// String stores symbols of the packet
        /// </summary>
        public String message { get; private set; }

        /// <summary>
        /// String stores code of the sentence
        /// </summary>
        public String codeOfMessage { get; private set; }
        /// <summary>
        /// String stores code of the full packet
        /// </summary>
        public String codeOfPacket { get; private set; }

        /// <summary>
        /// String stores code of full packet added checksum
        /// </summary>
        public String codeOfPacketWithChecksum { get; private set; }

        /// <summary>
        /// Constructor with a String of Full Code without checksum
        /// </summary>
        /// <param name="inputString"></param>
        public Packet(string inCode)
        {
            codeOfPacketWithChecksum = Checksum.addCheckSum(inCode);
            codeOfPacket = inCode;
            message = "";
            offset = int.Parse(inCode.Substring(0, C.SIZE_OF_OFFSET));
            mode = Mode.getMode(int.Parse(inCode.Substring(C.SIZE_OF_OFFSET, C.SIZE_OF_MODE)));
            
            
            // Convert to Data
            string codeOfMessage = inCode.Substring(C.SIZE_OF_OFFSET + C.SIZE_OF_MODE);

            message = decodeMessage(codeOfMessage, mode);
            length = message.Length;
            if (length != message.Length)
                throw new Exception("Length confliction in input code. Check again");
        }

        private string decodeMessage(string codeOfMessage, Mode decodeMode)
        {
            string result = "";

            switch (decodeMode.name){
                case C.NORMAL:
                    for (var i = 0; i < codeOfMessage.Length / 2; i++)
                    {
                        result = result + Symbol.getCharacter(int.Parse(codeOfMessage.Substring(i * 2, 2)));
                    }
                    break;
                case C.ZLIB:
                    {
                        byte[] bytes = Converter.getBytes(codeOfMessage, 10);
                        result = ZlibStream.UncompressString(bytes);
                        break;
                    }
                case C.SMAZ:
                    {
                        byte[] bytes = Converter.getBytes(codeOfMessage, 10);
                        result = Smaz.Uncompress(bytes);
                        break;
                    }
                default: 
                    break;
            }

            return result;
        }
        /// <summary>
        /// Constructor with a String of Data, offset and mode
        /// </summary>
        /// <param name="inOffset"></param>
        /// <param name="inMessage"></param>
        public Packet(int inOffset, Mode inMode, String inMessage)
        {
            offset = inOffset;
            mode = inMode;
            length = inMessage.Length;
            if (length > C.MAX_PACKET_LENGTH)
                throw new Exception(String.Format("Length of packet is too big. MAX = {0} Current = {1}", C.MAX_PACKET_LENGTH, length));
            message = inMessage;
            

            codeOfPacket =  Packet.getInt(offset, C.SIZE_OF_OFFSET);
            codeOfPacket += Packet.getInt(mode.index, C.SIZE_OF_MODE);
            codeOfPacket += codeOfMessage = encodeMessage(inMessage, inMode);

            codeOfPacketWithChecksum = Checksum.addCheckSum(codeOfPacket);
        }


        /// <summary>
        /// Encode input message with corresponding encode Mode.
        /// </summary>
        /// <param name="inMessage"></param>
        /// <param name="encodeMode"></param>
        /// <returns></returns>
        private string encodeMessage(String inMessage, Mode encodeMode)
        {
            string result = "";

            switch (encodeMode.name)
            {
                case C.NORMAL:
                    for (var i = 0; i < inMessage.Length; i++)
                    {
                        result += getInt(Symbol.getIndex(inMessage[i]), 2);
                    }
                    break;
                case C.ZLIB:{
                    byte[] bytes = ZlibStream.CompressString(inMessage);
                    result = Converter.getString(bytes, 10);
                    break;
                }
                case C.SMAZ:{
                    byte[] bytes = Smaz.Compress(inMessage);
                    result = Converter.getString(bytes, 10);
                    break;
                }
                default:
                    break;
            }
            return result;
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
            if (p.message == message && p.offset == offset)
                return 0;
            else return 1;
        }
    }
}
