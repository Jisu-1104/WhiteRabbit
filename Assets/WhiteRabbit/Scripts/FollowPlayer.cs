using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ�� Transform�� ������ ����
    public float smoothSpeed = 0.125f; // ������ ���� �ӵ�
    public float followDistance = 2f; // �÷��̾��� �������� ������ �Ÿ�

    private Vector3 velocity = Vector3.zero; // ������ ���� �ӵ� ����

    void LateUpdate()
    {
        if (player != null) // �÷��̾ ������ ���� ����
        {
            // �÷��̾��� ���ʿ� ������ �Ÿ��� ���� ���󰡵��� ������ ��ġ ���
            Vector3 desiredPosition = player.position - player.right * followDistance;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
