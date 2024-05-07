using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EpilogueManager : MonoBehaviour
{
    public GameObject conversationPanel;
    public Text talkText;
    public int currentDialogueIndex = 0; // ���� ��� �ε���
    private string[] dialogues = {
        "���ֹ��� ȣ�� ���п� �����ο���.",
        "�� ������ �Ŵ� �������� ����� ������ �������� �ִ�.",
        "���ֹ��� ���� ��ǰ�� ������ ����� ���ϵ� ���� �����ְڴٰ� �ߴ�.\n���� �����̴�.",
        "���� ��� ������ �ϴ� ���� ���� ���� �� ����. \n��� �߿�� ����� ����̴�.",
        "�׷����� �ϴ�. �� �� �ִ� ���� �̷��Գ� ���ٴ�!",
        "����� �ܼ��� ������ �ƴ϶�, �̰��� ���� ���� �̷�� �ϳ��� ��ȭ��.",
        "������ ���� ���ȴ�.",
        "���� 1��ġ ���� ��."
    };

    void UpdateDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            talkText.text = dialogues[currentDialogueIndex]; // ��� ������Ʈ
        }
        else
        {
            SceneManager.LoadScene("Scene2");
        }
    }

    public void OnButtonClick()
    {
        currentDialogueIndex++; // ���� ���� �̵�
        UpdateDialogue(); // ��� ������Ʈ
    }
}
