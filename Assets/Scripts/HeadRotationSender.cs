using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class HeadOrientationSender : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public int port = 9050;

    private UdpClient udpClient;

    [Serializable]
    public class OrientationData
    {
        public string timestamp;
        public float yaw;
        public float pitch;
        public float roll;
    }

    void Start()
    {
        udpClient = new UdpClient();
    }

    void Update()
    {
        Quaternion rotation = cameraRig.centerEyeAnchor.rotation;
        Vector3 euler = rotation.eulerAngles;

        OrientationData data = new OrientationData
        {
            timestamp = DateTime.UtcNow.ToString("o"),
            yaw = euler.y,
            pitch = euler.x,
            roll = euler.z
        };

        string json = JsonUtility.ToJson(data);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        udpClient.Send(bytes, bytes.Length, NetworkConfig.computerIP, port);
        Debug.Log("Sended Data:" + data.yaw);
    }
}
