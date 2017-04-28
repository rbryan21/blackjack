using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Player
    {
        private Hand hand;
        private int money;
        private int bet;
        private bool isDealer;

        public Player()
        {
        }

        public Player(bool isDealer)
        {
            this.isDealer = isDealer;
            money = isDealer ? 10000 : 1000;
        }

        public int Money => money;

        public int Bet => bet;

        public bool IsDealer => isDealer;

        public Hand Hand => hand;

        public void SetHand(Hand hand)
        {
            this.hand = hand;
        }

        public void SetBet(int bet)
        {
            if (bet > money)
            {
                throw new BetGreaterThanMoneyException();
            } 
            if (bet <= 0)
            {
                throw new LessThanMinimumBetException();
            }

            this.bet = bet;
        }

        public void IncreaseMoney(int money)
        {
            this.money += money;
        }
        
        public void DecreaseMoney(int money)
        {
            this.money -= money;
        }

        public int EvaluateHand(Player player, Player dealer)
        {
            int outcome = 0;
            int playerTotal = player.hand.GetValue(true) > 21 ? player.Hand.GetValue() : player.Hand.GetValue(true);
            int dealerTotal = dealer.hand.GetValue(true) > 21 ? dealer.Hand.GetValue() : dealer.Hand.GetValue(true);

            if (playerTotal > dealerTotal)
            {
                player.IncreaseMoney(player.Bet);
                dealer.DecreaseMoney(player.Bet);
                outcome = 1;
            }
            else if (playerTotal < dealerTotal)
            {
                player.DecreaseMoney(player.Bet);
                dealer.IncreaseMoney(player.Bet);
                outcome = -1;
            }

            return outcome;
        }

        public bool PlayerWin()
        {
            bool win = false;

            if (IsDealer == false)
            {
                win = Money >= 2000 ? true : false;
            }

            return win;
        }

        public bool PlayerLose()
        {
            bool lose = false;

            if (IsDealer == false)
            {
                lose = Money <= 0 ? true : false;
            }

            return lose;
        }

        public void NoNegativePlayerMoney()
        {
            money = Money < 0 ? 0 : money;
        }

        public bool DealerHitting(Player dealer)
        {
            int value = dealer.Hand.GetValue();
            return dealer.Hand.GetValue() < 17 && dealer.Hand.GetValue(true) < 17;
        }
    }
}
