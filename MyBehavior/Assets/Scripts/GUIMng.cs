using UnityEngine;
using System.Collections;
using BT;

public class GUIMng : MonoSingleton<GUIMng> {

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,100,100),"Click Me"))
		{
			onClickBtn();

		}

	}

	// Update is called once per frame
	void Update () {
	
	}

    void onClickBtn()
    {
        Debug.Log("hello");
        BTAction sayHello = new ActionSayHello();
        BTMng.Instance.addChild(sayHello);
    }
}
