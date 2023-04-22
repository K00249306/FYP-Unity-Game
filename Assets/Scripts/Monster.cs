using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Monster stats/info
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
    public int rangedUses;

    public int healAmount;
    public int healCost;
    public int healUses;

    public int specialDamage;
    public int specialCost;
    public int specialUses;

    // Subtracts melee damage and checks if attacked player is dead
    public bool TakeMeleeDamage(int meleeDamage)
    {
        currentHP -= meleeDamage;

        // Returns value
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
            currentEnergy = 0;
        }

        // Returns value
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

        // Returns value
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
            currentEnergy = 0;
        }

        // Returns value
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

        // Returns value
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
            currentEnergy = 0;
        }

        // Returns value
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

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        // Returns value
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
        currentEnergy -= healCost;

        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
        }

        // Returns value
        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Gives players energy every turn
    public bool EnergyPerTurn(int energyPerTurn)
    {
        currentEnergy += energyPerTurn;

        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
        }

        // Returns value
        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    // Takes away ranged use
    public bool RangedUses(int rangeUses)
    {
        rangeUses--;

        // Returns value
        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Takes away heal use
    public bool HealUses(int healUses)
    {
        healUses--;

        // Returns value
        if (currentEnergy <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Takes away special use
    public bool SpecialUses(int specialUses)
    {
        specialUses--;

        // Returns value
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
