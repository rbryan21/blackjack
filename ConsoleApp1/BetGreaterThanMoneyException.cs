using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class BetGreaterThanMoneyException: Exception
    {
        public BetGreaterThanMoneyException()
            :base("You cannot bet more money than you have")
        {

        }
    }
}
