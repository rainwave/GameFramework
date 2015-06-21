using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

namespace BT
{
    public class ActionHitForward: BTAction
    {
        Timer timer = new Timer();
        int count = 15;
        Unit m_srcUnit = null;
        float m_angle;
        float m_maxDis;

        public ActionHitForward(Unit srcUnit,float angle,float maxDis)
        {
            m_srcUnit = srcUnit;
            m_angle = angle;
            m_maxDis = maxDis;
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
            count = 15;
        }
    }
}
