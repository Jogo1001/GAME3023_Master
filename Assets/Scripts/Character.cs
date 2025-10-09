using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHP = 50;
    public int currentHP;

    private void Start()
    {
        currentHP = maxHP;
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
