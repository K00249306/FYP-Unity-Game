using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class Monster : ScriptableObject
{
    public new string monsterName; 

    public int hitPoints;
    public int energyCurrent;
    public int energyMax;
    public int energyRegeneration;

    public int meleeDamage;
    public int meleeCost;
    public int meleeNumber;

    public int rangedDamage;
    public int rangedCost;
    public int rangedNumber;

    public int specialDamage;
    public int specialCost;
    public int specialNumber;

    public int healthRegeneration;
    public int healthCost;
    public int healthNumber;
}

