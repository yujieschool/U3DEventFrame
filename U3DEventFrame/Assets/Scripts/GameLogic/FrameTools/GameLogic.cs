using UnityEngine;
using System.Collections;

using U3DEventFrame;
using System;

public class GameLogic : AssetBase {


    public override void ProcessEvent(MsgBase msg)
    {
       
       

    }



    void LoadResult()
    {

       Debuger.Log(" Check resource  finish ............");


       MsgBase tmpMsg = ObjectPoolManager<MsgBase>.Instance.GetFreeObject();


       tmpMsg.ChangeEventId((ushort)CheckAssetEvents.CheckFinish);

       SendMsg(tmpMsg);


       ObjectPoolManager<MsgBase>.Instance.ReleaseObject(tmpMsg);

    }

    void Awake()
    {

        CheckAssetData tmpCheck = new CheckAssetData(LoadResult);

        //Debuger.Log("  Game logic UIEventZL.StartShowLoading");
        //MsgBase tmpMsg = new MsgBase((ushort)UIEventZL.StartShowLoading);
        //SendMsg(tmpMsg);


        //StartCoroutine(tmpCheck.OnExtractResource());

        if (tmpCheck.CheckIsCopyFile())
        {

            StartCoroutine(tmpCheck.OnExtractResource());
        }
        else
        {
            //  Debug.Log("donot  copy  assets");
            LoadResult();
        }
       


    }
    // Use this for initialization
    void Start ()
    {

       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
