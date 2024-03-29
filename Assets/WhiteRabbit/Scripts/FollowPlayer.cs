using System.Collections;
using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �����ϱ� ���� ����
    public float followDistance = -2f; // �÷��̾�κ����� �Ÿ�
    public float delay = 0.5f; // �÷��̾� �̵� �� �꿡�� �̵��� ������ ������ �ð�

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("�÷��̾ �������� �ʾҽ��ϴ�.");
            return;
        }

        // �÷��̾��� ��ġ�� ��������
        Vector3 playerPosition = player.position;

        // �÷��̾��� ������ Ȯ���Ͽ� ���� �Ǵ� �����ʿ��� �÷��̾�κ��� ���� �Ÿ� ������ ��ġ ����
        Vector3 targetPosition = playerPosition + (spriteRenderer.flipX ? Vector3.left : Vector3.right) * followDistance;

        // �÷��̾�� ���� ���̷� ����
        targetPosition.y = playerPosition.y;

        // ���� ��ġ�� �����Ͽ� �÷��̾ ����ٴϵ��� ��
        transform.position = targetPosition;
    }
}
