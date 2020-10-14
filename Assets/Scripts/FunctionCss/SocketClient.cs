using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class SocketClient : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Client();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SimulateMultiUserClient();
        }
    }
    private void Client()
    {
        TcpClient client = new TcpClient();
        try
        {
            client.Connect(IPAddress.Parse("192.168.10.1"), 5040);//同步方法，连接成功、抛出异常、服务器不存在等之前程序会被阻塞
        }
        catch (Exception ex)
        {
            Debug.Log("客户端连接异常：" + ex.Message);
        }
        Debug.Log("LocalEndPoint = " + client.Client.LocalEndPoint + ". RemoteEndPoint = " + client.Client.RemoteEndPoint);
    }
    private void SimulateMultiUserClient()
    {
        TcpClient client;
        for (int i = 0; i < 2; i++)
        {
            client = new TcpClient();
            try
            {
                client.Connect(IPAddress.Parse("192.168.0.13"), 10001);
            }
            catch (Exception ex)
            {
                Debug.Log("客户端连接异常：" + ex.Message);
            }
            Debug.Log("LocalEndPoint = " + client.Client.LocalEndPoint + ". RemoteEndPoint = " + client.Client.RemoteEndPoint);
        }
    }
}

