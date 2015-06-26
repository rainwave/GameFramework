using System.Collections.Generic;
using UnityEngine;

namespace WTH
{
    public class UICommonData
    {
        public static GameObject curMainUI;
    }

    public class UIData<T> : MonoBehaviour
    {
        protected static T _instance;
        public static T Instance { get { return _instance; } }
        private static string _uiName;
        public static string UIName 
        {
            get
            {
                if(string.IsNullOrEmpty(_uiName))
                    _uiName = typeof(T).ToString().Split('.')[1];       // 取出namespace
                return _uiName;
            }
        }
        public enum UIType
        {
            None = 0,
            Main = 1,
            Left = 2,
            Right= 3
        }

        public UIType uiType;

        // ==========================================
        public Transform cachedTransform; 
        public List<object> children = new List<object>();
    }

    public class UIAction<T>: UIData<T>
    {

        protected virtual void Awake()
        {
            _instance = (T)(object)this;
        }

        protected virtual void Start()
        { 
        
        }

        protected virtual void Update()
        { 
        
        }

        protected virtual void OnDestory()
        {
            _instance = default(T);
        }

        public static bool isExit()
        {
            return (_instance == null || _instance.Equals(null)) ? false : true;
        }

        public static void simpleShow(params object[] paramList) {
            if (_instance == null)
            {
                UnityEngine.Object res = Resources.Load(UIName);
                if (res == null || res.Equals(null))
                {
                    Debug.LogError(string.Format("UI prefab {0} not exit", UIName));
                    return;
                }
                GameObject go = GameObject.Instantiate(res) as GameObject;
                if (go == null)
                {
                    Debug.LogError(string.Format("Init UI {0} fail", UIName));
                    return;
                }

                if (UICommonData.curMainUI != null && !UICommonData.curMainUI.Equals(null))
                {
                    UICommonData.curMainUI.gameObject.SetActive(false);
                }
                UICommonData.curMainUI = go;

                go.name = UIName;
                Transform transform = go.transform;
                transform.parent = Main.Instance.uiRoot;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.localScale = Vector3.one;
            }
        }

        public void hide()
        {
            if (gameObject != null)
                gameObject.SetActive(false);
        }
        public void close()
        {
            _instance = default(T);
            GameObject.Destroy(this.gameObject);
        }
        public void show()
        {
            if(gameObject != null)
                gameObject.SetActive(true);

            if (UICommonData.curMainUI != null && !UICommonData.curMainUI.Equals(null))
            {
                UICommonData.curMainUI.gameObject.SetActive(false);
            }
            UICommonData.curMainUI = this.gameObject;
        }

        public void addChild(object child)
        {
            if (!children.Contains(child))
                children.Add(child);
        }
        public void removeChild(object child)
        {
            if (children.Contains(child))
                children.Remove(child);
        }
    }

    public class UIBase<T> : UIAction<T>
    {
        
    }
}
