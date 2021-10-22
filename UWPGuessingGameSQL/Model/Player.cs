using System;

namespace UWPGuessingGameSQL.Model
{
    /// <summary>
    /// Represents a row in the players table
    /// </summary>
    class Player
    {
        //Field variables to represent the id, name, wins, losses and, ties of a player.
        #region FIELDS
        private int _id;
        private string _name;
        private int _wins;
        private int _losses;
        private int _ties;
        #endregion

        // Read-only properties for fields
        #region PROPERTIES
        public string Name { get; }
        public int Wins { get; }
        public int Losses { get; }
        public int Ties { get; }
        #endregion

        // Non-default constructor to initialize fields
        #region CONSTRUCTOR(S)
        public Player(string initName, int initWins, int initLosses, int initTies)
        {
            (Name, Wins, Losses, Ties) = (initName, initWins, initLosses, initTies);

            if (string.IsNullOrEmpty(Name))
            {
                // Throw an exception for empty string value
                throw new FormatException("Please enter a valid name.");
            }
        }

        #endregion

    }
}
