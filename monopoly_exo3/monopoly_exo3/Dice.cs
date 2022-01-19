using System;
using System.Collections.Generic;
using System.Text;

namespace monopoly_exo3
{
    class Dice
    {
        public int Roll()
        {
            return rng.Next(1, Constants.MAX_DICE_VALUE + 1);//Generates random number for the dice roll
        }
        private readonly Random rng = new Random();
    }
}
