using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BT
{
    public enum BTResult
    { 
        Running,
        Ended
    }

	public abstract class BTNode {

		public string m_Name;

		protected List<BTNode> m_children = new List<BTNode> ();

		protected BTPrecondition m_precondition;

        // 是否被激活，激活时为当前选中节点,可以执行tick循环了.
        public bool m_active = false;

		protected float m_interval = 0.1f;
		protected float m_previousTime = 0;

		public virtual void setInterval(float _time)
		{
			m_interval = _time;
		}

		public BTNode(): this(null)
		{

		}

		public BTNode(BTPrecondition precondition)
		{
			m_precondition = precondition;
		}

		public bool Evaluate()
		{
			bool coolDownOK = checkTimer ();

            return coolDownOK && (m_precondition == null || m_precondition.check()) && DoEvaluate();
		}

        /// <summary>
        /// 子类自定义执行条件
        /// 例如：评估子节点有否满足自身条件的
        /// </summary>
		protected virtual bool DoEvaluate()
		{

			return true;
		}


        // runing 表示节点还在进行中
        // end 表示节点完成，成功
        // 如果是条件节点，runing表示本条件还不满足 end 表示条件满足（例如在攻击范围内)
		public virtual BTResult Tick()
		{
            return BTResult.Running;
		}

		protected bool checkTimer()
		{
			if (Time.time - m_previousTime > m_interval) 
			{
				m_previousTime = Time.time;
				return true;
			}
			return false;

		}

		public virtual void addChild(BTNode child)
		{
			if(child != null && !m_children.Contains(child))
                m_children.Add(child);

		}

        public virtual void removeChild(BTNode child)
        {
            if (child != null && m_children.Contains(child))
                m_children.Remove(child);
        }

        public virtual void clear()
        { 
        }
	}


}
