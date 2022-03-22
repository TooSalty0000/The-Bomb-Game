using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class HandMotionDetection : MonoBehaviour
{
    static Socket listener;
    public CancellationTokenSource source;
    public ManualResetEvent allDone;
    private Color matColor;
    private Vector3 newPosition;
    public bool pressed = false;

    public static readonly int PORT = 1755;
    public static readonly int WAITTIME = 1;
    public Vector3 handPosition;



    HandMotionDetection()
    {
        source = new CancellationTokenSource();
        allDone = new ManualResetEvent(false);
    }

    // Start is called before the first frame update
    async void Start()
    {
        // Debug.Log(Application.dataPath);
        // System.Diagnostics.Process.Start((Application.dataPath) + "/mysocket.py");
        // objectRenderer = GetComponent<Renderer>();   
        if (listener != null)
        {
            listener.Close();
        }
        // DontDestroyOnLoad(gameObject);
        await Task.Run(() => ListenEvents(source.Token));
    }

    // Update is called once per frame
    void Update()
    {
        handPosition = newPosition;
        // objectRenderer.material.color = matColor;
    }

    private void ListenEvents(CancellationToken token)
    {

        
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);

         
        listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

         
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

             
            while (!token.IsCancellationRequested)
            {
                allDone.Reset();

                print("Waiting for a connection... host :" + ipAddress.MapToIPv4().ToString() + " port : " + PORT);
                listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);

                while(!token.IsCancellationRequested)
                {
                    if (allDone.WaitOne(WAITTIME))
                    {
                        break;
                    }
                }
      
            }

        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }

    void AcceptCallback(IAsyncResult ar)
    {  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
 
        allDone.Set();
  
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
    }

    void ReadCallback(IAsyncResult ar)
    {
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        int read = handler.EndReceive(ar);
  
        if (read > 0)
        {
            state.colorCode.Append(Encoding.ASCII.GetString(state.buffer, 0, read));
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }
        else
        {
            if (state.colorCode.Length > 1)
            { 
                // where it receieves data
                string content = state.colorCode.ToString();
                positionCube(content);
            }
            handler.Close();
        }
    }
    
    private void positionCube(string content) {
        string[] data = content.Split(',');
        Vector2 screenPosition = new Vector2(float.Parse(data[0]) * 1920/1280, 1080-(float.Parse(data[1]) * 1080 / 720));
        if (int.Parse(data[2]) < 300) {
            pressed = true;
        } else {
            pressed = false;
        }
        newPosition = screenPosition;
    }

    private void OnDestroy()
    {
        source.Cancel();
    }

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder colorCode = new StringBuilder();
    }


    public void OnApplicationQuit()
    {
        listener.Close();
    }
}