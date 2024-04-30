using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    public GameObject meteorPrefab; // 생성할 프리팹
    public float spawnY = 0f; // 프리팹의 Y 좌표 (고정값)
    public float minX = -10f; // 최소 X 좌표
    public float maxX = 10f; // 최대 X 좌표

    private void Start()
    {
        StartCoroutine(SpawnMeteorRoutine());
    }

    IEnumerator SpawnMeteorRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // 5초 기다림

            // 랜덤한 X 좌표 생성
            float randomX = Random.Range(minX, maxX);

            // 프리팹 생성 위치 설정 (고정된 Y 좌표, 랜덤한 X 좌표)
            Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

            // 프리팹 생성
            GameObject newMeteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            // 프리팹의 Obstacle 스크립트 컴포넌트 가져오기
            Obstacle obstacleScript = newMeteor.GetComponent<Obstacle>();

            if (obstacleScript != null)
            {
                // obsalpha 값 랜덤으로 설정
                string[] possibleAlpha = { "Value1", "Value2", "Value3" }; // 원하는 값으로 변경 가능
                string randomAlpha = possibleAlpha[Random.Range(0, possibleAlpha.Length)];

                // Obstacle 스크립트의 obsalpha 변수 설정
                obstacleScript.obsalpha = randomAlpha;
                Debug.Log("Meteor initialized with obsalpha: " + randomAlpha);
            }
            else
            {
                Debug.LogWarning("Meteor prefab does not have the required Obstacle component.");
            }
        }
    }
}
