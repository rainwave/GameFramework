using System;
using System.Collections.Generic;
using UnityEngine;
using BT;

namespace WTH
{
    public class AIMoveAttack : AI
    {
        public AIMoveAttack(Unit srcUnit) : base(srcUnit)
        {

        }

        public override void enter()
        {
            base.enter();

            BTSequence sequece = new BTSequence();
            BTAction sayHello = new ActionSayHello();
            BTAction run = new ActionRun();
            BTAction hitNear = new ActionHitNear();
            sequece.addChild(sayHello);
            sequece.addChild(run);
            sequece.addChild(hitNear);
            this.addBTChild(sequece);
        }


    }
}
