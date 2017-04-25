using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Hand
    {
        private List<Cards> hand;
        private int max = 21;

        public Hand()
        {
        }

        public Hand(List<Cards> cards)
        {
            this.hand = cards;
        }

        public List<Cards> PlayerHand => hand;

        public int GetValue(bool high = false)
        {
            int value = 0;

            foreach (var card in PlayerHand)
            {
                switch (card.Value)
                {
                    case 1:
                        value = high ? value + 11 : value + 1;
                        break;
                    case 11:
                    case 12:
                    case 13:
                        value += 10;
                        break;
                    default:
                        value += card.Value;
                        break;
                }
            }

            return value;
        }

        public bool IsAce(Cards card)
        {
            return card.Value == 1 ? true : false;
        }

        public bool IsAceHigh()
        {
            return Bust() ? true : false;
        }
        
        public bool Bust()
        {
            return GetValue() > 21 ? true : false;
        }

        public void TakeHit(Cards card)
        {
            hand.Add(card);
        }
    }
}
