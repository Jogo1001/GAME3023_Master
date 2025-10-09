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
    public void Use(Character user, Character target)
    {
        target.TakeDamage(power);
        Debug.Log($"{user.characterName} used {abilityName} on {target.characterName}, dealing {power} damage!");
    }
}
