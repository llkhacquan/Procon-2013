using System;
using System.Collections.Generic;
using System.Linq;

namespace Procon.Core
{
    public class Sentence
    {
        
        public Packet[] packets {get; private set;}
        public String sentence { get; private set; }

        /// <summary>
        /// Default constructor. Used for build a subSentence
        /// </summary>
        public Sentence()
        {
            sentence = "";
        }

        /// <summary>
        /// Constructor with a subSentence parameter. Used to build packets
        /// </summary>
        /// <param name="input"></param>
        public Sentence(string input)
        {
            sentence = input;
            packets = getPackets();
        }

        /// <summary>
        /// Create the result that encodes the full Sentence
        /// </summary>
        /// <returns></returns>
        public Packet[] getPackets()
        {
            int noOfPackets = sentence.Length / C.MAX_PACKET_LENGTH + 1;
            if ((int)sentence.Length / C.MAX_PACKET_LENGTH == (double)sentence.Length / C.MAX_PACKET_LENGTH)
                noOfPackets--;            

            Packet[] result = new Packet[noOfPackets];
            string[] partsOfSentence = splitSentence(sentence);

            for (int i = 0; i < noOfPackets; i++)
            {
                result[i] = new Packet(i * C.MAX_PACKET_LENGTH, C.MODES[0], partsOfSentence[i]);
            }

            packets = result;
            return result;
        }

        private string[] splitSentence(string subSentence)
        {
            int noOfParts = subSentence.Length / C.MAX_PACKET_LENGTH + 1;
            string[] result = new String[noOfParts];
            for (int i = 0; i < noOfParts; i++)
            {
                if (i == noOfParts - 1)
                    result[i] = subSentence.Substring(i * C.MAX_PACKET_LENGTH);
                else
                    result[i] = subSentence.Substring(i * C.MAX_PACKET_LENGTH, C.MAX_PACKET_LENGTH);
            }

            return result;
        }

        /// <summary>
        /// Create the Packets that encodes a part of Sentence
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Packet[] getPackets(int offset, Mode inMode,int length)
        {
            string subSentence = sentence.Substring(offset, length);
            int noOfPackets = length / C.MAX_PACKET_LENGTH + 1;
            Packet[] result = new Packet[noOfPackets];

            string[] partsOfSentence = splitSentence(subSentence);

            for (int i = 0; i < noOfPackets; i++)
            {
                result[i] = new Packet(offset + i * C.MAX_PACKET_LENGTH, inMode, partsOfSentence[i]);
            }

            return result;
        }

        /// <summary>
        /// Modify the Sentence by some result
        /// </summary>
        /// <param name="packet"></param>
        public void setPacket(Packet packet)
        {
            if (packet.offset == sentence.Length)
            {
                sentence += packet.message;
                return;
            }
            else if (packet.offset > sentence.Length)
            {
                throw new Exception("Offset out of length!!!");
            }
            else if (packet.length + packet.offset > sentence.Length)
            {
                sentence = sentence.Remove(packet.offset);
                sentence += packet.message;
            }
            else
            {
                sentence = sentence.Remove(packet.offset, packet.length);
                sentence = sentence.Insert(packet.offset, packet.message);
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
