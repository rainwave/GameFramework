using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

namespace BT
{
    public class ActionSayHello: BTAction
    {
        Timer timer;
        int count = 5;

        protected override bool DoEvaluate()
        {
            if (count > 0)
                return true;
            else
                return false;
        }

        protected override void Enter()
        {
            timer = new Timer();
            //UITownMain.simpleShow();
        }

        protected override BTResult Excute()
        {
            
            if (timer.timeReach())
            {
                count--;
                Debug.Log("in action hello " + count);
            }
            if (count == 0)
            {
                return BTResult.Ended;
            }
            return BTResult.Running;
        }

        protected override void Exit()
        {
            count = 5;
        }
    }
}
