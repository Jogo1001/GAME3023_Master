using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Character player;


    [Header("UI Buttons")]
    public GameObject saveButton;
    public GameObject continueButton;
    void Start()
    {
        if (continueButton != null)
            continueButton.SetActive(SaveSystem.HasSaveData());
    }
    public void SaveGame()
    {

        SaveSystem.SaveGame(player);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Devlog");
    }
    public void QuitGame()
    {
       Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
      
    }
    

}
