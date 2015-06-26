using UnityEngine;

namespace WTH
{
    public class Buff
    {
        public BuffType m_buffType;
        public float m_startTime = -9999f;
        public float m_lastDamageTime = -9999f;
        public float m_duration = 2.0f;
        public float m_timeGap = -1;
        public int m_value;
        public Unit m_srcUnit;
        public Unit m_effectUnit;

        public delegate void AttrAddtionFun(UnitAttr targetAttr);
        public AttrAddtionFun m_attrAddtionFun;

        public delegate void EffectReach(Buff buff, Unit targetUnit);
        public EffectReach m_effectReach;

        public Buff(Unit effectUnit,BuffType buffType, float duration,int value, float timeGap = -1,Unit srcUnit = null)
        {
            m_buffType = buffType;
            m_duration = duration;
            m_timeGap = timeGap;
            m_effectUnit = effectUnit;
            m_srcUnit = srcUnit;
            m_value = value;
            m_startTime = Time.realtimeSinceStartup;
        }

        public virtual void effectAura(UnitAttr unitAttr)
        {
            if (m_attrAddtionFun != null)
                m_attrAddtionFun(unitAttr);
        }

        public virtual void genDamage()
        {
            if (m_effectReach != null)
                m_effectReach(this, m_effectUnit);
        }

        public bool update()
        {
            if (Time.realtimeSinceStartup - m_startTime > m_duration)
            {
                return true;
            }
            else if( m_timeGap > 0 && Time.realtimeSinceStartup - m_lastDamageTime > m_timeGap)
            {
                m_lastDamageTime = Time.realtimeSinceStartup;

                genDamage();

                return false;
            }

            return false;
        }
    }
}
