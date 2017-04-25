using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack;
using NUnit.Framework;

namespace Tester
{

    class HandTester
    {
        private Hand hand;

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void TestOneCard()
        {
            List<Cards> cards = new List<Cards>();
            cards.Add(new Cards("S", 2));

            hand = new Hand(cards);

            Assert.That(hand.GetValue() == 2);
        }

        [Test]
        public void TestTwoCards()
        {
            List<Cards> cards = new List<Cards>();
            cards.Add(new Cards("S", 2));
            cards.Add(new Cards("H", 5));

            hand = new Hand(cards);

            Assert.That(hand.GetValue() == 7);
        }

        [Test]
        public void TestFaceCard()
        {
            List<Cards> cards = new List<Cards>();
            cards.Add(new Cards("H", 13));

            hand = new Hand(cards);

            Assert.That(hand.GetValue() == 10);
        }

        [Test]
        public void TestIsAce()
        {
            hand = new Hand();

            Assert.That(hand.IsAce(new Cards("H", 1)) == true);
        }

        [Test]
        public void TestIsAceHigh()
        {
            hand = new Hand(new List<Cards>
            {
                new Cards("H", 10),
                new Cards("H", 11),
                new Cards("H", 1)
            });

            Assert.That(hand.IsAceHigh() == false);
        }

        [Test]
        public void TestBust()
        {
            hand = new Hand(new List<Cards>
            {
                new Cards("H", 10),
                new Cards("H", 11),
                new Cards("H", 12)
            });

            Assert.That(hand.Bust() == true);
        }

        [Test]
        public void TestNoBust()
        {
            hand = new Hand(new List<Cards>
            {
                new Cards("H", 10)
            });

            Assert.That(hand.Bust() == false);
        }

        [Test]
        public void TestTakeHit()
        {
            hand = new Hand(new List<Cards>
            {
                new Cards("H", 10)
            });

            hand.TakeHit(new Cards("H", 2));
            Assert.That(hand.GetValue() == 12);
        }

        [Test]
        public void TestScoreWithHighAce()
        {
            hand = new Hand(new List<Cards>
            {
                new Cards("H", 10)
            });

            hand.TakeHit(new Cards("H", 1));
            Assert.That(hand.GetValue(true) == 21);
        }

        [Test]
        public void TestScoreWithLowAce()
        {
            hand = new Hand(new List<Cards>
            {
                new Cards("H", 10)
            });

            hand.TakeHit(new Cards("H", 1));
            Assert.That(hand.GetValue(false) == 11);
        }
    }
}
