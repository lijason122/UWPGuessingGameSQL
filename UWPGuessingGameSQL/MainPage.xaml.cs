using UWPGuessingGameSQL.Presentation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPGuessingGameSQL
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //1.1 Set up a navigation back button
            SystemNavigationManager navMgr = SystemNavigationManager.GetForCurrentView();

            //1.2 Associate the back button with a navigation event handler
            navMgr.BackRequested += OnNavigateBack;

            //1.3 Display the back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Visible;
        }
        /// <summary>
        /// Used to handle the back button functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigateBack(object sender, BackRequestedEventArgs e)
        {
            // Check to see if moving backward is allowed
            if (frmContent.CanGoBack)
            {
                // Go back
                frmContent.GoBack();

                // Signal that the event has been already handled
                e.Handled = true;
            }
        }

        /// <summary>
        /// Called when an item of the pane(ListView:lstPageNav) is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickItem(object sender, ItemClickEventArgs e)
        {

            // 1. Find the item that was clicked ( Item1/Item2/Item/...)
            NavMenuItem selectedItem = e.ClickedItem as NavMenuItem;


            // 2. Using the frame to navigate to the appropriate page
            if (selectedItem.Label.Equals("Start Game"))  // Go to Start Game Page
            {
                frmContent.Navigate(typeof(ScoreboardPage)); // NOTE: no data is being transferred
            }
            else if (selectedItem.Label.Equals("View Scoreboard"))// Go to View Scoreboard Page
            {
                // Load the page in a child frame
                frmContent.Navigate(typeof(ScoreboardPage));
            }
            else if (selectedItem.Label.Equals("Add Player"))// Go to Add Player Page
            {// Go to Page3
                {
                    frmContent.Navigate(typeof(AddPlayerPage));
                }

            }





        }
    }
}
