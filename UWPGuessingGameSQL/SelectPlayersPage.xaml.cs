using UWPGuessingGameSQL.Model;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPGuessingGameSQL
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectPlayersPage : Page
    {
        public SelectPlayersPage()
        {
            this.InitializeComponent();
            PlayerDBA dba = new PlayerDBA();
            // Initialize the list item source to load the player information
            playerList.ItemsSource = dba.GetPlayers((App.Current as App).connectionString);
            playerList2.ItemsSource = playerList.ItemsSource;
        }

        /// <summary>
        /// Navigate to the game emulator page when the user clicks the next button and the players are properly selected.
        /// If not, display warning message to the user. 
        /// </summary>
        private async void OnClickNext(object sender, RoutedEventArgs e)
        {
            if (playerList.SelectedValue == null || playerList2.SelectedValue == null)
            {
                // Display dialog box to the user to select players
                var dialog = new MessageDialog($"Warning: Please select players.");
                await dialog.ShowAsync();
            }   
            else if (playerList.SelectedValue.Equals(playerList2.SelectedValue)) // Check if the user selects two identical players
            {
                // Display dialog box to the user to select a different player
                var dialog = new MessageDialog($"Warning: {playerList.SelectedValue} is selected twice. Please select a different player.");
                await dialog.ShowAsync();
            }
            else
            {
                string[] selectedPlayers = { playerList.SelectedValue.ToString() , playerList2.SelectedValue.ToString() };
                Frame.Navigate(typeof(GameEmulatorPage), selectedPlayers);
            }
        }
    }
}
