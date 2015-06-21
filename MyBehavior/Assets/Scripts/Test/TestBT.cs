using System;
using System.Collections.Generic;
using UnityEngine;
using BT;

class TestBT : MonoBehaviour
{
    public void Start()
    { 
        UnitAttr attr1 = new UnitAttr();
        attr1.atk = 100;
        attr1.curHP = 1000;
        attr1.maxHP = 1000;
        attr1.def = 0;

        UnitAttr attr2 = new UnitAttr();
        attr2.atk = 100;
        attr2.curHP = 1000;
        attr2.maxHP = 1000;
        attr2.def = 0;

        UnitGenerator.genUnit(attr1);
        Unit player = UnitGenerator.genUnit<UnitPlayer>(attr2);
        player.m_cacheTransform.localPosition = new Vector3(5, 0, 0);
        
    }
}

