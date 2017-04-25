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

            if (player.Hand.GetValue() > dealer.Hand.GetValue())
            {
                player.IncreaseMoney(player.Bet);
                dealer.DecreaseMoney(player.Bet);
                outcome = 1;
            }
            else if (player.Hand.GetValue() < dealer.Hand.GetValue())
            {
                player.DecreaseMoney(player.Bet);
                dealer.DecreaseMoney(player.Bet);
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
    }
}
