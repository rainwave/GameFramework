using System;
using System.Collections.Generic;
using UnityEngine;
using BT;
using WTH;

public class UnitPlayer : Unit
{
    protected override void initAI()
    {
        createAI(new AIMoveAttack(this));
    }
}

