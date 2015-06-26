using UnityEngine;

namespace WTH
{
    public class BuffAura : Buff
    {

        public BuffAura(Unit effectUnit, BuffType buffType, float duration,int value =-1 ,float timeGap = -1, Unit srcUnit = null)
            :base(effectUnit,buffType,duration,value,timeGap,srcUnit)
        {
            effectUnit.addBuff(this);
        }

        public override void effectAura(UnitAttr unitAttr)
        {
            base.effectAura(unitAttr);
            switch (m_buffType)
            { 
                case BuffType.PhyDamUp:
                    unitAttr.phyDamUpPercent += this.m_value;
                    break;
                case BuffType.PhyDefDown:
                    unitAttr.phyDefUpPercent -= this.m_value;
                    break;
            }
        }
    }
}
