using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{

    [Header("Characters")]
    public Character player;
    public Character enemy;

    [Header("UI Elements")]
    public Button fleeButton;
    public Transform abilityButtonContainer;
    public Button abilityButtonPrefab;

    private List<Ability> playerAbilities = new List<Ability>();
    private List<Ability> enemyAbilities = new List<Ability>();
    private bool playerTurn = true;

    private void Start()
    {
        InitializeAbilities();
        GenerateAbilityButtons();
        fleeButton.onClick.AddListener(Flee);
    }
    private void InitializeAbilities()
    {
   
        bool firstWin = PlayerPrefs.GetInt("FirstWin", 0) == 1;
        //Player Abilities
        playerAbilities.Clear();
        playerAbilities.Add(new Ability("Tackle", 10, 5, "A basic hit."));
        playerAbilities.Add(new Ability("Blast", 15, 10, "A stronger attack."));
        if (firstWin)
            playerAbilities.Add(new Ability("Lightning", 20,15, "A powerful new move!"));

        //Enemy Abilities
        enemyAbilities.Clear();
        enemyAbilities.Add(new Ability("Claw Swipe", 8, 5, "A simple physical strike."));
        enemyAbilities.Add(new Ability("Fire Breath", 14, 10, "A fiery attack that burns energy."));
    }
    private void GenerateAbilityButtons()
    {
        // Clear old buttons
        foreach (Transform child in abilityButtonContainer)
            Destroy(child.gameObject);

        // Create buttons dynamically
        foreach (Ability ability in playerAbilities)
        {
            Button btn = Instantiate(abilityButtonPrefab, abilityButtonContainer);
            btn.GetComponentInChildren<Text>().text = $"{ability.abilityName} (MP: {ability.manaCost})";
            btn.onClick.AddListener(() => OnAbilitySelected(ability));
        }
    }
    private void OnAbilitySelected(Ability ability)
    {
        if (!playerTurn) return;
        if (player.currentMana < ability.manaCost)
        {
            Debug.Log("Not enough mana!");
            return;
        }

        ability.Use(player, enemy);

        if (enemy.IsDefeated())
        {
            PlayerWin();
            return;
        }

        playerTurn = false;
        Invoke(nameof(EnemyTurn), 1.2f);
    }
    private void EnemyTurn()
    {
        var usableAbilities = enemyAbilities
             .Where(a => enemy.currentMana >= a.manaCost)
             .ToList();

        if (usableAbilities.Count == 0)
        {
            Debug.Log("Enemy skips turn due to low mana!");
            playerTurn = true;
            return;
        }

       
        Ability chosenAbility = ChooseEnemyAbility(usableAbilities);
        chosenAbility.Use(enemy, player);

        if (player.IsDefeated())
        {
            Debug.Log("Player defeated!");
            SceneManager.LoadScene("Devlog");
            return;
        }

        playerTurn = true;
    }
    private Ability ChooseEnemyAbility(List<Ability> abilities)
    {

        return null;
    }


    private void PlayerWin()
    {
        if (PlayerPrefs.GetInt("FirstWin", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstWin", 1);
            PlayerPrefs.Save();
            Debug.Log("First win! New ability unlocked for future battles.");
        }

        SceneManager.LoadScene("Devlog");
    }
    public void Flee()
    {
        Debug.Log("Player fled the battle!");
        SceneManager.LoadScene("Devlog");
    }
}
