using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

namespace BT
{
    // 靠近 行为
    public class ActionSeek: BTAction
    {
        Unit m_srcUnit = null;
        Unit m_targetUnit = null;        // 后期加入pos，靠近某个位置，因为靠近目标不一定存在
        protected float disMax = 50f;    // 视野范围
        protected float disMin = 2f;     // 最近距离

        public ActionSeek()
        {
            
        }

        protected override bool DoEvaluate()
        {
            if (m_srcUnit == null)
                m_srcUnit = m_blackboard.GetData<Unit>("SrcUnit");
            if (m_targetUnit == null || m_targetUnit.isDie)
                m_targetUnit = UnitMng.Instance.collectNearUnit(m_srcUnit);
            if (m_targetUnit == null)
                return false;
            if (Global.v3SqrDis(m_targetUnit.m_cacheTransform.localPosition, m_srcUnit.m_cacheTransform.localPosition) < disMax * disMax)
                return true;

            return false;
        }

        protected override void Enter()
        {
            if(m_srcUnit == null)
                m_srcUnit = m_blackboard.GetData<Unit>("SrcUnit");
        }

        protected override BTResult Excute()
        {
            if (m_srcUnit.isNaving)
                return BTResult.Running;
            if (m_targetUnit == null || m_targetUnit.isDie)
                m_targetUnit = UnitMng.Instance.collectNearUnit(m_srcUnit);
            if (m_targetUnit == null)
               return  BTResult.Ended;

            m_srcUnit.moveTo(m_targetUnit.m_cacheTransform.localPosition);
            m_srcUnit.changeMotion("run");

            if (Global.v3SqrDis(m_srcUnit.m_cacheTransform.localPosition, m_targetUnit.m_cacheTransform.localPosition) < disMin*disMin)
                return BTResult.Ended;
            return BTResult.Running;
        }

        protected override void Exit()
        {
           
        }
    }
}
