using UnityEngine;
using System.Collections;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour{
	public static T Instance;

	protected virtual void Awake()
	{
		Instance = this.GetComponent<T>();

	}

	// Use this for initialization
    protected virtual void Start()
    {
	
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
	
	}
}
