using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttr
{
    public int curHP;
    public int maxHP;
    public int atk;
    public int def;
}

public partial class Unit : MonoBehaviour
{
    public UnitAttr finalAttr;

    public void updateHP()
    {
        Debug.Log(name + finalAttr.curHP);
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

