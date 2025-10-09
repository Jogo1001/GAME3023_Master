using System.Collections.Generic;
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

        playerAbilities.Clear();
        playerAbilities.Add(new Ability("Tackle", 10, "A basic hit."));
        playerAbilities.Add(new Ability("Blast", 15, "A stronger attack."));

        if (firstWin)
            playerAbilities.Add(new Ability("Lightning", 20, "A powerful new move!"));
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
            btn.GetComponentInChildren<Text>().text = ability.abilityName;  
            btn.onClick.AddListener(() => OnAbilitySelected(ability));
        }
    }
    private void OnAbilitySelected(Ability ability)
    {
        if (!playerTurn) return;

        ability.Use(player, enemy);

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
        Debug.Log("Enemy attacks!");
        player.TakeDamage(5);

        if (player.IsDefeated())
        {
            Debug.Log("Player defeated!");
            SceneManager.LoadScene("Devlog");
            return;
        }

        playerTurn = true;
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
