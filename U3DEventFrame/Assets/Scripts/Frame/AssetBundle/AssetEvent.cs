using UnityEngine;
using System.Collections;
using U3DEventFrame;


public class HunkAssetRes : MsgBase
{

    public string scenceName;

    public string bundleName;

    public string resName;

    public ushort backMsgId;

    public bool isSingle;

    public HunkAssetRes()
    {
        isSingle = true;

        this.msgId = 0;

        this.scenceName = null;

        this.bundleName = null;
        this.resName = null;

        this.backMsgId = 0;
    }
    public HunkAssetRes(bool single, ushort msgId,string scence ,string bundle,string tmpRes,ushort backId)
    {

        isSingle = single;

        this.msgId = msgId;

        this.scenceName = scence;

        this.bundleName = bundle;
        this.resName = tmpRes;

        this.backMsgId = backId;
    }

    public void ChangeHunkAssetMsg(bool single, ushort msgId, string scence, string bundle, string tmpRes, ushort backId)
    {
        isSingle = single;

        this.msgId = msgId;

        this.scenceName = scence;

        this.bundleName = bundle;
        this.resName = tmpRes;

        this.backMsgId = backId;
    }

    /// <summary>
    ///  释放单个obj
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="scence"></param>
    /// <param name="bundle"></param>
    /// <param name="tmpRes"></param>
    public void ChangeReleaseResMsg(ushort msgId, string scence, string bundle, string tmpRes)
    {
        this.msgId = msgId;

        this.scenceName = scence;

        this.bundleName = bundle;
        this.resName = tmpRes;
    }

    /// <summary>
    ///  释放一个bundle 里面 所有的obj
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="scence"></param>
    /// <param name="bundle"></param>
    public void ChangeReleaseBundleResMsg(ushort msgId, string scence, string bundle)
    {
        this.msgId = msgId;

        this.scenceName = scence;

        this.bundleName = bundle;

    }

    /// <summary>
    /// 释放一个场景的object
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="scence"></param>
    public void ChangeReleaseScenceResMsg(ushort msgId, string scence)
    {
        this.msgId = msgId;

        this.scenceName = scence;


    }

   /// <summary>
   /// 释放单个 bundle
   /// </summary>
   /// <param name="msgId"></param>
   /// <param name="scence"></param>
   /// <param name="bundle"></param>

    public void ChangeReleaseBundleMsg(ushort msgId, string scence, string bundle)
    {
        this.msgId = msgId;

        this.scenceName = scence;

        this.bundleName = bundle;

    }

    /// <summary>
    /// 释放一个场景的bundle
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="scence"></param>
    public void ChangeReleaseScenceMsg(ushort msgId, string scence)
    {
        this.msgId = msgId;

        this.scenceName = scence;


    }






}


public class HunkAssetResesBack : MsgBase
{
    public Object[] value;

    public HunkAssetResesBack()
    {
        this.msgId = 0;
        this.value = null;
    }

    public void ChangeMsg(ushort msgid, params Object[] tmpValue)
    {
        this.msgId = msgid;
        this.value = tmpValue;
    }


    public void ChangeMsg(ushort msgid)
    {
        this.msgId = msgid;
       
    }
    public void ChangeMsg(Object[] tmpValue)
    {
        this.value = tmpValue;

    }

    public HunkAssetResesBack(ushort msgid,  Object[] tmpValue)
    {
        this.msgId = msgid;
        this.value = tmpValue;
    }
}

public class HunkAssetResBack : MsgBase
{
    public Object value;

    public HunkAssetResBack()
    {
        this.msgId = 0;
        this.value = null;
    }
    public HunkAssetResBack(ushort msgid, Object tmpValue)
    {
        this.msgId = msgid;
        this.value = tmpValue;
    }


    public void ChangeMsg(ushort msgid, Object tmpValue)
    {
        this.msgId = msgid;
        this.value = tmpValue;
    }


    public void ChangeMsg(ushort msgid)
    {
        this.msgId = msgid;

    }
    public void ChangeMsg(Object tmpValue)
    {
        this.value = tmpValue;

    }





}











