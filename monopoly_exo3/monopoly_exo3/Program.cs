using System;
using System.Collections.Generic;

namespace monopoly_exo3
{
   
    class Program
    {
        static List<Tuple<string, int>> parsePlayers(string[] args)
        {
            List<Tuple<string, int>> players = new List<Tuple<string, int>>();
            players.Add(new Tuple<string, int>("Player1", 0));
            players.Add(new Tuple<string, int>("Player2", 0));

            foreach (string arg in args)
            {
                string[] parsed = arg.Split(':');
                if (parsed.Length == 0)
                {
                    Console.WriteLine($"{arg} is not a valid player, players should be formatted as \"name:position\"");
                    return null;
                }
                string name = parsed[0];
                int position = 0;
                if (parsed.Length != 1)
                {
                    if (int.TryParse(parsed[1], out position) == false)
                    {
                        Console.WriteLine($"{arg}:[{parsed[1]}] position is malformated, not an integer");
                        return null;
                    }
                }
                players.Add(new Tuple<string, int>(name, position));
            }
            return players;
        }
        static void Main(string[] args)
        {
            var players = parsePlayers(args);
            List<Tuple<IMonopolyObserver, List<MonopolyEvent>>> observers = new List<Tuple<IMonopolyObserver, List<MonopolyEvent>>>();
            List<MonopolyEvent> allEvents = new List<MonopolyEvent>();

            if (players.Count == 0)
            {
                Console.WriteLine("No players specified, pass them as arguments\n\t\"Name1:0\" \"Name2:4\"\nWhere NameX is the player name followed by a ':' with the initial position afterwards");
            }

            foreach (MonopolyEvent evt in Enum.GetValues(typeof(MonopolyEvent)))
            {
                allEvents.Add(evt);
            }

            observers.Add(new Tuple<IMonopolyObserver, List<MonopolyEvent>>(new Displayer(), allEvents));

            Monopoly game = new Monopoly(players, observers);
            game.Start();
        }
    }
}
