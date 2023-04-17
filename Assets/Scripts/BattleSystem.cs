using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Creates different states or stages of game 
public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, PLAYERACTION, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    // Buttons and objects to become active and not active when match is complete
    public Button mmButton;
    public Button backButton;
    public GameObject abilityPanel;

    // Accesses prefabs
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    // Player location
    public Transform player1Location;
    public Transform player2Location;

    // Monster variables
    Monster player1Monster;
    Monster player2Monster;

    //public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueText;

    // References HUDs
    public BattleHUD player1HUD;
    public BattleHUD player2HUD;

    // Calling battle state
    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        //StartCoroutine(PrepareBattle());
    }

    public void StartGame()
    {
        StartCoroutine(PrepareBattle());
    }

    IEnumerator PrepareBattle()
    {
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
        // Special attack when player 2 has more than half energy and HP is greater than 20%
        if (player2Monster.currentEnergy > player2Monster.maxEnergy/2 && player2Monster.currentHP > player2Monster.maxHP/5)
        {
            if (player2Monster.specialCost <= player2Monster.currentEnergy)
            {
                dialogueText.text = "Player 2's turn!";

                yield return new WaitForSeconds(1f);

                bool isDead = player1Monster.TakeSpecialDamage(player2Monster.specialDamage);
                player2Monster.TakeSpecialCost(player2Monster.specialCost);

                player1HUD.UpdateHP(player1Monster.currentHP);
                player1HUD.UpdateHPText(player1Monster.currentHP);
                dialogueText.text = "The attack landed!";

                player2HUD.UpdateEnergy(player2Monster.currentEnergy);

                yield return new WaitForSeconds(1f);

                player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
                player2HUD.UpdateEnergy(player2Monster.currentEnergy);
                dialogueText.text = "Energy restored!";

                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    // End battle 
                    state = BattleState.LOST;
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
                state = BattleState.PLAYER1TURN;
            }
        }
        // Ranged attack when player 2 enough energy but is below 50% and HP is greater than 20%
        else if (player2Monster.currentEnergy <= player2Monster.maxEnergy/2 && player2Monster.currentEnergy >= player2Monster.rangedCost && player2Monster.currentHP > player2Monster.maxHP/5)
        {
            if (player2Monster.rangedCost <= player2Monster.currentEnergy)
            {
                dialogueText.text = "Player 2's turn!";

                yield return new WaitForSeconds(1f);

                bool isDead = player1Monster.TakeRangedDamage(player2Monster.rangedDamage);
                player2Monster.TakeRangedCost(player2Monster.rangedCost);

                player1HUD.UpdateHP(player1Monster.currentHP);
                player1HUD.UpdateHPText(player1Monster.currentHP);
                dialogueText.text = "The attack landed!";

                player2HUD.UpdateEnergy(player2Monster.currentEnergy);

                yield return new WaitForSeconds(1f);
                player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
                player2HUD.UpdateEnergy(player2Monster.currentEnergy);
                dialogueText.text = "Energy restored!";

                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    // End battle 
                    state = BattleState.LOST;
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
                state = BattleState.PLAYER1TURN;
            }
        }
        // Melee when player 2 has enough energy and HP is greater than 20%
        else if (player2Monster.currentEnergy <= player2Monster.rangedCost && player2Monster.currentHP > player2Monster.maxHP/5)
        {
            if (player2Monster.meleeCost <= player2Monster.currentEnergy)
            {
                dialogueText.text = "Player 2's turn!";

                yield return new WaitForSeconds(1f);

                bool isDead = player1Monster.TakeMeleeDamage(player2Monster.meleeDamage);
                player2Monster.TakeMeleeCost(player2Monster.meleeCost);

                player1HUD.UpdateHP(player1Monster.currentHP);
                player1HUD.UpdateHPText(player1Monster.currentHP);
                dialogueText.text = "The attack landed!";

                player2HUD.UpdateEnergy(player2Monster.currentEnergy);

                yield return new WaitForSeconds(1f);
                player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
                player2HUD.UpdateEnergy(player2Monster.currentEnergy);
                dialogueText.text = "Energy restored!";

                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    // End battle 
                    state = BattleState.LOST;
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
                state = BattleState.PLAYER1TURN;
            }
        }
        // Heal when player 2's HP is below 20% or attacks if not enough energy
        else if (player2Monster.currentHP <= player2Monster.maxHP/5)
        {
            if (player2Monster.healCost <= player2Monster.currentEnergy)
            {
                yield return new WaitForSeconds(1f);

                bool isDead = player2Monster.PlayerHeal(player2Monster.healAmount);
                player2Monster.TakeHealCost(player2Monster.healCost);

                player2HUD.UpdateHP(player2Monster.currentHP);
                dialogueText.text = "You healed 15 points";

                player2HUD.UpdateEnergy(player2Monster.currentEnergy);

                yield return new WaitForSeconds(1f);

                player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
                player2HUD.UpdateEnergy(player2Monster.currentEnergy);
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
                 dialogueText.text = "Player 2's turn!";

                yield return new WaitForSeconds(1f);

                bool isDead = player1Monster.TakeMeleeDamage(player2Monster.meleeDamage);
                player2Monster.TakeMeleeCost(player2Monster.meleeCost);

                player1HUD.UpdateHP(player1Monster.currentHP);
                player1HUD.UpdateHPText(player1Monster.currentHP);
                dialogueText.text = "The attack landed!";

                player2HUD.UpdateEnergy(player2Monster.currentEnergy);

                yield return new WaitForSeconds(1f);
                player2Monster.EnergyPerTurn(player2Monster.energyPerTurn);
                player2HUD.UpdateEnergy(player2Monster.currentEnergy);
                dialogueText.text = "Energy Restored!";

                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    // End battle 
                    state = BattleState.LOST;
                    EndBattle();
                }
                else
                {
                    // Enemy turn
                    state = BattleState.PLAYER1TURN;
                    Player1Turn();
                }
            }
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
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost the battle!";
            mmButton.gameObject.SetActive(true);
            backButton.gameObject.SetActive(false);
            abilityPanel.gameObject.SetActive(false);
        }
    }
}
