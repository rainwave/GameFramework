using System;
using System.Collections.Generic;
using UnityEngine;
using BT;
using WTH;

public partial class Unit : MonoBehaviour
{
    public string LogCurNode = "";

    protected AI ai;

    protected virtual void initAI()
    {
        createAI(new AI(this));
    }

    protected void createAI(AI ai)
    {
        this.ai = ai == null ? new AI(this) : ai;
        ai.beforeEnter();
        ai.enter();
        ai.afterEnter();
    }

    protected void updateBT()
    {
        ai.update();
        if(!string.IsNullOrEmpty( Global.LogCurNode))
            LogCurNode = Global.LogCurNode;
	}

    public void addBTChild(BTNode child)
    {
        ai.addBTChild(child);
    }

    public void removeBTChild(BTNode child)
    {
        ai.addBTChild(child);
    }
}

