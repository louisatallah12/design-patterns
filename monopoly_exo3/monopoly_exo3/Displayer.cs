using System;
using System.Collections.Generic;
using System.Text;

namespace monopoly_exo3
{
    //This class manages all events from the Observer
    //Displays what happens when an event is notified
    class Displayer : IMonopolyObserver
    {
        public void notify(Player p, MonopolyEvent evt)
        {
            switch (evt)
            {
                case MonopolyEvent.PlayerMoved:
                    Console.WriteLine($"{p.Name} moved to cell {p.CellId}");
                    break;
                case MonopolyEvent.PlayerJailed:
                    Console.WriteLine($"{p.Name} is now in jail");
                    break;
                case MonopolyEvent.PlayerRolledDice:
                    Console.WriteLine($"{p.Name} rolled ({string.Join(", ", p.LastRolls)})");
                    break;
                case MonopolyEvent.PlayerRolledDouble:
                    Console.WriteLine($"{p.Name} rolled {p.NbDoublesInARow} double");
                    break;
                case MonopolyEvent.PlayerPardonned:
                    Console.WriteLine($"{p.Name} leaves the jail");
                    break;
                case MonopolyEvent.PlayerRolledTooMuchDouble:
                    Console.WriteLine($"{p.Name} rolled too much double");
                    break;
                case MonopolyEvent.PlayerIsForcedToLeaveJail:
                    Console.WriteLine($"{p.Name} is forced to leave the jail");
                    break;
                case MonopolyEvent.PlayerWin:
                    Console.WriteLine($"{p.Name} won the game");
                    break;
            }
        }

    }
}
