﻿using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    public void hit(Unit target)
    {
        if (target == null)
            return;

        if (target.finalAttr.curHP > 0)
        {
            changeMotion("hit");
            target.finalAttr.curHP -= (this.finalAttr.phyDam - target.finalAttr.phyDef);
            Debug.Log("hit target ,curhp == " + target.finalAttr.curHP);
        }
        else
        {
            Debug.Log("hit target , die");
        }

    }

    public void hitRound()
    { 
        
    }

}

