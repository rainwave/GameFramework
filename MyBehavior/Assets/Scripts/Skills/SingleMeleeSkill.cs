using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    // 单体近战技能
    public class SingleMeleeSkill: ISkill
    {

        public SingleMeleeSkill():base()
        {
            m_genDamageEveryTimeN = 0.8f;
            m_motionName = "hit";
        }

        // 产生一次IDamage
        public override void genDamage()
        {
            Unit targetUnit = UnitMng.Instance.collectNearUnit(m_srcUnit);
            IDamage damage = new SingleMeleeDamage(targetUnit);
            damage.m_srcUnit = m_srcUnit ;
            damage.m_callbackDamaged = onCallbackDamaged;
        }

    }
}
