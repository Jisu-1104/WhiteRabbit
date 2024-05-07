using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 2.0f; // �����̴� �ӵ�
    public float amplitude = 1.0f; // �������� ����
    public Vector3 direction = Vector3.up; // �����̴� ����

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        // ������Ʈ�� ���Ʒ��� �����̴� �ڵ�
        float offset = Mathf.Sin(Time.time * speed) * amplitude; // ���� �Լ��� �̿��Ͽ� ���Ʒ��� �������� ����
        transform.position = startPosition + direction * offset; // �������� �����Ͽ� ������Ʈ �̵�
    }
}
