using UnityEngine;

public class DestroyAndDamage : MonoBehaviour
{
    public string targetTag = "suwhaObject"; // ��� �±�
    public int damageAmount = 10; // ���� ���ط�
    public PlayerHealth playerHealth; // PlayerHealth ������Ʈ

    // Ʈ���� �浹�� ȣ��Ǵ� �̺�Ʈ
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�浹����");
                  
        playerHealth.TakeDamage(damageAmount);
        
        
        // �浹�� ������Ʈ �ı�         
        Destroy(other.gameObject);        
    }
}
