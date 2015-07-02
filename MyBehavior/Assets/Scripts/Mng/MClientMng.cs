using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
using WTH;

public class MClientMng : MonoSingleton<MClientMng>
{
    protected Queue<string> m_cmdQu = new Queue<string>();

    protected List<string> m_createdUnits = new List<string>();

    protected override void Start()
    {
        ClientMng.Instance.registerEvent(onReciveMsg);
    }

    // POS,name,x,y,z
    void onReciveMsg(string msg)
    {
        if (msg.StartsWith("CUnit"))
        {
            m_cmdQu.Enqueue(msg);
        }
    }

    protected override void Update()
    {
        if (m_cmdQu.Count == 0)
            return;
        int count = Mathf.Min(m_cmdQu.Count, 10);
        for (int i = 0; i < count; i++)
        {
            string str = m_cmdQu.Dequeue();
            handleMsg(str);
        }
    }


    void handleMsg(string msg)
    {
        if (msg.StartsWith("CUnit"))
        {
            string name = msg.Split(',')[1];
            if (m_createdUnits.Contains(name))
                return;
            foreach (string name1 in m_createdUnits)
            {
                ClientMng.Instance.ClientSend(string.Format("CUnit,{0},{1},{2},{3}", name1, 0, 0, 0));
            }

            m_createdUnits.Add(name);
        }
    }

}

