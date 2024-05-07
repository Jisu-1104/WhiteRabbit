using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConverationManager : MonoBehaviour
{
    public GameObject conversationPanel;
    public GameObject suwhaImage;
    private Image suwhaImageComponent;
    public Sprite[] sprite = new Sprite[4];
    public Text talkText;
    public GameObject lastPoint;
    public GameObject player;
    public GameObject GNDL;
    private string gesture;
    private bool gestureG = false; // 'ㄱ'을 수행했는지 여부를 나타내는 변수
    private bool gestureN = false; // 'ㄴ'을 수행했는지 여부를 나타내는 변수
    private bool gestureD = false; // 'ㄷ'을 수행했는지 여부를 나타내는 변수

    private void Start()
    {
        suwhaImageComponent = suwhaImage.GetComponent<Image>();
    }

    private void Update()
    {
        gesture = player.GetComponent<KNNPrediction>().gesture;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "NeedSpawn")
        {
            player.transform.position = lastPoint.transform.position;
        }
        if (collision.CompareTag("Point"))
        {
            conversationPanel.SetActive(true);
        } 
        switch (collision.gameObject.name)
        {
            case "StartPoint":
                talkText.text = "네 우주선이 떨어트린 부품이 근처에 있어. 내가 찾는 것을 도와줄게.";
                lastPoint = collision.gameObject;
                break;
            case "Point1":
                talkText.text = "네가 우리 별의 생명들을 존중한다면 그들도 너를 도와줄거야.";
                break;
            case "GieokPoint":
                talkText.text = "기억 풀이야. 지화 ㄱ을 해봐.";
                suwhaImageComponent.sprite = sprite[0];
                suwhaImage.SetActive(true);
                lastPoint = collision.gameObject;
                break;
            case "GieokEndPoint":
                talkText.text = "대단해! 우리 말에 소질이 있나봐.";
                lastPoint = collision.gameObject;
                break;
            case "NieonPoint":
                talkText.text = "앉아있는 모습이 귀여운 니은 고양이야. 지화 ㄴ을 해봐.";
                suwhaImageComponent.sprite = sprite[1];
                suwhaImage.SetActive(true);
                lastPoint = collision.gameObject;
                break;
            case "DigeodPoint":
                talkText.text = "언니 양말을 훔친 도둑 디귿 강아지야. 지화 ㄷ을 해봐.";
                suwhaImageComponent.sprite = sprite[2];
                suwhaImage.SetActive(true);
                lastPoint = collision.gameObject;
                break;
            case "LieolPoint":
                talkText.text = "온순한 리을 뱀이야. 지화 ㄹ을 해봐.";
                lastPoint = collision.gameObject;
                suwhaImageComponent.sprite = sprite[3];
                suwhaImage.SetActive(true);
                break;
            case "GNDLPoint":
                talkText.text = "큰 몸집이 매력적인 기역니은디귿리을 강아지야. 지화 ㄱㄴㄷㄹ을 순서대로 해봐.";
                lastPoint = collision.gameObject;
                suwhaImageComponent.sprite = sprite[0];
                suwhaImage.SetActive(true);
                break;
            case "GNDLEndPoint":
                talkText.text = "수고했어! 이제 네 우주선 부품까지 거의 다 왔어.";
                break;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "GNDLPoint")
        {
            if (gesture.Equals("ㄱ") && !gestureG)
            {
                suwhaImageComponent.sprite = sprite[1];
                gestureG = true; // 'ㄱ' 수행 표시
            }
            else if (gesture.Equals("ㄴ") && gestureG && !gestureN)
            {
                suwhaImageComponent.sprite = sprite[2];
                gestureN = true; // 'ㄴ' 수행 표시
            }
            else if (gesture.Equals("ㄷ") && gestureN && !gestureD)
            {
                suwhaImageComponent.sprite = sprite[3];
                gestureD = true; // 'ㄷ' 수행 표시
            }
            else if (gesture.Equals("ㄹ") && gestureD)
            {
                Destroy(GNDL);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // conversationPanel이나 suwhaImage가 null이 아닌지 확인
        if (conversationPanel != null)
        {
            conversationPanel.SetActive(false);
        }
        if (suwhaImage != null)
        {
            suwhaImage.SetActive(false);
        }

        // 충돌한 오브젝트가 Point 태그를 가진 경우
        if (collision.CompareTag("Point"))
        {
            // 충돌한 게임 오브젝트가 null이 아닌지 확인
            if (collision.gameObject != null)
            {
                // 게임 오브젝트를 비활성화
                collision.gameObject.SetActive(false);
            }
        }
    }

}
