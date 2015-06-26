using System;
using System.Collections.Generic;
using UnityEngine;
using WTH;

public enum SkillIndex
{
    Normal  = 1,
    SP1     = 2,
}

public partial class Unit : MonoBehaviour
{
    protected Dictionary<SkillIndex, ISkill> m_skillDict = new Dictionary<SkillIndex, ISkill>();
    protected ISkill curSkill = null;
    public void initSkill()
    {
        //SingleMeleeSkill skill = new SingleMeleeSkill();
        //skill.m_srcUnit = this;
        //setSkill(SkillIndex.Normal, skill);
    }

    public void setSkill(SkillIndex index, ISkill skill)
    {
        m_skillDict.Add(index, skill);
    }

    public void useSkill(SkillIndex index)
    {
        foreach (SkillIndex key in m_skillDict.Keys)
        {
            if (index == key)
            {
                if (curSkill != null)
                    curSkill.endSkill();
                curSkill = m_skillDict[key];
                curSkill.useSkill();

            }
        }
    }

    public void updateSkill()
    {
        if (curSkill == null)
            return;
        if (curSkill.updateSkill())
        {
            curSkill.endSkill();
            curSkill = null;
        }
    }
}

