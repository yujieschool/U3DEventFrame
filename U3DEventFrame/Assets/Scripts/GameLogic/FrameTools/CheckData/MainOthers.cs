using UnityEngine;
using System.Collections;
using U3DEventFrame;

using System.IO;
using System;

public class MainOthers : AssetBase
{

    public bool openDebug = true;

    public bool IsUsingLua = false;
    public override void ProcessEvent(MsgBase msg)
    {

        switch (msg.msgId)
        {

            case (ushort)CheckAssetEvents.CheckFinish:

                {

                   // Debuger.Log("msg center  have  add   iloadmanager ");
                    gameObject.AddComponent<ILoadManager>();

                    if (IsUsingLua)
                    {
                        Debuger.Log("msg center  have  add   LuaClient ");

                        gameObject.AddComponent<LuaClient>();
                    }



                   // gameObject.AddComponent<AudioPlayer>();
                }

                break;
        }
    }


    public void SetPathTools()
    {

#if UNITY_ANDROID

        IPathTools.pathTargetPlatform = 2;
 
#endif

#if UNITY_IPHONE
           IPathTools.pathTargetPlatform = 3;

#endif

#if UNITY_STANDALONE_WIN

          IPathTools.pathTargetPlatform = 1;

#endif



    }

   
    void Awake()
    {



        DontDestroyOnLoad(gameObject);


        msgIds = new ushort[]
            {
               (ushort)CheckAssetEvents.CheckFinish

            };

        RegistSelf(this,msgIds);


        //Debuger.EnableConsolLog = false;
        SetPathTools();

        gameObject.AddComponent<LuaAndCMsgCenter>();

        gameObject.AddComponent<NativeLoadRes>();
        gameObject.AddComponent<NativeLoadMutiRes>();

        //gameObject.AddComponent<TCPSocket>();


        gameObject.AddComponent<NPCController>();


        gameObject.AddComponent<GameLogic>();

    }

	// Use this for initialization
	void Start () {
       // Debug.logger.logEnabled = openDebug;
       // Debuger.EnableConsolLog = openDebug;
        Application.targetFrameRate = 40;
	}
	
	// Update is called once per frame
	void Update () {
	


	}
}
