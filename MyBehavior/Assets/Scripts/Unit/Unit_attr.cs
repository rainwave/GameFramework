using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitAttr
{
    public int curHP;
    public int maxHP;
    public int phyDam;
    public int phyDef;
    public float phyDamUpPercent;
    public float phyDefUpPercent;

    public UnitAttr clone()
    {
        UnitAttr newAttr = new UnitAttr();
        newAttr.curHP = curHP;
        newAttr.maxHP = maxHP;
        newAttr.phyDam = phyDam;
        newAttr.phyDef = phyDef;
        newAttr.phyDamUpPercent = phyDamUpPercent;
        newAttr.phyDefUpPercent = phyDefUpPercent;
        return newAttr;
    }
}

public partial class Unit : MonoBehaviour
{
    protected bool isAttrChange = true;
    private UnitAttr baseAttr = new UnitAttr();
    [SerializeField]
    protected UnitAttr finalAttr = new UnitAttr();
    public UnitAttr FinalAttr
    {
        get
        {
            if (isAttrChange)
            {
                isAttrChange = false;
                    
                int curHP = finalAttr.curHP;
                finalAttr = baseAttr.clone();
                for (int i = 0, len = m_buffList.Count; i < len; i++)
                {
                    m_buffList[i].effectAura(finalAttr);
                }

                finalAttr.phyDef = Mathf.Max(1, Mathf.FloorToInt(finalAttr.phyDef * (1 + finalAttr.phyDefUpPercent * 0.01f)));

                finalAttr.phyDam = Mathf.Max(1, Mathf.FloorToInt(finalAttr.phyDam * (1 + finalAttr.phyDamUpPercent * 0.01f)));
                  
                finalAttr.curHP = curHP;
            }

            return finalAttr;
        }
    }

    public void setAttrHP(int hp)
    {
        finalAttr.curHP = hp;
        finalAttr.maxHP = hp;
        isAttrChange = true;
    }

    public void setBaseAttr(UnitAttr attr)
    {
        if (attr == null)
            return;
        baseAttr = attr.clone();
        finalAttr = attr.clone();
        isAttrChange = true;
    }

    public void updateHP()
    {
        if (this.renderer != null)
        {
            Material mat = this.renderer.material;

            if (mat != null && finalAttr.curHP == 0)
            {
                mat.color = Color.red;
            }
            if (mat != null && finalAttr.curHP > 0)
            {
                mat.color = Color.green;
            }
        }

    }
}

