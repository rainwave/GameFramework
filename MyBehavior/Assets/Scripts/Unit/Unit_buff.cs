using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

public enum BuffType :ulong
{
    None            = 0x00000000,
    Invincible      = (ulong)1 << 1,
    Burn            = (ulong)1 << 2,   // 灼烧状态
    PhyDamUp = (ulong)1 << 3,   // 灼烧状态
    PhyDefDown = (ulong)1 << 4,   // 灼烧状态
    RecoverHP = (ulong)1 << 5,   // 灼烧状态
    RecoverMP = (ulong)1 << 6,   // 灼烧状态
    Pull = (ulong)1 << 7,   // 灼烧状态
    Strong = (ulong)1 << 8,   // 灼烧状态
    Rigidbody = (ulong)1 << 9,   // 灼烧状态
    All             = 0xffffffff,
}

public partial class Unit : MonoBehaviour
{
    protected List<Buff> m_buffList = new List<Buff>();
    [NonSerialized]
    protected BuffType m_buffState = BuffType.None;

    public string buffsDebug;

    public bool hasBuff(BuffType buffType)
    {
        BuffType buffState = m_buffState;
        buffState &= buffType;
        if (buffState == buffType)
            return true;
        
        return false;
    }

    public void addBuff(Buff buff)
    {
        if (hasBuff(buff.m_buffType))
        {
            return;
        }
        else
        { 
            
        }

        m_buffState |= buff.m_buffType;
        m_buffList.Add(buff);
        isAttrChange = true;

    }

    protected void removeBuff(Buff buff)
    {
        m_buffList.Remove(buff);
        m_buffState ^= ~buff.m_buffType;
        isAttrChange = true;
    }

    protected void updateBuffs()
    {
        buffsDebug = "";
        for (int i = 0, len = m_buffList.Count; i < len; i++)
        {
            buffsDebug += string.Format("-{0}", m_buffList[i].m_buffType.ToString());
            bool isFinish = m_buffList[i].update();
            if (isFinish)
            {
                removeBuff(m_buffList[i]);
                len--;
                i--;
            }

        }
    }
}

