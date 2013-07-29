using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using Procon.Properties;


namespace Procon
{
    /// <summary>
    /// Interaction logic for SenderWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Sentence sentence;
        Packet[] packets;
        BitmapImage[] imagesOfDices;
        BitmapImage imageOfSpace;
        Packet receivedPacket;
        string resultString = "";
		
        public MainWindow()
        {
            InitializeComponent();
            InitializeImages();
            FreezeComponents();

            InitializeComponent2();
        }

        private void InitializeComponent2()
        {
            dicesWrapPanelSender.Width = Constants.NUMBER_OF_DICES_IN_A_ROW * Constants.DICE_SIZE + Constants.NUMBER_OF_DICES_IN_A_ROW;
            dicesWrapPanelRecieved.Width = dicesWrapPanelSender.Width;
        }

        /// <summary>
        /// Load the images for dices
        /// </summary>
        private void InitializeImages() {
            imagesOfDices = new BitmapImage[10];
            for (int i = 0; i < 10; i++) {
                string path = String.Format("pack://application:,,,/Resources/{0}.png", i);
                imagesOfDices[i] = new BitmapImage(new Uri(path));
            }

            imageOfSpace = new BitmapImage(new Uri("pack://application:,,,/Resources/space.png"));
        }
        //********************************************************//
        //***************** SUPPORT FUNCTIONS ********************//
        private void DrawDices(Packet packet,WrapPanel dicesWrapPanel)
        {
            string code = packet.codeOfPacket;
            
            Image image;
            dicesWrapPanel.Children.Clear();
            for (int i = 0; i < code.Length; i++)
            {
                if (i % 5 == 0)
                {
                    image = new Image()
                    {
                        Source = imageOfSpace,
                        Height = Constants.DICE_SIZE,
                        Width = 5
                    };
                    dicesWrapPanel.Children.Add(image);
                }

                int index = int.Parse(code[i].ToString());
                image = new Image(){
                    Source = imagesOfDices[index],
                    Height = Constants.DICE_SIZE,
                    Width = Constants.DICE_SIZE
                };
                dicesWrapPanel.Children.Add(image);

                
                
            }
        }
        
        private void PacketToResult(Packet packet) {
            if (packet.mode == Constants.NEW) {
                resultString += packet.message;
                Result.Text = resultString;
                return;
            }
            if (packet.mode == Constants.MODE_OVERWRITE) {

                resultString = resultString.Remove(packet.offset, packet.length);
                resultString = resultString.Insert(packet.offset, packet.message);
                //MessageBox.Show(resultString);
                Result.Text = resultString;
                return;
            }
            if (packet.mode == Constants.MODE_DELETE) {
                resultString = resultString.Remove(packet.offset, packet.length);
                Result.Text = resultString;
                return;
            }
            if (packet.mode == Constants.MODE_INSERT) {
                resultString = resultString.Insert(packet.offset, packet.message);
                Result.Text = resultString;
                return;
            }
        }

        public void clearComponents() {
            packetsListComboBox.Items.Clear();

            numberOfPackets.Text = "";
            lengthOfChosenPacket.Text = "";
            offsetOfChosenPacket.Text = "";
            messageOfChosenPacket.Text = "";
            codeOfChosenPacket.Text = "";

            dicesWrapPanelSender.Children.Clear();
        }

        private void FreezeComponents() {
            numberOfPackets.IsReadOnly = true;
            lengthOfChosenPacket.IsReadOnly = true;
            offsetOfChosenPacket.IsReadOnly = true;
            modeOfChosenPacket.IsReadOnly = true;
            messageOfChosenPacket.IsReadOnly = true;
            codeOfChosenPacket.IsReadOnly = true;
        }
        //***************** BUTTON FUNCTION *******************//
        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            messageTextBox.IsReadOnly = false;
            messageTextBox.Text = "";
            clearComponents();

            sentence = null;
        }

        private void SubmitCode_Click(object sender, RoutedEventArgs e) {
            try {
                receivedPacket = new Packet(CodeReceivedTextBox.Text);
            } catch (Exception) {
                MessageBox.Show("Wrong format");
                return;
            }
            DrawDices(receivedPacket, dicesWrapPanelRecieved);
            string mode = "";
            if (receivedPacket.mode == Constants.NEW) mode = "NEW";
            if (receivedPacket.mode == Constants.MODE_OVERWRITE) mode = "OVERWRITE";
            if (receivedPacket.mode == Constants.MODE_DELETE) mode = "DELETE";
            if (receivedPacket.mode == Constants.MODE_INSERT) mode = "INSERT";
            CodeReceivedInfo.Content = string.Format("Mode: {0} \t Offset: {1} \t Length: {2} \n Data: {3}", mode, receivedPacket.offset, receivedPacket.length,receivedPacket.message);
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            PacketToResult(receivedPacket);
        }

        private void setMessageButton_Click(object sender, RoutedEventArgs e) {
            packetsListComboBox.Visibility = System.Windows.Visibility.Visible;
            
            if (messageTextBox.Text.Length == 0)
                return;
            if (messageTextBox.Text.Length > Constants.MAX_LENGTH_OF_MESSAGE)
            {
                MessageBox.Show("Length of input message is bigger than " + Constants.MAX_LENGTH_OF_MESSAGE + "! Try again.");
                return;
            }
                

            lengthOfMessage.Text = messageTextBox.Text.Length.ToString();

            // try to create the sentence and packets
            try
            {
                sentence = new Sentence(messageTextBox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
            packets = sentence.getPackets();
            for (int i = 0; i < packets.Length; i++)
            {
                packetsListComboBox.Items.Add(i + 1);
            }
            numberOfPackets.Text = packets.Length.ToString();
            modeOfChosenPacket.Text = "";


            // make messageTextBox cannot be edited
            messageTextBox.IsReadOnly = true;



            /**
            if (ModeSelection.SelectedIndex == 1) { // in OVERWRITE mode
                //MessageBox.Show("OVERWRITE");
                OverwriteMessage();
                return;
            }
            if (ModeSelection.SelectedIndex == 2) { // in INSERT mode
                InsertMessage();
                return;
            }
            if (ModeSelection.SelectedIndex == 3) { // in DELETE mdoe
                DeleteMessage();
            }
             **/
        }
        //****************************************************************************//
        private void packetsListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (packetsListComboBox.Items.IsEmpty == true)
                return;
            Packet chosenPacket = packets[packetsListComboBox.SelectedIndex];
            lengthOfChosenPacket.Text = chosenPacket.length.ToString();
            offsetOfChosenPacket.Text = chosenPacket.offset.ToString();
            messageOfChosenPacket.Text = chosenPacket.message;
            codeOfChosenPacket.Text = chosenPacket.codeOfPacket;
            modeOfChosenPacket.Text = chosenPacket.mode.ToString();
            DrawDices(chosenPacket,dicesWrapPanelSender);
            lengthOfCode.Text = chosenPacket.codeOfPacket.Length.ToString();
        }

    }
}
