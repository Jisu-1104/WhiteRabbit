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
            meteo.SetActive(false); //���׿� ��Ȱ��ȭ
            timer.SetActive(false); hp.SetActive(false); //timer, hp ui ��Ȱ��ȭ
            gameOverUI.SetActive(true); //���� ���� UI Ȱ��ȭ
        }
        else if (timer.GetComponent<HUD>().gameTime>=timer.GetComponent<HUD>().maxTime)
        {
            SceneManager.LoadScene("ending");
        }
    }
}
