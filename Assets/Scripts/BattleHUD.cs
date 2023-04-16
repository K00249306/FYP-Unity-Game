using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text hpText;
    public Slider hpSlider;
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

    // Updates HP slider
    //public void UpdateHPText(hpSlider.value)
    //{
    //    hpText.text = hpSlider.value;
    //}

    // Updates HP slider
    public void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }

    // Updates energy slider
    public void UpdateEnergy(int energy)
    {
        energySlider.value = energy;
    }
}
