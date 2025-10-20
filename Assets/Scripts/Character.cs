using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHP = 50;
    public int currentHP;
    public int maxMana = 200; // Devlog 4
    public int currentMana; // Devlog 4

    private void Start()
    {
        currentHP = maxHP;
        currentMana = maxMana;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        Debug.Log($"{characterName} took {damage} damage. Remaining HP: {currentHP}");
    }

    public bool IsDefeated()
    {
        return currentHP <= 0;
    }
}
