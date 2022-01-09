using System;

namespace yahtzee
{
    class Program
    {
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Yahtzee!\n");
            
            int[] userRoll = { 0, 0, 0, 0, 0 };
            //First User Roll
            userRoll = DiceRoll(userRoll);
            DisplayResults(userRoll, "first");

            //Asks which dice the user wants to keep
            userRoll = KeepDice(userRoll);

            //Second User Roll
            userRoll = DiceRoll(userRoll);
            DisplayResults(userRoll, "second");

            userRoll = KeepDice(userRoll);

            //Third & Final User Roll
            userRoll = DiceRoll(userRoll);
            DisplayResults(userRoll, "third");

            int userScore = CalculateScore(userRoll);
            Console.WriteLine("Your score is {0}\n", userScore);

            int[] computerRollOne = { 0, 0, 0, 0, 0 };
            int[] computerRollTwo = { 0, 0, 0, 0, 0 };
            int[] computerRollThree = { 0, 0, 0, 0, 0 };

            computerRollOne = DiceRoll(computerRollOne);
            DisplayComputerResults(computerRollOne, "first");
            int computerScore = CalculateScore(computerRollOne);

            computerRollTwo = DiceRoll(computerRollTwo);
            DisplayComputerResults(computerRollTwo, "second");
            if (CalculateScore(computerRollTwo) > computerScore)
                computerScore = CalculateScore(computerRollTwo);

            computerRollThree = DiceRoll(computerRollThree);
            DisplayComputerResults(computerRollThree, "third");
            if (CalculateScore(computerRollThree) > computerScore)
                computerScore = CalculateScore(computerRollThree);

            Console.WriteLine("\nYour score was {0}", userScore);
            Console.WriteLine("The computer's score is {0}", computerScore);

            if (userScore >= computerScore)
            {
                Console.WriteLine("You won!");
            }
            else
            {
                Console.WriteLine("You lost!");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Rolls 5 dice on the first round, then rerolls only the discarded dice on subsequent runs
        /// </summary>
        /// <param name="diceResults"></param>
        /// <returns>An int array containing the results of the dice roll(s)</returns>
        public static int[] DiceRoll(int[] diceResults)
        {
            //Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                if (diceResults[i] == 0)
                {
                    diceResults[i] = rnd.Next(1, 7);
                }
            }
            return diceResults;
        }

        /// <summary>
        /// Asks the user which dice they want to keep and saves discarded dice as "0"
        /// </summary>
        /// <param name="diceResults"></param>
        /// <returns>Returns the user's roll values with zeros used for discarded dice</returns>
        public static int[] KeepDice(int[] diceResults)
        {
            int intToChar = 65;
            for (int i = 0; i < 5; i++)
            {
                string answer = "";
                while (answer.ToLower() != "no" && answer.ToLower() != "n"
                    && answer.ToLower() != "yes" && answer.ToLower() != "y")
                {
                    Console.Write("Would you like to keep result ({0}), yes (y) or no (n)? ", ((char)intToChar).ToString());
                    answer = Console.ReadLine();
                }
                if (answer.ToLower() == "no" || answer.ToLower() == "n")
                {
                    diceResults[i] = 0;
                }
                intToChar++;
            }
            Console.WriteLine();
            return diceResults;
        }

        /// <summary>
        /// Displays the results of the dice roll
        /// </summary>
        /// <param name="diceResults"></param>
        /// <param name="rollNumber"></param>
        public static void DisplayResults(int[] diceResults, string rollNumber)
        {
            Console.WriteLine("The results of your {0} roll are: ", rollNumber);
            int intToChar = 65;
            foreach (int roll in diceResults)
            {
                Console.Write("(" + ((char)intToChar).ToString() + ") ");
                Console.WriteLine(roll);
                intToChar++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the computer results of the dice roll more compactly
        /// </summary>
        /// <param name="diceResults"></param>
        /// <param name="rollNumber"></param>
        public static void DisplayComputerResults(int[] diceResults, string rollNumber)
        {
            Console.Write("The results of the computer's {0} roll are: ", rollNumber);
            int intToLet = 65;
            foreach (int roll in diceResults)
            {
                Console.Write(roll + ", ");
                intToLet++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Calculates the user's final score
        /// </summary>
        /// <param name="diceResults"></param>
        /// <returns>An int of the user's final score</returns>
        public static int CalculateScore(int[] diceResults)
        {
            //Calculate the score (total) for each possible dice value
            int[] score = { 0, 0, 0, 0, 0, 0 };
            int total = 0;
            //Iterate through final die results
            for (int dice = 0; dice < 5; dice++)
            {
                //Determines how many of each die face there were in final roll
                for (int diceValue = 0; diceValue < 6; diceValue++)
                {
                    if (diceResults[dice] == diceValue + 1)
                        score[diceValue]++;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (score[i] > total)
                    total = score[i];
            }
            return total;
        }
    }
}
