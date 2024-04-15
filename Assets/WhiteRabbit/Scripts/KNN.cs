using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using Mediapipe.Unity.Sample.HandTracking;

public class KNNPrediction : MonoBehaviour
{
    public GameObject player;
    HandTrackingSolution handTrackingSolution; //HandTrackingSolution 클래스의 인스턴스
    float[][] angles; // 학습 데이터의 각도
    int[] labels;     // 학습 데이터의 라벨
    public string alpha;

    void Start()
    {
        // HandTrackingSolution 클래스의 인스턴스 찾기
        handTrackingSolution = FindObjectOfType<HandTrackingSolution>();

        // 상대 경로를 사용하여 모델 파일의 경로를 설정합니다.
        string modelRelativePath = @"Assets/WhiteRabbit/Scripts/python/dataset.txt";

        // 학습된 모델 로드
        LoadTrainedModelFromResources(modelRelativePath);
    }
    private void OnTriggerStay2D(Collider2D player)
    {
        if (handTrackingSolution != null && handTrackingSolution.data.Count > 0)
        {
            string lmDataString = handTrackingSolution.GetLmDataString();

            if (lmDataString != null)
            {
                // 가져온 데이터를 각도로 계산
                float[] testData = ProcessLandmarkData(lmDataString);

                // 예측 수행
                int predictedLabel = PredictGesture(testData);

                // 예측 결과 출력
                string gesture = GetGestureFromLabel(predictedLabel);
                Debug.Log($"Predicted Gesture: {gesture}");
                if(alpha == gesture)
                {
                    Debug.Log(alpha + "Destroyed");
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogWarning("No data available in HandTrackingSolution.");
            }
        }


    }

    private void LoadTrainedModelFromResources(string modelFilePath)
    {
        
        try
        {
            TextAsset modelFile = AssetDatabase.LoadAssetAtPath<TextAsset>(modelFilePath);
            if (modelFile == null)
            {
                Debug.LogError($"Failed to load model file: {modelFilePath}");
                return;
            }
            string temp = modelFile.text.Replace("\r", "");
            string[] lines = temp.Split('\n');
            int rowCount = lines.Length - 1;


            // 데이터 배열 초기화
            angles = new float[rowCount][];
            labels = new int[rowCount];

            // 모델 파일 데이터 파싱
            for (int i = 0; i < rowCount; i++)
            {
                string[] values = lines[i].Split(',');
                angles[i] = new float[values.Length - 1];
                if (values.Length > 2)
                {
                    for (int j = 0; j < values.Length - 2; j++)
                    {
                        if (values[j] == "") continue;
                        angles[i][j] = float.Parse(values[j]);
                    }
                    labels[i] = (int)float.Parse(values[values.Length - 1]);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load the trained model: {e}");        
        }
    }

    public static float[] ProcessLandmarkData(string message)
    {
        message = message.Replace("\r", "");
        List<float> landmarkDataList = message.Split(',').Select(x => float.Parse(x)).ToList();

        if (landmarkDataList.Count != 63)
        {
            Debug.Log("Invalid landmark data format.");
            return null;
        }

        float[,] transposedJoint = new float[3, 21];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                transposedJoint[i, j] = landmarkDataList[i * 21 + j];
            }
        }

        float[,] joint = Transpose(transposedJoint);

        int[] v1Indices = { 0, 1, 2, 3, 0, 5, 6, 7, 0, 9, 10, 11, 0, 13, 14, 15, 0, 17, 18, 19 };
        int[] v2Indices = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        float[,] v = new float[20, 3];
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                v[i, j] = joint[v2Indices[i], j] - joint[v1Indices[i], j];
            }
        }

        float[,] vNormalized = new float[20, 3];
        for (int i = 0; i < 20; i++)
        {
            float norm = (float)Math.Sqrt(v[i, 0] * v[i, 0] + v[i, 1] * v[i, 1] + v[i, 2] * v[i, 2]);
            for (int j = 0; j < 3; j++)
            {
                vNormalized[i, j] = v[i, j] / norm;
            }
        }

