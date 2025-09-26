using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{



    private void Start()
    {

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
