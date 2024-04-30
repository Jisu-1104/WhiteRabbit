using Platformer;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool playerInRange = false; // 플레이어가 범위 내에 있는지 여부를 나타내는 변수
    public GameObject player;
    public string obsalpha;
    private string gesture;

    private void Update()
    {
        gesture = player.GetComponent<KNNPrediction>().gesture;
        if (playerInRange && (gesture == obsalpha))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}