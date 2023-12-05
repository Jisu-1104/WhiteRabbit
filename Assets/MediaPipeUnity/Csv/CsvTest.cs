using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CsvTest : MonoBehaviour {
    void Start()
    {
        using (var writer = new CsvFileWriter("Assets/Resources/test.csv"))
        {
            List<string> columns = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };// making Index Row
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("{landmarkposition}"); // landmarkposition을 넣고 싶음
            writer.WriteRow(columns);
            columns.Clear();
        }
    }
}
