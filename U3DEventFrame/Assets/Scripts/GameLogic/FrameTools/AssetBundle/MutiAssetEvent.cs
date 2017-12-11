using UnityEngine;
using System.Collections;
using U3DEventFrame;

using System.Collections.Generic;


public class HunkAssetMutiRes : MsgBase
{

    public string scenceName;

    public string bundleName;

    public string[] resName;

    public ushort backMsgId;

    public bool isSingle;

    public HunkAssetMutiRes()
    {
        isSingle = true;

        this.msgId = 0;

        this.scenceName = null;

        this.bundleName = null;
        this.resName = null;

        this.backMsgId = 0;
    }

    public HunkAssetMutiRes(ushort msgId, ushort backId, bool single,  string scence, string bundle,params string[] tmpRes )
    {
        isSingle = single;

        this.msgId = msgId;

        this.scenceName = scence;

        this.bundleName = bundle;
        this.resName = tmpRes;

        this.backMsgId = backId;
    }
    public HunkAssetMutiRes(bool single, ushort msgId,string scence ,string bundle,ushort backId, params string[] tmpRes)
    {

        isSingle = single;

        this.msgId = msgId;

        this.scenceName = scence;

        this.bundleName = bundle;
        this.resName = tmpRes;

        this.backMsgId = backId;
    }

    public void ChangeHunkAssetMsg(bool single, ushort msgId, string scence, string bundle,  ushort backId,params string[] tmpRes)
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
    public void ChangeReleaseResMsg(ushort msgId, string scence, string bundle, params string[] tmpRes)
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

public class HunkMutiAssetObjects
{
    public Object[] value;

    public HunkMutiAssetObjects(params Object[] tmpObjs)
    {
        this.value = tmpObjs;

    }

}

public class HunkMutiAssetResesBack : MsgBase
{
  



    public Dictionary<string, HunkMutiAssetObjects> obj = null;

    public Object[] value
    {
        get
        {
            List<string> tmpKeys = new List<string>();
            tmpKeys.AddRange(obj.Keys);

            return obj[tmpKeys[0]].value;
        }

    }

    public Object[] this[string resName]
    {
        get
        {
            if (obj.ContainsKey(resName))
            {
                return obj[resName].value;
            }
            else
            {
                return null;
            }
        }

    }


    public HunkMutiAssetResesBack()
    {
        this.msgId = 0;
      
       obj = new Dictionary<string, HunkMutiAssetObjects>();

       
    }


    public void AddReses(string res,params Object[] tmpValue)
    {
        if (!obj.ContainsKey(res))
        {
            HunkMutiAssetObjects tmpObject = new HunkMutiAssetObjects(tmpValue);
            obj.Add(res, tmpObject);
        }

    }




    public void ChangeMsg(ushort msgid)
    {
        this.msgId = msgid;
       
    }

}

public class HunkMutiAssetResBack : MsgBase
{
    public Object value;

    public HunkMutiAssetResBack()
    {
        this.msgId = 0;
        this.value = null;
    }
    public HunkMutiAssetResBack(ushort msgid, Object tmpValue)
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









