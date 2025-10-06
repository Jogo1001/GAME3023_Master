using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class Ability
{
    public string abilityName;
    public int power;
    public string description;

    public Ability(string name, int power, string description)
    {
        this.abilityName = name;
        this.power = power;
        this.description = description;
    }

}
