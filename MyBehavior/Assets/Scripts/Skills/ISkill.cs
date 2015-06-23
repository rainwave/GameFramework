using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    public class ISkill
    {
        public string m_motionName;                     // 动作
        public string m_effect;
        public Unit m_srcUnit;
        public float m_genDamageEveryNorTime = 0.8f;      // 隔多少正交化时间产生一次IDamage
        public float m_endDamageNorTime = 0.9f;

        // ==============================================================
        private float nextGenDamageNorTime = -1;

        public ISkill()
        { 
        
        }

        // 技能的开始
        public void useSkill()
        {
            if (m_srcUnit == null || m_srcUnit.Equals(null) || m_srcUnit.m_animator == null)
                return;
            m_srcUnit.changeMotion(m_motionName);
        }

        // 技能的更新
        public bool updateSkill()
        {
            if (m_srcUnit == null || m_srcUnit.Equals(null) || m_srcUnit.m_animator == null)
                return true;
            AnimatorStateInfo    aniState = m_srcUnit.m_animator.GetCurrentAnimatorStateInfo(0);
            if (aniState.Equals(null))
                return true;
            // TODO 技能真实发动了才产生效果

            // 产生IDamage球
            if(m_genDamageEveryNorTime > 0)
            {
                if(nextGenDamageNorTime < 0)
                    nextGenDamageNorTime = m_genDamageEveryNorTime;
                if (aniState.normalizedTime > nextGenDamageNorTime)
                {
                    genDamage();
                    nextGenDamageNorTime += m_genDamageEveryNorTime;
                }

                if (nextGenDamageNorTime > m_endDamageNorTime || nextGenDamageNorTime > 1.0f)
                    return true;
                else
                    return false;
            }


            return true;
        }

        // 技能的结束
        public void endSkill()
        {
            nextGenDamageNorTime = -1;
        }
        // 产生一次IDamage
        public virtual void genDamage()
        {
            IDamage damage = new IDamage();
            damage.calDamageHPFun = calDamageHP;
        }

        public virtual int calDamageHP(Unit srcUnit ,Unit targetUnit)
        {
            if(srcUnit == null || targetUnit == null)
                return 0;
            return srcUnit.finalAttr.atk - targetUnit.finalAttr.def;
        }
    }
}
