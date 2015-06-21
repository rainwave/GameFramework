using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitMng : MonoSingleton<UnitMng>
{
    public List<Unit> m_units = new List<Unit>();

    public void addUnit(Unit unit)
    { 
        if(!m_units.Contains(unit))
        {
            m_units.Add(unit);
        }
    }

    public void removeUnit(Unit unit)
    {
        if (m_units.Contains(unit))
        {
            m_units.Remove(unit);
        }
    }

    public Unit collectNearUnit(Unit srcUnit)
    {
        float min = 99999999;
        Unit targetUnit = null;
        for (int i = 0, len = m_units.Count; i < len; i++)
        {
            if (m_units[i].Equals(srcUnit))
                continue;
            float dis = Global.v3SqrDis(srcUnit.m_cacheTransform.localPosition, m_units[i].m_cacheTransform.localPosition);
            if (dis < min)
            {
                min = dis;
                targetUnit = m_units[i];
            }
        }

        return targetUnit;
    }

}

