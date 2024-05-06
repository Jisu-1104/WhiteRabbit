using UnityEngine;

public class DestroyMeteorController : MonoBehaviour
{
    public GameObject square; // 기준 오브젝트 설정
    public GameObject spaceship; // 우주선 오브젝트 설정
    public string targetTag = "suwhaObject"; // 파괴할 대상 태그 설정

    public Collider2D squareCollider; // square 오브젝트의 2D 콜라이더

    void Update()
    {
        // square 오브젝트가 null이거나 spaceship 오브젝트가 null인 경우 처리
        if (square == null || spaceship == null)
        {
            Debug.LogWarning("기준 오브젝트 또는 우주선 오브젝트를 찾을 수 없습니다.");
            return;
        }

        // 우주선 오브젝트의 KNNPrediction 컴포넌트에서 Gesture 변수값 읽기
        string gestureValue = spaceship.GetComponent<KNNPrediction>().gesture;

        // squareCollider가 설정되어 있는지 확인
        if (squareCollider == null)
        {
            Debug.LogError("squareCollider가 할당되지 않았습니다. 인스펙터에서 square 오브젝트의 Collider2D를 할당해주세요.");
            return;
        }

        // squareCollider 범위 안에 있는 특정 태그를 가진 오브젝트 파괴
        DestroyObjectsInSquareCollider(gestureValue);
    }

    void DestroyObjectsInSquareCollider(string gestureValue)
    {
        // 태그가 targetTag인 모든 오브젝트들 찾기
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

        // 찾은 오브젝트들 중 squareCollider 범위 안에 있는 오브젝트 파괴
        foreach (GameObject obj in taggedObjects)
        {
            // obj의 위치가 squareCollider의 바운딩 박스 내에 있는지 확인
            Vector3 objectPosition = obj.transform.position;
            if (squareCollider.bounds.Contains(objectPosition))
            {
                // MovingMeteor 컴포넌트 가져오기
                MovingMeteor meteor = obj.GetComponent<MovingMeteor>();
                if (meteor != null && meteor.obsalpha == gestureValue)
                {
                    Debug.Log(meteor.obsalpha + "파괴됨");
                    Destroy(obj); // obsalpha 값이 gestureValue와 일치하는 오브젝트 파괴
                }
            }
        }
    }
}
