using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;

    public int maxHP;
    public int currentHP;
    public int maxEnergy;
    public int currentEnergy;
    public int energyPerTurn;

    public int meleeDamage;
    public int meleeCost;
    public int rangedDamage;
    public int rangedCost;
    public int specialDamage;
    public int specialCost;
    public int healAmount;
    public int healCost;

    // Subtracts melee damage and checks if attacked player is dead
    public bool TakeMeleeDamage(int meleeDamage)
    {
        currentHP -= meleeDamage;

        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Subtracts energy for melee attack
    public bool TakeMeleeCost(int meleeCost)
    {
        currentEnergy -= meleeCost;

        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Subtracts ranged damage and checks if attacked player is dead
    public bool TakeRangedDamage(int rangedDamage)
    {
        currentHP -= rangedDamage;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Subtracts energy for ranged attack
    public bool TakeRangedCost(int rangedCost)
    {
        currentEnergy -= rangedCost;

        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Subtracts special damage and checks if attacked player is dead
    public bool TakeSpecialDamage(int specialDamage)
    {
        currentHP -= specialDamage;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Subtracts energy for special attack
    public bool TakeSpecialCost(int specialCost)
    {
        currentEnergy -= specialCost;

        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Heals player for specified amount
    public bool PlayerHeal(int healAmount)
    {
        currentHP += healAmount;

        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Subtracts energy for Healing
    public bool TakeHealCost(int healCost)
    {
        currentEnergy -= specialCost;

        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
