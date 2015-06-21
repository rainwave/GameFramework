using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

namespace BT
{
    public class ActionRun: BTAction
    {
        Unit m_srcUnit = null;
        Vector3 m_targetPos = Vector3.zero;

        public ActionRun()
        {
            
        }

        protected override bool DoEvaluate()
        {
            return true;
        }

        protected override void Enter()
        {
            m_srcUnit = m_blackboard.GetData<Unit>("SrcUnit");
            Unit nearUnit = UnitMng.Instance.collectNearUnit(m_srcUnit);
            if (nearUnit != null)
                m_targetPos = nearUnit.m_cacheTransform.localPosition;
        }

        protected override BTResult Excute()
        {
            Debug.Log("m_targetPos " + m_targetPos);

            runToTarget(m_srcUnit.m_cacheTransform, m_targetPos, 0.5f);

            if (Global.v3Near(m_srcUnit.m_cacheTransform.localPosition, m_targetPos))
                return BTResult.Ended;
            return BTResult.Running;
        }

        protected void faceToTarget(Transform transform, Vector3 targetPos)
        {
            Vector3 dir = targetPos - transform.localPosition;
            transform.forward = dir;
        }

        protected void runToTarget(Transform transform, Vector3 targetPos,float disEveryTime)
        {
            //faceToTarget(m_srcUnit.m_cacheTransform, m_targetPos);
            //transform.localPosition += transform.forward * disEveryTime;
            Vector3 dir = targetPos - transform.localPosition;
            transform.Translate(dir * disEveryTime);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }

        protected override void Exit()
        {
           
        }
    }
}
