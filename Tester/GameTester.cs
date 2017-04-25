using BlackJack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class GameTester
    {
        private Game game;

        [SetUp]
        public void Init()
        {
            game = new Game();
        }

        [Test]
        public void TestYesInput()
        {
            Assert.That(game.ReadYesOrNoInput("Yes") == 1);
            Assert.That(game.ReadYesOrNoInput("Y") == 1);
            Assert.That(game.ReadYesOrNoInput("yes") == 1);
            Assert.That(game.ReadYesOrNoInput("y") == 1);
        }

        [Test]
        public void TestNoInput()
        {
            Assert.That(game.ReadYesOrNoInput("No") == -1);
            Assert.That(game.ReadYesOrNoInput("N") == -1);
            Assert.That(game.ReadYesOrNoInput("no") == -1);
            Assert.That(game.ReadYesOrNoInput("n") == -1);
        }

        [Test]
        public void TestInvalidInput()
        {
            Assert.That(game.ReadYesOrNoInput("1") == 0);
            Assert.That(game.ReadYesOrNoInput("noyes") == 0);
            Assert.That(game.ReadYesOrNoInput("YesNo") == 0);
            Assert.That(game.ReadYesOrNoInput("!") == 0);
        }
    }
}
