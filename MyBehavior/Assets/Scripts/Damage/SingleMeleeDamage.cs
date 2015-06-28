using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    // 单体近战伤害
    public class SingleMeleeDamage: IDamage
    {
        Unit m_targetUnit;
        public SingleMeleeDamage(Unit targetUnit) : base()
        {
            m_targetUnit = targetUnit;
        }

        protected override DamageResult generateDamageResult(Unit targetUnit)
        {
            DamageResult result = new DamageResult();
            result.damage = calDamageHP(m_srcUnit, targetUnit);
            result.damgeUnit = targetUnit;

            targetUnit.FinalAttr.curHP -= result.damage;

            if (m_callbackDamaged != null)
                m_callbackDamaged(m_srcUnit, targetUnit);
            return result;
        }

        public override DamageResultSet updateDamage(float deltaTime)
        {
            DamageResultSet resultSet = base.updateDamage(deltaTime);
            resultSet.m_listResult.Add(generateDamageResult(m_targetUnit));

            resultSet.isFinish = true;
            return resultSet;
        }

    }
}
