using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public GameObject Image;
    private Image ImageComponent;
    public Sprite[] sprite = new Sprite[3];
    public int currentSprite = 0;
    // Start is called before the first frame update
    void Start()
    {
        ImageComponent = Image.GetComponent<Image>();
    }

    public void OnButtonClick()
    {
        ImageComponent.sprite = sprite[currentSprite];
        currentSprite++;
    }
}
