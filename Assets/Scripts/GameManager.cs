using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{



    [Header("UI Buttons")]
    public GameObject saveButton;
    public GameObject continueButton;
    void Start()
    {
        
    }



    public void NewGame()
    {
        SceneManager.LoadScene("Devlog");
    }


}
