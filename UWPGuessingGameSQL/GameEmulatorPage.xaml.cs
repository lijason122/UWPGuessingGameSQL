using UWPGuessingGameSQL.Model;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPGuessingGameSQL
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameEmulatorPage : Page
    {
        private string[] selectedPlayers;
        PlayerDBA dba = new PlayerDBA();

        int roundCount = 1; // Keep track of rounds
        // Create a random object
        Random rndObject = new Random(); // represents a Random class object
        GuessingGame gameGuess = new GuessingGame(); // represents a Random value between

        public GameEmulatorPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Navigate to the scoreboard page when the user clicks the back button.
        /// </summary>
        private void OnClickBack(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ScoreboardPage));
        }

        /// <summary>
        /// Execute the guessing game when the user clicks the enter button after inputting the numbers to guess.
        /// </summary>
        private async void OnClickEnter(object sender, RoutedEventArgs e)
        {
            // Declarations & Initializations
            
            int userGuess; // represents the guess the user has made
            int userGuess2; // represents the guess the user2 has made
            
            try
            {
                
                userGuess = int.Parse(this.txtNumber.Text);
                userGuess2 = int.Parse(this.txtNumber2.Text);

                // Check the user guess against the game guess
                GuessHint guessHint;
                GuessHint guessHint2;
                if (gameGuess.CheckGuess(userGuess, out guessHint) || gameGuess.CheckGuess(userGuess2, out guessHint2))
                {
                    // Display correct guess message
                    if (gameGuess.CheckGuess(userGuess, out guessHint) && gameGuess.CheckGuess(userGuess2, out guessHint2))
                    {
                        var dialog = new MessageDialog($"CONGRATULATIONS!  Both {selectedPlayers[0]} and {selectedPlayers[1]} have guessed correctly");
                        await dialog.ShowAsync();
                        this.txtWinner.Text = "Tie, No Winners";

                        // Retrieve the win, loss, and tie count from specific user
                        int[] score = dba.SelectPlayer((App.Current as App).connectionString, selectedPlayers[0]);
                        dba.UpdatePlayer((App.Current as App).connectionString, selectedPlayers[0], 0, 0, ++score[2]);

                        int[] score2 = dba.SelectPlayer((App.Current as App).connectionString, selectedPlayers[1]);
                        dba.UpdatePlayer((App.Current as App).connectionString, selectedPlayers[1], 0, 0, ++score2[2]);

                        Frame.Navigate(typeof(ScoreboardPage));
                    }
                    else if (gameGuess.CheckGuess(userGuess, out guessHint))
                    {
                        var dialog = new MessageDialog($"CONGRATULATIONS!  {selectedPlayers[0]} have guessed correctly");
                        await dialog.ShowAsync();
                        this.txtWinner.Text = selectedPlayers[0];

                        // Retrieve the win, loss, and tie count from specific user
                        int[] score = dba.SelectPlayer((App.Current as App).connectionString, selectedPlayers[0]);
                        dba.UpdatePlayer(((App.Current as App).connectionString), selectedPlayers[0], ++score[0], 0, 0);

                        int[] score2 = dba.SelectPlayer((App.Current as App).connectionString, selectedPlayers[1]);
                        dba.UpdatePlayer(((App.Current as App).connectionString), selectedPlayers[1], 0, ++score2[1], 0);

                        Frame.Navigate(typeof(ScoreboardPage));
                    }
                    else if (gameGuess.CheckGuess(userGuess2, out guessHint2))
                    {
                        var dialog = new MessageDialog($"CONGRATULATIONS!  {selectedPlayers[1]} have guessed correctly");
                        await dialog.ShowAsync();
                        this.txtWinner.Text = selectedPlayers[1];

                        // Retrieve the win, loss, and tie count from specific user
                        int[] score = dba.SelectPlayer((App.Current as App).connectionString, selectedPlayers[1]);
                        dba.UpdatePlayer(((App.Current as App).connectionString), selectedPlayers[1], ++score[0], 0, 0);

                        int[] score2 = dba.SelectPlayer((App.Current as App).connectionString, selectedPlayers[0]);
                        dba.UpdatePlayer(((App.Current as App).connectionString), selectedPlayers[0], 0, ++score2[1], 0);

                        Frame.Navigate(typeof(ScoreboardPage));
                    }
                    
                    // Reset the textbox for users to re-enter numbers
                    this.txtNumber.Text = "";
                    this.txtNumber2.Text = "";
                    this.txtGuess.Text = gameGuess.NumberToGuess.ToString();

                    // Reset the round to start at 1
                    roundCount = 1;
                    // NOTE: setting the values are removed
                    gameGuess = new GuessingGame();
                }
                else if (guessHint == GuessHint.TooHigh)
                {
                    // Display wrong guess message
                    var dialog = new MessageDialog($"*WRONG GUESS* {selectedPlayers[0]}'s Guess is too high!");
                    await dialog.ShowAsync();
                    // Reset the textbox for users to re-enter numbers and update the round
                    roundCount++;
                    this.txtRound.Text = roundCount.ToString();
                    this.txtNumber.Text = "";
                    this.txtNumber2.Text = "";
                    this.txtWinner.Text = "";
                    this.txtGuess.Text = "";
                }
                else
                {
                    // Display wrong guess message
                    var dialog = new MessageDialog($"*WRONG GUESS* {selectedPlayers[0]}'s Guess is too low!");
                    await dialog.ShowAsync();
                    // Reset the textbox for users to re-enter numbers and update the round
                    roundCount++;
                    this.txtRound.Text = roundCount.ToString();
                    this.txtNumber.Text = "";
                    this.txtNumber2.Text = "";
                    this.txtWinner.Text = "";
                    this.txtGuess.Text = "";
                }

            }
            catch (FormatException) // When the user enters non-numerical values
            {
                // Display error message in a TextBlock
                var dialog = new MessageDialog("Error: Please enter a valid number");
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Retrieve players name from the select players page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            selectedPlayers = (string[])e.Parameter;

            Player player = new Player(selectedPlayers[0], 0, 0, 0);
            Player player2 = new Player(selectedPlayers[1], 0, 0, 0);
            var players = new ObservableCollection<Player>();
            players.Add(player);
            players.Add(player2);

            play.ItemsSource = players;
            
        }
    }
}
