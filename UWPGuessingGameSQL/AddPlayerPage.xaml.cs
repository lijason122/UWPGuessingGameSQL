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
    public sealed partial class AddPlayerPage : Page
    {
        public AddPlayerPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets called when the user clicks on the Add button
        /// Display error message when the user enters an empty string or the same name that exists in the PlayerDB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnClickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                //  Create the database object     
                PlayerDBA dba = new PlayerDBA();

                // Initialize the info that the user has entered
                Player myPlayer = new Player(this.txtName.Text, 0, 0, 0);

                // Initialize the list item source to load the player information
                dba.InsertPlayer(((App.Current as App).connectionString), myPlayer);

            }
            catch (FormatException) // When the user enters an empty string value
            {
                // Display error message in a TextBlock
                var dialog = new MessageDialog("Error: Please enter a valid name");
                await dialog.ShowAsync();
            }
        }
        
    }
}
