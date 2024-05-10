using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Text creditsText;
    public float scrollSpeed = 20f;
    public GameObject retry;
    public GameObject quit;

    private bool scrolling = true;

    private void Start()
    {
        // 텍스트의 초기 위치를 아래쪽에 설정합니다.
        RectTransform rectTransform = creditsText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, -rectTransform.sizeDelta.y);
    }

    private void Update()
    {
        if (scrolling)
        {
            // 텍스트를 위로 스크롤합니다.
            RectTransform rectTransform = creditsText.GetComponent<RectTransform>();
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // 텍스트가 화면 위로 벗어나면 스크롤을 멈춥니다.
            if (rectTransform.anchoredPosition.y >= 0)
            {
                scrolling = false;
                scrollSpeed = 0f;
                retry.SetActive(true); quit.SetActive(true);
                creditsText.gameObject.SetActive(false);
            }
        }
    }
}
