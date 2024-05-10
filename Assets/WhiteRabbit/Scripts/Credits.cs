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
        // �ؽ�Ʈ�� �ʱ� ��ġ�� �Ʒ��ʿ� �����մϴ�.
        RectTransform rectTransform = creditsText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, -rectTransform.sizeDelta.y);
    }

    private void Update()
    {
        if (scrolling)
        {
            // �ؽ�Ʈ�� ���� ��ũ���մϴ�.
            RectTransform rectTransform = creditsText.GetComponent<RectTransform>();
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // �ؽ�Ʈ�� ȭ�� ���� ����� ��ũ���� ����ϴ�.
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
