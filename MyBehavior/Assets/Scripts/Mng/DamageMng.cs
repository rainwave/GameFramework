using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    public class DamageMng : MonoSingleton<DamageMng>
    {
        public List<IDamage> m_damages = new List<IDamage>();

        public delegate void OnDamageUnit(IDamage damage, DamageResult result);
        protected OnDamageUnit m_onDamageUnit;

        protected override void Awake()
        {
            base.Awake();
            regitDamageUnit(manageDamageUnit);
        }

        public void regitDamageUnit(OnDamageUnit damageUnit)
        {
            m_onDamageUnit += damageUnit;
        }

        public void removeDamageUnit(OnDamageUnit damageUnit)
        {
            m_onDamageUnit -= damageUnit;
        }

        public void addDamage(IDamage damage)
        {
            if (damage == null)
                return;
            if(!m_damages.Contains(damage))
            {
                m_damages.Add(damage);
            }
        }

        public void removeDamage(IDamage damage)
        {
            if (damage == null)
                return;
            if (m_damages.Contains(damage))
            {
                m_damages.Remove(damage);
            }
        }

        protected override void Update()
        {
            for (int i = 0, len = m_damages.Count; i < len; i++)
            {
                DamageResultSet resultSet = m_damages[i].updateDamage(Time.deltaTime);
                for (int j = 0, len2 = resultSet.m_listResult.Count; j < len2; j++)
                {
                    m_onDamageUnit(m_damages[i], resultSet.m_listResult[j]);
                }
                if (resultSet   != null && resultSet.isFinish)
                {
                    removeDamage(m_damages[i]);
                    len--;
                    i--;
                }
            }
        }

        protected void manageDamageUnit(IDamage damage, DamageResult result)
        { 
            UIFloatMng.Instance.genFloatText(string.Format("-[ff0000]{0}",result.damage),result.damgeUnit.m_cacheTransform.localPosition);
        }
    }
}

