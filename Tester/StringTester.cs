using BlackJack;
using ConsoleApp1;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    [TestFixture]
    public class StringTester
    {
        private CreateStrings create;
        private Player player;
        private Player dealer;
        private string line;

        [SetUp]
        public void Init()
        {
            player = new Player(false);
            dealer = new Player(true);
            create = new CreateStrings(player, dealer);
        }

        [Test]
        public void TestPrintDealerCurrentMoney()
        {
            line = create.CurrentMoney(true);
            Assert.That(line.Contains("The dealer's curent money is"));
            Assert.That(line.Contains("10000"));
        }

        [Test]
        public void TestPrintPlayerCurrentMoney()
        {
            line = create.CurrentMoney();
            TestContext.Progress.WriteLine(line);
            Assert.That(line.Contains("Your current money is"));
            Assert.That(line.Contains("1000"));
        }

        [Test]
        public void TestGetBet()
        {
            line = create.GetBet();
            Assert.That(line.Equals("Please enter a bet:"));
        }

        [Test]
        public void TestGetHit()
        {
            line = create.GetHit();
            Assert.That(line.Equals("Do you want to hit?"));
        }

        [Test]
        public void TestPlayerWin()
        {
            line = create.PlayerWin();
            Assert.That(line.Equals("You have won!"));
        }

        [Test]
        public void TestDealerWin()
        {
            line = create.DealerWin();
            Assert.That(line.Equals("The dealer has won!"));
        }

        [Test]
        public void TestPush()
        {
            line = create.Push();
            Assert.That(line.Equals("You and the dealer have pushed. No money won or lost"));
        }

        [Test]
        public void TestPlayerGameWin()
        {
            line = create.PlayerGameWin();
            Assert.That(line.Equals("You have doubled your money and won!"));
        }

        [Test]
        public void TestPlayerGameLose()
        {
            line = create.PlayerGameLose();
            Assert.That(line.Equals("You have no more money. You have lost"));
        }

        [Test]
        public void TestGetCard()
        {
            line = create.GetCard(new Cards("H", 5));
            Assert.That(line.Equals("5 of Hearts"));
        }

        [Test]
        public void TestDealerInitialHand()
        {
            line = create.DealerInitialHand();
            Assert.That(line.Equals("The dealer is showing:"));
        }
    }
}
