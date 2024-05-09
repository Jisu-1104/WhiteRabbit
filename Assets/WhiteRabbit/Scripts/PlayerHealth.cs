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
        // 플레이어 사망 시 처리할 로직을 추가할 수 있습니다.
        gameObject.SetActive(false); // 플레이어 오브젝트 비활성화 등
    }

    // 현재 체력을 반환하는 메서드
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
