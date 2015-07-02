using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;

public enum HostType
{ 
    Client = 0,
    MClient = 1
}

public class TestSocket : MonoSingleton<TestSocket> {
    public HostType m_hostType = HostType.Client;

   
    // ================Client================================

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

    // ================================================

    protected override void Awake()
    {
        base.Awake();
        
        if (m_hostType == HostType.Client)
        {
            //InitConnect();
            this.gameObject.AddComponent<ClientMng>();
        }
        else
        {
            //Listen();
            this.gameObject.AddComponent<MClientMng>();

            this.gameObject.AddComponent<ClientMng>();
        }
        
    }
}