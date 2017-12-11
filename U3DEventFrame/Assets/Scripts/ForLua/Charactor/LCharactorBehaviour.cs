using UnityEngine;
using System.Collections;

public class LCharactorBehaviour : MonoBehaviour {


     void Awake()
    {


        CallMethod("RegistGameObject", gameObject);
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /// <summary>
    /// 执行Lua方法
    /// </summary>
    protected object[] CallMethod( string func, params object[] args)
    {
        //if (!LuaController.Instance.isInit) return null;

        return null;
    }

    //-----------------------------------------------------------------
    protected void OnDestroy()
    {


     //   Util.ClearMemory();
        Debug.Log("~" + name + " was destroy!");
    }



}
