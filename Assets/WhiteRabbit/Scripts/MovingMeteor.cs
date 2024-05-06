using UnityEngine;

public class MovingMeteor : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public GameObject targetObject;
    public string obsalpha;

    public Transform spriteTransform; // 스프라이트의 Transform
    public Transform textTransform; // 텍스트의 Transform

    void Update()
    {
        if (targetObject != null)
        {
            // 운석을 목표 지점으로 이동
            Vector3 targetPosition = targetObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 운석의 방향 벡터 계산
            Vector3 direction = (targetPosition - spriteTransform.position).normalized;

            // 스프라이트의 각도 계산 (방향 벡터를 이용하여)
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
