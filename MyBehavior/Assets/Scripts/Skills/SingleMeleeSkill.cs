using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    public class SingleMeleeSkill: ISkill
    {

        public SingleMeleeSkill():base()
        {
            m_genDamageEveryNorTime = 0.8f;
            m_motionName = "hit";
        }

        // 产生一次IDamage
        public override void genDamage()
        {
            Unit targetUnit = UnitMng.Instance.collectNearUnit(m_srcUnit);
            IDamage damage = new SingleMeleeDamage(targetUnit);
            damage.m_srcUnit = m_srcUnit ;
            damage.calDamageHPFun = calDamageHP;
        }

        public override int calDamageHP(Unit srcUnit, Unit targetUnit)
        {
            if(srcUnit == null || targetUnit == null)
                return 0;
            return srcUnit.finalAttr.atk - targetUnit.finalAttr.def;
        }
    }
}
