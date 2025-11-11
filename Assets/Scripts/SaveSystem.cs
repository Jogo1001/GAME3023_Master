using UnityEngine;
using System.IO;


[System.Serializable]
public class PlayerData
{
    public float positionX;
    public float positionY;
    public int currentHP;
    public int currentMana;


}


public class SaveSystem
{
    private static string saveFile = Application.persistentDataPath + "/save.json";
   

    public static void SaveGame(Character player)
    {
        PlayerData data = new PlayerData
        {
            positionX = player.transform.position.x,
            positionY = player.transform.position.y,  
            currentHP = player.currentHP,
            currentMana = player.currentMana


        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFile, json);
        Debug.Log($"Game saved at {saveFile}");
    }

    public static PlayerData LoadGame()
    {
        if(File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Game loaded successfully.");
            return data;
        }

        Debug.LogWarning("No save file found!");
        return null;
    }
    public static void DeleteSave()
    {
        if (File.Exists(saveFile))
            File.Delete(saveFile);
    }
    public static bool HasSaveData() => File.Exists(saveFile);
}
