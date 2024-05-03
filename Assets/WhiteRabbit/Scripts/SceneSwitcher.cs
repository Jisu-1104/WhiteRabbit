using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject player;
    public void SwitchSceneBasedOnCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // ���� ���� ���� �ٸ� ������ �̵�
        switch (sceneName)
        {
            case "StartScene":
                SceneManager.LoadScene("Prologue");
                break;
            case "Prologue":
                SceneManager.LoadScene("Scene1");
                break;
            case "Scene1":
                SceneManager.LoadScene("Scene2");
                break;
            case "Scene2":
                SceneManager.LoadScene("ending");
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
}
