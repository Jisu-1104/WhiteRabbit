using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterController : MonoBehaviour
{
    public GameObject Enter;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateEnter", 2.0f);
    }

    void ActivateEnter()
    {
        Enter.SetActive(true); 
    }
}
