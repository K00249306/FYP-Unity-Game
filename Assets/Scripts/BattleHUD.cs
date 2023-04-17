using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public Slider hpSlider;

    public TextMeshProUGUI energyText;
    public Slider energySlider;

    
    // Method to update UI
    public void UpdateHud(Monster monster)
    {
        // hpText.text = monster.currentHP;
        hpSlider.maxValue = monster.maxHP;
        hpSlider.value = monster.currentHP;
        energySlider.maxValue = monster.maxEnergy;
        energySlider.value = monster.currentEnergy;
    }

    // Updates HP text
    public void UpdateHPText(int hp)
    {
        // HP = 0 if energy goes into the negative
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
}
