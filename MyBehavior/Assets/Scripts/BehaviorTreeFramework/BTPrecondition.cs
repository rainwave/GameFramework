using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BT
{
    // 事实上，条件有两种，一种是在树上，例如Sequence 序列，一堆条件节点满足后才能执行最后的行为节点
    // 一种是 执行行为节点前，这个行为节点还需要一些外部的条件，这个条件可以传入行为节点的构造器中
	public abstract class BTPrecondition : BTNode {

		public string m_Name;

        // 条件节点特有的,给其他节点做为外部条件判断的，构造器中传入的节点就要用这个判断,不挂在树上
		public virtual bool check()
		{
			return true;
		}

        // 例如sequence序列时可能就要用到此，这是挂在树上的
        public override BTResult Tick()
        {
            bool succ = check();
            if (succ)
                return BTResult.Ended;
            else
                return BTResult.Running;
        }
	}


}
