# MasterMind-Program
This C# console application is a simple version of Mastermind game which can be run on Visual Studio. 
Import the project and run in Visual Studio
The main C# source code for the business logic of the application can be found in Program.cs file in the ~MasterMind-Program/Mastermind/ folder path.

Rules/Guidelines:
The application generates a random 4-digit number (each digit is from 1 (inclusive) to 6(inclusive)). User can guess a maximum of 10 times after which the game ends by printing "You Lose!". For each user-entered digit which is in the randomly generated digits and in the correct position as well, the application prints a plus (+) sign and for user-entered digits which are in the randomly generated digits but in the incorrect position, the application prints a minus (-) sign and prints nothing if the digit does not exist in the correct combination. All the plus (+) signs are printed first, followed by all the minus (-) signs. Example: If the randomly generated number is 4431, the program prints "----" if 1344 is entered because all the digits are correct but in the wrong position. If the number is guessed correctly, that means the result was "++++" and so on. 
