using System.Collections;
using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 저장하기 위한 변수
    public float followDistance = -2f; // 플레이어로부터의 거리
    public float delay = 0.5f; // 플레이어 이동 후 펫에게 이동을 적용할 딜레이 시간

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("플레이어가 지정되지 않았습니다.");
            return;
        }

        // 플레이어의 위치를 가져오기
        Vector3 playerPosition = player.position;

        // 플레이어의 방향을 확인하여 왼쪽 또는 오른쪽에서 플레이어로부터 일정 거리 떨어진 위치 설정
        Vector3 targetPosition = playerPosition + (spriteRenderer.flipX ? Vector3.left : Vector3.right) * followDistance;

        // 플레이어와 같은 높이로 설정
        targetPosition.y = playerPosition.y;

        // 펫의 위치를 조정하여 플레이어를 따라다니도록 함
        transform.position = targetPosition;
    }
}
