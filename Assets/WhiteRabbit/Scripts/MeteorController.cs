using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    public GameObject meteorPrefab; // ������ ������
    public float spawnY = 0f; // �������� Y ��ǥ (������)
    public float minX = -10f; // �ּ� X ��ǥ
    public float maxX = 10f; // �ִ� X ��ǥ

    private void Start()
    {
        StartCoroutine(SpawnMeteorRoutine());
    }

    IEnumerator SpawnMeteorRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // 5�� ��ٸ�

            // ������ X ��ǥ ����
            float randomX = Random.Range(minX, maxX);

            // ������ ���� ��ġ ���� (������ Y ��ǥ, ������ X ��ǥ)
            Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

            // ������ ����
            GameObject newMeteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            // �������� Obstacle ��ũ��Ʈ ������Ʈ ��������
            Obstacle obstacleScript = newMeteor.GetComponent<Obstacle>();

            if (obstacleScript != null)
            {
                // obsalpha �� �������� ����
                string[] possibleAlpha = { "Value1", "Value2", "Value3" }; // ���ϴ� ������ ���� ����
                string randomAlpha = possibleAlpha[Random.Range(0, possibleAlpha.Length)];

                // Obstacle ��ũ��Ʈ�� obsalpha ���� ����
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
