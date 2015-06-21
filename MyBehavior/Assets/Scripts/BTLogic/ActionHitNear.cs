using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

namespace BT
{
    public class ActionHitNear: BTAction
    {
        Timer timer;
        int count = 15;
        Unit m_srcUnit = null;

        public ActionHitNear()
        {
            
        }

        protected override bool DoEvaluate()
        {
            if (count > 0)
                return true;
            else
                return false;
        }

        protected override void Enter()
        {
            m_srcUnit = m_blackboard.GetData<Unit>("SrcUnit");
            timer = new Timer();
        }

        protected override BTResult Excute()
        {
            
            if (timer.timeReach())
            {
                count--;
                if (m_srcUnit != null)
                {
                    Unit targetUnit = UnitMng.Instance.collectNearUnit(m_srcUnit);
                    m_srcUnit.hit(targetUnit);
                }
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
