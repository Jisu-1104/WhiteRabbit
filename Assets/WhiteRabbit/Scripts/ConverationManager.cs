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
    private bool gestureG = false; // '��'�� �����ߴ��� ���θ� ��Ÿ���� ����
    private bool gestureN = false; // '��'�� �����ߴ��� ���θ� ��Ÿ���� ����
    private bool gestureD = false; // '��'�� �����ߴ��� ���θ� ��Ÿ���� ����

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
                talkText.text = "�� ���ּ��� ����Ʈ�� ��ǰ�� ��ó�� �־�. ���� ã�� ���� �����ٰ�.";
                lastPoint = collision.gameObject;
                break;
            case "Point1":
                talkText.text = "�װ� �츮 ���� ������� �����Ѵٸ� �׵鵵 �ʸ� �����ٰž�.";
                break;
            case "GieokPoint":
                talkText.text = "��� Ǯ�̾�. ��ȭ ���� �غ�.";
                suwhaImageComponent.sprite = sprite[0];
                suwhaImage.SetActive(true);
                lastPoint = collision.gameObject;
                break;
            case "GieokEndPoint":
                talkText.text = "�����! �츮 ���� ������ �ֳ���.";
                lastPoint = collision.gameObject;
                break;
            case "NieonPoint":
                talkText.text = "�ɾ��ִ� ����� �Ϳ��� ���� ����̾�. ��ȭ ���� �غ�.";
                suwhaImageComponent.sprite = sprite[1];
                suwhaImage.SetActive(true);
                lastPoint = collision.gameObject;
                break;
            case "DigeodPoint":
                talkText.text = "��� �縻�� ��ģ ���� ��� ��������. ��ȭ ���� �غ�.";
                suwhaImageComponent.sprite = sprite[2];
                suwhaImage.SetActive(true);
                lastPoint = collision.gameObject;
                break;
            case "LieolPoint":
                talkText.text = "�¼��� ���� ���̾�. ��ȭ ���� �غ�.";
                lastPoint = collision.gameObject;
                suwhaImageComponent.sprite = sprite[3];
                suwhaImage.SetActive(true);
                break;
            case "GNDLPoint":
                talkText.text = "ū ������ �ŷ����� �⿪������ڸ��� ��������. ��ȭ ���������� ������� �غ�.";
                lastPoint = collision.gameObject;
                suwhaImageComponent.sprite = sprite[0];
                suwhaImage.SetActive(true);
                break;
            case "GNDLEndPoint":
                talkText.text = "�����߾�! ���� �� ���ּ� ��ǰ���� ���� �� �Ծ�.";
                break;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "GNDLPoint")
        {
            if (gesture.Equals("��") && !gestureG)
            {
                suwhaImageComponent.sprite = sprite[1];
                gestureG = true; // '��' ���� ǥ��
            }
            else if (gesture.Equals("��") && gestureG && !gestureN)
            {
                suwhaImageComponent.sprite = sprite[2];
                gestureN = true; // '��' ���� ǥ��
            }
            else if (gesture.Equals("��") && gestureN && !gestureD)
            {
                suwhaImageComponent.sprite = sprite[3];
                gestureD = true; // '��' ���� ǥ��
            }
            else if (gesture.Equals("��") && gestureD)
            {
                Destroy(GNDL);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // conversationPanel�̳� suwhaImage�� null�� �ƴ��� Ȯ��
        if (conversationPanel != null)
        {
            conversationPanel.SetActive(false);
        }
        if (suwhaImage != null)
        {
            suwhaImage.SetActive(false);
        }

        // �浹�� ������Ʈ�� Point �±׸� ���� ���
        if (collision.CompareTag("Point"))
        {
            // �浹�� ���� ������Ʈ�� null�� �ƴ��� Ȯ��
            if (collision.gameObject != null)
            {
                // ���� ������Ʈ�� ��Ȱ��ȭ
                collision.gameObject.SetActive(false);
            }
        }
    }

}
