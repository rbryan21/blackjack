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
            hand = new Hand();
        }

        [Test]
        public void TestOneFaceUp()
        {
            List<Cards> faceUp = new List<Cards>();
            faceUp.Add(new Cards("S", 2));
            hand.SetFaceUp(faceUp);
            Assert.That(hand.GetTotalValue() == 2);
        }

        [Test]
        public void TestOneFaceDown()
        {
            List<Cards> faceDown = new List<Cards>();
            faceDown.Add(new Cards("S", 3));
            hand.SetFaceDown(faceDown);
            Assert.That(hand.GetTotalValue() == 3);
        }

        [Test]
        public void TestMultipleFaceUp()
        {
            List<Cards> faceUp = new List<Cards>();
            faceUp.Add(new Cards("S", 2));
            faceUp.Add(new Cards("H", 2));
            faceUp.Add(new Cards("D", 2));
            hand.SetFaceDown(faceUp);
            Assert.That(hand.GetTotalValue() == 6);
        }

        [Test]
        public void TestMultipleFaceDown()
        {
            List<Cards> faceDown = new List<Cards>();
            faceDown.Add(new Cards("S", 2));
            faceDown.Add(new Cards("H", 2));
            faceDown.Add(new Cards("D", 2));
            hand.SetFaceDown(faceDown);
            Assert.That(hand.GetTotalValue() == 6);
        }

        [Test]
        public void TestFaceDownAndFaceUp()
        {
            List<Cards> faceDown = new List<Cards>();
            faceDown.Add(new Cards("S", 2));
            faceDown.Add(new Cards("H", 2));
            List<Cards> faceUp = new List<Cards>();
            faceUp.Add(new Cards("C", 2));
            hand.SetFaceDown(faceDown);
            hand.SetFaceUp(faceUp);
            Assert.That(hand.GetTotalValue() == 6);
        }

        [Test]
        public void TestFaceCards()
        {
            List<Cards> faceDown = new List<Cards>();
            faceDown.Add(new Cards("S", 11));
            faceDown.Add(new Cards("H", 12));
            faceDown.Add(new Cards("D", 13));
            hand.SetFaceDown(faceDown);
            Assert.That(hand.GetTotalValue() == 30);
        }

        [Test]
        public void TestAceActingAsOne()
        {
            List<Cards> faceDown = new List<Cards>();
            faceDown.Add(new Cards("S", 2));
            faceDown.Add(new Cards("H", 9));
            faceDown.Add(new Cards("D", 9));
            faceDown.Add(new Cards("C", 1));
            hand.SetFaceDown(faceDown);
            Assert.That(hand.GetTotalValue() == 21);
        }

        [Test]
        public void TestAceActingAsEleven()
        {
            List<Cards> faceDown = new List<Cards>();
            faceDown.Add(new Cards("S", 3));
            faceDown.Add(new Cards("H", 7));
            faceDown.Add(new Cards("C", 1));
            hand.SetFaceDown(faceDown);
            Assert.That(hand.GetTotalValue() == 21);
        }
    }
}
