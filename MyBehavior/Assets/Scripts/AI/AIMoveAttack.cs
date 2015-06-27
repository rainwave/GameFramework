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
            BTParallelFlexible para = new BTParallelFlexible();
            BTAction sayHello = new ActionSayHello();
            BTAction run = new ActionRun();
            BTAction hitNear = new ActionHitNear();

            sequece.addChild(sayHello);
            sequece.addChild(run);

            para.addChild(sequece);
            para.addChild(hitNear);

            this.addBTChild(para);
        }


    }
}
