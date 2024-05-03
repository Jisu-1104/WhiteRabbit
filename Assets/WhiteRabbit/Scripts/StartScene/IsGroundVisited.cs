using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundVisited : MonoBehaviour
{
    public GameObject player;
    public GameObject startPlay;
    public GameObject npc;
    public PhysicsMaterial2D newMaterial;
    private bool hasCollided = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !hasCollided)
        {
            npc.SetActive(true);
            startPlay.SetActive(true);
            hasCollided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            // 충돌 종료 시 no bounce Material로 변경
            player.GetComponent<BoxCollider2D>().sharedMaterial = newMaterial;
        }
    }
}
