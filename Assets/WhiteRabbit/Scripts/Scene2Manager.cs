using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Manager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverUI;
    public GameObject timer; public GameObject hp;
    public GameObject meteo;
    public GameObject sceneSwitcher;


    private void Update()
    {
        if (player.GetComponent<PlayerHealth>().isPlayerDead == true)
        {
            meteo.SetActive(false); //메테오 비활성화
            timer.SetActive(false); hp.SetActive(false); //timer, hp ui 비활성화
            gameOverUI.SetActive(true); //게임 오버 UI 활성화
        }
        else if (timer.GetComponent<HUD>().gameTime>=timer.GetComponent<HUD>().maxTime)
        {
            SceneManager.LoadScene("ending");
        }
    }
}
