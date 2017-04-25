using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Cards
    {
        private string suit;
        private int value;
        private List<Cards> deck;
        private static Random rng = new Random();

        public Cards()
        {
        }

        public Cards(string suit, int value)
        {
            this.suit = suit;
            this.value = value;
        }

        public int Value => value;

        public string Suit => suit;

        public List<Cards> Deck
        {
            get
            {
                deck = deck ?? CreateDeck();
                deck = deck.Count == 0 ? CreateDeck() : deck;
                return deck;
            }
        }

        public List<Cards> ShuffledDeck
        {
            get
            {
                if (deck == null)
                {
                    deck = CreateDeck();
                    Shuffle(true);
                }
                else if (deck.Count == 0)
                {
                    deck = CreateDeck();
                    Shuffle(true);
                }

                return deck;
            }
        }

        public List<Cards> CreateDeck()
        {
            var cards = new List<Cards>
            {
                new Cards("S", 1),
                new Cards("S", 2),
                new Cards("S", 3),
                new Cards("S", 4),
                new Cards("S", 5),
                new Cards("S", 6),
                new Cards("S", 7),
                new Cards("S", 8),
                new Cards("S", 9),
                new Cards("S", 10),
                new Cards("S", 11),
                new Cards("S", 12),
                new Cards("S", 13),
                new Cards("D", 1),
                new Cards("D", 2),
                new Cards("D", 3),
                new Cards("D", 4),
                new Cards("D", 5),
                new Cards("D", 6),
                new Cards("D", 7),
                new Cards("D", 8),
                new Cards("D", 9),
                new Cards("D", 10),
                new Cards("D", 11),
                new Cards("D", 12),
                new Cards("D", 13),
                new Cards("H", 1),
                new Cards("H", 2),
                new Cards("H", 3),
                new Cards("H", 4),
                new Cards("H", 5),
                new Cards("H", 6),
                new Cards("H", 7),
                new Cards("H", 8),
                new Cards("H", 9),
                new Cards("H", 10),
                new Cards("H", 11),
                new Cards("H", 12),
                new Cards("H", 13),
                new Cards("C", 1),
                new Cards("C", 2),
                new Cards("C", 3),
                new Cards("C", 4),
                new Cards("C", 5),
                new Cards("C", 6),
                new Cards("C", 7),
                new Cards("C", 8),
                new Cards("C", 9),
                new Cards("C", 10),
                new Cards("C", 11),
                new Cards("C", 12),
                new Cards("C", 13),
            };

            return cards;
        }

        public List<Cards> DealMultiple(int times, bool shuffled = true)
        {
            var cards = new List<Cards>();

            for (int i = 0; i < times; i++)
            {
                cards.Add(Deal(shuffled));
            }

            return cards;
        }

        public Cards Deal(bool shuffled = true)
        {
            var useDeck = shuffled ? ShuffledDeck : Deck;
            var card = useDeck.First();
            Deck.RemoveAt(0);
            return card;
        }

        public void Shuffle(bool shuffle)
        {
            if (shuffle)
            {
                int n = Deck.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    Cards value = deck[k];
                    deck[k] = deck[n];
                    deck[n] = value;
                }
            }
        }

        private bool IsZero(int count)
        {
            return count == 0;
        }
    }
}