using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

namespace BT
{
    public class ActionSayWTH: BTAction
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
        }

        protected override BTResult Excute()
        {
            if (count == 0)
            {
                return BTResult.Ended;
            }
            if (timer.timeReach())
            {
                count--;
                Debug.Log("in action WTH");
            }
            return BTResult.Running;
        }

        protected override void Exit()
        {
           
        }
    }
}
