using UnityEngine;

public class DestroyMeteorController : MonoBehaviour
{
    public GameObject square; // ���� ������Ʈ ����
    public GameObject spaceship; // ���ּ� ������Ʈ ����
    public string targetTag = "suwhaObject"; // �ı��� ��� �±� ����

    public Collider2D squareCollider; // square ������Ʈ�� 2D �ݶ��̴�

    void Update()
    {
        // square ������Ʈ�� null�̰ų� spaceship ������Ʈ�� null�� ��� ó��
        if (square == null || spaceship == null)
        {
            Debug.LogWarning("���� ������Ʈ �Ǵ� ���ּ� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ���ּ� ������Ʈ�� KNNPrediction ������Ʈ���� Gesture ������ �б�
        string gestureValue = spaceship.GetComponent<KNNPrediction>().gesture;

        // squareCollider�� �����Ǿ� �ִ��� Ȯ��
        if (squareCollider == null)
        {
            Debug.LogError("squareCollider�� �Ҵ���� �ʾҽ��ϴ�. �ν����Ϳ��� square ������Ʈ�� Collider2D�� �Ҵ����ּ���.");
            return;
        }

        // squareCollider ���� �ȿ� �ִ� Ư�� �±׸� ���� ������Ʈ �ı�
        DestroyObjectsInSquareCollider(gestureValue);
    }

    void DestroyObjectsInSquareCollider(string gestureValue)
    {
        // �±װ� targetTag�� ��� ������Ʈ�� ã��
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

        // ã�� ������Ʈ�� �� squareCollider ���� �ȿ� �ִ� ������Ʈ �ı�
        foreach (GameObject obj in taggedObjects)
        {
            // obj�� ��ġ�� squareCollider�� �ٿ�� �ڽ� ���� �ִ��� Ȯ��
            Vector3 objectPosition = obj.transform.position;
            if (squareCollider.bounds.Contains(objectPosition))
            {
                // MovingMeteor ������Ʈ ��������
                MovingMeteor meteor = obj.GetComponent<MovingMeteor>();
                if (meteor != null && meteor.obsalpha == gestureValue)
                {
                    Debug.Log(meteor.obsalpha + "�ı���");
                    Destroy(obj); // obsalpha ���� gestureValue�� ��ġ�ϴ� ������Ʈ �ı�
                }
            }
        }
    }
}
