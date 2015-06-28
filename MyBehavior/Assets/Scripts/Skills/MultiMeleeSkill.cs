using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    // 单体近战技能
    public class MultiMeleeSkill: ISkill
    {

        public MultiMeleeSkill()
            : base()
        {
            m_genDamageEveryTimeN = 0.8f;
            m_motionName = "hit";
        }

        // 产生一次IDamage
        public override void genDamage()
        {
            IDamage damage = new MultiMeleeDamage(m_srcUnit);
            damage.m_callbackDamaged = onCallbackDamaged;
        }

    }
}
