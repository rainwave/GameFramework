using System;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class UnitPlayer : Unit
{
    protected void Start()
    {
        base.Start();

        BTSequence sequece = new BTSequence();
        BTAction sayHello = new ActionSayHello();
        BTAction run = new ActionRun(this);
        BTAction hitNear = new ActionHitNear(this);
        sequece.addChild(sayHello);
        sequece.addChild(run);
        sequece.addChild(hitNear);
        this.addBTChild(sequece);
    }
}

