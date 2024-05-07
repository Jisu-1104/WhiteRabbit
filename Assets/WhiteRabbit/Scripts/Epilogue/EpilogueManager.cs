using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EpilogueManager : MonoBehaviour
{
    public GameObject conversationPanel;
    public Text talkText;
    public int currentDialogueIndex = 0; // 현재 대사 인덱스
    private string[] dialogues = {
        "원주민의 호의 덕분에 순조로웠다.",
        "이 곳에는 거대 강아지를 비롯한 위험한 생물들이 있다.",
        "원주민은 남은 부품의 개수를 묻고는 내일도 나를 도와주겠다고 했다.\n정말 다행이다.",
        "내가 수어를 배우려고 하는 것을 좋게 봐준 것 같다. \n수어를 중요시 여기는 모양이다.",
        "그럴만도 하다. 할 수 있는 것이 이렇게나 많다니!",
        "수어는 단순한 몸짓이 아니라, 이곳의 많은 것을 이루는 하나의 문화다.",
        "내일이 조금 기대된다.",
        "조난 1일치 일지 끝."
    };

    void UpdateDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            talkText.text = dialogues[currentDialogueIndex]; // 대사 업데이트
        }
        else
        {
            SceneManager.LoadScene("Scene2");
        }
    }

    public void OnButtonClick()
    {
        currentDialogueIndex++; // 다음 대사로 이동
        UpdateDialogue(); // 대사 업데이트
    }
}
