using System.Collections;
using UnityEngine;
using TMPro; // TMP_Text를 사용하기 위한 네임스페이스

public class MeteorController : MonoBehaviour
{
    public GameObject meteorPrefab; // 운석 프리팹
    public GameObject[] spawnPoints; // 운석 생성 위치 오브젝트 배열
    public float spawnInterval = 5f; // 운석 생성 간격 (초)

    private void Start()
    {
        StartCoroutine(SpawnMeteorRoutine());
    }

    IEnumerator SpawnMeteorRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 랜덤한 생성 위치 오브젝트 선택
            GameObject randomSpawnPoint = GetRandomSpawnPoint();

            if (randomSpawnPoint != null)
            {
                // 선택된 위치에서 운석 생성
                Vector3 spawnPosition = randomSpawnPoint.transform.position;
                GameObject newMeteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

                // MovingMeteor 컴포넌트 확인
                MovingMeteor movingMeteor = newMeteor.GetComponent<MovingMeteor>();

                if (movingMeteor != null)
                {
                    // 랜덤 obsalpha 선택
                    string randomAlpha = GetRandomKoreanCharacter();

                    // MovingMeteor 컴포넌트의 obsalpha 설정
                    movingMeteor.obsalpha = randomAlpha;
                    Debug.Log($"운석이 obsalpha 값 {randomAlpha}으로 초기화되었습니다.");

                    // 운석의 자식 오브젝트에서 TMP_Text 컴포넌트 찾기
                    TMP_Text tmpText = newMeteor.GetComponentInChildren<TMP_Text>();

                    if (tmpText != null)
                    {
                        // TMP_Text 컴포넌트의 텍스트 값을 randomAlpha로 설정
                        tmpText.text = randomAlpha;
                    }
                    else
                    {
                        Debug.LogWarning("운석 자식 오브젝트에서 TMP_Text 컴포넌트를 찾을 수 없습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("운석 프리팹에 필요한 MovingMeteor 컴포넌트가 없습니다.");
                }
            }
            else
            {
                Debug.LogWarning("운석 생성 위치를 찾을 수 없습니다.");
            }
        }
    }

    GameObject GetRandomSpawnPoint()
    {
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            // 랜덤하게 생성 위치 오브젝트 선택
            return spawnPoints[Random.Range(0, spawnPoints.Length)];
        }
        else
        {
            Debug.LogWarning("운석 생성 위치 오브젝트가 설정되지 않았습니다.");
            return null;
        }
    }

    string GetRandomKoreanCharacter()
    {
        // 한글 자음/모음 배열
        string[] possibleAlpha = {
            "ㄱ", "ㄴ", "ㄷ", "ㄹ", "ㅁ", "ㅂ", "ㅅ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ",
            "ㅍ", "ㅎ", "ㅏ", "ㅑ", "ㅓ", "ㅕ", "ㅗ", "ㅛ", "ㅜ", "ㅠ", "ㅡ", "ㅣ",
            "ㅐ", "ㅒ", "ㅔ", "ㅖ", "ㅚ", "ㅟ", "ㅢ"
        };

        // 랜덤하게 선택된 한글 자음/모음 반환
        return possibleAlpha[Random.Range(0, possibleAlpha.Length)];
    }
}
