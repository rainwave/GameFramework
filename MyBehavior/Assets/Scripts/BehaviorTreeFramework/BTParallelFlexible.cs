using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT
{
    /// <summary>
    /// BTParallelFlexible evaluates all children, if all children fails evaluation, it fails. 
    /// Any child passes the evaluation will be regarded as active.
    /// 
    /// BTParallelFlexible ticks all active children, if all children ends, it ends.
    /// 
    /// NOTE: Order of child node added does matter!
    /// </summary>
    public class BTParallelFlexible : BTNode
    {

        private List<bool> _activeList = new List<bool>();


        public BTParallelFlexible(BTPrecondition precondition = null) : base(precondition) { }

        protected override bool DoEvaluate()
        {
            int numActiveChildren = 0;

            for (int i = 0; i < m_children.Count; i++)
            {
                BTNode child = m_children[i];
                if (child.Evaluate())
                {
                    _activeList[i] = true;
                    numActiveChildren++;
                }
                else
                {
                    _activeList[i] = false;
                }
            }

            if (numActiveChildren == 0)
            {
                return false;
            }

            return true;
        }

        public override BTResult Tick()
        {
            int numRunningChildren = 0;

            for (int i = 0; i < m_children.Count; i++)
            {
                bool active = _activeList[i];
                if (active)
                {
                    BTResult result = m_children[i].Tick();
                    if (result == BTResult.Running)
                    {
                        numRunningChildren++;
                    }
                }
            }

            if (numRunningChildren == 0)
            {
                return BTResult.Ended;
            }

            return BTResult.Running;
        }

        public override void addChild(BTNode aNode)
        {
            base.addChild(aNode);
            _activeList.Add(false);
        }

        public override void removeChild(BTNode aNode)
        {
            int index = m_children.IndexOf(aNode);
            _activeList.RemoveAt(index);
            base.removeChild(aNode);
        }

        public override void clear()
        {
            base.clear();

            foreach (BTNode child in m_children)
            {
                child.clear();
            }
        }
    }
}
