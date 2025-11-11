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
    public void SaveGameButtonClicked()
    {

        SaveSystem.SaveGame(player);

    }


    public void OnContinueButtonClicked()
    {
        if (SaveSystem.HasSaveData())
        {
            PlayerData data = SaveSystem.LoadGame();
            SceneManager.LoadScene("Devlog");

            PlayerPrefs.SetFloat("SavedX", data.positionX);
            PlayerPrefs.SetFloat("SavedY", data.positionY);
            PlayerPrefs.SetInt("SavedHP", data.currentHP);
            PlayerPrefs.SetInt("SavedMana", data.currentMana);
        }
    }
    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene("Devlog");
    }
    public void QuitGameButtonClicked()
    {
       Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
      
    }
    

}
