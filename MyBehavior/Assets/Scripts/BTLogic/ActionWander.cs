using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

namespace BT
{
    // 徘徊 行为
    public class ActionWander: BTAction
    {
        Unit m_srcUnit = null;
        public List<Vector3> m_wanderPosList = new List<Vector3>();
        

        public ActionWander()
        {
            
        }

        protected override bool DoEvaluate()
        {
            if (m_srcUnit == null)
                m_srcUnit = m_blackboard.GetData<Unit>("SrcUnit");
            
            return false;
        }

        protected override void Enter()
        {
            if(m_srcUnit == null)
                m_srcUnit = m_blackboard.GetData<Unit>("SrcUnit");
        }

        protected override BTResult Excute()
        {
            
            return BTResult.Running;
        }

        protected override void Exit()
        {
           
        }

        public void addWanderPos(Vector3 pos)
        {
            m_wanderPosList.Add(pos);
        }

        public void removeWanderPos(Vector3 pos)
        {
            m_wanderPosList.Remove(pos);
        }
    }
}
