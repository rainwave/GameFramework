using System;
using System.Collections.Generic;
using UnityEngine;

public class AniInfo
{
    public string name;
    public int action;
    public int nameHash;
    public float exitNorTime = 0.9f;
}

public partial class Unit : MonoBehaviour
{
   // 通过读取脚本、配置来配置每个Unit的动作信息
    // 此处简单起见，定死
    protected List<AniInfo> m_aniInfoes = new List<AniInfo>();

    public Animator m_animator;

    protected AniInfo m_curAniInfo;

    protected void initAni()
    {
        AniInfo run = new AniInfo();
        run.name = "run";
        run.action = 1;
        run.nameHash = Animator.StringToHash("Base Layer.run");

        AniInfo hit = new AniInfo();
        run.name = "hit";
        run.action = 9;
        run.nameHash = Animator.StringToHash("Base Layer.skill1");

        m_aniInfoes.Add(run);
        m_aniInfoes.Add(hit);

        m_animator = this.GetComponent<Animator>();
    }

    public void changeMotion(string animName)
    {
        if (m_animator == null || string.IsNullOrEmpty(animName))
            return;

        foreach (AniInfo aniInfo in m_aniInfoes)
        {
            if (aniInfo.name == animName)
            {
                m_curAniInfo = aniInfo;
                break;
            }
        }

        m_animator.SetInteger("action", m_curAniInfo.action);
    }

    protected void updateAni()
    {
        if (m_animator == null || m_curAniInfo == null)
            return;

        AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
        // 超时回到默认动作
        if (info.nameHash == m_curAniInfo.nameHash)
        {
            if (info.normalizedTime > m_curAniInfo.exitNorTime)
            {
                m_animator.SetInteger("action", 0);
            }
        }
        
    }
}

