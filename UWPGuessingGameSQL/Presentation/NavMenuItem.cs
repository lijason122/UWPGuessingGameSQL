using Windows.UI.Xaml.Controls;

namespace UWPGuessingGameSQL.Presentation
{
    /// <summary>
    /// Model to represent a ListViewItem in the ListView
    /// </summary>
    class NavMenuItem
    {

        public string Label { get; set; }
        public Symbol Symbol { get; set; }

        public char SymbolAsChar => (char)Symbol;


    }
}
