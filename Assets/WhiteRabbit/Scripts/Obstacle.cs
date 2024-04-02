using Platformer;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool playerInRange = false; // �÷��̾ ���� ���� �ִ��� ���θ� ��Ÿ���� ����
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

    // �ٸ� �ڵ忡�� ȣ��� �޼���
    public void DisableObstacle()
    {
        if (playerInRange)
        {
            gameObject.SetActive(false);
        }
    }
}