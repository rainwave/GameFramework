using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class ClientMng : MonoSingleton<ClientMng>
{
    int serverPort = 8000;//服务器端口

    public delegate void OnReciveMsg(string msg);
    public OnReciveMsg m_onReciveMsg;
    public void registerEvent(OnReciveMsg onReciveMsg)
    {
        m_onReciveMsg += onReciveMsg;
    }
    public void removeEvent(OnReciveMsg onReciveMsg)
    {
        m_onReciveMsg -= onReciveMsg;
    }

    protected override void Awake()
    {
        base.Awake();
       
        InitConnect();
    }

    public static Socket ClientSocket;

    private void InitConnect()
    {

        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        string ip = GetIP().ToString();//服务器ip

        IPAddress ipa = IPAddress.Parse(ip);

        IPEndPoint iep = new IPEndPoint(ipa, serverPort);

        try
        {

            ClientSocket.Connect(iep);//连接到服务器

            Thread thread = new Thread(new ThreadStart(ClientReceive));

            thread.Start();

        }

        catch (Exception ex)
        {

            Debug.Log(ex.Message);

            //clientReceiveValue = ex.Message;

        }

    }

    private IPAddress GetIP()
    {     /*获取本地服务器的ip地址  */

        IPAddress ip = IPAddress.Parse("127.0.0.1");

        return ip;

    }
    /*接收来自服务器上的信息*/

    public void ClientReceive()
    {

        //clientReceiveValue = "已经建立连接准备接受数据";

        while (true)
        {

            byte[] bytes = new byte[100];

            int rev = ClientSocket.Receive(bytes, bytes.Length, 0);//将数据从连接的   Socket   接收到接收缓冲区的特定位置。

            if (rev <= 0)
            {

                break;

            }

            string strev = System.Text.Encoding.Default.GetString(bytes);

            //clientReceiveValue = ("服务器对客户端说:" + strev + "\r\n");

            // 回调，通知
            m_onReciveMsg(strev.Trim());
        }

    }

    public void ClientSend(string msg)
    {

        if (ClientSocket.Connected)//判断Socket是否已连接
        {
            byte[] SendMessage = new byte[100];

            SendMessage = Encoding.ASCII.GetBytes(msg);

            ClientSocket.Send(SendMessage);//从数据中的指示位置开始将数据发送到连接的Socket。

        }

        else
        {

            Debug.Log("未建立连接！");

            //clientSendValue = "未建立连接！";

        }

    }

    void OnDestroy()
    {
        ClientSocket.Close();
        ClientSocket = null;
    }

}

