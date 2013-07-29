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
using Procon.Classes;


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
            dicesWrapPanelSender.Width = Constants.NUMBER_OF_DICES_IN_A_ROW * Constants.DICE_SIZE + Constants.NUMBER_OF_DICES_IN_A_ROW;
        }

        /// <summary>
        /// Load the images for dices
        /// </summary>
        private void InitializeImages() {
            imagesOfDices = new BitmapImage[10];
            for (int i = 0; i < 10; i++) {
                string path = "pack://application:,,,/Resources/" + i + ".png";
                imagesOfDices[i] = new BitmapImage(new Uri(path));
            }

            imageOfSpace = new BitmapImage(new Uri("pack://application:,,,/Resources/space.png"));
        }
        
        // *********************mode Functions*************************
        private void NewMessage() {
            packetsListComboBox.Visibility = System.Windows.Visibility.Visible;
            if (sentence != null)
                return;
            // try to create the sentence and packets
            try {
                sentence = new Sentence(messageTextBox.Text);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
                return;
            }
            packets = sentence.getPackets();
            for (int i = 0; i < packets.Length; i++) {
                packetsListComboBox.Items.Add(i + 1);
            }
            numberOfPackets.Text = packets.Length.ToString();
            //modeOfChosenPacket.Text = "NEW";


            // make messageTextBox cannot be edited
            messageTextBox.IsReadOnly = true;
        }

        private void OverwriteMessage() {
            int overwriteOffset = messageTextBox.SelectionStart;
            string overwriteMessage = messageTextBox.SelectedText;
            if (overwriteMessage.Length > Constants.MAX_PACKET_LENGTH) {
                MessageBox.Show("message is too big");
                return;
            } else {
                Packet overwritePacket = new Packet(overwriteOffset, Constants.OVERWRITE, overwriteMessage);
                //modeOfChosenPacket.Text = "OVERWRITE";
                offsetOfChosenPacket.Text = overwriteOffset.ToString();
                lengthOfChosenPacket.Text = overwriteMessage.Length.ToString();
                messageOfChosenPacket.Text = overwriteMessage;
                //packetsListComboBox.Visibility = System.Windows.Visibility.Hidden;
                codeOfChosenPacket.Text = overwritePacket.codeOfPacket;
                DrawDices(overwritePacket, dicesWrapPanelSender);
            }

        }

        private void InsertMessage() {

            int insertOffset = messageTextBox.SelectionStart;
            string insertMessage = messageTextBox.SelectedText;
            if (insertMessage.Length > Constants.MAX_PACKET_LENGTH) {
                MessageBox.Show("message is too big");
                return;
            } else {
                Packet insertPacket = new Packet(insertOffset, Constants.INSERT, insertMessage);
                //modeOfChosenPacket.Text = "INSERT";
                offsetOfChosenPacket.Text = insertOffset.ToString();
                lengthOfChosenPacket.Text = insertMessage.Length.ToString();
                messageOfChosenPacket.Text = insertMessage;
                //packetsListComboBox.Visibility = System.Windows.Visibility.Hidden;
                codeOfChosenPacket.Text = insertPacket.codeOfPacket;
                DrawDices(insertPacket, dicesWrapPanelSender);
            }

        }

        private void DeleteMessage() {
            int deleteOffset = int.Parse(offsetOfChosenPacket.Text);
            int deleteLength = int.Parse(lengthOfChosenPacket.Text);
            Packet deletePacket = new Packet(deleteOffset, Constants.DELETE, deleteLength);
            codeOfChosenPacket.Text = deletePacket.codeOfPacket;
            DrawDices(deletePacket, dicesWrapPanelSender);
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
            if (packet.mode == Constants.OVERWRITE) {

                resultString = resultString.Remove(packet.offset, packet.length);
                resultString = resultString.Insert(packet.offset, packet.message);
                //MessageBox.Show(resultString);
                Result.Text = resultString;
                return;
            }
            if (packet.mode == Constants.DELETE) {
                resultString = resultString.Remove(packet.offset, packet.length);
                Result.Text = resultString;
                return;
            }
            if (packet.mode == Constants.INSERT) {
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
            //modeOfChosenPacket.IsReadOnly = true;
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
            if (receivedPacket.mode == Constants.OVERWRITE) mode = "OVERWRITE";
            if (receivedPacket.mode == Constants.DELETE) mode = "DELETE";
            if (receivedPacket.mode == Constants.INSERT) mode = "INSERT";
            CodeReceivedInfo.Content = string.Format("Mode: {0} \t Offset: {1} \t Length: {2} \n Data: {3}", mode, receivedPacket.offset, receivedPacket.length,receivedPacket.message);
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            PacketToResult(receivedPacket);
        }

        private void setMessageButton_Click(object sender, RoutedEventArgs e) {
            if (ModeSelection.SelectedIndex == 0) { // in NEW MODE
                NewMessage();
                return;
            }



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
            int chosenPacket = packetsListComboBox.SelectedIndex;
            lengthOfChosenPacket.Text = packets[chosenPacket].length.ToString();
            offsetOfChosenPacket.Text = packets[chosenPacket].offset.ToString();
            messageOfChosenPacket.Text = packets[chosenPacket].message;
            codeOfChosenPacket.Text = packets[chosenPacket].codeOfPacket;

            DrawDices(packets[chosenPacket],dicesWrapPanelSender);
        }

        private void messageOfChosenPacket_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int start = messageOfChosenPacket.SelectionStart;
            int length = messageOfChosenPacket.SelectionLength;
        }

        

        private void ModeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            clearComponents();
            if (ModeSelection.SelectedIndex == 0) { // in NEW mode
                packetsListComboBox.Visibility = System.Windows.Visibility.Visible;
            } else {
                packetsListComboBox.Visibility = System.Windows.Visibility.Collapsed;
            }
            lengthOfChosenPacket.IsReadOnly = true;
            offsetOfChosenPacket.IsReadOnly = true;
            if (ModeSelection.SelectedIndex == 3) { // in DELETE mode
                lengthOfChosenPacket.IsReadOnly = false;
                offsetOfChosenPacket.IsReadOnly = false;
            }
        }

    }
}
