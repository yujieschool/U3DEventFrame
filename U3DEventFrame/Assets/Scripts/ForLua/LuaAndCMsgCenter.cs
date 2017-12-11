using UnityEngine;
using System.Collections;
using LuaInterface;

using U3DEventFrame;



public class LuaAndCMsgCenter : MonoBase
{



    public static LuaAndCMsgCenter instance = null;


    LuaFunction callBack = null;

    public override void ProcessEvent(MsgBase msg)
    {

        if (callBack != null)
        {

            //网络数据
            if (msg.GetState() != 127)
            {
                NetMsgBase tmpBase = (NetMsgBase)msg;


                byte[] proto = tmpBase.GetProtoBuffer();

                if (proto != null && proto.Length>0)
                {
                    LuaByteBuffer buffer = new LuaByteBuffer(proto);

                    callBack.Call(true, tmpBase.msgId, tmpBase.GetState(), buffer);
                }
                else
                {
                    callBack.Call(true, tmpBase.msgId, tmpBase.GetState());
                }


            }
            else
            {
                // 其它消息

                //Debug.Log("122222222222221tmpBase.GetState()==" + msg.GetState());
                callBack.Call( false, msg);

            }
            
		
        }


    }


    public void SettingLuaCallBack(LuaFunction luafuc)
    {
       // Debug.Log("have call back lua");
        callBack = luafuc;
    }

    void Awake()
    {
        instance = this;

        LuaEventProcess.instance.SettingChild(this);

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
