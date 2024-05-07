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

    public int currentDialogueIndex = 0; // ���� ��� �ε���
    private string[] dialogues = {
        "�з� �Ϸ�.",
        "�� ���� �ֹε��� ��� ����մϴ�. �༺ ���� ���� �ѱ� ������� ��ġ�� 99%.",
        "���� ��� Ȱ��ȭ", //3
        "�̶����� ���� ū�� ���� �� �˾Ҵ�. \n����� �Ҹ��� �ƴ϶�, ���� ����ϴ� ���� �����̴�.",
        "AI �����⸦ ���� �ǹ̸� �� �� �ְ� �� �ͱ��� ��������, \n���� ��� �𸣴� ��, ���� �ǰ��� ǥ���� �� ������.",
        "�� ���ּ��� �簡���Ǵ� ���� �ʿ��� ��ǰ 8���� \n�� �༺�� �Ҿ���ȴ�...",
        "�ƹ� ������ ���� �༺�� �ƹ� ���� ���� ���ƴٴϴ� ���� �ʹ� ������ ���̴�.\n�ٽ� ������ ���ư��� ���ؼ��� ���ֹε��� ������ �����ߴ�.",
        "���� �ִ��� ������ ������ �Ϸ� ����ߴ�.", //8
        "...",//9
        "�� ��ü�� �Ƹ� �� �������� Ÿ�� �� ���ǿ��� �������� �ſ���. \n������ �𸣴� �����̴�, �������� �ű�� �սô�.",//10
        "���� �ʻ������� ���� �ű�ڴٴ� ���� ǥ���ߴ�. \n�ֹε��� �߸��� ������� �Ű�ٰ��� ������ �� ���� ������.", //11
        "�ڱⰡ �ű�ڴٴ� �� ��������?", //12
        "����. �׷� ���� ��ħ�� �������.", //13
        "���� ���� �������� ���ֹε��� Ǯ������ �������.", //14
        "���Ϻ��ʹ� ���������� �� �༺�� Ž���ؾ� �Ѵ�.",
        "������ ������ ��ȣ�� �ֱ⸦...",
        "���� 0���� ���� ��"
    }; // ����� ����

    // Start is called before the first frame update
    void Start()
    {
        blackcolor.a = 0.6f;
        whitecolor.a = 0.6f;
        Invoke("ActivatePlayerObject", 2.0f); // 2�� �ڿ� ActivatePlayerObject �Լ� ȣ��
    }
    void ActivatePlayerObject()
    {
        player.SetActive(true); // �÷��̾� ������Ʈ Ȱ��ȭ
        Image panelRenderer = conversationPanel.GetComponent<Image>();
        panelRenderer.color = blackcolor;
        talkText.color = Color.white;
        conversationPanel.SetActive(true); //���â Ȱ��ȭ
        talkText.text = "��� �з� ��...";
        currentDialogueIndex = 0; // ���� ��� �ε���
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
            talkText.text = dialogues[currentDialogueIndex]; // ��� ������Ʈ
            
        }
        else
        {
            SceneManager.LoadScene("Scene1");
        }
    }
    public void OnButtonClick()
    {
        currentDialogueIndex++; // ���� ���� �̵�
        UpdateDialogue(); // ��� ������Ʈ
    }
    void StopMovement()
    {
        npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // �ӵ��� 0���� �����Ͽ� ����
    }
    void ActivatePanel()
    {
        conversationPanel.SetActive(true); // ��ȭ �г� Ȱ��ȭ
    }
}
