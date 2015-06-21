using UnityEngine;
using System.Collections;
using BT;

public class BTMng : MonoSingleton<BTMng>{
    BTNode root = new BTPrioritySelector();
	
	// Update is called once per frame
	protected override void Update () {
        if (root.Evaluate())
            root.Tick();
	}

    public void addChild(BTNode child )
    {
        root.addChild(child);
    }

    public void removeChild(BTNode child)
    {
        root.removeChild(child);
    }
}
