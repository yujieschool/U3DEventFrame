using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoateSelf : MonoBehaviour {

	// Use this for initialization
	void Start () {

       // gameObject.AddComponent<DemoMsg>();


        transform.GetComponent<CanvasRenderer>().EnableRectClipping(Rect.zero);

        transform.GetComponent<CanvasRenderer>().EnableRectClipping(new Rect(0,0,100,100));
		
	}


    public void ShowMuti(float  a , float  b)
    {

        Debug.Log(a*b);
    }



    void CallMethod(string moudle, string func, GameObject args)
    {
        //if (!LuaController.Instance.isInit) return null;

        string funcName = moudle + "." + func;

        LuaClient.Instance.CallFuncWithGameObject(funcName, args);

    }


    public void Rote( float   tmpSpeed)
    {

        transform.Rotate(Vector3.up,tmpSpeed);
 

    }

	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.A))
        {


            CallMethod("LTestC", "ChangeName",gameObject);

        }
		
	}
}
