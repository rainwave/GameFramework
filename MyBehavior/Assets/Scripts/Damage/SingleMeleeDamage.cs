using System;
using System.Collections.Generic;


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

        public override DamageResult generateDamageResult()
        {
            DamageResult result = new DamageResult();
            result.damage = calDamageHPFun(m_srcUnit,m_targetUnit);
            result.damgeUnit = m_targetUnit;

            m_targetUnit.FinalAttr.curHP -= result.damage;

            if (callbackDamaged != null)
                callbackDamaged(m_srcUnit, m_targetUnit);
            return result;
        }

        public override DamageResultSet updateDamage(float deltaTime)
        {
            DamageResultSet resultSet = base.updateDamage(deltaTime);
            resultSet.m_listResult.Add(generateDamageResult());

            resultSet.isFinish = true;
            return resultSet;
        }
    }
}
