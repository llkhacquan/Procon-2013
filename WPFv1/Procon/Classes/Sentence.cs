using System;
using System.Collections.Generic;
using System.Linq;

namespace Procon.Classes
{
    public class Sentence
    {
        
        private Packet[] packets;
        private String sentence;

        /// <summary>
        /// Default constructor. Used for build a subSentence
        /// </summary>
        public Sentence()
        {

        }

        /// <summary>
        /// Constructor with a subSentence parameter. Used to build result
        /// </summary>
        /// <param name="input"></param>
        public Sentence(string input)
        {
            sentence = input;
            packets = this.getPackets();
        }

        /// <summary>
        /// Create the result that encodes the full Sentence
        /// </summary>
        /// <returns></returns>
        public Packet[] getPackets()
        {
            int noOfPackets = sentence.Length / Constants.MAX_PACKET_LENGTH + 1;
            if ((int)sentence.Length / Constants.MAX_PACKET_LENGTH == (double)sentence.Length / Constants.MAX_PACKET_LENGTH)
                noOfPackets--;
            

            Packet[] result = new Packet[noOfPackets];
            string[] partsOfSentence = splitSentence(sentence);

            for (int i = 0; i < noOfPackets; i++)
            {
                result[i] = new Packet(i * Constants.MAX_PACKET_LENGTH, Constants.NEW, partsOfSentence[i]);
            }

            packets = result;
            return result;
        }

        private string[] splitSentence(string subSentence)
        {
            int noOfParts = subSentence.Length / Constants.MAX_PACKET_LENGTH + 1;
            string[] result = new String[noOfParts];
            for (int i = 0; i < noOfParts; i++)
            {
                if (i == noOfParts - 1)
                    result[i] = subSentence.Substring(i * Constants.MAX_PACKET_LENGTH);
                else
                    result[i] = subSentence.Substring(i * Constants.MAX_PACKET_LENGTH, Constants.MAX_PACKET_LENGTH);
            }

            return result;
        }

        /// <summary>
        /// Create the Packets that encodes a part of Sentence
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Packet[] getPackets(int offset, int length)
        {
            string subSentence = sentence.Substring(offset, length);
            int noOfPackets = length / Constants.MAX_PACKET_LENGTH + 1;
            Packet[] result = new Packet[noOfPackets];

            string[] partsOfSentence = splitSentence(subSentence);

            for (int i = 0; i < noOfPackets; i++)
            {
                result[i] = new Packet(offset + i * Constants.MAX_PACKET_LENGTH, Constants.OVERWRITE, partsOfSentence[i]);
            }

            return result;
        }

        /// <summary>
        /// Modify the Sentence by some result
        /// </summary>
        /// <param name="packet"></param>
        public void setPacket(Packet packet)
        {
            if (packet.mode == Constants.DELETE)
            {
                sentence = sentence.Remove(packet.offset, packet.length);
                return;
            }

            if (packet.mode == Constants.INSERT)
            {
                sentence = sentence.Insert(packet.offset, packet.message);
                return;
            }

            if (packet.mode == Constants.OVERWRITE)
            {
                if (packet.offset == sentence.Length)
                {
                    sentence += packet.message;
                    return;
                }else if (packet.offset > sentence.Length)
                {
                    throw new Exception("Offset out of length");
                }
                else if (packet.length + packet.offset > sentence.Length)
                    sentence.Remove(packet.offset);
                else
                {
                    sentence = sentence.Remove(packet.offset, packet.length);
                    sentence = sentence.Insert(packet.offset, packet.message);
                }
                return;
            }
        }

        /// <summary>
        /// Return the subSentence. It may be not completed
        /// </summary>
        /// <returns></returns>
        public String getSentence()
        {
            return sentence;
        }
    }
}
