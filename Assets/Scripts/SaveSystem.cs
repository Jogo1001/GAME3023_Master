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
    public static bool HasSaveData() => File.Exists(saveFile);
}
