using UnityEngine;
using System.Collections;

using LuaInterface;


public class LuaUIPanel : MonoBehaviour {




    void Awake()
    {
		Transform[] childrens = GetComponentsInChildren<Transform>();
		
		for (int i = 1; i < childrens.Length; i++) {
			if (!childrens[i].name.EndsWith("_n")) {
				LuaUIBehaviour behav = childrens[i].gameObject.AddComponent<LuaUIBehaviour>();
				behav.panelName = this.name;
			}
		}

       

        CallMethod( name,"Awake",gameObject);
    }
	// Use this for initialization
	void Start () {

     //   if(StartFunc != null)
   //     StartFunc.Invoke(gameObject);
       CallMethod(name,"Start", gameObject);
    }



	// Update is called once per frame
	void Update () {
	
	}



    /// <summary>
    /// 执行Lua方法
    /// </summary>
    /// <summary>
    /// 执行Lua方法
    /// </summary>
    protected void  CallMethod(string moudle, string func, GameObject args)
    {
        //if (!LuaController.Instance.isInit) return null;

        string funcName = moudle + "." + func;

        LuaClient.Instance.CallFuncWithGameObject(funcName, args);
        
    }

    //-----------------------------------------------------------------
    protected void OnDestroy()
    {
   

      //  LuaClient.Instance.LuaGC();
      //  Debug.Log("~" + name + " was destroy!");
    }



}
