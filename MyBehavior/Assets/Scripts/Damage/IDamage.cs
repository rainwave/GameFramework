using System;
using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    public enum DamageType
    {
        Normal = 0,
        Crit = 1,
        Kill = 2,
    }

    public class DamageResult
    {
        public Unit damgeUnit;
        public int damage;
        public DamageType damageType;
    }

    public class DamageResultSet
    {
        public Unit m_srcUnit;
        public List<DamageResult> m_listResult = new List<DamageResult>();
        public bool isFinish = true;
    }

    public class IDamage
    {
        public float m_timeElaspe;

        public Vector3 m_startPos;
        public Vector3 m_dir;
        public float m_flySpeed = 9999999f;
        public float m_maxDis = 0;
        public Vector3 CurPos
        {
            get
            {
                float dis = m_timeElaspe * m_flySpeed;
                dis = dis > m_maxDis ? m_maxDis : dis;
                return m_startPos + m_dir.normalized * dis;
            }
        }
        // 伤害半径 9999999f 为全图(主要为buff技能)
        public float m_radius = 9999999f;
        public float CurRadius
        {
            get
            {
                return m_radius;
            }
        }

        public Unit m_srcUnit;

        public delegate int CalDamageHPFun(Unit srcUnit, Unit targetUnit);
        public CalDamageHPFun m_calDamageHPFun;

        public delegate void CallbackDamaged(Unit srcUnit, Unit targetUnit);
        public CallbackDamaged m_callbackDamaged;

        public IDamage()
        {
            DamageMng.Instance.addDamage(this);
        }


        protected virtual DamageResult generateDamageResult(Unit targetUnit)
        {
           
            return null;
        }

        public virtual DamageResultSet updateDamage(float deltaTime)
        {
            m_timeElaspe += deltaTime;
            DamageResultSet resultSet = new DamageResultSet();
            return resultSet;
        }

        // 伤害统一
        protected int calDamageHP(Unit srcUnit, Unit targetUnit)
        {
            int hpChange = 0;
            //if (calDamageHPFun != null)
            //    hpChange += calDamageHPFun(srcUnit, targetUnit);
            hpChange = srcUnit.FinalAttr.phyDam - targetUnit.FinalAttr.phyDef;
            hpChange = Mathf.Max(1, hpChange);
            return hpChange;
        }
    }
}
