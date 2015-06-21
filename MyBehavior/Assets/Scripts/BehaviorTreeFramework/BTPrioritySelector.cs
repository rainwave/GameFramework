using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BT
{
	public class BTPrioritySelector: BTNode {

        protected BTNode m_activeChild = null;

        protected override bool DoEvaluate()
        {
            for (int i = 0, len = m_children.Count; i < len; i++)
            {
                if (m_children[i].Evaluate())
                {
                    if (m_activeChild != null && m_activeChild != m_children[i])
                    {
                        m_activeChild.clear();
                    }
                    m_activeChild = m_children[i];

                    return true;
                }
            }

            if (m_activeChild != null)
            {
                m_activeChild.clear();
            }

            return false;
        }

        public override BTResult Tick()
        {
            BTResult result = BTResult.Ended;
            if (m_activeChild != null)
            {
                result = m_activeChild.Tick();
                if (result == BTResult.Ended)
                {
                    m_activeChild.clear();
                    m_activeChild = null;
                }
            }

            return result;
        }
	}


}
