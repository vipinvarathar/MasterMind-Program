using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind
{
    class Program
    {
        private static List<int> randomDigits; // Variable which holds the randomly generated combination that user is trying to guess
        private static int guessesRemaining = 10; // Variable holds the number of guesses left
        private static bool winFlag = false; // Flag which indicates whether the user has found the right combination
        static void Main(string[] args)
        {
            randomDigits = GenerateFourDigitRandomNumber(); // Method generates and stores the randomly generated 4 digit number into the variable
            Console.WriteLine("Guess the 4-digit Random Number Challenge");
            Console.WriteLine("You have " + guessesRemaining + " guesses remaining!");
            while(guessesRemaining != 0)
            {
                Console.Write("Enter a 4 digit combination (digits in range 1 to 6) Eg:- 1234: ");
                string userCombination = Console.ReadLine(); // Reads user input for the combination and stores it
                List<int> userCombinationList;
                // While the combination is invalid, it gives error message and prompts user back for a valid combination of digits
                // No guesses are deducted for invalid guesses and no hints (pluses and minuses) are shown back to the user
                while (!IsValidCombination(userCombination)) 
                {
                    Console.Write("Enter a 4 digit combination (digits in range 1 to 6) Eg:- 1234: ");
                    userCombination = Console.ReadLine();
                }
                userCombinationList = userCombination.Select(i => Int32.Parse(i.ToString())).ToList<int>(); // Converts the user combination from int to List<int> type
                guessesRemaining--; // User has one less chance to guess
                CalculatePredictionResults(userCombinationList); // Calculates the hints (pluses & minuses) shown to user and determines whether the user has won
                
                if (winFlag) 
                {
                    // If user has found the right combination
                    Console.WriteLine("You won! You guessed it right!");
                    Console.WriteLine("Press any key to exit!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                
            }
            // User used up all the guesses they have and thus they have lost
            Console.WriteLine("You lose!");
            Console.WriteLine("The correct number was " + String.Join("", randomDigits.Select(i => i.ToString()).ToArray()));
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
        public static List<int> GenerateFourDigitRandomNumber()
        {
            List<int> randomNums = new List<int>(); // Local variable holds the randomly generated combination to return
            Random rand = new Random();
            for(int i=0;i<4;i++)
            {
                randomNums.Add(rand.Next(6) + 1); // Generates and adds a number from 1 (inclusive) to 6 (inclusive) once for each iteration
            }
            return randomNums;
        }
        public static bool IsValidCombination(string entry) // Checks whether the user input for the combination is valid; Returns boolean
        {
            bool isValidInt = Int32.TryParse(entry, out int num); // Variable holds the results to check whether the input is parsable and outputs to 'num' variable
            if (num < 0)
            {
                // User entered a negative combination
                Console.WriteLine("Combination cannot be negative!");
                return false;
            }
            else if (!isValidInt)
            {
                // User entered a string or invalid number
                Console.WriteLine("Invalid Number!");
                return false;
            } else if (entry.Length != 4)
            {
                Console.WriteLine("Number needs to be exactly 4 digits!");
                return false;
            }

            int digit; // Holds the digit value for each digit entered by user
          
            foreach (char c in entry)
            {
                // Loops through each digit of user input to see if any digit is outside of the range of valid values (from 1 to 6)
                Int32.TryParse(c.ToString(), out digit);
                if ((digit > 6) || (digit < 1)) {
                    Console.WriteLine("Invalid combination! All digits need to be between 1 and 6!");
                    return false;
                }
            }
            return true;
        }
        public static void CalculatePredictionResults(List<int> userCombinationList)
        {
            int minusCount = 0; // Holds the count for the number of digits which are correct but in wrong position
            int plusCount = 0; // Holds the count for the number of digits which are correct and in correct position
            List<int> randomDigitsLocal = new List<int>(randomDigits); // Local copy of the original random number variable for temporary value modifications to prevent double-counting in case same digit is repeating multiple times in the combination
            for (int i = 0; i < userCombinationList.Count; i++)
            {
                // Loops through the digits first to determine the number of pluses and removes the digits from the local random number variable
                if (randomDigits[i] == userCombinationList[i])
                {
                    randomDigitsLocal.Remove(userCombinationList[i]);
                    plusCount++;
                }
            }
            for (int i = 0; i < userCombinationList.Count; i++)
            {
                // Loops through the digits once again to determine all digits in wrong position
                // Having two seperate for loops are required to ensure that no single digits are double-counted (to keep track of duplicate digits)
                if (randomDigitsLocal.Contains(userCombinationList[i]))
                {
                    minusCount++;
                }
                randomDigitsLocal.Remove(userCombinationList[i]); // Removes the matching combination from the local variable to prevent double-counting digits
            }
            if(plusCount == 4)
            {
                // If there are 4 pluses, then all digits are in correct position and thus user wins
                winFlag = true;
                return;
            }
            for (; plusCount > 0; plusCount--) // Printing all pluses (+) first
            {
                Console.Write('+');
            }
            for (; minusCount > 0; minusCount--) // Printing all minuses (-) second
            {
                Console.Write('-');
            }
            Console.WriteLine();
            // User still hasn't won so it prints out the number of guesses remaining
            Console.WriteLine("Incorrect! You have " + guessesRemaining + " guesses remaining!"); 
        }
    }
}
