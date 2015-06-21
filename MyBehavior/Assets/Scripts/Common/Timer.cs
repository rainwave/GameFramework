using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    class Timer
    {
        float m_interval;
        float m_previousTime;
        public Timer(float interval = 1.0f)
        {
            m_interval = interval;
            m_previousTime = Time.time;
        }

        public bool timeReach()
        {
            float curTime = Time.time;
            if (curTime - m_previousTime > m_interval)
            {
                m_previousTime = curTime;
                return true;
            }
            return false;
        }
    }
}
