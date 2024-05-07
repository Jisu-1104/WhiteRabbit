using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 2.0f; // 움직이는 속도
    public float amplitude = 1.0f; // 움직임의 진폭
    public Vector3 direction = Vector3.up; // 움직이는 방향

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // 초기 위치 저장
    }

    void Update()
    {
        // 오브젝트를 위아래로 움직이는 코드
        float offset = Mathf.Sin(Time.time * speed) * amplitude; // 사인 함수를 이용하여 위아래로 움직임을 생성
        transform.position = startPosition + direction * offset; // 움직임을 적용하여 오브젝트 이동
    }
}
