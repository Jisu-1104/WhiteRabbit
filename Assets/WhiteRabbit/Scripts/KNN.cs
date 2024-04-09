using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using Mediapipe.Unity.Sample.HandTracking;

public class KNNPrediction : MonoBehaviour
{
    HandTrackingSolution handTrackingSolution; //HandTrackingSolution Ŭ������ �ν��Ͻ�
    float[][] angles; // �н� �������� ����
    int[] labels;     // �н� �������� ��

    void Start()
    {
        // HandTrackingSolution Ŭ������ �ν��Ͻ� ã��
        handTrackingSolution = FindObjectOfType<HandTrackingSolution>();

        // ��� ��θ� ����Ͽ� �� ������ ��θ� �����մϴ�.
        string modelRelativePath = @"Assets/WhiteRabbit/Scripts/python/dataset.txt";

        // �н��� �� �ε�
        LoadTrainedModelFromResources(modelRelativePath);
    }
    void Update()
    {
        if (handTrackingSolution != null && handTrackingSolution.data.Count > 0)
        {
            // HandTrackingSolution Ŭ������ data ����Ʈ���� ������ ������ ��������
            string[] lastData = handTrackingSolution.data[handTrackingSolution.data.Count - 1];

            if (lastData != null && lastData.Length > 0)
            {
                // ������ �����͸� float �迭�� ��ȯ
                float[] testData = Array.ConvertAll(lastData, float.Parse);

                // ���� ����
                int predictedLabel = PredictGesture(testData);

                // ���� ��� ���
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

            // ������ �迭 �ʱ�ȭ
            angles = new float[rowCount][];
            labels = new int[rowCount];

            // �� ���� ������ �Ľ�
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
            int k = 3; // KNN�� k �� ����
            int[] nearestIndices = new int[k];
            float[] nearestDistances = new float[k];
            for (int i = 0; i < k; i++)
            {
                nearestDistances[i] = float.MaxValue;
            }

            // ��� �н� �����Ϳ��� �Ÿ� ���
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

            // ���� ����� �̿����� �� Ȯ��
            int[] nearestLabels = new int[k];
            for (int i = 0; i < k; i++)
            {
                nearestLabels[i] = labels[nearestIndices[i]];
            }

            // �ֺ� ���
            int[] labelCounts = new int[32]; // 32���� ����ó
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
        // �󺧿� �ش��ϴ� ����ó ����
        string[] gestures = {
            "��", "��", "��", "��", "��", "��", "��", "��",
            "��", "��", "��", "��", "��", "��", "��", "��",
            "��", "��", "��", "��", "��", "��", "��", "��",
            "��", "��", "��", "��", "��", "��", "��", "None"
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
