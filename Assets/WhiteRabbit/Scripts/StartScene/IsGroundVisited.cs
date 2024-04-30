using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundVisited : MonoBehaviour
{
    public GameObject player;
    public GameObject howtoplay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            howtoplay.SetActive(true);
        }
    }
}
