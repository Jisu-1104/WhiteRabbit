using UnityEngine;

public class MovingMeteor : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public GameObject targetObject;
    public string obsalpha;

    public Transform spriteTransform; // ��������Ʈ�� Transform
    public Transform textTransform; // �ؽ�Ʈ�� Transform

    void Update()
    {
        if (targetObject != null)
        {
            // ��� ��ǥ �������� �̵�
            Vector3 targetPosition = targetObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // ��� ���� ���� ���
            Vector3 direction = (targetPosition - spriteTransform.position).normalized;

            // ��������Ʈ�� ���� ��� (���� ���͸� �̿��Ͽ�)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 135f;
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            textTransform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("Target object is not assigned.");
        }
    }
}
