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
        private Player dealer;
        
        [SetUp]
        public void Init()
        {
            player = new Player(false);
            dealer = new Player(true);
        }

        [Test]
        public void TestPlayerDefaultMoney()
        {
            Assert.That(player.Money == 1000);
        }

        [Test]
        public void TestDealerDefaultMoney()
        {
            Assert.That(dealer.Money == 10000);
        }

        [Test]
        public void DecreasePlayerMoney()
        {
            player.DecreaseMoney(500);
            Assert.That(player.Money == 500);
        }

        [Test]
        public void IncreasePlayerMoney()
        {
            player.IncreaseMoney(500);
            Assert.That(player.Money == 1500);
        }

        [Test]
        public void TestMinimumBet()
        {
            try
            {
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
                player.SetBet(1200);
            }
            catch(BetGreaterThanMoneyException e)
            {
                Assert.IsTrue(e.Message.Contains("You cannot bet more money than you have"));
            }
        }

        [Test]
        public void TestPlayerWin()
        {
            player.IncreaseMoney(1000);
            Assert.IsTrue(player.PlayerWin());
        }

        [Test]
        public void TestPlayerLoss()
        {
            player.DecreaseMoney(1000);
            Assert.IsTrue(player.PlayerLose());
        }

        [Test]
        public void TestNoNegaviteMoney()
        {
            player.DecreaseMoney(1001);
            player.NoNegativePlayerMoney();
            Assert.IsTrue(player.Money == 0);
        }

        [Test]
        public void TestSetBet()
        {
            player.SetBet(500);
            Assert.That(player.Bet == 500);
        }

        [Test]
        public void TestSetHand()
        {
            var hand = new Hand(new List<Cards>
            {
                new Cards("H", 10)
            });
            player.SetHand(hand);
            Assert.That(hand.GetValue() == 10);
        }

        [Test]
        public void TestPlayerWiningHand()
        {
            var hand = new Hand(new List<Cards>
            {
                new Cards("H", 10)
            });
            player.SetHand(hand);

            var hand2 = new Hand(new List<Cards>
            {
                new Cards("H", 9)
            });
            dealer.SetHand(hand2);

            Assert.That(player.EvaluateHand(player, dealer) == 1);
        }

        [Test]
        public void TestPlayerLosingHand()
        {
            var hand = new Hand(new List<Cards>
            {
                new Cards("H", 9)
            });
            player.SetHand(hand);

            var hand2 = new Hand(new List<Cards>
            {
                new Cards("H", 10)
            });
            dealer.SetHand(hand2);

            Assert.That(player.EvaluateHand(player, dealer) == -1);
        }

        [Test]
        public void TestPush()
        {
            var hand = new Hand(new List<Cards>
            {
                new Cards("H", 12)
            });
            player.SetHand(hand);

            var hand2 = new Hand(new List<Cards>
            {
                new Cards("H", 10)
            });
            dealer.SetHand(hand2);

            Assert.That(player.EvaluateHand(player, dealer) == 0);
        }

        [Test]
        public void TestDealerIsHitting()
        {
            var hand = new Hand(new List<Cards>
            {
                new Cards("H", 12),
                new Cards("H", 2)
            });
            player.SetHand(hand);

            Assert.That(player.DealerHitting(player) == true);
        }

        [Test]
        public void TestDealerSoft17()
        {
            var hand = new Hand(new List<Cards>
            {
                new Cards("H", 1),
                new Cards("H", 6)
            });
            player.SetHand(hand);

            Assert.That(player.DealerHitting(player) == false);
        }

        [Test]
        public void TestDealer21()
        {
            var hand = new Hand(new List<Cards>
            {
                new Cards("H", 1),
                new Cards("H", 11)
            });
            player.SetHand(hand);

            Assert.That(player.DealerHitting(player) == false);
        }
    }
}
