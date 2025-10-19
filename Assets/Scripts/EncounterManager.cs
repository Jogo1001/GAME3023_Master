using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // ?? Important for TextMesh Pro

public class EncounterManager : MonoBehaviour
{
    [Header("Characters")]
    public Character player;
    public Character enemy;

    [Header("UI Elements")]
    public Button fleeButton;
    public Transform abilityButtonContainer;
    public Button abilityButtonPrefab;
    public TextMeshProUGUI battleLogText; 
    public TextMeshProUGUI playerStatsText;
    public TextMeshProUGUI enemyStatsText;
    public GameObject battleLogPanel; 
    public Button toggleLogButton;

    private List<Ability> playerAbilities = new List<Ability>();
    private List<Ability> enemyAbilities = new List<Ability>();
    private bool playerTurn = true;
    private bool isBattleLogVisible = false;

    private void Start()
    {
        InitializeAbilities();
        GenerateAbilityButtons();
        fleeButton.onClick.AddListener(Flee);
        UpdateStatsUI();
        LogMessage("A wild enemy appears!");

        toggleLogButton.onClick.AddListener(ToggleBattleLog);


        battleLogPanel.SetActive(false);
    }

    private void InitializeAbilities()
    {
        bool firstWin = PlayerPrefs.GetInt("FirstWin", 0) == 1;

        // Player Abilities
        playerAbilities.Clear();
        playerAbilities.Add(new Ability("Tackle", 10, 5, "A basic hit."));
        playerAbilities.Add(new Ability("Blast", 15, 10, "A stronger attack."));
        if (firstWin)
            playerAbilities.Add(new Ability("Lightning", 20, 15, "A powerful new move!"));

        // Enemy Abilities
        enemyAbilities.Clear();
        enemyAbilities.Add(new Ability("Claw Swipe", 8, 5, "A simple physical strike."));
        enemyAbilities.Add(new Ability("Fire Breath", 14, 10, "A fiery attack that burns energy."));
    }

    private void GenerateAbilityButtons()
    {
        foreach (Transform child in abilityButtonContainer)
            Destroy(child.gameObject);
        foreach (Ability ability in playerAbilities)
        {
            Button btn = Instantiate(abilityButtonPrefab, abilityButtonContainer);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = ability.abilityName; 
            btn.onClick.AddListener(() => OnAbilitySelected(ability));
        }
    }

    private void OnAbilitySelected(Ability ability)
    {
        if (!playerTurn) return;

        if (player.currentMana < ability.manaCost)
        {
            LogMessage("Not enough mana!");
            return;
        }

        ability.Use(player, enemy, LogMessage);
        UpdateStatsUI();

        if (enemy.IsDefeated())
        {
            PlayerWin();
            return;
        }

        playerTurn = false;
        Invoke(nameof(EnemyTurn), 1f);
    }

    private void EnemyTurn()
    {
        var usableAbilities = enemyAbilities
            .Where(a => enemy.currentMana >= a.manaCost)
            .ToList();

        if (usableAbilities.Count == 0)
        {
            LogMessage("Enemy skips turn due to low mana!");
            playerTurn = true;
            return;
        }

        Ability chosenAbility = ChooseEnemyAbility(usableAbilities);
        chosenAbility.Use(enemy, player, LogMessage);
        UpdateStatsUI();

        if (player.IsDefeated())
        {
            LogMessage("You were defeated...");
            Invoke(nameof(ReturnToOverworld), 2f);
            return;
        }

        playerTurn = true;
    }

    private Ability ChooseEnemyAbility(List<Ability> abilities)
    {
        bool goStrong = Random.value < 0.6f;
        return goStrong
            ? abilities.OrderByDescending(a => a.power).First()
            : abilities[Random.Range(0, abilities.Count)];
    }

    private void PlayerWin()
    {
        LogMessage("Enemy defeated!");

        if (PlayerPrefs.GetInt("FirstWin", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstWin", 1);
            PlayerPrefs.Save();
            LogMessage("First win! New ability unlocked for future battles!");
        }

        Invoke(nameof(ReturnToOverworld), 2f);
    }

    public void Flee()
    {
        LogMessage("You fled from battle!");
        Invoke(nameof(ReturnToOverworld), 1.5f);
    }

    private void ReturnToOverworld()
    {
        SceneManager.LoadScene("Devlog");
    }

    private void LogMessage(string message)
    {
        if (battleLogText == null)
        {
            Debug.LogWarning("Battle Log Text not assigned!");
            return;
        }

      
        battleLogText.text += message + "\n";
    }

    private void UpdateStatsUI()
    {
        playerStatsText.text = $"{player.characterName}\nHP: {player.currentHP}/{player.maxHP}\nMP: {player.currentMana}/{player.maxMana}";
        enemyStatsText.text = $"{enemy.characterName}\nHP: {enemy.currentHP}/{enemy.maxHP}\nMP: {enemy.currentMana}/{enemy.maxMana}";
    }
    private void ToggleBattleLog()
    {
        isBattleLogVisible = !isBattleLogVisible;
        battleLogPanel.SetActive(isBattleLogVisible);

        string newLabel = isBattleLogVisible ? "Hide Log" : "Battle Log";
        toggleLogButton.GetComponentInChildren<TextMeshProUGUI>().text = newLabel;

    }
}
