using System;
using System.Collections.Generic;
using System.Text;

namespace monopoly_exo3
{

    /// <summary>
    /// Observer that is going to be triggered each time that an event happens on Player p 
    /// </summary>
    interface IMonopolyObserver
    {
        void notify(Player p, MonopolyEvent evt);//Function that you need to code in classes using the observer
    }
}
