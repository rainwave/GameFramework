using System;
using System.Collections.Generic;
using UnityEngine;
using BT;
using WTH;

class TestBT : MonoBehaviour
{
    public void Start()
    { 
        UnitAttr attr1 = new UnitAttr();
        attr1.phyDam = 200;
        attr1.curHP = 1000;
        attr1.maxHP = 1000;
        attr1.phyDef = 100;

        UnitAttr attr2 = new UnitAttr();
        attr2.phyDam = 200;
        attr2.curHP = 1000;
        attr2.maxHP = 1000;
        attr2.phyDef = 100;

        

        UnitGenerator.genUnit(attr1);
        Unit player = UnitGenerator.genUnit<UnitPlayer>(attr2);
        player.m_cacheTransform.localPosition = new Vector3(5, 0, 0);

        SingleMeleeSkill skill = new SingleMeleeSkill();
        skill.m_srcUnit = player;
        skill.callbackDamaged = (srcUnit, targetUnit) =>
            {
                new BuffAura(targetUnit, BuffType.PhyDefDown, 10f,50);
            };
        player.setSkill(SkillIndex.Normal, skill);
        
    }

    public void onEffectReach(Buff buff, Unit targetUnit)
    {
        targetUnit.FinalAttr.curHP += 1000;
    }
}

