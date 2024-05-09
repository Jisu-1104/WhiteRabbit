using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // �÷��̾� ��� �� ó���� ������ �߰��� �� �ֽ��ϴ�.
        gameObject.SetActive(false); // �÷��̾� ������Ʈ ��Ȱ��ȭ ��
    }

    // ���� ü���� ��ȯ�ϴ� �޼���
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
