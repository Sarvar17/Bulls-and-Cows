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
                "and in this game you should guess four-digit secret number, in addition these digits are different \r\n" +
                "If the matching digits are in their right positions, they are <<bulls>>, \r\n" +
                "if in different positions, they are <<cows>>. So let's get started\r\n");

            List<int> listNumbers = ShuffledDigitsList();
            int[] shuffledNumbers = listNumbers.ToArray();

            int[] fourDigitNumber = new int[4];
            Array.Copy(shuffledNumbers, fourDigitNumber, 4);

            Console.WriteLine("Guess four-digit number");
            while (!Game(Console.ReadLine(), fourDigitNumber))
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

        private static bool Game(string guess, int[] answer)
        {
            /*метод чтобы: 
            - проверить правильное значение ввел пользователь или нет
            - сосчитать сколько коров и быков ввел пользователь и вывести ответ на экран*/

            char[] guessedNumber = guess.ToCharArray();
            int bullsCount = 0, cowsCount = 0;

            if (guessedNumber.Length != 4)
            {
                //ошибка
                Console.WriteLine("ERROR. Your guess isn't valid. Try again");
                return false;
            }

            for (int i = 0; i < 4; i++)
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
                    for (int n = 0; n < 4; n++)
                    {
                        if (currentGuess == answer[n])
                            cowsCount += 1;
                    }
                }
            }

            return PrintScore(bullsCount, cowsCount);
        }

        private static bool PrintScore(int bullsCount, int cowsCount)
        {
            //метод чтобы выыести сколько коров и быков пользователь угадал

            if (bullsCount == 4)
            {
                Console.WriteLine("Congratulations! You have won!");
                return true;
            }
            else if (cowsCount == 4)
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