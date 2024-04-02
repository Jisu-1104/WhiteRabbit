using Platformer;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool playerInRange = false; // 플레이어가 범위 내에 있는지 여부를 나타내는 변수
    public PlayerController playerController;
    public string obsalpha;

    private void Update()
    {
        if (playerInRange && (playerController.alphabet == obsalpha))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // 다른 코드에서 호출될 메서드
    public void DisableObstacle()
    {
        if (playerInRange)
        {
            gameObject.SetActive(false);
        }
    }
}