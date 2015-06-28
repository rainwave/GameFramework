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
        public float m_genDamageTimeN = 0.4f;           // 开始产生伤害的正交化时间(有些技能需要吟唱)
        public float m_genDamageEveryTimeN = 0.8f;      // 隔多少正交化时间产生一次IDamage
        public float m_endDamageTimeN = 0.9f;


        public delegate void CallbackDamaged(Unit srcUnit, Unit targetUnit);
        public CallbackDamaged callbackDamaged;

        // ===================临时数据相关==================================
        private float nextGenDamageNorTime = -1;
        private bool hasGenEffect = false;

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
            if (aniState.nameHash != m_srcUnit.getMotionHash(m_motionName))
                return false;
            if (aniState.normalizedTime < m_genDamageTimeN)
                return false;

            if (!hasGenEffect)
            {
                hasGenEffect = true;
                if (!string.IsNullOrEmpty(m_effect))
                {
                    EffectMng.genEffect(m_srcUnit.m_cacheTransform, m_effect, Vector3.zero);
                }
            }

            // 产生IDamage球
            if(m_genDamageEveryTimeN > 0)
            {
                if(nextGenDamageNorTime < 0)
                    nextGenDamageNorTime = m_genDamageEveryTimeN;
                if (aniState.normalizedTime > nextGenDamageNorTime)
                {
                    genDamage();
                    nextGenDamageNorTime += m_genDamageEveryTimeN;
                }

                if (nextGenDamageNorTime > m_endDamageTimeN || nextGenDamageNorTime > 1.0f)
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
            hasGenEffect = false;
        }
        // 产生一次IDamage
        public virtual void genDamage()
        {
            IDamage damage = new IDamage();
            damage.m_calDamageHPFun = calDamageHP;
            damage.m_callbackDamaged = onCallbackDamaged;
        }

        public virtual int calDamageHP(Unit srcUnit ,Unit targetUnit)
        {
            if(srcUnit == null || targetUnit == null)
                return 0;
            return srcUnit.FinalAttr.phyDam - targetUnit.FinalAttr.phyDef;
        }

        public virtual void onCallbackDamaged(Unit srcUnit, Unit targetUnit)
        {
            if (callbackDamaged != null)
                callbackDamaged(srcUnit, targetUnit);
        }

    }
}
