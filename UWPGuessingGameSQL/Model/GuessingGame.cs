using System;


namespace UWPGuessingGameSQL.Model
{

    #region STRUCT(S)
    public struct GuessRange
    {
        private int _lowerBound;
        private int _upperBound;

        #region CONSTRUCTOR
        public GuessRange(int lower, int upper)
        {
            this._lowerBound = lower;
            this._upperBound = upper;
        }

        #endregion

        #region PROPERTIES

        public int LowerBound
        {

            get
            {
                return _lowerBound;
            }

            set
            {
                _lowerBound = value;
            }

        }

        public int UpperBound
        {

            get
            {
                return _upperBound;
            }

            set
            {
                _upperBound = value;
            }
        }



        #endregion

    }
    #endregion




    #region ENUM(S)
    /// <summary>
    /// Represents the values too low, too high , and just right for the hint
    /// </summary>
    public enum GuessHint
    {
        TooLow = -1,
        JustRight,
        TooHigh
    }





    #endregion



    // Represents the business logic of the program, the simple game of generating a random 
    // number between a lower and upper limit and checking guess against that number

    class GuessingGame
    {

        #region CONSTANTS
        /// <summary>
        /// Represents the default lower bound value of 0
        /// </summary>
        public const int DEFAULT_LOWER_BOUND = 0;

        /// <summary>
        /// Represents the default upper bound value of 10
        /// </summary>
        public const int DEFAULT_UPPER_BOUND = 10;

        #endregion





        // The bounds for allowed guesses
        private GuessRange _bound;


        // The number the game is generating and that is supposed to be guessed

        private int _numberToGuess;

        // A random generator that is used to generate the number to guess

        private Random _randomGen;


        #region CONSTRUCTORS

        public GuessingGame() : this(new GuessRange(DEFAULT_LOWER_BOUND, DEFAULT_UPPER_BOUND))
        {

        }


        /// <summary>
        /// Constructor of the class that forces clients to provide lower and upper bounds. As soon as
        ///the constructor is called and the game is thus created the number to guess has been generated        /// </summary>
        /// <param name="lowerBound">the lower bound for acceptable guesses</param>
        /// <param name="upperBound">the upper bound for acceptable guesses</param>
        public GuessingGame(GuessRange bound)
        {
            //initialize the lower and upper bounds
            _bound = bound;


            //generate a random integer between the two bounds
            _randomGen = new Random();
            _numberToGuess = _randomGen.Next(_bound.LowerBound, (_bound.UpperBound + 1));
        }

        #endregion
        #region PROPERTIES

        public int NumberToGuess
        {
            get
            {
                return this._numberToGuess;
            }
        }

        #endregion
        /// <summary>
        /// Checks a given guess against the number to guess as established by the game
        /// </summary>
        /// <param name="guess">the guess to be checked returns</param>
        /// <returns>
        /// +1 if the given guess is too high
        /// -1 if the given guess is too low
        /// 0 if the given guess is equal to the number to guess 
        /// </returns>
        public bool CheckGuess(int guess, out GuessHint hint)
        {
            hint = GuessHint.JustRight;

            if (guess == _numberToGuess)// correct guess
            {
                hint = GuessHint.JustRight;
                return true;
            }
            else  // incorrect guess
            {

                if (guess < _numberToGuess)
                {
                    //too low
                    hint = GuessHint.TooLow;

                }
                else if (guess > _numberToGuess) // NOTE: equal was not needed
                {
                    //too high
                    hint = GuessHint.TooHigh;

                }
            }


            return false;

        }

    }
}
