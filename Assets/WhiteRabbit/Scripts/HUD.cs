using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Health, Time }
    public InfoType type;
    public GameObject player;
    PlayerHealth playerHealth;
    public float maxHp;
    public float curHp;
    public float gameTime = 0;
    public float maxTime = 60;

    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(type) { 
            case InfoType.Health:
                curHp = player.GetComponent<PlayerHealth>().currentHealth;
                maxHp = player.GetComponent<PlayerHealth>().maxHealth;
                slider.value = (curHp / maxHp);
                break; 

            case InfoType.Time:
                gameTime += Time.deltaTime;
                slider.value = gameTime / maxTime;
                break;
        }
    }

}
