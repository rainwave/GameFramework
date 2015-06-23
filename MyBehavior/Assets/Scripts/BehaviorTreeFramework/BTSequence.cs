using System;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    // 一个节点 evaluate fail，则BTsequece fail
    // 与BTparallel 不同的是，sequence 会记录当前节点，当前节点条件fail 则fail
    // 而parallel 是每次评价条件时 从children头评价，一个fail，则parallel fail
    public class BTSequence : BTNode
    {
        protected BTNode m_activeChild = null;

        protected int m_activeIndex = -1;   // 下次tick 从activechild开始，而非从头开始

        protected override bool DoEvaluate()
        {
            if (m_children.Count == 0)
                return false;

            if (m_activeChild != null)
            {
                bool result = m_activeChild.Evaluate();

                if (!result)
                {
                    m_activeIndex = -1;     //只要有一个前节点不满足条件 从头开始判断
                    m_activeChild.clear();
                    m_activeChild = null;
                }

                return result;
            }
            else
            {
                return m_children[0].Evaluate();        // 一轮结束，重新再开始一轮
            }

        }

        public override BTResult Tick()
        {
            BTResult result = BTResult.Running;

            // first time
            if (m_activeChild == null)
            {
                m_activeChild = m_children[0];
                m_activeIndex = 0;
            }

            if (m_activeChild != null)
            {
                BTResult childResult = m_activeChild.Tick();
                if (childResult == BTResult.Ended)
                {
                    m_activeChild.clear();

                    m_activeIndex++;
                    if (m_activeIndex >= m_children.Count)
                    {
                        m_activeChild = null;
                        result = BTResult.Ended;
                    }
                    else
                    {
                        m_activeChild = m_children[m_activeIndex];
                    }

                }
            }

//            Debug.Log(result);
            return result;
        }
    }
}
