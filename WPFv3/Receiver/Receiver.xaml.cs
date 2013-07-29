using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Procon.Core;



namespace Procon.Receiver {
    /// <summary>
    /// Interaction logic for Receiver.xaml
    /// </summary>
    public partial class ReceiverWindow : Window {
        private Packet currentPacket;
        private BitmapImage[] imagesOfDices;
        private BitmapImage imageOfSpace;
        private string receivedCode = string.Empty;

        private Stack<Packet> receivedPackets = new Stack<Packet>();
        private Stack<string> readedLinesOfCode = new Stack<string>();

        private Sentence sentence = new Sentence();


        public ReceiverWindow() {
            InitializeComponent();
            InitializeImages();

            codeInput_Text.Focus();
        }


        private void InitializeImages() {
            imagesOfDices = new BitmapImage[10];
            for (var i = 0; i < 10; i++) {
                var path = String.Format("pack://application:,,,/Resources/{0}.png", i);
                imagesOfDices[i] = new BitmapImage(new Uri(path));
            }
            imageOfSpace = new BitmapImage(new Uri("pack://application:,,,/Resources/space.png"));
        }

        private void codeInput_Text_TextChanged(object sender, TextChangedEventArgs e) {
            var length = codeInput_Text.Text.Length;
            var caretIndex = codeInput_Text.CaretIndex;

            if (length == 0) {
                return;
            }
            char justTyped;
            if (caretIndex > 0) {
                justTyped = codeInput_Text.Text[caretIndex - 1];
                if ((justTyped < '0' || justTyped > '6') && (justTyped != '@' && justTyped != '#' && justTyped != '^' && justTyped != C.SPACE)) {
                    codeInput_Text.Text = codeInput_Text.Text.Remove(caretIndex - 1, 1);
                    codeInput_Text.CaretIndex = caretIndex - 1;
                    return;
                }
            }

            var codeLine = codeInput_Text.Text.Replace(C.SPACE.ToString(), string.Empty).Replace("@", "7").Replace("#", "8").Replace("^", "9");
            try {
                currentChecksum.Text = Checksum.calculateChecksum(codeLine).ToString();
            } catch (Exception) {
                codeInput_Text.Text = string.Empty;
            }

            if (length == (C.BLOCK_OF_DICES_WIDTH + 1) * C.NUMBER_OF_BLOCKS_PER_ROW) {
                endCodeLine();
            }
            drawDices(codeLine);
        }
        private void drawDices(string code) {
            Image image;
            dices_WrapPanel.Children.Clear();

            for (var i = 0; i < code.Length; i++) {
                if (i % C.BLOCK_OF_DICES_WIDTH == 0) {
                    image = new Image()
                    {
                        Source = imageOfSpace,
                        Height = C.DICE_SIZE,
                        Width = C.SPACE_SIZE
                    };

                    dices_WrapPanel.Children.Add(image);
                }

                if (i % (C.NUMBER_OF_DICES_PER_ROW * C.BLOCK_OF_DICES_HEIGHT) == 0) {
                    image = new Image()
                    {
                        Source = imageOfSpace,
                        Height = C.SPACE_SIZE,
                        Width = C.DICE_SIZE * C.NUMBER_OF_DICES_PER_ROW + 25
                    };
                    dices_WrapPanel.Children.Add(image);
                }

                var index = int.Parse(code[i].ToString());
                image = new Image()
                {
                    Source = imagesOfDices[index],
                    Height = C.DICE_SIZE,
                    Width = C.DICE_SIZE
                };


                dices_WrapPanel.Children.Add(image);
            }
        }
        private bool endCodeLine() {
            var codeLine = codeInput_Text.Text.Replace("@", "7").Replace("#", "8").Replace("^", "9");
            var temp = codeLine;
            codeLine = codeLine.Replace(" ", string.Empty);

            if (Checksum.checkChecksumUnit(codeLine)) {
                readedLinesOfCode.Push(temp);
                receivedCode_Text.Text += String.Format("{0}\t{1}\n", readedLinesOfCode.Count, codeInput_Text.Text);
                receivedCode += codeLine.Substring(0, codeLine.Length - 1);
                codeInput_Text.Text = string.Empty;

                return true;
            } else {
                MessageBox.Show("error");
                if (codeInput_Text.Text.Length == 0) {
                    return false;
                }
                codeInput_Text.Text = codeInput_Text.Text.Substring(0, codeInput_Text.Text.Length - 1);
                codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
                return false;
            }
        }

        private void setPacket_Button_Click(object sender, RoutedEventArgs e) {

            if (codeInput_Text.Text.Length > 0 && endCodeLine() == false)
            {
                return;
            }
            try {
                currentPacket = new Packet(receivedCode);
                
                
            } catch (Exception ex) {
                
                MessageBox.Show(ex.Message);
                return;
            }
            sentence.setPacket(currentPacket);
            receivedPackets.Push(currentPacket);

            receivedMessage_TextBox.Text = sentence.getSentence();
            receivedCode = string.Empty;
            receivedCode_Text.Text = string.Empty;

            readedLinesOfCode.Clear();
            
            //codeInput_Text.Length = 0;
        }

        private void removeLastLine_Click(object sender, RoutedEventArgs e) {
            codeInput_Text.Text = readedLinesOfCode.Pop();

            receivedCode_Text.Text = string.Empty;
            for (var i = 0; i < readedLinesOfCode.Count; i++) {
                receivedCode_Text.Text += String.Format("{0}\t{1}\n", i, readedLinesOfCode.ElementAt(i));
            }
        }

        private void reset_Button_Click(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void dice0_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "0";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "1";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "2";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "3";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "4";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "5";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "6";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "@";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "#";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }

        private void dice9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            codeInput_Text.Text += "^";
            codeInput_Text.CaretIndex = codeInput_Text.Text.Length;
        }
    }
}
