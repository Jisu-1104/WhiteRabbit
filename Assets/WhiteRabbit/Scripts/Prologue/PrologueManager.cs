using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
    public GameObject player;
    public GameObject conversationPanel;
    public Text talkText;
    public GameObject npc; public GameObject npc1; public GameObject npc2; public GameObject npc3; public GameObject npc4;
    private Color blackcolor = Color.black;
    private Color whitecolor = Color.white;

    public int currentDialogueIndex = 0; // 현재 대사 인덱스
    private string[] dialogues = {
        "분류 완료.",
        "이 곳의 주민들은 수어를 사용합니다. 행성 지구 내의 한국 수어와의 일치율 99%.",
        "번역 기능 활성화", //3
        "이때에는 정말 큰일 나는 줄 알았다. \n수어는 소리가 아니라, 몸을 사용하는 언어기 때문이다.",
        "AI 번역기를 통해 의미를 알 수 있게 된 것까진 좋았지만, \n내가 수어를 모르는 한, 나의 의견을 표현할 수 없었다.",
        "내 우주선이 재가동되는 데에 필요한 부품 8개를 \n이 행성에 잃어버렸다...",
        "아무 정보도 없는 행성을 아무 도움도 없이 돌아다니는 것은 너무 위험한 일이다.\n다시 집으로 돌아가기 위해서는 원주민들의 도움이 절실했다.",
        "나는 최대한 무해한 몸짓을 하려 노력했다.", //8
        "...",//9
        "그 물체는 아마 저 외지인이 타고 온 물건에서 떨어졌을 거에요. \n뭔지도 모르는 물건이니, 외지인이 옮기게 합시다.",//10
        "나는 필사적으로 내가 옮기겠다는 것을 표현했다. \n주민들이 잘못된 방법으로 옮겼다가는 고장이 날 수도 있으니.", //11
        "자기가 옮기겠다는 것 같은데요?", //12
        "좋아. 그럼 내일 아침에 출발하자.", //13
        "내가 고개를 끄덕이자 원주민들은 풀숲으로 사라졌다.", //14
        "내일부터는 본격적으로 이 행성을 탐험해야 한다.",
        "나에게 우주의 가호가 있기를...",
        "조난 0일차 일지 끝"
    }; // 변경될 대사들

    // Start is called before the first frame update
    void Start()
    {
        blackcolor.a = 0.6f;
        whitecolor.a = 0.6f;
        Invoke("ActivatePlayerObject", 2.0f); // 2초 뒤에 ActivatePlayerObject 함수 호출
    }
    void ActivatePlayerObject()
    {
        player.SetActive(true); // 플레이어 오브젝트 활성화
        Image panelRenderer = conversationPanel.GetComponent<Image>();
        panelRenderer.color = blackcolor;
        talkText.color = Color.white;
        conversationPanel.SetActive(true); //대사창 활성화
        talkText.text = "언어 분류 중...";
        currentDialogueIndex = 0; // 현재 대사 인덱스
}
    void UpdateDialogue()
    {
        Image panelRenderer = conversationPanel.GetComponent<Image>();
        if (currentDialogueIndex < dialogues.Length)
        {
            if (currentDialogueIndex == 4)
            {
                npc.GetComponent<ObjectMover>().enabled = false;
                npc1.GetComponent<ObjectMover>().enabled = false;
                npc2.GetComponent<ObjectMover>().enabled = false;
                npc3.GetComponent<ObjectMover>().enabled = false;
                npc4.GetComponent<ObjectMover>().enabled = false;
            }
            if (currentDialogueIndex == 8)
            {
                conversationPanel.SetActive(false);
                player.GetComponent<Rigidbody2D>().AddForce(transform.up * 6, ForceMode2D.Impulse);
                panelRenderer.color = whitecolor;
                talkText.color = Color.black;
                Invoke("ActivatePanel", 1f);
            }
            if (currentDialogueIndex == 9)
            {
                conversationPanel.SetActive(false);
                npc.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 2.0f, ForceMode2D.Impulse);
                Invoke("StopMovement", 1.0f);
                panelRenderer.color = whitecolor;
                talkText.color = Color.black;
                conversationPanel.SetActive(true);
            }
            if (currentDialogueIndex == 10)
            {
                panelRenderer.color = blackcolor;
                talkText.color = Color.white;
            }
            if (currentDialogueIndex == 11)
            {
                panelRenderer.color = whitecolor;
                talkText.color = Color.black;
            }
            if (currentDialogueIndex == 13)
            {
                panelRenderer.color = blackcolor;
                talkText.color = Color.white;
                npc.SetActive(false); npc1.SetActive(false); npc2.SetActive(false); npc3.SetActive(false); npc4.SetActive(false);
            }
            talkText.text = dialogues[currentDialogueIndex]; // 대사 업데이트
            
        }
        else
        {
            SceneManager.LoadScene("Scene1");
        }
    }
    public void OnButtonClick()
    {
        currentDialogueIndex++; // 다음 대사로 이동
        UpdateDialogue(); // 대사 업데이트
    }
    void StopMovement()
    {
        npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // 속도를 0으로 설정하여 멈춤
    }
    void ActivatePanel()
    {
        conversationPanel.SetActive(true); // 대화 패널 활성화
    }
}
