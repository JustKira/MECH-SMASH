using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client : MonoBehaviour
{
    const int PORT_NO = 5555;
    const string Server_IP = "127.0.0.1";

    TcpClient client = new TcpClient();

    public void Start()
    {
        //Start Connection
        StartConnection();
    }

    public void StartConnection() 
    {
        client.Connect(Server_IP, PORT_NO);

        StartCoroutine(sendCoroutine(client));
    }

    IEnumerator sendCoroutine(TcpClient client)
    {

        NetworkStream stream = client.GetStream();
        byte[] init_send_buffer = Encoding.UTF8.GetBytes("Connected");
        stream.Write(init_send_buffer, 0, init_send_buffer.Length);

        byte[] init_recv_buffer = new byte[client.ReceiveBufferSize];
        int init_recv = stream.Read(init_recv_buffer, 0, client.ReceiveBufferSize);

        string init_data = Encoding.UTF8.GetString(init_recv_buffer, 0, init_recv);
        Debug.Log(init_data);

        int testloopsize = 5;
        int test = 0;

        int[] ints = new int[testloopsize];

        TestObject testObject = new TestObject(ints);

        while (true)
        {
            
            for (int i = 0; i < testloopsize; i++)
            {
                testObject.ints[i] =  Random.Range(0, 10);
            }

            int[] intsRecv = new int[testloopsize];

            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(testObject));
            stream.Write(buffer, 0, buffer.Length);

            yield return new WaitForSecondsRealtime(2f);

            byte[] recvbuffer = new byte[client.ReceiveBufferSize];
            int recv = stream.Read(recvbuffer, 0, client.ReceiveBufferSize);
            string data = Encoding.UTF8.GetString(recvbuffer, 0, recv);

            Debug.Log(data); 
            test++;  
        }
    }
}

class TestObject
{
    public int[] ints;
    public TestObject(int[] ints)
    {
        this.ints = ints;
    }   
}


