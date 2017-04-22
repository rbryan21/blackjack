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
        private Boolean isDealer;

        public Player()
        {

        }

        public Player(Hand hand, Boolean isDealer)
        {
            this.hand = hand;
            this.isDealer = isDealer;
            if (isDealer)
            {
                money = 10000;
            }
            else
            {
                money = 1000;
            }
        }

        public int GetMoney()
        {
            return money;
        }

        public int GetBet()
        {
            return bet;
        }

        public Boolean IsDealer()
        {
            return isDealer;
        }

        public void SetDealer(Boolean isDealer)
        {
            this.isDealer = isDealer;
        }

        public Hand Hand
        {
            get
            {
                return hand;
            }
        }

        public void SetMoney(int money)
        {
            this.money = money;
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

        public void DecreaseMoney(int money)
        {
            this.money -= money;
        }

        public void PlayerWin()
        {
            this.money *= 2;
        }

        public void PlayerLose()
        {
            this.money -= this.bet;
        }

    }
}
