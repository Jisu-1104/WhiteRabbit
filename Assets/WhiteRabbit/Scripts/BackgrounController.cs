using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrounController : MonoBehaviour
{
    private MeshRenderer renderer;
    public float speed;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        renderer.material.mainTextureOffset = new Vector2(0,offset);
    }
}
