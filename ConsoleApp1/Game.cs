using ConsoleApp1;
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
        private CreateStrings create;

        static void Main(string[] args)
        {
            Cards cards = new Cards();
            Game game = new Game();
            bool isPlaying = true;
            bool isPlayingBlackJack = false;
            bool playerBust = false;
            bool dealerBust = false;

            while (isPlaying)
            {
                Player player = new Player(false);
                Player dealer = new Player(true);
                CreateStrings create = new CreateStrings(player, dealer);

                isPlayingBlackJack = game.ValidateResponse("Welcome to BlackJack would you like to play?");

                //quit = response == -1 ? true : false;
                while (isPlayingBlackJack)
                {
                    game.PrintBlankLine();
                    game.Print(create.CurrentMoney());
                    game.Print(create.CurrentMoney(true));

                    game.PrintBlankLine();
                    game.ValidateBet(player, create.GetBet());

                    var deck = cards.ShuffledDeck;
                    Console.WriteLine(deck.Count);

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
                    playerBust = false;
                    dealerBust = false;

                    while (isHitting)
                    {
                        game.PrintHand(player);

                        if (player.Hand.Is21())
                        {
                            break;
                        }

                        game.PrintBlankLine();
                        var hit = game.ValidateResponse(create.GetHit());

                        if (hit)
                        {
                            player.Hand.TakeHit(cards.Deal());

                            playerBust = player.Hand.Bust();
                            if (playerBust)
                            {
                                isHitting = false;
                                game.PrintBust(player);
                            }
                        }
                        else
                        {
                            isHitting = false;
                        }
                    }

                    if (!playerBust)
                    {
                        game.PrintHand(dealer, true);

                        while (dealer.DealerHitting(dealer))
                        {
                            dealer.Hand.TakeHit(cards.Deal());

                            if (!dealer.Hand.Bust())
                            {
                                game.PrintHand(dealer, true);
                            }
                            else
                            {
                                dealerBust = true;
                                game.PrintBust(dealer, true);
                                break;
                            }
                        }
                    }

                    game.PrintBlankLine();

                    if (dealerBust)
                    {
                        game.Print(create.PlayerWin());

                        player.IncreaseMoney(player.Bet);
                        dealer.DecreaseMoney(player.Bet);
                    }
                    else if (playerBust)
                    {
                        game.Print(create.DealerWin());

                        player.DecreaseMoney(player.Bet);
                        dealer.IncreaseMoney(player.Bet);
                    }
                    else
                    {
                        int outcome = player.EvaluateHand(player, dealer);
                        if (outcome == 1)
                        {
                            game.Print(create.PlayerWin());
                        }
                        else if (outcome == -1)
                        {
                            game.Print(create.DealerWin());
                        }
                        else
                        {
                            game.Print(create.Push());
                        }
                    }

                    game.PrintBlankLine();
                    if (player.PlayerWin())
                    {
                        game.Print(create.PlayerGameWin());
                    }

                    if (player.PlayerLose())
                    {
                        game.Print(create.PlayerGameLose());
                    }
                }
            }
        }

        private void Print(string text, bool blank = false)
        {
            if (blank)
            {
                PrintBlankLine();
            }

            Console.WriteLine(text);
        }

        private void PrintBlankLine()
        {
            Console.WriteLine();
        }

        public int ValidateBet(Player player, string question) 
        {
            int response = 0;
            while (response == 0)
            {
                try
                {
                    Console.WriteLine(question);
                    response = ReadNumericInput();
                    player.SetBet(response);
                }
                catch
                {
                    response = 0;
                }
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
            create = new CreateStrings(player, dealer);

            PrintBlankLine();
            Print(create.DealerInitialHand());
            Print(create.GetCard(dealer.Hand.PlayerHand[1]));
        }

        public void PrintHand(Player player, bool isDealer = false)
        {
            create = new CreateStrings(player);

            PrintBlankLine();
            Print(create.HandFirstLine(isDealer));

            bool hasAce = false;
            foreach (var x in player.Hand.PlayerHand)
            {
                if (x.IsAce() && player.Hand.IsAceHigh())
                {
                    hasAce = true;
                }

                Print(create.GetCard(x));
            }

            Print(create.GetTotal(hasAce, isDealer));
        }

        public void PrintBust(Player player, bool isDealer = false)
        {
            create = new CreateStrings(player);
            PrintBlankLine();
            Print(create.GetBust(player, isDealer));
            foreach (var x in player.Hand.PlayerHand)
            {
                Print(create.GetCard(x));
            }

            Print(create.GetTotal(false, isDealer));
        }
    }
}

