using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiPlayerData
{
    // Stats to be stored
    // Player 1 Stats
    public int gamesPlayed1;
    public int wins1;
    public int losses1;

    // Player 2 Stats
    public int gamesPlayed2;
    public int wins2;
    public int losses2;

    public MultiPlayerData(BattleSystemMultiplayer multiPlayerData)
    {
        gamesPlayed1 = multiPlayerData.gamesPlayed1;
        wins1 = multiPlayerData.wins1;
        losses1 = multiPlayerData.losses1;

        gamesPlayed2 = multiPlayerData.gamesPlayed2;
        wins2 = multiPlayerData.wins2;
        losses2 = multiPlayerData.losses2;
    }
}
