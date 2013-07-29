using System;
using System.Collections.Generic;
using System.Linq;
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
        "Ramen,_a_part_of_Japanese_food_culture,_has_been_spreading_throughout_the_world_at_a_tremendous_pace._This,_in_turn,_has_drawn_foreign_tourists_from_different_countries_to_visit_Japan_in_search_of_ramen._Economic_growth_in_ASEAN_countries_along_with_the_yen's_depreciation_in_particular,_have_led_to_a_steady_growth_of_tourism_from_Southeast_Asia._Additionally,_individual_travel_from_Europe_and_the_United_States_has_been_growing_by_leaps_and_bounds._Meanwhile,_requests_for_Islamic-law_and_vegetarian_menus_are_also_on_a_sharp_rise._Presently,_there_are_over_1,000_ramen_shops_overseas,_and_although_preparation_of_menus_for_Islamic_law-bound_and_vegetarian_diet_at_shops_in_each_country_is_both_commonplace_and_common-sense,_such_support_from_shops_in_Japan_has_been_little_to_none_thus_far._If_ramen_is_going_to_globalize_in_the_true_sense_of_the_concept,_it_is_essential_that_such_vegetarian_versions_be_made_available._Though_Shin-Yokohama_Raumen_Museum_had_responded_to_vegetarian_requests,_a_system_to_always_provide_vegetarian_dishes_has_been_put_into_place_beginning_on_Monday_July_1,_2013._We_continue_to_strengthen_our_support_for_the_improved_travel_experience_for_foreign_tourists._The_lack_of_Wi-Fi_accessibility_has_been_said_to_be_the_number-one_ranking_difficulty_for_tourists_to_Japan,_so_we've_set_up_a_Wi-Fi_(free_public-access_wireless_LAN_accessible_environment._As_stated_before,_the_economic_growth_of_ASEAN_has_brought_about_a_rapid_increase_in_tourism_from_the_likes_of_Thai,_Malaysian,_and_Indonesian_nationalities._With_nearly_1.6_billion_people,_Muslims_make_up_about_a_fourth_of_the_world's_population,_and_about_220_million,_or_14_of_that_figure_is_from_Malaysian,_and_Indonesian_nationals._An_aspect_of_Islam_is_the_taboo_against_consuming_certain_food_ingredients_like_pork_and_alcohol,_and_the_desire_for_halal_food_products_made_from_food_sources_in_environments_that_are_permissible_and_in_accordance_with_Islamic_law._gs1e.jpg_Nevertheless,_according_to_one_Indonesian_association_for_travel_agencies,_analysis_showed_that_70%_of_Muslims_in_Indonesia_are_liberal,_and_30%_are_strict,_and_that_liberal_Muslims_can_be_found_especially_in_large_metropolitan_areas_such_as_Jakarta._In_a_program_run_by_the_Ministry_of_Foreign_Affairs,_it_was_claimed_by_Japanese_travel_agencies_arranging_travel_for_Southeast_Asian_students_that_Indonesians_were_content_as_long_as_pork_wasn't_evident_outright_in_the_food_that_was_served.*Starting_on_Monday_July_1,_2013_is_Shin-Yokohama_Raumen_Museum's_Muslim-Friendly_menu,_with_dishes_that_exclude_alcohol,_pork_and_other_taboo_ingredients_not_approved_under_halal."
        };

       public MainWindow()
        {
            InitializeComponent();
        }

        private void zipButton_Click(object sender, RoutedEventArgs e)
        {
            output.Text = "Original \tSmaz \t .NET \t 7Zip";
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
            string temp = Smaz.Uncompress(Compressed);
            input.Text = String.Format("{0}\n{1}", temp, inputString);
            MessageBox.Show((temp == inputString).ToString());

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
