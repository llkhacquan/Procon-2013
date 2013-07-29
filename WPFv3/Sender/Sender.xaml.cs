using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Procon.Core;

namespace Procon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string message = "";
        private string chosenText = "";
        private int numberOfSentPackets = 0;
        private int startOfChosenText;
        private int lengthOfChosenText;
        private Packet[] currentPackets;
        private Stack<Packet> sentPackets = new Stack<Packet>();
        private Sentence sentence = new Sentence();

        private BitmapImage[] imagesOfDices;
        private BitmapImage imageOfSpace;

        private const string testMessage = "Ramen,_a_part_of_Japanese_food_culture,_has_been_spreading_throughout_the_world_at_a_tremendous_pace._This,_in_turn,_has_drawn_foreign_tourists_from_different_countries_to_visit_Japan_in_search_of_ramen._Economic_growth_in_ASEAN_countries_along_with_the_yen's_depreciation_in_particular,_have_led_to_a_steady_growth_of_tourism_from_Southeast_Asia._Additionally,_individual_travel_from_Europe_and_the_United_States_has_been_growing_by_leaps_and_bounds._Meanwhile,_requests_for_Islamic-law_and_vegetarian_menus_are_also_on_a_sharp_rise._Presently,_there_are_over_1,000_ramen_shops_overseas,_and_although_preparation_of_menus_for_Islamic_law-bound_and_vegetarian_diet_at_shops_in_each_country_is_both_commonplace_and_common-sense,_such_support_from_shops_in_Japan_has_been_little_to_none_thus_far._If_ramen_is_going_to_globalize_in_the_true_sense_of_the_concept,_it_is_essential_that_such_vegetarian_versions_be_made_available._Though_Shin-Yokohama_Raumen_Museum_had_responded_to_vegetarian_requests,_a_system_to_always_provide_vegetarian_dishes_has_been_put_into_place_beginning_on_Monday_July_1,_2013._We_continue_to_strengthen_our_support_for_the_improved_travel_experience_for_foreign_tourists._The_lack_of_Wi-Fi_accessibility_has_been_said_to_be_the_number-one_ranking_difficulty_for_tourists_to_Japan,_so_we've_set_up_a_Wi-Fi_(free_public-access_wireless_LAN_accessible_environment._As_stated_before,_the_economic_growth_of_ASEAN_has_brought_about_a_rapid_increase_in_tourism_from_the_likes_of_Thai,_Malaysian,_and_Indonesian_nationalities._With_nearly_1.6_billion_people,_Muslims_make_up_about_a_fourth_of_the_world's_population,_and_about_220_million,_or_14_of_that_figure_is_from_Malaysian,_and_Indonesian_nationals._An_aspect_of_Islam_is_the_taboo_against_consuming_certain_food_ingredients_like_pork_and_alcohol,_and_the_desire_for_halal_food_products_made_from_food_sources_in_environments_that_are_permissible_and_in_accordance_with_Islamic_law._gs1e.jpg_Nevertheless,_according_to_one_Indonesian_association_for_travel_agencies,_analysis_showed_that_70%_of_Muslims_in_Indonesia_are_liberal,_and_30%_are_strict,_and_that_liberal_Muslims_can_be_found_especially_in_large_metropolitan_areas_such_as_Jakarta._In_a_program_run_by_the_Ministry_of_Foreign_Affairs,_it_was_claimed_by_Japanese_travel_agencies_arranging_travel_for_Southeast_Asian_students_that_Indonesians_were_content_as_long_as_pork_wasn't_evident_outright_in_the_food_that_was_served.*Starting_on_Monday_July_1,_2013_is_Shin-Yokohama_Raumen_Museum's_Muslim-Friendly_menu,_with_dishes_that_exclude_alcohol,_pork_and_other_taboo_ingredients_not_approved_under_halal.";

        public MainWindow()
        {
            InitializeComponent();
            InitializeImages();

            message_TextBox.Text = testMessage;


            dices_WrapPanel.Width = (C.NUMBER_OF_DICES_PER_ROW * (C.DICE_SIZE) + 25);
        }

        private void InitializeComboBoxMode()
        {
            
            ComboBoxItem[] modes = new ComboBoxItem[C.NUMBER_OF_MODES];
            for (int i = 0; i < modes.Length; i++)
            {
                modes[i] = new ComboBoxItem()
                {
                    Content = String.Format("{0}-{1}", i, C.MODES[i].name)
                };
                mode_ComboBox.Items.Add(modes[i]);
            }
        }

        private void InitializeImages() {
            imagesOfDices = new BitmapImage[10];
            for (int i = 0; i < 10; i++) {
                string path = String.Format("pack://application:,,,/Resources/{0}.png", i);
                imagesOfDices[i] = new BitmapImage(new Uri(path));
            }
            imageOfSpace = new BitmapImage(new Uri("pack://application:,,,/Resources/space.png"));
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            message = message_TextBox.Text;
            lengthOfMessage_TextBox.Text = message.Length.ToString();
            message_TextBox.IsReadOnly = true;
            message_TextBox.Background = SystemColors.ActiveBorderBrush;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void drawDices(string code)
        {
            Image image;
            dices_WrapPanel.Children.Clear();
            
            for (int i = 0; i < code.Length; i++)
            {
                // Insert space in a row
                if (i % C.BLOCK_OF_DICES_WIDTH == 0)
                {
                    image = new Image()
                    {
                        Source = imageOfSpace,
                        Height = C.DICE_SIZE,
                        Width = C.SPACE_SIZE
                    };

                    dices_WrapPanel.Children.Add(image);
                }

                if (i % (C.NUMBER_OF_DICES_PER_ROW * C.BLOCK_OF_DICES_HEIGHT) == 0)
                {
                        image = new Image()
                        {
                            Source = imageOfSpace,
                            Height = C.SPACE_SIZE,
                            Width = C.DICE_SIZE * C.NUMBER_OF_DICES_PER_ROW + 25
                        };
                        dices_WrapPanel.Children.Add(image);                    
                }

                int index = int.Parse(code[i].ToString());
                image = new Image()
                {
                    Source = imagesOfDices[index],
                    Height = C.DICE_SIZE,
                    Width = C.DICE_SIZE
                };


                dices_WrapPanel.Children.Add(image);
            }
            
        }

        private void createPacket_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mode_ComboBox.Items.Count==0)
                InitializeComboBoxMode();

            //message_TextBox.
            currentPackets = new Packet[C.NUMBER_OF_MODES];
            for (int i = 0; i < C.NUMBER_OF_MODES; i++)
            {
                try
                {
                    currentPackets[i] = new Packet(startOfChosenText, Mode.getMode(i), chosenText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            // Change lengthOfPackets TextBox
            lengthOfPackets.Text = "";
            for (int i = 0; i < C.NUMBER_OF_MODES; i++)
            {
                int temp = currentPackets[i].codeOfPacketWithChecksum.Length;
                lengthOfPackets.Text += temp + " ";
            }

            // Choose the shortest packets
            {
                int min = 0;
                for (int i = 1; i < C.NUMBER_OF_MODES; i++)
                {
                    if (currentPackets[min].codeOfPacketWithChecksum.Length > currentPackets[i].codeOfPacketWithChecksum.Length)
                    {
                        min = i;
                    }
                }
                mode_ComboBox.SelectedIndex = min;
            }
        }

        private void chosenTextChanged(object sender, RoutedEventArgs e)
        {
            chosenText = message_TextBox.SelectedText;
            if (chosenText_TextBox != null)
                chosenText_TextBox.Text = chosenText;
            startOfChosenText = message_TextBox.SelectionStart;
            if (startOfChosenText_TextBox !=null)
                startOfChosenText_TextBox.Text = startOfChosenText.ToString();
            lengthOfChosenText = message_TextBox.SelectionLength;
            if (lengthOfChosenText_TextBox != null)
                lengthOfChosenText_TextBox.Text = lengthOfChosenText.ToString();
        }

        private void viewDices_Button_Click(object sender, RoutedEventArgs e)
        {

            showCode(currentPackets[mode_ComboBox.SelectedIndex].codeOfPacketWithChecksum);
            drawDices(currentPackets[mode_ComboBox.SelectedIndex].codeOfPacketWithChecksum);
            sendPacket_Button.IsEnabled = true;
        }

        private void showCode(string code)
        {
            codeViewer.Text = "";

            for (int i = 0; i < code.Length; i++)
            {
                if (i % C.BLOCK_OF_DICES_WIDTH == 0)
                {
                    codeViewer.Text += " ";
                }

                if (i % (C.NUMBER_OF_DICES_PER_ROW) == 0)
                {
                    codeViewer.Text += "\n";
                }
                codeViewer.Text += code[i];
            }
            codeViewer.Text = codeViewer.Text.Replace("0", "_");
        }

        private void sendPacket_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sentence.setPacket(currentPackets[mode_ComboBox.SelectedIndex]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sentPackets.Push(currentPackets[mode_ComboBox.SelectedIndex]);
            
            sentMessage_TextBox.Text = sentence.getSentence();
            //removePacket_Button.IsEnabled = true;

            numberOfSentPackets++;
            numberOfSentPackets_Text.Text = numberOfSentPackets.ToString();
        }

        private void mode_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sendPacket_Button.IsEnabled = false;
            viewDices_Button.IsEnabled = true;
        }

        private void removePacket_Button_Click(object sender, RoutedEventArgs e)
        {
            sentPackets.Pop();

            sentence = new Sentence();
            foreach (Packet p in sentPackets)
            {
                sentence.setPacket(p);
            }

            sentMessage_TextBox.Text = sentence.getSentence();

            removePacket_Button.IsEnabled = false;

            numberOfSentPackets--;
            numberOfSentPackets_Text.Text = numberOfSentPackets.ToString();
        }
    }
}
