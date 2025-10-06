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
    public void Flee()
    {
        Debug.Log("Player fled the battle!");
        SceneManager.LoadScene("Devlog"); 
    }

    private void UseAbility(string abilityName)
    {

    }
}