        int[] compareV1Indices = { 0, 1, 2, 4, 5, 6, 8, 9, 10, 12, 13, 14, 16, 17, 18 };
        int[] compareV2Indices = { 1, 2, 3, 5, 6, 7, 9, 10, 11, 13, 14, 15, 17, 18, 19 };

        float[] angles = new float[15];
        for (int i = 0; i < 15; i++)
        {
            float dotProduct = vNormalized[compareV1Indices[i], 0] * vNormalized[compareV2Indices[i], 0]
                             + vNormalized[compareV1Indices[i], 1] * vNormalized[compareV2Indices[i], 1]
                             + vNormalized[compareV1Indices[i], 2] * vNormalized[compareV2Indices[i], 2];

            angles[i] = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        }

        return angles;
    }

    private static float[,] Transpose(float[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        float[,] transposed = new float[cols, rows];

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                transposed[i, j] = matrix[j, i];
            }
        }

        return transposed;
    }

    private int PredictGesture(float[] testData)
    {
        try
        {
            int k = 3; // KNN의 k 값 설정
            int[] nearestIndices = new int[k];
            float[] nearestDistances = new float[k];
            for (int i = 0; i < k; i++)
            {
                nearestDistances[i] = float.MaxValue;
            }

            // 모든 학습 데이터와의 거리 계산
            for (int i = 0; i < angles.Length; i++)
            {
                float distance = CalculateEuclideanDistance(angles[i], testData);
                if (distance < nearestDistances[k - 1])
                {
                    for (int j = 0; j < k; j++)
                    {
                        if (distance < nearestDistances[j])
                        {
                            for (int m = k - 1; m > j; m--)
                            {
                                nearestDistances[m] = nearestDistances[m - 1];
                                nearestIndices[m] = nearestIndices[m - 1];
                            }
                            nearestDistances[j] = distance;
                            nearestIndices[j] = i;
                            break;
                        }
                    }
                }
            }

            // 가장 가까운 이웃들의 라벨 확인
            int[] nearestLabels = new int[k];
            for (int i = 0; i < k; i++)
            {
                nearestLabels[i] = labels[nearestIndices[i]];
            }

            // 최빈값 계산
            int[] labelCounts = new int[32]; // 32개의 제스처
            foreach (int label in nearestLabels)
            {
                labelCounts[label]++;
            }

            int maxCount = 0;
            int predictedLabel = -1;
            for (int i = 0; i < labelCounts.Length; i++)
            {
                if (labelCounts[i] > maxCount)
                {
                    maxCount = labelCounts[i];
                    predictedLabel = i;
                }
            }

            return predictedLabel;
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred during prediction: {e}");
            return -1;
        }
    }

    private float CalculateEuclideanDistance(float[] vector1, float[] vector2)
    {
        if (vector1.Length != vector2.Length)
        {
            throw new ArgumentException("Vector dimensions must be the same");
        }

        float sum = 0.0f;
        for (int i = 0; i < vector1.Length; i++)
        {
            sum += Mathf.Pow(vector1[i] - vector2[i], 2);
        }

        return Mathf.Sqrt(sum);
    }

    private string GetGestureFromLabel(int label)
    {
        // 라벨에 해당하는 제스처 매핑
        string[] gestures = {
            "ㄱ", "ㄴ", "ㄷ", "ㄹ", "ㅁ", "ㅂ", "ㅅ", "ㅇ",
            "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ", "ㅏ", "ㅑ",
            "ㅓ", "ㅕ", "ㅗ", "ㅛ", "ㅜ", "ㅠ", "ㅡ", "ㅣ",
            "ㅐ", "ㅒ", "ㅔ", "ㅖ", "ㅢ", "ㅚ", "ㅟ", "None"
        };

        if (label >= 0 && label < gestures.Length)
        {
            return gestures[label];
        }
        else
        {
            return "Unknown Gesture";
        }
    }
}
