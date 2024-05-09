using UnityEngine;

public class DestroyAndDamage : MonoBehaviour
{
    public string targetTag = "suwhaObject"; // 대상 태그
    public int damageAmount = 10; // 입힐 피해량
    public PlayerHealth playerHealth; // PlayerHealth 컴포넌트

    // 트리거 충돌시 호출되는 이벤트
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌감지");
                  
        playerHealth.TakeDamage(damageAmount);
        
        
        // 충돌한 오브젝트 파괴         
        Destroy(other.gameObject);        
    }
}
