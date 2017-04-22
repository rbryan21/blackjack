using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Tester
{
    [TestFixture]
    public class PlayerTester
    {
        private Player player;
        
        [SetUp]
        public void Init()
        {
            player = new Player();
        }

        [Test]
        public void TestDecreaseMoney()
        {
            player.SetMoney(1000);
            player.DecreaseMoney(800);
            Assert.That(player.GetMoney() == 200);
        }

        [Test]
        public void TestPlayerWin()
        {
            player.SetMoney(200);
            player.PlayerWin();
            Assert.That(player.GetMoney() == 400);
        }

        [Test]
        public void TestPlayerLose()
        {
            player.SetMoney(1000);
            player.SetBet(200);
            player.PlayerLose();
            Assert.That(player.GetMoney() == 800);
        }

        [Test]
        public void TestMinimumBet()
        {
            try
            {
                player.SetMoney(1000);
                player.SetBet(-1);
            }
            catch (LessThanMinimumBetException e)
            {
                Assert.IsTrue(e.Message.Contains("The minimum bet is $1."));
            }
        }

        [Test]
        public void TestAboveMaxBet()
        {
            try
            {
                player.SetMoney(1000);
                player.SetBet(1200);
            }
            catch(BetGreaterThanMoneyException e)
            {
                Assert.IsTrue(e.Message.Contains("You cannot bet more money than you have"));
            }
        }
    }
}
