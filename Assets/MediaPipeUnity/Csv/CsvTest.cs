using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mediapipe.Tasks.Vision.HandLandmarker;
using Mediapipe;

public class CsvTest : MonoBehaviour
{
    void Start()
    {
        List<NormalizedLandmarkList> handLandmarks = null;
        using (var writer = new CsvFileWriter("Assets/Resources/test.csv"))
        {
            List<string> columns = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };// making Index Row
            writer.WriteRow(columns);
            columns.Clear();
            for (int i = 0; i < 21; i++)
            {
                foreach (var landmarks in handLandmarks)
                {
                    // top of the head
                    var landmarkposition = landmarks.Landmark[i];
                    Debug.Log($"{i} : {landmarkposition}");
                    string lmp = landmarkposition.ToString();
                    columns.Add(lmp); // landmarkposition을 넣고 싶음
                }
            }
            writer.WriteRow(columns);
            columns.Clear();
        }
    }
    private void Update()
    {
        //Csvtest();
    }
    public void Csvtest() {

        List<NormalizedLandmarkList> handLandmarks = null;
        using (var writer = new CsvFileWriter("Assets/Resources/test.csv"))
        {
            List<string> columns = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };// making Index Row
            writer.WriteRow(columns);
            columns.Clear();
            for (int i = 0; i < 21; i++)
            {
                foreach (var landmarks in handLandmarks)
                {
                    // top of the head
                    var landmarkposition = landmarks.Landmark[i];
                    Debug.Log($"{i} : {landmarkposition}");
                    string lmp = landmarkposition.ToString();
                    columns.Add(lmp); // landmarkposition을 넣고 싶음
                }
            }
            writer.WriteRow(columns);
            columns.Clear();
        }
    }
}
