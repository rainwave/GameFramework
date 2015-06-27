using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

namespace BT
{
    public class ActionHitNear: BTAction
    {
        Timer timer;
        int count = 2;
        Unit m_srcUnit = null;
        Unit m_targetUnit = null;
        protected float hitDis = 3.0f;

        public ActionHitNear()
        {
            
        }

        protected override bool DoEvaluate()
        {
            if (m_srcUnit == null)
                m_srcUnit = m_blackboard.GetData<Unit>("SrcUnit");
            if (m_targetUnit == null)
                m_targetUnit = UnitMng.Instance.collectNearUnit(m_srcUnit);
            if (m_targetUnit == null)
                return false;
            if (Global.v3SqrDis(m_targetUnit.m_cacheTransform.localPosition, m_srcUnit.m_cacheTransform.localPosition) < hitDis * hitDis)
                return true;

            return false;
        }

        protected override void Enter()
        {
            m_srcUnit = m_blackboard.GetData<Unit>("SrcUnit");
            timer = new Timer();
            UITownPlayer.simpleShow();
        }

        protected override BTResult Excute()
        {
            
            if (timer.timeReach())
            {
                count--;
                if (m_srcUnit != null)
                {
                    m_srcUnit.useSkill(SkillIndex.Normal);
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
