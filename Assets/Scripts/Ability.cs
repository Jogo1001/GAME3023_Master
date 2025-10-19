using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class Ability
{
    public string abilityName;
    public int power;
    public int manaCost;
    public string description;

    public Ability(string name, int power, int manaCost, string description)
    {
        this.abilityName = name;
        this.power = power;
        this.manaCost = manaCost;
        this.description = description;
    }
    public void Use(Character user, Character target)
    {
        if (user.currentMana < manaCost)
        {
            Debug.Log($"{user.characterName} tried to use {abilityName} but didn’t have enough mana!");
            return;
        }

        user.currentMana -= manaCost;
        target.TakeDamage(power);
        Debug.Log($"{user.characterName} used {abilityName} on {target.characterName}, dealing {power} damage!");
    }
}

