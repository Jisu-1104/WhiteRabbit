using UnityEngine;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UnityPythonCommunication : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        try
        {
            Process psi = new Process();
            psi.StartInfo.FileName = @"python";
            //�Ʒ� ���� ��θ� �ڱ� ���� ��ġ�� �°� �����ؾ� ��!!
            psi.StartInfo.Arguments = @"C:\Users\rlawl\OneDrive\����\GitHub\WhiteRabbit\python\UnityPythonCommunication.py";
            psi.StartInfo.CreateNoWindow = true;
            psi.StartInfo.UseShellExecute = false;
            psi.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Unable to launch app: " + e.Message);
        }
        ConnectToPython();
    }

    void Update()
    {
        SendIntToPython(42); // ���÷� 42�� ����
    }

    void ConnectToPython()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 8888); // Python ���� �ּ� �� ��Ʈ
            stream = client.GetStream();
            UnityEngine.Debug.Log("Connected to Python");

            // ��׶��� �����忡�� �޽��� ������ ó��
            Thread receiveThread = new Thread(ReceiveMessagesFromPython);
            receiveThread.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Failed to connect to Python: {e.Message}");
        }
    }

    void SendIntToPython(int intValue)
    {
        string message = intValue.ToString(); // int�� ���ڿ��� ��ȯ
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
        UnityEngine.Debug.Log($"Sent int to Python: {intValue}");
    }

    void ReceiveMessagesFromPython()
    {
        byte[] buffer = new byte[4096];
        while (true)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                int receivedInt = int.Parse(response); // ���ڿ��� int�� ��ȯ
                UnityEngine.Debug.Log($"Received int from Python: {receivedInt}");
            }
        }
    }

    void OnApplicationQuit()
    {
        if (client != null)
            client.Close();
    }
}
