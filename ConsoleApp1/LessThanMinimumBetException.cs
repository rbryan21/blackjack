using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class LessThanMinimumBetException: Exception
    {
        public LessThanMinimumBetException()
            :base("The minimum bet is $1.")
        {
        }
    }
}
