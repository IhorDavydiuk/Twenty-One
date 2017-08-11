using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card_game_21
{

    enum PlayingCard
    {
        Jack = 2,
        Lady,
        King,
        Six = 6,
        Seven,
        Eight,
        Nine,
        Ten,
        Ace

    }
    enum TypeCard
    {
        Diamonds,
        Spades,
        Hearts,
        Clubs
    }
    struct Card
    {
        public TypeCard TypeC;
        public PlayingCard ValueC;

        public Card(PlayingCard ValueC, TypeCard TypeC)
        {
            this.ValueC = ValueC;
            this.TypeC = TypeC;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Card[] DeckCards = new Card[36];

            int numberDC = 0;
            for (TypeCard i = TypeCard.Diamonds; i <= TypeCard.Clubs; i++)
            {
                for (PlayingCard j = PlayingCard.Jack; j <= PlayingCard.Ace; j++)
                {
                    if ((int)j == 5) continue;
                    DeckCards[numberDC] = new Card(j, i);
                    numberDC++;
                }
            }

            Random rndNumber = new Random();

            Console.Write("=========== GAME TWENTY-ONE ===========");

            //  THE GAME OF THE PLAYER

            Console.WriteLine(@"
Who first receives cards? You = 1; 
                          Opponent = 2;");

            int firstPlayerInt;
            bool firstPlayer = true;

            for (int i = 0; i < 10; i++)
            {

                bool firstPlayerBool = int.TryParse(Console.ReadLine(), out firstPlayerInt);
                if (firstPlayerBool && firstPlayerInt == 1)
                {
                    firstPlayer = true;
                    break;
                }
                else if (firstPlayerBool && firstPlayerInt == 2)
                {
                    firstPlayer = false;
                    break;
                }
                else
                {
                    Console.Write("Incorrect.Enter the value again '1' or '2'.Enter: ");
                }
            }

            char continueGameChar = 'y';
            bool continueGameBool = true;
            int victoryPlayer = 0, victoryComputer = 0;
            do
            {
                Console.WriteLine(@"
  =========================================================
||_________________________VICTORY_________________________||
||        YOU                               COMPUTER       ||
||         {0}                                    {1}          ||
  ========================================================="
, victoryPlayer, victoryComputer);

                for (int i = 0; i < DeckCards.Length; i++)
                {
                    int rndCard = rndNumber.Next(DeckCards.Length);
                    Card temp = DeckCards[i];
                    DeckCards[i] = DeckCards[rndCard];
                    DeckCards[rndCard] = temp;
                }

                int scorePlayer = 0, scoreComputer = 0;
                int numberCard = 0;

                for (int z = 0; z < 2; z++)
                {
                    if (firstPlayer)
                    {
                        Console.WriteLine(" ============================");
                        Console.WriteLine("|\t     YOU");
                        Console.WriteLine("|============================");
                        for (int i = 0; i < 2; i++)
                        {
                            Console.WriteLine("| " + DeckCards[numberCard].ValueC + " " + DeckCards[numberCard].TypeC);
                            Console.WriteLine("|----------------------------");
                            scorePlayer += (int)DeckCards[numberCard].ValueC;
                            numberCard++;
                        }
                        while (numberCard < DeckCards.Length)
                        {
                            if (scorePlayer <= 21)
                            {
                                Console.Write("|\tNext('y' or 'n')? ");
                                char answerChar;
                                bool answerBool = char.TryParse(Console.ReadLine(), out answerChar);
                                Console.WriteLine("|----------------------------");
                                if (scorePlayer <= 21 && (answerChar == 'y' || answerChar == 'Y') && answerBool)
                                {
                                    Console.WriteLine("| " + DeckCards[numberCard].ValueC + " " + DeckCards[numberCard].TypeC);
                                    Console.WriteLine("|----------------------------");
                                    scorePlayer += (int)DeckCards[numberCard].ValueC;
                                    numberCard++;
                                }

                                else if (scorePlayer <= 21 && (answerChar == 'n' || answerChar == 'N') && answerBool)
                                {
                                    Console.WriteLine("|\t\t    |Enough");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("|Enter 'y' or 'n' ");
                                    Console.WriteLine("|----------------------------");
                                }
                            }
                            else
                            {
                                Console.WriteLine("|\t\t    |More");
                                break;
                            }
                        }
                        firstPlayer = !firstPlayer;
                        Console.WriteLine("|\t\t    |Sum: " + scorePlayer);
                        Console.WriteLine(" ============================");
                    }
                    else
                    {
                        Console.WriteLine(" \t\t\t\t============================ ");
                        Console.WriteLine(" \t\t\t\t\t COMPUTER\t    |");
                        Console.WriteLine(" \t\t\t\t============================|");
                        for (int i = 0; i < 2; i++)
                        {
                            Console.WriteLine(" \t\t\t\t {0} {1}", DeckCards[numberCard].ValueC, DeckCards[numberCard].TypeC);
                            Console.WriteLine(" \t\t\t\t----------------------------|");
                            scoreComputer += (int)DeckCards[numberCard].ValueC;
                            numberCard++;
                        }
                        while (numberCard < DeckCards.Length)
                        {
                            if (scoreComputer <= 21)
                            {
                                bool answerBool = true;
                                if (scoreComputer >= 18)
                                {
                                    answerBool = false;
                                }
                                else if (scoreComputer >= 15 && scoreComputer < 18)
                                {
                                    if (rndNumber.Next(10) % 2 == 0)
                                    {
                                        answerBool = true;
                                    }
                                    else answerBool = false;
                                }
                                else answerBool = true;

                                if (answerBool)
                                {
                                    Console.WriteLine(" \t\t\t\t\t Next? y\t    |");
                                    Console.WriteLine(" \t\t\t\t----------------------------|");
                                    Console.WriteLine(" \t\t\t\t " + DeckCards[numberCard].ValueC + " " + DeckCards[numberCard].TypeC);
                                    Console.WriteLine(" \t\t\t\t----------------------------|");
                                    scoreComputer += (int)DeckCards[numberCard].ValueC;
                                    numberCard++;
                                }

                                else
                                {
                                    Console.WriteLine(" \t\t\t\t\t Next? n\t    |");
                                    Console.WriteLine(" \t\t\t\t----------------------------|");
                                    Console.WriteLine("\t\t\t\t\t\t   |Enough  |");
                                    break;
                                }

                            }
                            else
                            {
                                Console.WriteLine(" \t\t\t\t\t Next? y\t    |");
                                Console.WriteLine(" \t\t\t\t----------------------------|");
                                Console.WriteLine("\t\t\t\t\t\t   |More    |");
                                break;
                            }
                        }

                        firstPlayer = !firstPlayer;
                        Console.WriteLine("\t\t\t\t\t\t   |Sum: " + scoreComputer + " |");
                        Console.WriteLine(" \t\t\t\t============================");
                    }
                }
                if (scorePlayer <= 21 && scoreComputer <= 21)
                {
                    if (scorePlayer > scoreComputer)
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU WIN !!!                    ||
||                                                         ||
  =========================================================");
                        victoryPlayer += 1;
                    }
                    else if (scorePlayer < scoreComputer)
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU LOSE !!!                   ||
||                                                         ||
  =========================================================");
                        victoryComputer += 1;
                    }
                    else
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                         EQUALLY !!                      ||
||                                                         ||
  =========================================================");
                    }
                }
                else if (scorePlayer <= 21 && scoreComputer > 21)
                {
                    Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU WIN !!!                    ||
||                                                         ||
  =========================================================");
                    victoryPlayer += 1;
                }
                else if (scorePlayer > 21 && scoreComputer <= 21)
                {
                    Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU LOSE !!!                   ||
||                                                         ||
  =========================================================");
                    victoryComputer += 1;
                }
                else if (scorePlayer > 21 && scoreComputer > 21)
                {
                    if (scorePlayer > scoreComputer)
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU LOSE !!!                   ||
||                                                         ||
  =========================================================");
                        victoryComputer += 1;
                    }
                    else if (scorePlayer < scoreComputer)
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU WIN !!!                    ||
||                                                         ||
  =========================================================");
                        victoryPlayer += 1;
                    }
                    else
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                         EQUALLY !!                      ||
||                                                         ||
  =========================================================");
                    }

                }

                Console.Write("Do you want to continue?Enter y/n:");
                for (int i = 0; i < 100; i++)
                {

                    continueGameBool = char.TryParse(Console.ReadLine(), out continueGameChar);
                    if (continueGameBool && (continueGameChar == 'y' || continueGameChar == 'Y'))
                    {
                        break;
                    }
                    else if (continueGameBool && (continueGameChar == 'n' || continueGameChar == 'N'))
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("Incorrect.Enter the value again 'y' or 'n'.Enter: ");
                    }
                }
            }
            while (continueGameBool && (continueGameChar == 'y' || continueGameChar == 'Y'));

            Console.WriteLine(@"
  =========================================================
||_________________________VICTORY_________________________||
||        YOU                               OPPONENT       ||
||         {0}                                    {1}          ||
  ========================================================="
, victoryPlayer, victoryComputer);
            Console.ReadLine();

        }
    }
}
