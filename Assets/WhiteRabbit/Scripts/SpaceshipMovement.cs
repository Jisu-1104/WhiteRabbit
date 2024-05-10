using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceshipMovement : MonoBehaviour
{
    public float movement;
    public float rotate;

    public Transform transform;
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        movement = Mathf.Sin(time);
        rotate = Mathf.Sin(45f + time);
        transform.position = new Vector3(movement, 0, 0);
        transform.rotation = Quaternion.Euler(0f, 0f, -10 * rotate);
    }
}
