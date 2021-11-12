using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Heeyy!! It's a game called <<Bulls And Cows>> \r\n" +
                "and in this game you should guess secret number, in addition these digits are different \r\n" +
                "If the matching digits are in their right positions, they are <<bulls>>, \r\n" +
                "if in different positions, they are <<cows>>. So let's get started\r\n");

            List<int> listNumbers = ShuffledDigitsList();

            int[] shuffledNumbers = listNumbers.ToArray();

            int nDigit = InputNumberOfDigits();

            int[] nDigitNumber = new int[nDigit];
            Array.Copy(shuffledNumbers, nDigitNumber, nDigit);

            Console.WriteLine($"Guess {nDigit}-digit number");
            while (!Game(Console.ReadLine(), nDigitNumber, nDigit))
            {
                Console.WriteLine("Try again");
            }

            //цикл повторения игры
            Console.WriteLine("Press ENTER to exit Console, or other button to play again \r\n"); 
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Main(args);
            }
        }

        private static int InputNumberOfDigits()
        {
            //метод чтобы пользователь ввел число цифер, и еще этот метод проверяет что число 1-9

            int nDigit;
            string input;

            Console.WriteLine("Enter number of digits that you want to guess (it MUST be between 1 and 9)");

            do
            {
                input = Console.ReadLine();
            }
            while (!int.TryParse(input, out nDigit) || nDigit < 1 || nDigit > 9);
            return nDigit;
        }

        private static List<int> ShuffledDigitsList()
        {
            //метод чтобы перемешать числа с 1 до 9

            List<int> possible = Enumerable.Range(1, 9).ToList();
            List<int> listNumbers = new List<int>();
            Random random = new Random();

            for (int i = 0; i < 9; i++)
            {
                int index = random.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }

            return listNumbers;
        }

        private static bool Game(string guess, int[] answer, int n)
        {
            /*метод чтобы: 
            - проверить правильное значение ввел пользователь или нет
            - сосчитать сколько коров и быков ввел пользователь и вывести ответ на экран*/

            char[] guessedNumber = guess.ToCharArray();
            int bullsCount = 0, cowsCount = 0;

            if (guessedNumber.Length != n)
            {
                //ошибка
                Console.WriteLine("ERROR. Your guess isn't valid. Try again");
                return false;
            }

            for (int i = 0; i < n; i++)
            {
                int currentGuess = (int)char.GetNumericValue(guessedNumber[i]);
                if (currentGuess < 1 || currentGuess > 9)
                {
                    //ошибка
                    Console.WriteLine("ERROR. Your digits have to be between 1 and 9. Try again");
                    return false;
                }

                if (currentGuess == answer[i])
                    bullsCount += 1;
                else
                {
                    for (int a = 0; a < n; a++)
                    {
                        if (currentGuess == answer[a])
                            cowsCount += 1;
                    }
                }
            }

            return PrintScore(bullsCount, cowsCount, n);
        }

        private static bool PrintScore(int bullsCount, int cowsCount, int n)
        {
            //метод чтобы выыести сколько коров и быков пользователь угадал

            if (bullsCount == n)
            {
                Console.WriteLine("Congratulations! You have won!");
                return true;
            }
            else if (cowsCount == n)
            {
                Console.WriteLine("You find all needed digits, so you just need to order them in right way");
                return false;
            }
            else
            {
                Console.WriteLine($"Your Score is {bullsCount} bulls and {cowsCount} cows");
                return false;
            }
        }
    }
}