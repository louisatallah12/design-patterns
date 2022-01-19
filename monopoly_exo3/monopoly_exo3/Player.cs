using System;
using System.Collections.Generic;
using System.Text;

namespace monopoly_exo3
{
    class Player
    {
        // Players don't need to interact with each others so all the game happens in the Player Class
        public Player(string name, int cellId)
        {
            SetOnCell(cellId);
            _name = name;
        }
        private Dictionary<MonopolyEvent, List<IMonopolyObserver>> _observers = new Dictionary<MonopolyEvent, List<IMonopolyObserver>>();
        public int[] LastRolls { get; set; } = new int[0];
        private int _cellId = 0; //Cell where the player is position
        private string _name = "";
        private bool _isInJail = false; //Indicates if the player is in jail or not
        private int _nbDoublesInARow = 0;
        private int _prisonTries = 0; //Indicates the number of times that the player tries to leave from prison
        private int _boardTurns = 0;

        //Getters:
        public string Name => _name;
        public int CellId => _cellId;
        public bool IsInJail => _isInJail;
        public int BoardTurns => _boardTurns;
        public int NbDoublesInARow => _nbDoublesInARow;

        /// <summary>
        /// Subscribes to an event, creates a link between the observer and the event, to activate the observer when the event happens
        /// </summary>
        /// <param name="observer">Observer</param>
        /// <param name="evt">Event associated to the Observer</param>
        /// <returns></returns>
        public bool Subscribe(IMonopolyObserver observer, MonopolyEvent evt)
        {
            if (_observers.ContainsKey(evt))
            {
                if (_observers[evt].Contains(observer))
                {
                    return false;
                }
                _observers[evt].Add(observer);
                return true;
            }
            _observers[evt] = new List<IMonopolyObserver>();
            _observers[evt].Add(observer);
            return true;
        }

        //Opposite of the Subscribe function
        public bool Unsubscribe(IMonopolyObserver observer, MonopolyEvent evt)
        {
            if (_observers.ContainsKey(evt) && _observers[evt].Contains(observer))
            {
                _observers[evt].Remove(observer);
                return true;
            }
            return false;
        }

        //Changes the position of the player
        public void SetOnCell(int cellId)
        {
            _cellId = cellId % Constants.MAX_CELLS; //Modulo so that the cellId is always < 39
            notify(MonopolyEvent.PlayerMoved); //Notifies that the player moved
            if (cellId >= Constants.MAX_CELLS)
            {
                _boardTurns += 1;
                if (_boardTurns >= Constants.TURNS_TO_WIN)  
                {
                    notify(MonopolyEvent.PlayerWin); //Notifies that the player wins
                    return;
                }
            }
            if (cellId == Constants.GOTO_JAIL_CELL_POSITION)//if you land on the jail cell, it calls the jail function
            {
                Jail();
            }
        }
        // Intern function of Player, notify each observer associated to concerned events
        private void notify(MonopolyEvent evt) 
        {
            if (!_observers.ContainsKey(evt))
                return;
            foreach (IMonopolyObserver obs in _observers[evt])
            {
                obs.notify(this, evt);
            }
        }
        
        //Function to Move if you're not in Jail
        public void Move(int noOfCells)
        {
            if (!IsInJail)
            {
                SetOnCell(_cellId + noOfCells);
            }
        }

 
        public void Jail()
        {
            _isInJail = true;
            notify(MonopolyEvent.PlayerJailed); //Notifies that the player is in jail
            SetOnCell(Constants.JAIL_CELL_POSITION);//Sets the player on cell 10
        }

        /// <summary>
        /// Function that puts you out of jail
        /// </summary>
        public void Pardon()
        {
            _isInJail = false;
            _prisonTries = 0;
            notify(MonopolyEvent.PlayerPardonned);
        }

        //Tells you if the roll is a double Roll or not
        private bool isADoubleRoll(int[] values)
        {
            foreach (int value in values)
            {
                if (value != values[0])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Calculates the total from the dice roll
        /// </summary>
        /// <param name="values">combination of the dice roll</param>
        /// <returns></returns>
        private int rollsSum(int[] values)
        {
            int total = 0;

            //We use a foreach loop in case we want to use more than 2 dice and change the value in the Constants class
            foreach (int value in values)
            {
                total += value;
            }
            return total;
        }

        //The game itself:
        public void PlayTurn(Dice dice)
        {
            int[] values = new int[Constants.DICE_COUNT];
            bool isDouble;
            int count;

            //We roll the dice
            for (int i = 0; i < values.Length; ++i)
            {
                values[i] = dice.Roll();
            }
            LastRolls = values;
            notify(MonopolyEvent.PlayerRolledDice);//We notify that the player has rolled the dice
            count = rollsSum(values); //We calculate how many cells the player can move to
            isDouble = isADoubleRoll(values); //We check if the player rolled a double
            if (_isInJail) //If the player is in jail
            {
                _prisonTries += 1;
                if (isDouble)
                {
                    _nbDoublesInARow = 1;
                    notify(MonopolyEvent.PlayerRolledDouble);//We notify that the player rolled a double
                    Pardon(); //The player can go out of jail
                    isDouble = false; //The double doesn't count but the player can move
                }
                else if (_prisonTries >= Constants.MAX_PRISON_TRIES)//If the player tried more than 3 times to leave jail
                {
                    notify(MonopolyEvent.PlayerIsForcedToLeaveJail);//We notify that the player was forced to leave jail
                    Pardon();
                    isDouble = false;
                }
            }
            if (isDouble)// If the player does a double but isn't in jail
            {
                _nbDoublesInARow++;
                notify(MonopolyEvent.PlayerRolledDouble); //We notify that the player has done a Double
                if (_nbDoublesInARow >= Constants.MAX_DOUBLES_IN_A_ROW) //If the player has made too many doubles in a row
                {
                    notify(MonopolyEvent.PlayerRolledTooMuchDouble);// We notify that the player rolled too many doubles
                    _nbDoublesInARow = 0; //We reset the number of doubles in a row
                    Jail();// The player is sent to jail
                    return; // The player won't move for this turn
                }
                Move(count);
                if (!IsInJail)
                {
                    PlayTurn(dice);//Calls the function again since the player has done a double 
                }
                return;
            }
            _nbDoublesInARow = 0;//We reset the number of doubles in a row at the end of the turn
            Move(count); 
        }
    }
}
