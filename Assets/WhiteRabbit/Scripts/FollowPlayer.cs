using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform을 참조할 변수
    public float smoothSpeed = 0.125f; // 보간에 사용될 속도
    public float followDistance = 2f; // 플레이어의 왼쪽으로 유지할 거리

    private Vector3 velocity = Vector3.zero; // 보간에 사용될 속도 벡터

    void LateUpdate()
    {
        if (player != null) // 플레이어가 존재할 때만 실행
        {
            // 플레이어의 왼쪽에 일정한 거리를 더해 따라가도록 보간된 위치 계산
            Vector3 desiredPosition = player.position - player.right * followDistance;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
