using System;
using System.Collections.Generic;
using UnityEngine;
using BT;

public partial class Unit : MonoBehaviour
{
    BTNode root = new BTPrioritySelector();

    protected void updateBT()
    {
        if (root.Evaluate())
            root.Tick();
	}

    public void addBTChild(BTNode child)
    {
        root.addChild(child);
    }

    public void removeBTChild(BTNode child)
    {
        root.removeChild(child);
    }
}

