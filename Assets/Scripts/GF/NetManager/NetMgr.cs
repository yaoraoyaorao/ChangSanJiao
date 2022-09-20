using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GF;
using System.Net.Sockets;
using System.Net;
using System.Threading;

public class NetMgr : SingletonMono<NetMgr>
{
    public string IP;
    public int Port;
    public bool isConnect;

    //Socket
    private Socket socket;
    private byte[] data = new byte[1024];
    private int receiveNum;

    //发送消息队列
    private Queue<object> sendQueue;

    private Queue<object> receiveQueue;

    public void Connect()
    {
        if (isConnect) return;
        try
        {
            if (string.IsNullOrEmpty(IP) || Port == 0)
            {
                IP = "127.0.0.1";
                Port = 8080;
            }

            //IP信息
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(IP), Port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sendQueue = new Queue<object>();
            receiveQueue = new Queue<object>();

            socket.Connect(ip);

            //ThreadPool.QueueUserWorkItem(SendMsg);
            //ThreadPool.QueueUserWorkItem(ReceiveMsg);
        }
        catch (SocketException e)
        {
            if (e.ErrorCode == 10061)
            {
                print("服务器拒绝连接");
            }
            else
            {
                print("服务器连接错误：错误代码:" + e.Message + " 错误提示：" + e.Message);
            }
            throw;
        }
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
    {
        if (socket == null) return;
        isConnect = false;
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}
