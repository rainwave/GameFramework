using System;
using System.Collections.Generic;
using UnityEngine;
using BT;

namespace WTH
{
    public interface IAI
    {
        void beforeEnter();
        void enter();
        void update();
        void exit();
    }

    public class AI : IAI
    {
        protected Unit m_contoller;
        BTNode root = new BTPrioritySelector();

        public Blackboard m_blackboard;
        public const string RESET = "Rest";
        public const string SRCUNIT = "SrcUnit";
        public bool m_isRunning = true;              // 暂时留着  

        protected AI() : this(null) { }

        public AI(Unit contoller)
        {
            m_contoller = contoller;
        }

        // =============================================================
        // 顺序不能乱 先创建黑板、再构建树，然后激活树(传入黑板)

        public virtual void beforeEnter()
        { 
            if (m_contoller == null)
                return;

            m_blackboard = m_contoller.GetComponent<Blackboard>();
            if (m_blackboard == null)
            {
                m_blackboard = m_contoller.gameObject.AddComponent<Blackboard>();
            }

            m_blackboard.SetData<bool>(RESET, false);
            m_blackboard.SetData<Unit>(SRCUNIT, m_contoller);

        }

        public virtual void enter()
        {
            
        }

        public virtual void afterEnter()
        {
            root.active(m_blackboard);
        }


        // ====================================================================

        public virtual void update()
        {
            if (!m_isRunning)
                return;

            if (m_blackboard.GetData<bool>(RESET))
            {
                reset();
                m_blackboard.SetData<bool>(RESET, false);
            }

            if (root.Evaluate())
                root.Tick();
        }

        public void reset()
        {
            if (root != null)
            {
                root.clear();
            }
        }

        public void addBTChild(BTNode child)
        {
            root.addChild(child);
        }

        public void removeBTChild(BTNode child)
        {
            root.removeChild(child);
        }

        public virtual void exit()
        {
            reset();
        }
    }
}
