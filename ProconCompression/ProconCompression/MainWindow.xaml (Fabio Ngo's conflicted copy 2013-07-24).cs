using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SevenZip.Compression.LZMA;
using Procon;

using Ionic.Zlib;


namespace ProconCompression
{



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public string[] OriginalText =
        {
        "Ramen, a part of Japanese food culture, has been spreading throughout the world at a tremendous pace. This, in turn, has drawn foreign tourists from different countries to visit Japan in search of ramen. Economic growth in ASEAN countries along with the yen's depreciation in particular, have led to a steady growth of tourism from Southeast Asia. Additionally, individual travel from Europe and the United States has been growing by leaps and bounds. Meanwhile, requests for Islamic-law and vegetarian menus are also on a sharp rise. Presently, there are over 1,000 ramen shops overseas, and although preparation of menus for Islamic law-bound and vegetarian diet at shops in each country is both commonplace and common-sense, such support from shops in Japan has been little to none thus far. If ramen is going to globalize in the true sense of the concept, it is essential that such vegetarian versions be made available. Though Shin-Yokohama Raumen Museum had responded to vegetarian requests, a system to always provide vegetarian dishes has been put into place beginning on Monday July 1, 2013. We continue to strengthen our support for the improved travel experience for foreign tourists. The lack of Wi-Fi accessibility has been said to be the number-one ranking difficulty for tourists to Japan, so we've set up a Wi-Fi (free public-access wireless LAN accessible environment. As stated before, the economic growth of ASEAN has brought about a rapid increase in tourism from the likes of Thai, Malaysian, and Indonesian nationalities. With nearly 1.6 billion people, Muslims make up about a fourth of the world's population, and about 220 million, or 14 of that figure is from Malaysian, and Indonesian nationals. An aspect of Islam is the taboo against consuming certain food ingredients like pork and alcohol, and the desire for halal food products made from food sources in environments that are permissible and in accordance with Islamic law. gs1e.jpg Nevertheless, according to one Indonesian association for travel agencies, analysis showed that 70% of Muslims in Indonesia are liberal, and 30% are strict, and that liberal Muslims can be found especially in large metropolitan areas such as Jakarta. In a program run by the Ministry of Foreign Affairs, it was claimed by Japanese travel agencies arranging travel for Southeast Asian students that Indonesians were content as long as pork wasn't evident outright in the food that was served.*Starting on Monday July 1, 2013 is Shin-Yokohama Raumen Museum's Muslim-Friendly menu, with dishes that exclude alcohol, pork and other taboo ingredients not approved under halal.",
        };

       public MainWindow()
        {
            InitializeComponent();
        }

        private void zipButton_Click(object sender, RoutedEventArgs e)
        {
            output.Text = String.Format("Original \tSmaz \t .NET \t 7Zip");
            for (int i = 0; i < OriginalText.Length; i++) {
                output.Text += String.Format("\n{0}\t{1}\t{2}\t{3}",
                                                OriginalText[i].Length,
                                                SmazCompression(OriginalText[i]),
                                                DotNetZipCompression(OriginalText[i]),
                                                SevenZipCompression(OriginalText[i]));
            }           

        }

        private int SmazCompression(String inputString)
        {
            byte[] Compressed = Smaz.Compress(inputString);
            //throw new Exception("");
            string temp = Smaz.Uncompress(Compressed);
            //if(input.CompareTo(Smaz.Uncompress(Compressed))!=0) throw new Exception(Smaz.Uncompress(Compressed)+"\n\n" + input);
            //if (temp.CompareTo(inputString) != 0) throw new Exception("Smaz") ;
            input.Text = temp + "\n" + inputString;
            return Compressed.Length;
        }

        private int DotNetZipCompression(String input)
        {
            byte[] DataBytes = new byte[input.Length];
            for (int i = 0; i < input.Length;i++) {
                DataBytes[i] = Convert.ToByte(input[i]);
            }
            
            // Compress it
            byte[] Compressed = DeflateStream.CompressBuffer(DataBytes);
            if (input.CompareTo(DeflateStream.UncompressString(Compressed)) != 0) throw new Exception(".NET error");
            return Compressed.Length;
        }

        private int SevenZipCompression(String input)
        {
            byte[] DataBytes = new byte[input.Length];
            for (int i = 0; i < input.Length; i++) {
                DataBytes[i] = Convert.ToByte(input[i]);
            }
            // Compress it
            byte[] Compressed = SevenZipHelper.Compress(DataBytes);
            //if (input.CompareTo(SevenZipHelper.Decompress(Compressed)) != 0) throw new Exception("Smaz error");
            return Compressed.Length;
        }    
    }
}
