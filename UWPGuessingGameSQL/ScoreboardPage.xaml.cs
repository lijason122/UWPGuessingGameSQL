using UWPGuessingGameSQL.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPGuessingGameSQL
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScoreboardPage : Page
    {
        public ScoreboardPage()
        {
            this.InitializeComponent();
            PlayerDBA dba = new PlayerDBA();
            // Initialize the list item source to load the player information
            playerList.ItemsSource = dba.GetPlayers((App.Current as App).connectionString);
        }

        /// <summary>
        /// Navigate to the select players page when the user clicks the start button
        /// </summary>
        private void OnClickStart(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SelectPlayersPage));
        }
    }
}
