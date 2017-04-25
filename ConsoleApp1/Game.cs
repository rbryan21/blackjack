using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Game
    {
        private Cards cards;

        static void Main(string[] args)
        {
            Player player = new Player(false);
            Player dealer = new Player(true);
            Cards cards = new Cards();
            Game game = new Game();
            bool isPlaying = true;
            bool isPlayingBlackJack = false;
            bool playerBust = false;
            bool dealerBust = false;

            while (isPlaying)
            {
                isPlayingBlackJack = game.ValidateResponse("Welcome to BlackJack would you like to play?");

                //quit = response == -1 ? true : false;
                while (isPlayingBlackJack)
                {
                    Console.WriteLine(string.Format("Your current money is {0}", player.Money));
                    Console.WriteLine(string.Format("Dealer's curent money is {0}", dealer.Money));

                    player.SetBet(game.ValidateBet("\nPlease Enter a bet:"));

                    Console.WriteLine("Player's bet = " + player.Bet);

                    var deck = cards.ShuffledDeck;

                    var hands = game.InitialDeal();

                    player.SetHand(new Hand(new List<Cards>
                    {
                        hands[0],
                        hands[1]
                    }));

                    dealer.SetHand(new Hand(new List<Cards>
                    {
                        hands[2],
                        hands[3]
                    }));

                    game.PrintInitialHand(player, dealer);

                    bool isHitting = true;
                    while (isHitting)
                    {
                        game.PrintHand(player);

                        var hit = game.ValidateResponse("\nDo you want to hit?");

                        if (hit)
                        {
                            player.Hand.TakeHit(cards.Deal());

                            playerBust = player.Hand.Bust();
                            if (playerBust)
                            {
                                game.PrintBust(player);
                            }

                            isHitting = playerBust ? false : isHitting;
                        }
                        else
                        {
                            isHitting = false;
                        }
                    }

                    if (!playerBust)
                    {
                        while (dealer.Hand.GetValue() < 17)
                        {
                            dealer.Hand.TakeHit(cards.Deal());

                            game.PrintHand(dealer, true);
                        }

                        if (dealer.Hand.Bust())
                        {
                            dealerBust = true;

                            game.PrintBust(dealer, true);
                        }
                    }

                    if (dealerBust)
                    {
                        player.DoublePlayersMoney();
                        dealer.DecreaseMoney(player.Bet);
                    }
                    else if (playerBust)
                    {
                        player.DecreaseMoney(player.Bet);
                        dealer.IncreaseMoney(player.Bet);
                    }
                    else
                    {
                        int outcome = player.EvaluateHand(player, dealer);
                        if (outcome == 1)
                        {
                            Console.WriteLine("You have won!");
                        }
                        else if (outcome == -1)
                        {
                            Console.WriteLine("The dealer has won!");
                        }
                        else
                        {
                            Console.WriteLine("You and dealer pushed");
                        }
                    }

                    if (player.PlayerWin())
                    {
                        Console.WriteLine("You have doubled your money and won!");
                        break;
                    }

                    if (player.PlayerLose())
                    {
                        Console.WriteLine("You have no more money. You have lost.");
                        break;
                    }
                }
            }
        }

        public int ValidateBet(string question)
        {
            int response = 0;
            while (response == 0)
            {
                Console.WriteLine(question);
                response = ReadNumericInput();
            }
            return response;
        }

        public bool ValidateResponse(string question)
        {
            int response = 0;
            while (response == 0)
            {
                Console.WriteLine(question);
                response = ReadYesOrNoInput();
            }

            return response == 1 ? true : false;
        }

        public int ReadYesOrNoInput(string input = "")
        {
            input = string.IsNullOrEmpty(input) ? Console.ReadLine() : input;

            int response = 0;
            if (input.Equals("Yes") || input.Equals("yes") || input.Equals("Y") || input.Equals("y"))
            {
                response = 1;
            }
            else if (input.Equals("No") || input.Equals("no") || input.Equals("N") || input.Equals("n"))
            {
                response = -1;
            }

            return response;
        }

        public int ReadNumericInput(string input = "")
        {
            input = string.IsNullOrEmpty(input) ? Console.ReadLine() : input;
            int response = 0;
            try
            {
                response = int.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Incorrect input");
            }

            return response;
        }

        public List<Cards> InitialDeal()
        {
            cards = cards ?? new Cards();
            var hands = cards.DealMultiple(4);
            var temp = hands[1];
            hands.RemoveAt(1);
            hands.Insert(2, temp);

            return hands;
        }

        public void PrintInitialHand(Player player, Player dealer)
        {
            PrintHand(player);
            Console.WriteLine();
            Console.WriteLine("The dealer is showing the {0} of {1}", dealer.Hand.PlayerHand[1].Value, dealer.Hand.PlayerHand[1].Suit);
        }

        public void PrintHand(Player player, bool isDealer = false)
        {
            string firstLine = isDealer ? "The dealer's" : "Your";
            Console.WriteLine(string.Format("{0} current hand is: ", firstLine));
            foreach (var x in player.Hand.PlayerHand)
            {
                Console.WriteLine(string.Format("{0} of {1}", x.Value, x.Suit));
            }

            Console.WriteLine(string.Format("{0} total is {1}", firstLine, player.Hand.GetValue()));
        }

        public void PrintBust(Player player, bool isDealer = false)
        {
            string firstLine = isDealer ? "The dealer's" : "Your";
            Console.WriteLine(string.Format("{0} busted. {1} hand was ", firstLine, firstLine));
            foreach (var x in player.Hand.PlayerHand)
            {
                Console.WriteLine(string.Format("{0} of {1}", x.Value, x.Suit));
            }
            Console.WriteLine(string.Format("{0} total was {1}", firstLine, player.Hand.GetValue()));
        }
    }
}

