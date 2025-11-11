using UnityEngine;
using System.IO;


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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
