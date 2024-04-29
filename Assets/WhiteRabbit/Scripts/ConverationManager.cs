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

    private void Start()
    {
        suwhaImageComponent = suwhaImage.GetComponent<Image>();
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
                talkText.text = "큰 몸집이 매력적인 기억니은디귿리을 강아지야. 지화 ㄱㄴㄷㄹ을 해봐.";
                lastPoint = collision.gameObject;
                suwhaImageComponent.sprite = sprite[0];
                suwhaImage.SetActive(true);
                /*
                if(gesture=='ㄱ')
                {
                    suwhaImageComponent.sprite = sprite[1];
                    if(gesture=='ㄴ') {
                        suwhaImageComponent.sprite = sprite[2];
                        if(gesture=='ㄷ') {
                            suwhaImageComponent.sprite = sprite[3];
                            if(gesture=='ㄹ') {
                                Destroy(GNDL);
                            }
                        }
                    }
                }
                */
                break;
            case "GNDLEndPoint":
                talkText.text = "수고했어! 이제 네 우주선 부품까지 거의 다 왔어.";
                break;

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        conversationPanel.SetActive(false);
        suwhaImage.SetActive(false);
        if (collision.CompareTag("Point"))
        {
            collision.gameObject.SetActive(false);
        }
    }

}
