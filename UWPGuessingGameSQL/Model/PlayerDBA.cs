using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using Windows.UI.Popups;

namespace UWPGuessingGameSQL.Model
{
    class PlayerDBA
    {
        /// <summary>
        /// Get the list of players as an Observable List (Collection)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public ObservableCollection<Player> GetPlayers(string connectionString)
        {
            // Representing the Query String (SQL Query)
            string getPlayersQuery = "SELECT PlayerName, WinCount, LossCount, TieCount from players ORDER BY WinCount DESC";

            var players = new ObservableCollection<Player>();
            try
            {
                // Establish the connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open(); // Open the connection

                    // Check if the state is open
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        //Create a command
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            // Set the command's text property to set the query string
                            cmd.CommandText = getPlayersQuery;

                            // Get the information (SqlDataReader) and returns SqlDataReader object
                            SqlDataReader reader = cmd.ExecuteReader();

                            // Use a while loop to keep reading the information
                            while (reader.Read())
                            {
                                // Create a player object and modify the properties of the object
                                Player player = new Player(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));

                                // Add the modified object to the collection
                                players.Add(player);
                            }

                        }
                    }
                }

                return players;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }

            return null;

        }// End of GetPlayers method

        /// <summary>
        /// Inserts a player into the database
        /// </summary>
        /// <param name="connectionString"> connection information (i.e. database name)</param>
        /// <param name="play">The player object to be inserted into the database</param>
        public async void InsertPlayer(string connectionString, Player play)
        {
            // Parameterized SQL query
            string insertPlayerQuery = "INSERT players (PlayerName, WinCount, LossCount, TieCount)" +
                " VALUES (@playername, @wincount, @losscount, @tiecount)";

            try
            {
                // Establish a connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open connection
                    conn.Open();
                    // Ensure the connection is opened
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        // Create a Command object
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            // Set the text property of the command
                            cmd.CommandText = insertPlayerQuery;

                            // Set the parameters
                            cmd.Parameters.AddWithValue("@playername", play.Name);
                            cmd.Parameters.AddWithValue("@wincount", play.Wins);
                            cmd.Parameters.AddWithValue("@losscount", play.Losses);
                            cmd.Parameters.AddWithValue("@tiecount", play.Ties);

                            // Execute the query (method used to apply an update to the database)
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                // Display dialog box to the user to enter a different player name
                var dialog = new MessageDialog($"Exception: {play.Name} already exists in the Players table. Please add a different player.");
                await dialog.ShowAsync();
            }

        } // End of InsertPlayer method

        /// <summary>
        /// Update the Player from the database
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public async void UpdatePlayer(string connectionString, string name, int win, int loss, int tie)
        {
            string updatePlayerQuery = "";
            // Representing the Query String (SQL Query)
            if (loss == 0 && tie == 0)
            {
                updatePlayerQuery = $"UPDATE players SET WinCount = {win} WHERE PlayerName = '{name}'";
            }
            else if (win == 0 && tie == 0)
            {
                updatePlayerQuery = $"UPDATE players SET LossCount = {loss} WHERE PlayerName = '{name}'";
            }
            else if (win == 0 && loss == 0)
            {
                updatePlayerQuery = $"UPDATE players SET TieCount = {tie} WHERE PlayerName = '{name}'";
            }

            try
            {
                // Establish a connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open connection
                    conn.Open();
                    // Ensure the connection is opened
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        // Create a Command object
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            // Set the text property of the command
                            cmd.CommandText = updatePlayerQuery;

                            // Execute the query (method used to apply an update to the database)
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                // Display exception message to the user
                var dialog = new MessageDialog($"Exception: " + eSql.Message);
                await dialog.ShowAsync();
            }

        } // End of UpdatePlayer method

        /// <summary>
        /// Select specific player and get their score data
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int[] SelectPlayer(string connectionString, string name)
        {
            int[] score = new int[3];
            // Representing the Query String (SQL Query)
            string getPlayersQuery = $"SELECT WinCount, LossCount, TieCount from players WHERE PlayerName = '{name}'";

            try
            {
                // Establish the connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open(); // Open the connection

                    // Check if the state is open
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        //Create a command
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            // Set the command's text property to set the query string
                            cmd.CommandText = getPlayersQuery;

                            // Get the information (SqlDataReader) and returns SqlDataReader object
                            SqlDataReader reader = cmd.ExecuteReader();

                            // Use a while loop to keep reading the information
                            while (reader.Read())
                            {
                                // Add the score data to the array (win, loss, tie)
                                score[0] = reader.GetInt32(0);
                                score[1] = reader.GetInt32(1);
                                score[2] = reader.GetInt32(2);
                            }

                        }
                    }
                }
                return score;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }

            return null;

        } // End of SelectPlayer method

    }
}
