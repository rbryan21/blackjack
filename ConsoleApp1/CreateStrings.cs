using BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CreateStrings
    {
        private Player player;
        private Player dealer;

        public CreateStrings(Player player)
        {
            this.player = player;
        }

        public CreateStrings(Player player, Player dealer)
        {
            this.player = player;
            this.dealer = dealer;
        }

        public string CurrentMoney(bool isDealer = false)
        {
            return isDealer ? string.Format("The dealer's curent money is {0}", dealer.Money) : string.Format("Your current money is {0}", player.Money);
        }

        public string GetBet()
        {
            return "Please enter a bet:";
        }

        public string GetHit()
        {
            return "Do you want to hit?";
        }

        public string PlayerWin()
        {
            return "You have won!";
        }

        public string DealerWin()
        {
            return "The dealer has won!";
        }

        public string Push()
        {
            return "You and the dealer have pushed. No money won or lost";
        }

        public string PlayerGameWin()
        {
            return "You have doubled your money and won!";
        }

        public string PlayerGameLose()
        {
            return "You have no more money. You have lost";
        }

        public string GetCard(Cards card)
        {
            string suit;
            string value;

            if (card.Suit.Equals("H"))
            {
                suit = "Hearts";
            }
            else if (card.Suit.Equals("S"))
            {
                suit = "Spades";
            }
            else if (card.Suit.Equals("C"))
            {
                suit = "Clubs";
            }
            else if (card.Suit.Equals("D"))
            {
                suit = "Diamonds";
            }
            else
            {
                suit = "";
            }

            if (card.Value == 1)
            {
                value = "Ace";
            }
            else if (card.Value == 11)
            {
                value = "Jack";
            }
            else if (card.Value == 12)
            {
                value = "Queen";
            }
            else if (card.Value == 13)
            {
                value = "King";
            }
            else
            {
                value = card.Value.ToString();
            }

            return string.Format("{0} of {1}", value, suit);
        }

        public string DealerInitialHand()
        {
            return "The dealer is showing:";
        }

        public string HandFirstLine(bool isDealer = false)
        {
            string firstLine = isDealer ? "The dealer's" : "Your";

            return string.Format("{0} current hand is: ", firstLine);
        }

        public string GetTotal(bool hasAce, bool isDealer = false)
        {
            string line;
            if (hasAce)
            {
                line = isDealer ? string.Format("The dealer's total is {0} or {1}", player.Hand.GetValue(), player.Hand.GetValue(true)) :
                            string.Format("Your total is {0} or {1}", player.Hand.GetValue(), player.Hand.GetValue(true));
            }
            else
            {
                line = isDealer ? string.Format("The dealer's total is {0}", player.Hand.GetValue()) : string.Format("Your total is {0}", player.Hand.GetValue());
            }

            return line;
        }

        public string GetBust(Player player, bool isDealer = false)
        {
            return isDealer ? "The dealer has busted.  Their hand was:" : "You have busted. Your hand was:";
        }
    }
}
