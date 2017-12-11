

using UnityEngine;
using System.Collections;

using U3DEventFrame;


using System.Collections.Generic;

/// <summary>
/// 返回给指定的类
/// </summary>
//public class ResCallBack
//{

//    public string scenceName;

//    public string bundleName;

//    public string resName;
//    public ushort msgId;

//    public bool isSingle;

//    public ResCallBack(ushort msgid ,string tmpSence ,string tmpBundle,string res )
//    {

//        msgId = msgid;
//        this.scenceName = tmpSence;

//        this.bundleName = tmpBundle;
//        this.resName = res;

//        isSingle = true;
//    }


//    public ResCallBack(ushort msgid, string tmpSence, string tmpBundle, string res, bool  single)
//    {

//        msgId = msgid;
//        this.scenceName = tmpSence;

//        this.bundleName = tmpBundle;

//        this.resName = res;
//        isSingle = single;
//    }

//}


public delegate void NativeResCallBack(NativeResCallBackNode  tmpNode);




public class NativeResCallBackNode
{
   
    public NativeResCallBackNode nextValue;

    public NativeResCallBack callBack;

    public string resName;

    public string bundleName;

    public string scenceName;

    public ushort  msgId;

    public bool isSingle;

    public NativeResCallBackNode(string scence, string bundle, string res, ushort msgid, bool single, NativeResCallBack tmpBack, NativeResCallBackNode tmpNode)
    {

        this.callBack = tmpBack;

        this.bundleName = bundle;
        this.resName = res;

        this.nextValue = tmpNode;

        this.msgId = msgid;

        this.isSingle = single;
        this.scenceName = scence;
    }

    public void Dispose()
    {
        nextValue = null;
        callBack = null;
        resName = null;
        bundleName = null;
        scenceName = null;

    }

}


public class NativeResCallBackManager
{




    //bundleName  相对路径
    Dictionary<string, NativeResCallBackNode> manager = null;


    public NativeResCallBackManager()
    {
        manager = new Dictionary<string, NativeResCallBackNode>();


    }

    public bool ContainsKey(string name)
    {
        return manager.ContainsKey(name);
    }

    public void AddBundle(string bundle, NativeResCallBackNode tmpNode)
    {

        if (manager.ContainsKey(bundle))
        {
            NativeResCallBackNode topNode = manager[bundle];

            while (topNode.nextValue != null)
            {
                topNode = topNode.nextValue;
            }

            topNode.nextValue = tmpNode;


        }
        else
        {
            manager.Add(bundle, tmpNode);
        }
    }

    public void Dispose()
    {

        manager.Clear();
    }

    public void Dispose(string bundle)
    {
        if (manager.ContainsKey(bundle))
        {
            NativeResCallBackNode topNode = manager[bundle];

            // 挨个释放
            while (topNode.nextValue != null)
            {
                NativeResCallBackNode curNode = topNode;

                topNode = topNode.nextValue;


                curNode.Dispose();
            }
            // 最后一个结点的释放
            topNode.Dispose();


            manager.Remove(bundle);
        }
    }

    public void CallBackRes(string bundle)
    {
        if (manager.ContainsKey(bundle))
        {
            NativeResCallBackNode topNode = manager[bundle];


            do
            {


                topNode.callBack(topNode);


                topNode = topNode.nextValue;
            }
            while (topNode != null);



        }
        else
        {
          //  Debuger.Log("extern contain bundle ==" + bundle);
        }
    }





}




public class NativeLoadRes : AssetBase
{

    #region   C#LoadRes


    NativeResCallBackManager callBack = null;


    NativeResCallBackManager CallBack
    {

        get
        {
            if (callBack == null)
            {
                callBack = new NativeResCallBackManager();
            }

            return callBack;
        }
    }


    //  void NativeResCallBack(NativeResCallBackNode tmpNode)

