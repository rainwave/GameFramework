using System;
using System.Collections.Generic;
using UnityEngine;
using BT;
using WTH;

public partial class Unit : MonoBehaviour
{
    protected Queue<string> m_cmdQu = new Queue<string>();
    void initCmd()
    {
        ClientMng.Instance.registerEvent(onReciveMsg);
    }

    // POS,name,x,y,z
    void onReciveMsg(string msg)
    {
        Debug.Log(msg);
        if (msg.StartsWith("POS"))
        {
            m_cmdQu.Enqueue(msg);
        }
    }

    void updateCmd()
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
        Debug.Log(msg);
        if (msg.StartsWith("POS"))
        {
            string[] str = msg.Split(',');
            string name = str[1];
            if (name != this.gameObject.name)
                return;

            Vector3 pos = new Vector3(int.Parse(str[2]), int.Parse(str[3]), int.Parse(str[4]));
            //GameObject go = GameObject.Find(name);
            //go.transform.localPosition = pos;
            this.moveToCmd(pos);
        }
    }
}

