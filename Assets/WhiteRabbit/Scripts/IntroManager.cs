using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject Image;
    private Image ImageComponent;
    public Sprite[] sprite = new Sprite[4];
    public int currentSprite = 0;
    // Start is called before the first frame update
    void Start()
    {
        ImageComponent = Image.GetComponent<Image>();
    }

    public void OnButtonClick()
    {
        if (currentSprite >= 3)
        {
            SceneManager.LoadScene("Prologue");
        }
        else
        {
            ImageComponent.sprite = sprite[currentSprite];
            currentSprite++;
        }
    }

}