    public void SendToBackMsg(NativeResCallBackNode tmpNode)
    {
        if (tmpNode.isSingle)
        {

            Object tmpObj = ILoadManager.Instance.GetSingleResources(tmpNode.scenceName, tmpNode.bundleName, tmpNode.resName);

            // topNode.scenceName, topNode.bundleName, topNode.resName, tmpObj
            this.ResesBackMsg.ChangeMsg(tmpNode.msgId, tmpObj);
            SendMsg(ResesBackMsg);
        }
        else
        {



            Object[] tmpObj = ILoadManager.Instance.GetMutiResources(tmpNode.scenceName, tmpNode.bundleName, tmpNode.resName);

            // topNode.scenceName, topNode.bundleName, topNode.resName, tmpObj
            this.ResesBackMsg.ChangeMsg(tmpNode.msgId, tmpObj);
            SendMsg(ResesBackMsg);

        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="scenceName"> 场景名字</param>
    /// <param name="bundleName"> bundle</param>
    /// <param name="res">资源名字</param>
    /// <param name="single">加载的是一个资源　还是多个</param>
    /// <param name="luaFunc">ｌｕａ回调函数</param>
    public void GetResources(string scenceName, string bundleName, string res, bool single, ushort msgid)
    {



        //Debug.LogWarning("begin  load  bundle name ==="+bundleName);


        ///没有加载
        if (!ILoadManager.Instance.IsLoadingAssetBundle(scenceName, bundleName))
        {
            ILoadManager.Instance.LoadAsset(scenceName, bundleName, LoaderProgrocess);

            string bundlFullName = ILoadManager.Instance.GetBundleRelateName(scenceName, bundleName);

            if (bundlFullName != null)
            {


                NativeResCallBackNode tmpNode = new NativeResCallBackNode(scenceName, bundleName, res, msgid, single, SendToBackMsg, null);
                CallBack.AddBundle(bundlFullName, tmpNode);

            }
            else
            {
                Debuger.LogWarning("doest not  contain bundle" + bundleName);
            }

        } // 已经加载完成
        else if (ILoadManager.Instance.IsLoadingBundleFinish(scenceName, bundleName))
        {
            if (single)
            {

                Object tmpObj = ILoadManager.Instance.GetSingleResources(scenceName, bundleName, res);

                // topNode.scenceName, topNode.bundleName, topNode.resName, tmpObj
                this.ResesBackMsg.ChangeMsg(msgid, tmpObj);
                SendMsg(ResesBackMsg);
            }
            else
            {



                Object[] tmpObj = ILoadManager.Instance.GetMutiResources(scenceName, bundleName, res);

                // topNode.scenceName, topNode.bundleName, topNode.resName, tmpObj
                this.ResesBackMsg.ChangeMsg(msgid, tmpObj);
                SendMsg(ResesBackMsg);

            }

        }
        //  已经加载  并且  没有完成
        else
        {


            string bundlFullName = ILoadManager.Instance.GetBundleRelateName(scenceName, bundleName);

          
            NativeResCallBackNode tmpNode = new NativeResCallBackNode(scenceName, bundleName, res, msgid, single, SendToBackMsg, null);
            CallBack.AddBundle(bundlFullName, tmpNode);
            ILoadManager.Instance.AddLoadFinishBackDelegate(scenceName, bundleName, LoaderProgrocess);





        }
    }


    HunkAssetResesBack resesBackMsg = null;


    HunkAssetResesBack ResesBackMsg
    {
        get
        {
            if (resesBackMsg == null)
            {
                resesBackMsg = new HunkAssetResesBack();
            }

            return resesBackMsg;
        }


    }







    //HunkRes = ManagerID.AssetManager+1,

    //ReleaseSingleObj , //释放单个object

    //ReleaseBundleObjes,//释放一个bundle　包里　所有的object

    //ReleaseScenceObjes,//　释放　单个场景中所有的 object

    //ReleaseSingleBundle,//释放单个　assetbundle

    //ReleaseScenceBundle,//释放　一个场景中的所有的assetbundle

    //ReleaseAll,//　释放　一个场景中所有的 bundle 和 objects

    public override void ProcessEvent(MsgBase msg)
    {
        switch (msg.msgId)
        {

            case (ushort)AssetEvent.HunkRes:

                {

                    HunkAssetRes tmpMsg = (HunkAssetRes)msg;


                    GetResources(tmpMsg.scenceName, tmpMsg.bundleName, tmpMsg.resName,tmpMsg.isSingle, tmpMsg.backMsgId);
                   
                }

                break;
            //释放单个object
            case (ushort)AssetEvent.ReleaseSingleObj:


                {

                    HunkAssetRes tmpMsg = (HunkAssetRes)msg;


                    ILoadManager.Instance.UnloadResObj(tmpMsg.scenceName, tmpMsg.bundleName, tmpMsg.resName);
                }


                break;

            //释放一个bundle　包里　所有的object
            case (ushort)AssetEvent.ReleaseBundleObjes:


                {

                    HunkAssetRes tmpMsg = (HunkAssetRes)msg;


                    ILoadManager.Instance.UnloadBundleResObj(tmpMsg.scenceName, tmpMsg.bundleName);
                }


                break;

            //　释放　单个场景中所有的 object
            case (ushort)AssetEvent.ReleaseScenceObjes:


                {

                    HunkAssetRes tmpMsg = (HunkAssetRes)msg;


                    ILoadManager.Instance.UnloadAllResObj(tmpMsg.scenceName);
                }


                break;
            //ReleaseSingleBundle,//释放单个　assetbundle

            case (ushort)AssetEvent.ReleaseBundleAndObject:


                {

                    HunkAssetRes tmpMsg = (HunkAssetRes)msg;

                  
                    ILoadManager.Instance.UnloadAssetBundleAndRes(tmpMsg.scenceName, tmpMsg.bundleName);
                }


                break;


            case (ushort)AssetEvent.ReleaseSingleBundle:


                {

                    HunkAssetRes tmpMsg = (HunkAssetRes)msg;


                    ILoadManager.Instance.UnloadAssetBundle(tmpMsg.scenceName, tmpMsg.bundleName);
                }


                break;

            //ReleaseScenceBundle,//释放　一个场景中的所有的assetbundle
            case (ushort)AssetEvent.ReleaseScenceBundle:


                {

                    HunkAssetRes tmpMsg = (HunkAssetRes)msg;


                    ILoadManager.Instance.UnloadAllAssetBundle(tmpMsg.scenceName);
                }


                break;

            //ReleaseAll,//　释放　一个场景中所有的 bundle 和 objects
            case (ushort)AssetEvent.ReleaseAll:


                {

                    HunkAssetRes tmpMsg = (HunkAssetRes)msg;


                    ILoadManager.Instance.UnloadAllAssetBundleAndResObj(tmpMsg.scenceName);
                }


                break;


            default:
                break;

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bundle"> bundle 相对路径</param>
    /// <param name="progress"></param>
    void LoaderProgrocess(string bundle, float progress)
    {

        if (progress >= 1.0f)
        {


            CallBack.CallBackRes(bundle);

            CallBack.Dispose(bundle);


        }

    }







    #endregion

    void Awake()
    {
        msgIds = new ushort[] {
            (ushort)AssetEvent.HunkRes,
            (ushort)AssetEvent.ReleaseSingleObj,
             (ushort)AssetEvent.ReleaseBundleObjes,
             (ushort)AssetEvent.ReleaseScenceObjes,
              (ushort)AssetEvent.ReleaseBundleAndObject,
                         (ushort)AssetEvent.ReleaseSingleBundle,
             (ushort)AssetEvent.ReleaseScenceBundle,
             (ushort)AssetEvent.ReleaseAll,

        };

        RegistSelf(this,msgIds);

        
    }





    // Use this for initialization
    void Start ()
    {


      
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.L))
        {

          //  LuaLoadRes.Instance.DebugBundle("UIScene");

        }
     
	
	}
}
