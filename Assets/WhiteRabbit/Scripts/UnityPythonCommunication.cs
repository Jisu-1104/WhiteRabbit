using Mediapipe.Unity.Sample.HandTracking;
using Platformer;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UnityPythonCommunication : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    public PlayerController playerController;

    // HandTrackingSolution �ν��Ͻ�
    HandTrackingSolution handTrackingSolution;

    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    void Start()
    {
        try
        {
            Process psi = new Process();
            psi.StartInfo.FileName = @"python";
            psi.StartInfo.Arguments = @"C:\Users\Admin\Documents\GitHub\WhiteRabbit\python\GestureRecognition.py";
            psi.StartInfo.CreateNoWindow = false;
            psi.StartInfo.UseShellExecute = false;
            psi.StartInfo.RedirectStandardOutput = true;
            psi.Start();

        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Unable to launch app: " + e.Message);
        }

        ConnectToPython();

        // HandTrackingSolution �ν��Ͻ� ����
        handTrackingSolution = FindObjectOfType<HandTrackingSolution>();
    }

    void Update()
    {
        // HandTrackingSolution�� GetLmDataString �޼��带 ȣ���Ͽ� lmDataString ���� ������
        string lmDataString = handTrackingSolution.GetLmDataString();

        // ������ lmDataString ���� Python���� ����
        SendStringToPython(lmDataString);
    }

    void ConnectToPython()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 8888);
            stream = client.GetStream();
            UnityEngine.Debug.Log("Connected to Python");

            Thread receiveThread = new Thread(ReceiveMessagesFromPython);
            receiveThread.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Failed to connect to Python: {e.Message}");
        }
    }

    void SendStringToPython(string stringValue)
    {
        byte[] data = Encoding.UTF8.GetBytes(stringValue);
        stream.Write(data, 0, data.Length);
        UnityEngine.Debug.Log($"Sent string to Python: {stringValue}");
    }

    void ReceiveMessagesFromPython()
    {
        byte[] buffer = new byte[4096];
        while (true)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                playerController.setAlphabet(response);
                UnityEngine.Debug.Log($"Received message from Python: {response}");
            }
        }
    }

    void OnApplicationQuit()
    {
        if (client != null)
            client.Close();
    }
}
