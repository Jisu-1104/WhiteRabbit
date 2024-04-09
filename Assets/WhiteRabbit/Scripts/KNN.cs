using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using Mediapipe.Unity.Sample.HandTracking;

public class KNNPrediction : MonoBehaviour
{
    HandTrackingSolution handTrackingSolution; //HandTrackingSolution 클래스의 인스턴스
    float[][] angles; // 학습 데이터의 각도
    int[] labels;     // 학습 데이터의 라벨

    void Start()
    {
        // HandTrackingSolution 클래스의 인스턴스 찾기
        handTrackingSolution = FindObjectOfType<HandTrackingSolution>();

        // 상대 경로를 사용하여 모델 파일의 경로를 설정합니다.
        string modelRelativePath = @"Assets/WhiteRabbit/Scripts/python/dataset.txt";

        // 학습된 모델 로드
        LoadTrainedModelFromResources(modelRelativePath);
    }
    void Update()
    {
        if (handTrackingSolution != null && handTrackingSolution.data.Count > 0)
        {
            // HandTrackingSolution 클래스의 data 리스트에서 마지막 데이터 가져오기
            string[] lastData = handTrackingSolution.data[handTrackingSolution.data.Count - 1];

            if (lastData != null && lastData.Length > 0)
            {
                // 가져온 데이터를 float 배열로 변환
                float[] testData = Array.ConvertAll(lastData, float.Parse);

                // 예측 수행
                int predictedLabel = PredictGesture(testData);

                // 예측 결과 출력
                string gesture = GetGestureFromLabel(predictedLabel);
                Debug.Log($"Predicted Gesture: {gesture}");

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
            int rowCount = lines.Length;

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
