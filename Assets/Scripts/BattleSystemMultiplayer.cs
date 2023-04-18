using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystemMultiplayer : MonoBehaviour
{
    // Creates different states or stages of game 
    public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, PLAYERACTION, WON, LOST }

    // Stats to be stored
    // Player 1 Stats
    public int gamesPlayed1 = 0;
    public int wins1 = 0;
    public int losses1 = 0;
    
    // Player 2 Stats
    public int gamesPlayed2 = 0;
    public int wins2 = 0;
    public int losses2 = 0;

    // UI text
    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI gamesPlayed1Text;
    public TextMeshProUGUI wins1Text;
    public TextMeshProUGUI losses1Text;

    public TextMeshProUGUI gamesPlayed2Text;
    public TextMeshProUGUI wins2Text;
    public TextMeshProUGUI losses2Text;

    // Buttons and objects to become active and not active when match is complete
    public Button mmButton;
    public Button backButton;
    public GameObject abilityPanel;
    public GameObject abilityPanel2;

    // Accesses prefabs
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    // Player location
    public Transform player1Location;
    public Transform player2Location;

    // Monster variables
    Monster player1Monster;
    Monster player2Monster;

    // References HUDs
    public BattleHUD player1HUD;
    public BattleHUD player2HUD;

    // Calling battle state
    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        gamesPlayed1Text.text = gamesPlayed1.ToString();
        wins1Text.text = wins1.ToString();
        losses1Text.text = losses1.ToString();

        gamesPlayed2Text.text = gamesPlayed2.ToString();
        wins2Text.text = wins2.ToString();
        losses2Text.text = losses2.ToString();

        state = BattleState.START;
        //StartCoroutine(PrepareBattle());
    }

    public void StartGame()
    {
        StartCoroutine(PrepareBattle());
    }

    // Save data
    public void SaveData()
    {
        SaveSystemM.SaveStats(this);
    }

    // Load Data
    public void LoadData()
    {
        MultiPlayerData data = SaveSystemM.LoadPlayer();

        gamesPlayed1 = data.gamesPlayed1;
        wins1 = data.wins1;
        losses1 = data.losses1;

        gamesPlayed2 = data.gamesPlayed2;
        wins2 = data.wins2;
        losses2 = data.losses2;
    }

    IEnumerator PrepareBattle()
    {
        // Games played counter increases
        gamesPlayed1++;
        gamesPlayed2++;

        SaveData();

        // Get information about monsters so UI can be updated
        GameObject player1 = Instantiate(player1Prefab, player1Location);
        player1Monster = player1.GetComponent<Monster>();

        GameObject player2 = Instantiate(player2Prefab, player2Location);
        player2Monster = player2.GetComponent<Monster>();

        player1HUD.UpdateHud(player1Monster);
        player2HUD.UpdateHud(player2Monster);

        dialogueText.text = "Fight!";

        // Waits 3 seconds before changing states
        yield return new WaitForSeconds(3f);

        // Sets state to Player 1's turn
        state = BattleState.PLAYER1TURN;
        Player1Turn();
    }

    void Player1Turn()
    {
        dialogueText.text = "Player 1's turn!";
    }

    // Use melee attack when melee button is pressed
    public void OnMeleeButton()
    {
        if (state != BattleState.PLAYER1TURN)
        {
            return;
        }
        // Stops players from being able to use abilities over and over
        state = BattleState.PLAYERACTION;
        StartCoroutine(Player1Melee());
    }

    // Attacked player takes melee damage and battle ends if they die
    IEnumerator Player1Melee()
    {
        // Checks if player has enough energy to use ability
        if (player1Monster.meleeCost <= player1Monster.currentEnergy)
        {
            yield return new WaitForSeconds(1f);

            bool isDead = player2Monster.TakeMeleeDamage(player1Monster.meleeDamage);
            player1Monster.TakeMeleeCost(player1Monster.meleeCost);

            player2HUD.UpdateHP(player2Monster.currentHP);
            player2HUD.UpdateHPText(player2Monster.currentHP);
            dialogueText.text = "The attack landed!";

            player1HUD.UpdateEnergy(player1Monster.currentEnergy);
            player1HUD.UpdateEnergyText(player1Monster.currentEnergy);

            yield return new WaitForSeconds(1f);

            player1Monster.EnergyPerTurn(player1Monster.energyPerTurn);
            player1HUD.UpdateEnergy(player1Monster.currentEnergy);
            player1HUD.UpdateEnergyText(player1Monster.currentEnergy);
            dialogueText.text = "Energy restored!";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                // End battle 
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                // Enemy turn
                state = BattleState.PLAYER2TURN;
                StartCoroutine(Player2Turn());
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            state = BattleState.PLAYER1TURN;
        }
    }

    // Use ranged attack when ranged button is pressed
    public void OnRangedButton()
    {
        if (state != BattleState.PLAYER1TURN)
        {
            return;
        }
        // Stops players from being able to use abilities over and over
        state = BattleState.PLAYERACTION;
        StartCoroutine(Player1Ranged());
    }

    // Attacked player takes ranged damage and battle ends if they die
    IEnumerator Player1Ranged()
    {
        // Checks if player has enough energy to use ability
        if (player1Monster.rangedCost <= player1Monster.currentEnergy)
        {
            yield return new WaitForSeconds(1f);

            bool isDead = player2Monster.TakeRangedDamage(player1Monster.rangedDamage);
            player1Monster.TakeRangedCost(player1Monster.rangedCost);

            player2HUD.UpdateHP(player2Monster.currentHP);
            player2HUD.UpdateHPText(player2Monster.currentHP);
            dialogueText.text = "The attack landed!";

            player1HUD.UpdateEnergy(player1Monster.currentEnergy);
            player1HUD.UpdateEnergyText(player1Monster.currentEnergy);

            yield return new WaitForSeconds(1f);

            player1Monster.EnergyPerTurn(player1Monster.energyPerTurn);
            player1HUD.UpdateEnergy(player1Monster.currentEnergy);
            player1HUD.UpdateEnergyText(player1Monster.currentEnergy);
            dialogueText.text = "Energy restored!";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                // End battle 
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                // Enemy turn
                state = BattleState.PLAYER2TURN;
                StartCoroutine(Player2Turn());
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            state = BattleState.PLAYER1TURN;
        }
    }

    // Player heals
    public void OnHealButton()
    {
        if (state != BattleState.PLAYER1TURN)
        {
            return;
        }
        // Stops players from being able to use abilities over and over
        state = BattleState.PLAYERACTION;
        StartCoroutine(Player1Heal());
    }

    // Player heals
    IEnumerator Player1Heal()
    {
        // Checks if player has enough energy to use ability
        if (player1Monster.healCost <= player1Monster.currentEnergy)
        {
            yield return new WaitForSeconds(1f);

            bool isDead = player1Monster.PlayerHeal(player1Monster.healAmount);
            player1Monster.TakeHealCost(player1Monster.healCost);

            player1HUD.UpdateHP(player1Monster.currentHP);
            player1HUD.UpdateHPText(player1Monster.currentHP);
            dialogueText.text = "You healed 15 points";

            player1HUD.UpdateEnergy(player1Monster.currentEnergy);
            player1HUD.UpdateEnergyText(player1Monster.currentEnergy);

            yield return new WaitForSeconds(1f);

            player1Monster.EnergyPerTurn(player1Monster.energyPerTurn);
            player1HUD.UpdateEnergy(player1Monster.currentEnergy);
            player1HUD.UpdateEnergyText(player1Monster.currentEnergy);
            dialogueText.text = "Energy restored";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                // End battle 
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                // Enemy turn
                state = BattleState.PLAYER2TURN;
                StartCoroutine(Player2Turn());
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            state = BattleState.PLAYER1TURN;
        }
    }

    // Use special attack when special button is pressed
    public void OnSpecialButton()
    {
        if (state != BattleState.PLAYER1TURN)
        {
            return;
        }
        // Stops players from being able to use abilities over and over
        state = BattleState.PLAYERACTION;
        StartCoroutine(Player1Special());
    }

    // Attacked player takes special damage and battle ends if they die
    IEnumerator Player1Special()
    {
        // Checks if player has enough energy to use ability
        if (player1Monster.specialCost <= player1Monster.currentEnergy)
        {
            yield return new WaitForSeconds(1f);

            bool isDead = player2Monster.TakeSpecialDamage(player1Monster.specialDamage);
            player1Monster.TakeSpecialCost(player1Monster.specialCost);

            player2HUD.UpdateHP(player2Monster.currentHP);
            player2HUD.UpdateHPText(player2Monster.currentHP);
            dialogueText.text = "The attack landed!";

            player1HUD.UpdateEnergy(player1Monster.currentEnergy);
            player1HUD.UpdateEnergyText(player1Monster.currentEnergy);

            yield return new WaitForSeconds(1f);

            player1Monster.EnergyPerTurn(player1Monster.energyPerTurn);
            player1HUD.UpdateEnergy(player1Monster.currentEnergy);
            player1HUD.UpdateEnergyText(player1Monster.currentEnergy);
            dialogueText.text = "Energy restored!";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                // End battle 
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                // Enemy turn
                state = BattleState.PLAYER2TURN;
                StartCoroutine(Player2Turn());
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            state = BattleState.PLAYER1TURN;
        }
    }

    // Player 2 turn
    IEnumerator Player2Turn()
    {
        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYER2TURN;
        dialogueText.text = "Player 2's turn!";
    }

    // Use melee attack when melee button is pressed
    public void OnMeleeButton2()
    {
        if (state != BattleState.PLAYER2TURN)
        {
            return;
        }
        // Stops players from being able to use abilities over and over
        state = BattleState.PLAYERACTION;
        StartCoroutine(Player2Melee());
    }

    IEnumerator Player2Melee()
    {
        // Checks if player has enough energy to use ability
        if (player2Monster.meleeCost <= player2Monster.currentEnergy)
        {
            yield return new WaitForSeconds(1f);

            bool isDead = player1Monster.TakeMeleeDamage(player2Monster.meleeDamage);
            player2Monster.TakeMeleeCost(player2Monster.meleeCost);

            player1HUD.UpdateHP(player1Monster.currentHP);
            player1HUD.UpdateHPText(player1Monster.currentHP);
            dialogueText.text = "The attack landed!";

            player2HUD.UpdateEnergy(player2Monster.currentEnergy);
            player2HUD.UpdateEnergyText(player2Monster.currentEnergy);

            yield return new WaitForSeconds(1f);

            player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
            player2HUD.UpdateEnergy(player2Monster.currentEnergy);
            player2HUD.UpdateEnergyText(player2Monster.currentEnergy);
            dialogueText.text = "Energy restored!";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                // End battle 
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                // Enemy turn
                state = BattleState.PLAYER1TURN;
                Player1Turn();
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            state = BattleState.PLAYER2TURN;
        }
    }

    // Use ranged attack when ranged button is pressed
    public void OnRangedButton2()
    {
        if (state != BattleState.PLAYER2TURN)
        {
            return;
        }
        // Stops players from being able to use abilities over and over
        state = BattleState.PLAYERACTION;
        StartCoroutine(Player2Ranged());
    }

    // Attacked player takes ranged damage and battle ends if they die
    IEnumerator Player2Ranged()
    {
        // Checks if player has enough energy to use ability
        if (player2Monster.rangedCost <= player1Monster.currentEnergy)
        {
            yield return new WaitForSeconds(1f);

            bool isDead = player1Monster.TakeRangedDamage(player2Monster.rangedDamage);
            player2Monster.TakeRangedCost(player2Monster.rangedCost);

            player1HUD.UpdateHP(player1Monster.currentHP);
            player1HUD.UpdateHPText(player1Monster.currentHP);
            dialogueText.text = "The attack landed!";

            player2HUD.UpdateEnergy(player2Monster.currentEnergy);
            player2HUD.UpdateEnergyText(player2Monster.currentEnergy);

            yield return new WaitForSeconds(1f);

            player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
            player2HUD.UpdateEnergy(player2Monster.currentEnergy);
            player2HUD.UpdateEnergyText(player2Monster.currentEnergy);
            dialogueText.text = "Energy restored!";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                // End battle 
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                // Enemy turn
                state = BattleState.PLAYER1TURN;
                Player1Turn();
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            state = BattleState.PLAYER2TURN;
        }
    }

    // Player heals
    public void OnHealButton2()
    {
        if (state != BattleState.PLAYER2TURN)
        {
            return;
        }
        // Stops players from being able to use abilities over and over
        state = BattleState.PLAYERACTION;
        StartCoroutine(Player2Heal());
    }

    // Player heals
    IEnumerator Player2Heal()
    {
        // Checks if player has enough energy to use ability
        if (player2Monster.healCost <= player2Monster.currentEnergy)
        {
            yield return new WaitForSeconds(1f);

            bool isDead = player2Monster.PlayerHeal(player2Monster.healAmount);
            player2Monster.TakeHealCost(player2Monster.healCost);

            player2HUD.UpdateHP(player2Monster.currentHP);
            player2HUD.UpdateHPText(player2Monster.currentHP);
            dialogueText.text = "You healed 15 points!";

            player2HUD.UpdateEnergy(player2Monster.currentEnergy);
            player2HUD.UpdateEnergyText(player2Monster.currentEnergy);

            yield return new WaitForSeconds(1f);

            player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
            player2HUD.UpdateEnergy(player2Monster.currentEnergy);
            player2HUD.UpdateEnergyText(player2Monster.currentEnergy);
            dialogueText.text = "Energy restored!";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                // End battle 
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                // Enemy turn
                state = BattleState.PLAYER1TURN;
                Player1Turn();
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            state = BattleState.PLAYER2TURN;
        }
    }

    // Use special attack when special button is pressed
    public void OnSpecialButton2()
    {
        if (state != BattleState.PLAYER2TURN)
        {
            return;
        }
        // Stops players from being able to use abilities over and over
        state = BattleState.PLAYERACTION;
        StartCoroutine(Player2Special());
    }

    // Attacked player takes special damage and battle ends if they die
    IEnumerator Player2Special()
    {
        // Checks if player has enough energy to use ability
        if (player2Monster.specialCost <= player2Monster.currentEnergy)
        {
            yield return new WaitForSeconds(1f);

            bool isDead = player1Monster.TakeSpecialDamage(player2Monster.specialDamage);
            player2Monster.TakeSpecialCost(player2Monster.specialCost);

            player1HUD.UpdateHP(player1Monster.currentHP);
            player1HUD.UpdateHPText(player1Monster.currentHP);
            dialogueText.text = "The attack landed!";

            player2HUD.UpdateEnergy(player2Monster.currentEnergy);
            player2HUD.UpdateEnergyText(player2Monster.currentEnergy);

            yield return new WaitForSeconds(1f);

            player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
            player2HUD.UpdateEnergy(player2Monster.currentEnergy);
            player2HUD.UpdateEnergyText(player2Monster.currentEnergy);
            dialogueText.text = "Energy restored!";

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                // End battle 
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                // Enemy turn
                state = BattleState.PLAYER1TURN;
                Player1Turn();
            }
        }
        else
        {
            dialogueText.text = "Not Enough Energy!";
            state = BattleState.PLAYER2TURN;
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Player 1 Wins!";
            mmButton.gameObject.SetActive(true);
            backButton.gameObject.SetActive(false);
            abilityPanel.gameObject.SetActive(false);
            abilityPanel2.gameObject.SetActive(false);

            // Wins and losses counters adjusted
            wins1++;
            losses2++;

            SaveData();
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Player 2 Wins!";
            mmButton.gameObject.SetActive(true);
            backButton.gameObject.SetActive(false);
            abilityPanel.gameObject.SetActive(false);
            abilityPanel2.gameObject.SetActive(false);

            // Wins and losses counters adjusted
            wins2++;
            losses1++;

            SaveData();
        }
    }
}
