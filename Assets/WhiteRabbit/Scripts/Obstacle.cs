using Platformer;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool playerInRange = false; // �÷��̾ ���� ���� �ִ��� ���θ� ��Ÿ���� ����
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