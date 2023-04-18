using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SinglePlayerData
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

    public SinglePlayerData(BattleSystem singlePlayerData)
    {
        gamesPlayed1 = singlePlayerData.gamesPlayed1;
        wins1 = singlePlayerData.wins1;
        losses1 = singlePlayerData.losses1;

        gamesPlayed2 = singlePlayerData.gamesPlayed2;
        wins2 = singlePlayerData.wins2;
        losses2 = singlePlayerData.losses2;
    }
}
