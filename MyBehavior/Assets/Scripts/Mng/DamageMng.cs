using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    public class DamageMng : MonoSingleton<DamageMng>
    {
        public List<IDamage> m_damages = new List<IDamage>();

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
                if (resultSet   != null && resultSet.isFinish)
                {
                    removeDamage(m_damages[i]);
                    len--;
                    i--;
                }
            }
        }
    }
}

