using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    class Program
    {
        private static List<int> randomDigits;
        private static int guessesRemaining = 10;
        private static bool winFlag = false;
        static void Main(string[] args)
        {
            randomDigits = GenerateFourDigitRandomNumber();
            Console.WriteLine("Guess the 4-digit Random Number Challenge");
            Console.WriteLine("You have " + guessesRemaining + " guesses remaining!");
            while(guessesRemaining != 0)
            {
                Console.Write("Enter a 4 digit combination (digits in range 1 to 6) Eg:- 1234: ");
                string userCombination = Console.ReadLine();
                List<int> userCombinationList;
                while (!IsValidCombination(userCombination))
                {
                    Console.Write("Enter a 4 digit combination (digits in range 1 to 6) Eg:- 1234: ");
                    userCombination = Console.ReadLine();
                }
                userCombinationList = userCombination.Select(i => Int32.Parse(i.ToString())).ToList<int>();
                guessesRemaining--;
                CalculatePredictionResults(userCombinationList);
                if (winFlag)
                {
                    Console.WriteLine("You won! You guessed it right!");
                    Console.WriteLine("Press any key to exit!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                
            }
            Console.WriteLine("You lose!");
            Console.WriteLine("The correct number was " + String.Join("", randomDigits.Select(i => i.ToString()).ToArray()));
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
        public static List<int> GenerateFourDigitRandomNumber()
        {
            List<int> randomNums = new List<int>();
            Random rand = new Random();
            for(int i=0;i<4;i++)
            {
                randomNums.Add(rand.Next(6) + 1);
            }
            return randomNums;
        }
        public static bool IsValidCombination(string entry)
        {
            bool isValidInt = Int32.TryParse(entry, out int num);
            if (num < 0)
            {
                Console.WriteLine("Combination cannot be negative!");
                return false;
            }
            else if (!isValidInt)
            {
                Console.WriteLine("Invalid Number!");
                return false;
            } else if (entry.Length != 4)
            {
                Console.WriteLine("Number needs to be exactly 4 digits!");
                return false;
            }
            return true;
        }
        public static void CalculatePredictionResults(List<int> userCombinationList)
        {
            int minusCount = 0;
            int plusCount = 0;
            List<int> randomDigitsLocal = new List<int>(randomDigits);
            for (int i = 0; i < userCombinationList.Count; i++)
            {
                if (randomDigits.Contains(userCombinationList[i]))
                {
                    if (randomDigits[i] == userCombinationList[i])
                    {
                        plusCount++;
                    }
                    else if (randomDigitsLocal.Contains(userCombinationList[i]))
                    {
                        minusCount++;
                    }
                    randomDigitsLocal.Remove(userCombinationList[i]);
                }
            }
            if(plusCount == 4)
            {
                winFlag = true;
                return;
            }
            for (; plusCount > 0; plusCount--)
            {
                Console.Write('+');
            }
            for (; minusCount > 0; minusCount--)
            {
                Console.Write('-');
            }
            Console.WriteLine();
            Console.WriteLine("Incorrect! You have " + guessesRemaining + " guesses remaining!");
        }
    }
}
