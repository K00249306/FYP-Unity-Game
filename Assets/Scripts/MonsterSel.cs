using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSel : MonoBehaviour
{
    // Array of monsters
    public GameObject[] monsters;

    // Contains data of current monster (first in the array)
    public int selectedMonster = 0;

    // Shows data of the next monster in array
    public void NextMonster()
    {
        monsters[selectedMonster].SetActive(false);
        selectedMonster = (selectedMonster + 1) % monsters.Length;
        monsters[selectedMonster].SetActive(true);
    }

    // Shows data of the previous monster in array
    public void PreviousMonster()
    {
        monsters[selectedMonster].SetActive(false);
        selectedMonster--;
        if (selectedMonster < 0)
        {
            selectedMonster += monsters.Length;
        }
        monsters[selectedMonster].SetActive(true);
    }
}
