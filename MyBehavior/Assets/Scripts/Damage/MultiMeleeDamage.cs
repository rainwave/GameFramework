using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    // 多体近战伤害
    public class MultiMeleeDamage: IDamage
    {
        public MultiMeleeDamage(Unit srcUnit)
            : base()
        {
            // 攻击正前方
            m_srcUnit = srcUnit;
            m_startPos = m_srcUnit.m_cacheTransform.localPosition + m_srcUnit.m_cacheTransform.forward *2* m_srcUnit.m_radius;
            m_radius =  4 * m_srcUnit.m_radius;
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
            List<Unit> list = UnitMng.Instance.collectUnitsByCircle(this.CurPos, this.CurRadius, m_srcUnit.m_campSet, (int)m_srcUnit.m_campSet);
            for (int i = 0, len = list.Count; i < len; i++)
            {
                resultSet.m_listResult.Add(generateDamageResult(list[i]));
            }

            resultSet.isFinish = true;
            return resultSet;
        }

    }
}
