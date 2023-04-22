using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    // UI Elements
    public TextMeshProUGUI hpText;
    public Slider hpSlider;

    public TextMeshProUGUI energyText;
    public Slider energySlider;

    public TextMeshProUGUI rangedUsesText;
    public TextMeshProUGUI healUsesText;
    public TextMeshProUGUI specialUsesText;

    // Method to update UI
    public void UpdateHud(Monster monster)
    {
        hpSlider.maxValue = monster.maxHP;
        hpSlider.value = monster.currentHP;
        energySlider.maxValue = monster.maxEnergy;
        energySlider.value = monster.currentEnergy;
    }

    // Updates HP text
    public void UpdateHPText(int hp)
    {
        // HP = 0 if HP goes into the negative
        if (hp <= 0)
        {
            hpText.text = 0.ToString();
        }
        else
        {
            hpText.text = hp.ToString();
        }
    }

    // Updates HP slider
    public void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }

    // Updates energy text
    public void UpdateEnergyText(int energy)
    {
        energyText.text = energy.ToString();
    }

    // Updates energy slider
    public void UpdateEnergy(int energy)
    {
        energySlider.value = energy;
    }

    // Updates ranged uses
    public void UpdateRangedUses(int rangedUses)
    {
        rangedUsesText.text = rangedUses.ToString();
    }

    // Updates heal uses
    public void UpdateHealUses(int healUses)
    {
        healUsesText.text = healUses.ToString();
    }

    // Updates ranged uses
    public void UpdateSpecialUses(int specialUses)
    {
        specialUsesText.text = specialUses.ToString();
    }
}
