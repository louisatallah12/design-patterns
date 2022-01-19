using System;
using System.Collections.Generic;
using System.Text;

namespace monopoly_exo3
{
    class Monopoly : IMonopolyObserver
    {

        public void notify(Player p, MonopolyEvent evt)
        {
            _isRunning = false;
        }
        private List<Player> _players;
        private bool _isRunning = false;

        /// <summary>
        /// For each game event, we subscribe the player for each observer and event assigned. 
        /// The Monopoly observes itself to know when to call the notify function and end the game. 
        /// </summary>
        /// <param name="players">List of tuple of players defined by their names and position</param>
        /// <param name="observers">List Of Tuple of observers with their associated events</param>
        public Monopoly(List<Tuple<string, int>> players, List<Tuple<IMonopolyObserver, List<MonopolyEvent>>> observers = null)
        {
            _players = new List<Player>(players.Count);
            foreach (Tuple<string, int> player in players)
            {
                Player newPlayer = new Player(player.Item1, player.Item2); //Item1 is the name of the player and Item2 his starting position
                newPlayer.Subscribe(this, MonopolyEvent.PlayerWin); //If PlayerWin happens in the PLayer Class, the notify function is called and the game ends
                if (observers != null)
                {
                    foreach (Tuple<IMonopolyObserver, List<MonopolyEvent>> pair in observers)
                    {
                        foreach (MonopolyEvent evt in pair.Item2)
                        {
                            newPlayer.Subscribe(pair.Item1, evt);
                        }
                    }
                }
                _players.Add(newPlayer);
            }
        }

        /// <summary>
        /// We create a new Dice that we use as the parameter for the player's turn
        /// The game plays while _isRunning is true, until the notify function is called
        /// </summary>
        public void Start()
        {
            if (_players.Count == 0)
            {
                return;
            }
            Dice dice = new Dice();
            _isRunning = true;
            while (_isRunning)
            {
                foreach (Player p in _players)
                {
                    p.PlayTurn(dice);
                    if (_isRunning == false)
                    {
                        return;
                    }
                }
            }
        }
    }
}
