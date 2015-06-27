using UnityEngine;
using System.Collections;
using BT;

public class GUIMng : MonoSingleton<GUIMng> {

	// Use this for initialization
    protected override 	void Start () {
	
	}

    protected void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,100,100),"Click Me"))
		{
			onClickBtn();

		}

	}

	// Update is called once per frame
    protected override void Update()
    {
	
	}

    void onClickBtn()
    {
        Debug.Log("hello");
        BTAction sayHello = new ActionSayHello();
        BTMng.Instance.addChild(sayHello);
    }
}
