using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject player;
    public GameObject HP;
    public void SwitchSceneBasedOnCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // 현재 씬에 따라 다른 씬으로 이동
        switch (sceneName)
        {
            case "StartScene":
                SceneManager.LoadScene("Intro");
                break;
            case "Intro":
                SceneManager.LoadScene("Prologue");
                break;
            case "Prologue":
                SceneManager.LoadScene("Scene1");
                break;
            case "Scene1":
                SceneManager.LoadScene("Epilogue");
                break;
            case "Epilogue":
                SceneManager.LoadScene("Scene2");
                break;
            case "Scene2":
                SceneManager.LoadScene("StartScene");
                break;
            case "ending":
                SceneManager.LoadScene("StartScene");
                break;

            default:
                Debug.LogWarning("Unknown scene: " + sceneName);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            SwitchSceneBasedOnCurrentScene();
        }
    }

    public void QuitGame()
    {
        // 게임 종료
        Application.Quit();
    }
}
