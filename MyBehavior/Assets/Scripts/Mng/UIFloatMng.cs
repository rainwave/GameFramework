using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    public class UIFloatMng : MonoSingleton<UIFloatMng>
    {
        protected Transform m_cachedTransform;
        protected override void Start()
        {
            base.Start();
            m_cachedTransform = this.transform;
        }
        public void genFloatText(string content, Vector3 worldPos)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);
            pos.z = 0f;   //z一定要为0.
            Vector3 pos2 = UICamera.currentCamera.ScreenToWorldPoint(pos);
            GameObject go = GameObject.Instantiate(Resources.Load("UIFloatText")) as GameObject;

            go.transform.parent = m_cachedTransform;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localPosition = pos2;
            go.GetComponent<UILabel>().text = content;
            GameObject.Destroy(go, 1f);
            //temp.transform.position = pos2; //temp为NGUI控件.
        }
    }
}
