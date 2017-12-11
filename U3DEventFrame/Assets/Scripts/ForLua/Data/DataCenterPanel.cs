using UnityEngine;
using System.Collections;

public class DataCenterPanel : MonoBehaviour {

    void Awake()
    {
        CallMethod( name,"Awake",gameObject);
    }

	void Start () {

        CallMethod(name,"Start", gameObject);
    }

	void OnDestroy()
	{
//		CallMethod(name,"OnDestroy", gameObject);
		
		//  LuaClient.Instance.LuaGC();
		Debug.Log("~" + name + " was destroy!");
	}
	
	
	protected int CallMethod(string moudle, string func, GameObject args)
    {

        string funcName = moudle + "." + func;

		LuaClient.Instance.CallFuncWithGameObject(funcName, args);
		return  0 ;
    }





}
