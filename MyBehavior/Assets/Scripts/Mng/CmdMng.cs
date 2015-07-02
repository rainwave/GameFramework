using System.Collections.Generic;
using UnityEngine;
using WTH;

public class CmdMng : MonoSingleton<CmdMng>
{
    protected Queue<string> m_cmdQu = new Queue<string>();

    protected override void Start()
    {
        ClientMng.Instance.registerEvent(onReciveMsg);
    }

    // POS,name,x,y,z
    void onReciveMsg(string msg)
    {
        if (msg.StartsWith("Damage"))
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
        if (msg.StartsWith("Damage"))
        {
            DamageMng.Instance.addResultSet(msg);
        }
    }

}

