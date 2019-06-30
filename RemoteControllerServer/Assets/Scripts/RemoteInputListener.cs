using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class RemoteInputListener : MonoBehaviour
{
    private static RemoteInputListener SharedInstance;
    private InputModel InputModel;
    private volatile bool runTask = true;
    public int listenPort = 6000;

    public static RemoteInputListener GetSharedInstance()
    {
        return SharedInstance;
    }

    public InputModel GetInputModel()
    {
        return InputModel;
    }

    private void Awake()
    {
        if(SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }else if (SharedInstance != this)
        {
            Destroy(gameObject);
        }
        InputModel = new InputModel();
    }

    // Start is called before the first frame update
    void Start()
    {
        Thread t1 = new Thread(InitInputServer);
        t1.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitInputServer()
    {
        byte[] receivedData;
        string json;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, listenPort);
        //IPEndPoint ipepReceive = new IPEndPoint(IPAddress.Any, 0);
        using (var udpClient = new UdpClient(ipep))
        {
            while (runTask)
            {
                receivedData = udpClient.Receive(ref ipep);
                json = Encoding.ASCII.GetString(receivedData);
                InputModel = JsonConvert.DeserializeObject<InputModel>(json);
            }
        }
    }

    private void OnDestroy()
    {
        runTask = false;
    }
}
