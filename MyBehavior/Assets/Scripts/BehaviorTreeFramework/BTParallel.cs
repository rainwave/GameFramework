using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BT
{
    // 如果一个child evaluate fail ，则BTparallel fail
    // Parallel 在一个BTParallel Tick中 所有子节点都执行一次 (并行)
    // 对于返回End 的子节点，以后的Tick 不再更新此节点
	public  class BTParallel : BTNode {
        protected ParallelType m_parallelType;
        protected List<BTResult> m_resultList = new List<BTResult>();

        public BTParallel(ParallelType parallelType)
            : this(parallelType,null)
        {
           
        }

        public BTParallel(ParallelType parallelType, BTPrecondition precondition)
            : base(precondition)
        {
            m_parallelType = parallelType;
        }

        protected override bool DoEvaluate()
        {
            for (int i = 0, len = m_children.Count; i < len; i++)
            {
                if (!m_children[i].Evaluate())
                    return false;
            }

            return true;
        }

        public override BTResult Tick()
        {
            BTResult result = BTResult.Running;

            if (m_parallelType == ParallelType.And)
            {
                int endNodeCount = 0;
                for (int i = 0, len = m_children.Count; i < len; i++)
                {
                   if(m_resultList[i] == BTResult.Running)
                   {
                        m_resultList[i] = m_children[i].Tick();
                   }
                   if(m_resultList[i] == BTResult.Ended)
                   {
                        endNodeCount++;
                   }
                }
                // 所有子节点结束 BTparallel 结束
                if(endNodeCount == m_children.Count)
                {
                    result = BTResult.Ended;
                }
            }
            else
            {
                result = BTResult.Running;

                for (int i = 0, len = m_children.Count; i < len; i++)
                {
                    if (m_resultList[i] == BTResult.Running)
                    {
                        m_resultList[i] = m_children[i].Tick();
                    }
                    // 只要有一个结束 则BTparallel 结束
                    if (m_resultList[i] == BTResult.Ended)
                    {
                        result = BTResult.Ended;
                        break;
                    }
                }
            }

            if (result == BTResult.Ended)
            {
                resetResultList();
            }

            return result;
        }

        protected void resetResultList()
        {
            for (int i = 0, len = m_resultList.Count; i < len; i++)
            {
                m_resultList[i] = BTResult.Running;
            }
        }

        public override void addChild(BTNode child)
        {
            if (child != null && !m_children.Contains(child))
            {
                base.addChild(child);
                m_resultList.Add(BTResult.Running);
            }
        }

        public override void removeChild(BTNode child)
        {
            if (child != null && m_children.Contains(child))
            {
                int index = m_children.IndexOf(child);
                m_resultList.RemoveAt(index);
                base.removeChild(child);
            }
        }

        public enum ParallelType
        { 
            And = 0,
            Or = 1,
        }
	}


}
