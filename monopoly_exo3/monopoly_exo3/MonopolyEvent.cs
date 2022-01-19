using System;
using System.Collections.Generic;
using System.Text;

namespace monopoly_exo3
{
    //ALL events used in our game, we can add some if needed
    enum MonopolyEvent
    {
        PlayerMoved,
        PlayerJailed,
        PlayerRolledDice,
        PlayerRolledDouble,
        PlayerPardonned,
        PlayerRolledTooMuchDouble,
        PlayerIsForcedToLeaveJail,
        PlayerWin,
    }
}
