using BlackJack;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tester
{
    [TestFixture]
    public class CardTester
    {
        private Cards cards;
        private List<Cards> beforeShuffle;

        [SetUp]
        public void Init()
        {
            cards = new Cards();
        }

        [Test]
        public void TestDeckSize()
        {
            Assert.That(cards.Deck.Count == 52);
        }

        [Test]
        public void VerifyCardOrder()
        {
            int value = 1;
            var deck = cards.Deck;
            for (int i = 0; i < deck.Count; i++)
            {
                value = ResetValue(value);
                Assert.That(deck[i].Value == value++);
            }
        }

        [Test]
        public void VerifyMultipleDeals()
        {
            int value = 0;
            var cardList = cards.DealMultiple(100, false);

            for (int i = 0; i < cardList.Count; i++)
            {
                value = ResetValue(++value);
                Assert.That(cardList[i].Value == value);
            }
        }

        [Test]
        public void VerifyDeal()
        {
            var card = cards.Deal(false);
            Assert.That(card.Value == 1);
        }

        [Test]
        public void VerifyShuffle()
        {
            beforeShuffle = cards.CreateDeck();
            cards.Shuffle(true);
            var afterShuffle = cards.Deck;
            
            CollectionAssert.AreNotEquivalent(beforeShuffle, afterShuffle);
        }

        private int ResetValue(int value)
        {
            if (value == 14)
            {
                value = 1;
            }

            return value;
        }
    }
}
