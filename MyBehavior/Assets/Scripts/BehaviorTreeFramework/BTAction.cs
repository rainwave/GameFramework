using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BT
{
	
	public abstract class BTAction : BTNode {
        protected BTActionStatus m_actionStatus = BTActionStatus.Ready;

        public BTAction() : base() { }

        public BTAction(BTPrecondition precondition) : base(precondition)
		{
			
		}

        protected virtual void Enter()
        { 
        }

        protected virtual void Exit()
        {
        }

        protected virtual BTResult Excute()
        {
            return BTResult.Running;
        }

        // tick 和excute 区别在于，实际上都是循环调用的
        //　只不过为了增加enter exit，所以才细分了
        public override BTResult Tick()
        {
            BTResult result = BTResult.Ended;
            if (m_actionStatus == BTActionStatus.Ready)
            {
                Enter();
                m_actionStatus = BTActionStatus.Running;
            }
            if (m_actionStatus == BTActionStatus.Running)
            {
                result = Excute();
            }
            if (result == BTResult.Ended)
                Exit();
            return result;
        }

        // 为了下次还能进入此action时为重置，不然一直处于running状态
        public override void clear()
        {
            if (m_actionStatus != BTActionStatus.Ready)
            {
                Exit();
                m_actionStatus = BTActionStatus.Ready;
            }
        }

        public enum BTActionStatus
        {
            Ready = 1,
            Running = 2,
        }
	}


}
