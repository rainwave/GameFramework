using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
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
            damage.calDamageHPFun = calDamageHP;
            damage.callbackDamaged = onCallbackDamaged;
        }

        public override int calDamageHP(Unit srcUnit, Unit targetUnit)
        {
            if(srcUnit == null || targetUnit == null)
                return 0;
            return srcUnit.FinalAttr.phyDam - targetUnit.FinalAttr.phyDef;
        }
    }
}
