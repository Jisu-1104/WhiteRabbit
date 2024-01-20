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
            //아래 문서 경로를 자기 파일 위치에 맞게 수정해야 함!!
            psi.StartInfo.Arguments = @"C:\Users\rlawl\OneDrive\문서\GitHub\WhiteRabbit\python\UnityPythonCommunication.py";
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
        SendIntToPython(42); // 예시로 42를 보냄
    }

    void ConnectToPython()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 8888); // Python 서버 주소 및 포트
            stream = client.GetStream();
            UnityEngine.Debug.Log("Connected to Python");

            // 백그라운드 스레드에서 메시지 수신을 처리
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
        string message = intValue.ToString(); // int를 문자열로 변환
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
                int receivedInt = int.Parse(response); // 문자열을 int로 변환
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
