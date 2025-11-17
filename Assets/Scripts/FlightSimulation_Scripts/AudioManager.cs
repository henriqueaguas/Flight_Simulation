using UnityEngine;
using System.Net.Sockets;
using System.Text;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public int port = 6666;

    void Start()
    {
        SendAudioSignal("Plane_Audio");
    }

    public void SendAudioSignal(string audioName)
    {
        if (string.IsNullOrEmpty(NetworkConfig.computerIP))
        {
            Debug.LogWarning("PC IP is empty!");
            return;
        }

        UdpClient client = new UdpClient();
        string message = "LOAD_AUDIO:" + audioName;
        byte[] data = Encoding.UTF8.GetBytes(message);

        client.Send(data, data.Length, NetworkConfig.computerIP, port);
        client.Close();

        Debug.Log($"Sending to {NetworkConfig.computerIP}:{port} - Message: {message}");
    }
    
}
