using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SceneReceiver : MonoBehaviour
{
    UdpClient udpClient;
    private SceneStarter sceneStarter;
    private bool started = false;

    void Start()
    {
        udpClient = new UdpClient(7777); // Port
        udpClient.BeginReceive(OnReceive, null);

        // Find the SceneStarter component in the scene
        sceneStarter = FindObjectOfType<SceneStarter>();
        
        // Check if we found it
        if (sceneStarter == null)
        {
            Debug.LogError("SceneStarter not found in the scene!");
        }
    }

    void OnReceive(IAsyncResult result)
    {
        IPEndPoint source = new IPEndPoint(IPAddress.Any, 7777);
        byte[] data = udpClient.EndReceive(result, ref source);
        string message = Encoding.UTF8.GetString(data);
        Debug.Log("Received: " + message);
        
        if (message.StartsWith("IP:"))
        {
            string IP = message.Substring("IP:".Length);
            NetworkConfig.computerIP = IP;
            Debug.Log("Received: " + NetworkConfig.computerIP);
        }

        if (message.StartsWith("LOAD_SCENE:"))
        {
            string sceneName = message.Substring("LOAD_SCENE:".Length);
            Loader.Load(sceneName);
        }

        if (message.StartsWith("START_SCENE"))
        {
            if (!started && sceneStarter != null) {
                sceneStarter.StartGameScene();
                Debug.Log(sceneStarter);
                started = true;
            }
            else if (sceneStarter == null) {
                Debug.LogError("Cannot start scene - SceneStarter reference is null");
            }
        }

        udpClient.BeginReceive(OnReceive, null); // Continue listening
    }

    void OnDestroy()
    {
        udpClient?.Close();
    }

    public void QuitApp () {
        Application.Quit();
    }
}
