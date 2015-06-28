using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    public class EffectMng
    {

        public static void genEffect(Transform parent, string effectName, Vector3 offset)
        {
            GameObject effectGO= GameObject.Instantiate(Resources.Load(effectName)) as GameObject;
            Transform effectTrans = effectGO.transform;
            if(parent != null)
                effectTrans.parent = parent;
            effectTrans.localScale = Vector3.one;
            effectTrans.localRotation = Quaternion.identity;
            effectTrans.localPosition = offset;
        }
    }
}
