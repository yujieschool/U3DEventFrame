

using UnityEngine;
using System.Collections;

using U3DEventFrame;


using System.Collections.Generic;







public delegate  void  ResRequesetCallBack(BundleBackNode  node);



/// <summary>
/// 一个nodes 就是一个bundle 包所有请求
/// </summary>
public class BundleBackNode : NodeBase
{


    public BundleInfo[] bundleInfo;
   
   // public string[] resName;

    //AB 的全称 like   test.ld
    public string fullBundleName;

    //AB 文件夹   Test 这样的文件夹
     public string bundleName;

     public string scenceName;

  //  public bool[] isSingle;

  //  public Object[][] resObjs;


    public ushort backId;

    public byte  initialCount ;

    ResRequesetCallBack callBack;



    public string ScenceName
    {
        get
        {

            return scenceName;
        }
        set
        {

            scenceName = value;
        }
    }




    public string BundleName
    {
        get
        {
           
            

            return bundleName;
        }
        set
        {

            bundleName = value;
        }
    }



    public string FullBundleName
    {
        get
        {

            return fullBundleName;
        }
        set
        {

            fullBundleName = value;
        }
    }




    public BundleBackNode(bool[] single, string scence, string bundle, BundleBackNode nextValue, params string[] res)
    {



        this.next = nextValue;


        this.scenceName = scence;

        this.bundleName = bundle;

        this.bundleInfo = new BundleInfo[res.Length];

        for (int i = 0; i < res.Length; i++)
        {
            this.bundleInfo[i] = new BundleInfo(single[i],scence,bundle, res[i] , AddReses);
        }

        //    this.resName = res;



       // this.resObjs = new Object[resName.Length][];

        this.initialCount =0 ;
    }


    public void AddCallBack(ResRequesetCallBack  back)
    {

        this.callBack = back;
 
    }



    public void DebugObj()
    {


        int tmpBundleInfoLenght = this.bundleInfo.Length;

      //  Debuger.Log("this.bundleInfo.Length===" + tmpBundleInfoLenght);

      
        for (int i = 0; i < tmpBundleInfoLenght; i++)
         {
            int  tmpCount =   this.bundleInfo[i].resObj.Length;

           // Debuger.Log("this.bundleInfo[index].resObj.Length===" + this.bundleInfo[i].resObj.Length);
            //for (int j = 0; j < tmpCount; j++)
            //{
            //    Debuger.Log(this.bundleInfo[i].resObj[j]);

            //}
               
         }
        
    }


    public Object[] GetResObjs(int index)
    {

        return this.bundleInfo[index].resObj;
    }


    public Object[] GetResObjs(string res)
    {

        for (int i = 0; i < this.bundleInfo.Length; i++)
        {
            if (this.bundleInfo[i].resName==res)
            {
               // Debug.Log("res have  fund ==" + this.bundleInfo[i].resObj[0].name);

                return this.bundleInfo[i].resObj;
            }
            else
            {

              // Debuger.Log("res contain resName ==" + bundleInfo[i].resName);
            }

        }

        return null;
 
    }


    public void AddReses(BundleInfo  tmpBundle)
    {

        this.initialCount++;



       // Debug.Log("bundleCount==" + initialCount);


       // Debuger.Log("hunck Back bundle ==" + tmpBundle.bundleName + "==res Name==" + tmpBundle.resName);
        if (this.initialCount == this.bundleInfo.Length)
        {

            callBack(this);
        }

    }




    public override void  Dispose()
    {
        base.Dispose();

        for (int i = 0; i < this.bundleInfo.Length; i++)
        {
           this.bundleInfo[i].Dispose();
        }

        this.bundleInfo = null;
        
       // resName = null;
        fullBundleName = null;
        bundleName = null;
        scenceName = null;

    }


}




public class AssetRequesetInfo
{
    public string scenceName;
    public string[] bundles;

   
    public string[][] resNames;

    public bool[][] resSingle;

    public ushort backId;


    public void Dispose()
    {
        bundles = null;

        resNames = null;

        resSingle = null;
    }




    public AssetRequesetInfo(ushort backid, string scence, string[] bundle, string[][] res  ,bool[][] singles)
    {

       // Debuger.Log("AssetRequesetInfo =="+bundle.Length);


        this.backId = backid;


        this.scenceName = scence;

        this.bundles = bundle;

        this.resNames = res;

        this.resSingle = singles;
    }

    public int GetResIndex(string tmpBundle)
    {
        for (int i = 0; i < bundles.Length; i++)
        {
            if (bundles[i]==tmpBundle)
            {
                return i;
            }
        }
        return -1;
    }

   

    public void Debug()
    {

 

        UnityEngine.Debug.Log("back  id ==" +  backId);

        UnityEngine.Debug.Log("bundles count==" + bundles.Length);

        for (int i = 0; i < bundles.Length; i++)
        {
            UnityEngine.Debug.Log("bundles i==" + bundles[i]);
        }



        UnityEngine.Debug.Log("resNames count==" + resNames.Length);

//        for (int i = 0; i < resNames.Length; i++)
//        {
//            for (int j = 0; j < resNames[i].Length; j++)
//            {
//                UnityEngine.Debug.Log("resNames i j==" + resNames[i][j]);
//
//            }
//               
//        }


    }


}


/// <summary>
///  BundleRequest  包含 多种bundles 请求 包含多个nodees   一个nodes 就是一个bundle 包所有请求  一个nodes 又包含包内多个 res 加载请求
/// </summary>
public class BundleRequest
{


      public  delegate void  BundleRequesetBack (BundleRequest  requeset) ;



    public AssetRequesetInfo requesetInfo;


    public BundleBackNode[] bundleNode;


    public byte bundleCount;

    private AssetBase assetBase ;

    private BundleRequesetBack requesetCallBack;


    public void Dispose()
    {

      //  Debug.Log("BundleRequest   release");

      //  assetBase = null;
        requesetCallBack = null;

        requesetInfo.Dispose();

        for (int i = 0; i < bundleNode.Length; i++)
        {
            bundleNode[i].Dispose();

            bundleNode[i] = null;
        }

       bundleNode = null;

    }

    /// <summary>
    /// 唯一关键id
    /// </summary>
    public ushort BackId
    {
        get
        {
            return requesetInfo.backId;
        }
    }


    public BundleBackNode GetBackNode(string bundleName)
    {

        int tmpIndex = requesetInfo.GetResIndex(bundleName);


        if (tmpIndex != -1)
        {

            return bundleNode[tmpIndex];
        }
        else
        {
            return null;
        }
    }


    public void DebugBundle()
    {


       // Debuger.Log("bundleNode.Length====" + bundleNode.Length);
        for (int i = 0; i < bundleNode.Length; i++)
        {

           // Debuger.Log("bundleNode.Length=2222===" + i);
            BundleBackNode backNode = bundleNode[i];

            backNode.DebugObj();
        }
 
    }

    public Object[] GetBundleObjs(int index,int resIndex)
    {
      BundleBackNode backNode =   bundleNode[index];


      return backNode.GetResObjs(resIndex);

    }


    public Object[] GetBundleObjs(string bundleName, string resName)
    {
        BundleBackNode backNode = GetBackNode(bundleName);

      //  Debug.Log("bundleName ==" + bundleName);
        return backNode.GetResObjs(resName);
    }




    public BundleRequest(AssetRequesetInfo info, AssetBase mono, BundleRequesetBack callBack)
    {


        requesetCallBack = new BundleRequesetBack( callBack);
        this.requesetInfo = info;
        bundleCount = 0;

        this.assetBase = mono;


        //info.Debug();

        bundleNode = new BundleBackNode[info.bundles.Length];

        for (int i = 0; i < info.bundles.Length;i++ )
        {
            BundleBackNode tmpBundle = new BundleBackNode(info.resSingle[i],info.scenceName, info.bundles[i], null, info.resNames[i]);

            tmpBundle.AddCallBack(ResLoadFinish);
            bundleNode[i] = tmpBundle;
        }



       
    }




    public void ResLoadFinish(BundleBackNode  node)
    {



        bundleCount++;


       

      

        if (bundleCount == bundleNode.Length)
        {
//            Debug.Log("bundleCount==" + bundleCount);
//            Debuger.Log("hunck   the whole Bundle  back ==" + node.bundleName);
           
           AssetResponseMsg tmpResponse = new AssetResponseMsg(this);
            assetBase.SendMsg(tmpResponse);


           requesetCallBack(this);
           

    
        }

    }

}


public class BundleRequestManager
{

    Dictionary<ushort, BundleRequest> bundleRequeset;

    private AssetBase monoBase;

    public BundleRequestManager()
    {
        bundleRequeset = new Dictionary<ushort, BundleRequest>();
    }

    public void AddRequeset(  BundleRequest  requeset)
    {
        if (!bundleRequeset.ContainsKey(requeset.BackId))
        {

            bundleRequeset.Add(requeset.BackId, requeset);
        }
        else
        {
            Debuger.LogError("Bundle Requeset have contain  key=== " + requeset.BackId);
        }

       
    }



    //public void ResRequesetBack(BundleBackNode node)
    //{
    //    if (bundleRequeset.ContainsKey(node.backId))
    //    {

    //       BundleRequest  tmpRequeset =   bundleRequeset[node.backId];


    //       tmpRequeset.ResLoadFinish (node);
    //    }
    //}


    public void Remove(BundleRequest tmpRequeset)
    {


        bundleRequeset.Remove(tmpRequeset.BackId);

        tmpRequeset.Dispose();


    }


}

/// <summary>
/// BundleRequestManager  上层的请求管理类   一次请求包含  多个bundle 多个res
/// node manager  包含 一个包的 所有res 请求
/// </summary>
public class MutiResManager : NodeManagerBase
{
    private BundleRequestManager requesetManager;

    private ResRequesetCallBack requesetCallBack;


    private AssetBase monoBase;

    public MutiResManager( AssetBase  mono, ResRequesetCallBack  callBack)
    {

        monoBase = mono;
        requesetCallBack = callBack;
        requesetManager = new BundleRequestManager();
    }


    //public string scenceName;
    //public string[] bundles;

    //public string[][] resNames;

    //public ushort backId;

    /// <summary>
    ///  上层消息 转化为requeset 表
    /// </summary>
    /// <param name="tmpMsg"></param>
    /// <returns></returns>
    public BundleRequest AddRequeset(AssetRequesetMsg tmpMsg)
    {
     //   BundleBackNode tmpNode = new BundleBackNode();

        BundleRequest tmpRequeset = new BundleRequest(tmpMsg.requesetInfo, monoBase, FinishRequeset);

       // Debug.Log("tmpRequeset back msgid ===="+tmpRequeset.BackId);
        requesetManager.AddRequeset(tmpRequeset);

       // ABReuqeset(tmpRequeset);


        return tmpRequeset;
    }
    /// <summary>
    /// 以assetbundle  full name 为链表 key
    /// </summary>
    /// <param name="tmpNode"></param>
    public void AddNodes(BundleBackNode tmpNode)
    {
        this.AddNode(tmpNode.FullBundleName, tmpNode);

    }





    /// <summary>
    ///    bundle reuqeset  已经向上层发送object 完毕
    /// </summary>
    /// <param name="requeset"></param>
    public void FinishRequeset(BundleRequest requeset)
    {



        for (int i = 0; i < requeset.bundleNode.Length; i++)
        {
          
            this.ReleaseNode(requeset.bundleNode[i].FullBundleName, requeset.bundleNode[i]);
        }


            requesetManager.Remove(requeset);

           
    }






    /// <summary>
    /// 释放一个ｋｅｙ　链
    /// </summary>
    /// <param name="bundle"></param>
    private void ReleaseNodeWithoutDispose(string  bundle)
    {
        if (manager.ContainsKey(bundle))
        {
            NodeBase topNode = manager[bundle];

            // 挨个释放
            while (topNode.next != null)
            {
                NodeBase curNode = topNode;

                topNode = topNode.next;

            }

            manager.Remove(bundle);
        }
    }

    /// <summary>
    /// assetbundle  已经load 到内存中回调 
    /// </summary>
    /// <param name="fullBundleName"></param>
    public void BundleLoadFinish(string fullBundleName)
    {

        if (manager.ContainsKey(fullBundleName))
        {

            NodeBase topNode = manager[fullBundleName];


            do
            {
                BundleBackNode tmpNode = (BundleBackNode)topNode;

                requesetCallBack(tmpNode);  // 去请求资源

                topNode = topNode.next;
            }
            while (topNode != null);

         //bundle 加载完成
            manager.Remove(fullBundleName);


        

        }

       // requesetManager
    }
      
}





public class NativeLoadMutiRes : AssetBase
{

    #region   C#LoadRes



    public void RequesetBundle(string scenceName,string  bundleName,string fullBundleName)
    {

        ///没有加载
        if (!ILoadManager.Instance.IsLoadingAssetBundle(scenceName, bundleName))
        {

            ILoadManager.Instance.LoadAsset(scenceName, bundleName, LoaderProgrocess);



        }
        else if (ILoadManager.Instance.IsLoadingBundleFinish(scenceName, bundleName))
        {

          //  Debug.LogError("bundleName  IsLoadingBundleFinish1111111111==" + bundleName);
            MutiResManager.BundleLoadFinish(fullBundleName);
        }
        else
        {

            ILoadManager.Instance.AddLoadFinishBackDelegate(scenceName, bundleName, LoaderProgrocess);
          //  Debuger.LogError("Bundle  have  requeseted  wait for times ==" + bundleName);
        }
    }

    public void GetResources(BundleInfo   tmpBundle)
    {
       // Debug.LogWarning("Get  Resources ===" + tmpBundle.bundleName);

        try
        {

            ILoadManager.Instance.GetResAsys(tmpBundle);
        }
        catch(System.Exception  e)
        {
            //Debug.Log("exception==" + e);
        }
        

    }




    HunkMutiAssetResesBack resesBackMsg = null;


    HunkMutiAssetResesBack ResesBackMsg
    {
        get
        {
            if (resesBackMsg == null)
            {
                resesBackMsg = new HunkMutiAssetResesBack();
            }

            return resesBackMsg;
        }


    }


    /// <summary>
    /// bundle load 到内存中了 去加载 res
    /// </summary>
    /// <param name="node"></param>
    void RequesetNode(BundleBackNode node)
    {



   
        int  tmpCount=  node.bundleInfo.Length ;

        for (int i = 0; i < tmpCount; i++)
        {

         

            BundleInfo tmpInfo = node.bundleInfo[i];

            GetResources(tmpInfo);

           
          
        }
      
            




    }

    MutiResManager   mutiResManager = null;


    MutiResManager MutiResManager
    {
        get
        {
            if (mutiResManager == null)
            {
                mutiResManager = new MutiResManager(this, new ResRequesetCallBack(  RequesetNode));
            }

            return mutiResManager;
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

            case (ushort)AssetEvent.HunkMutiRes:

                {

                  
                    AssetRequesetMsg tmpMsg = (AssetRequesetMsg)msg;


                   

                   tmpMsg.requesetInfo.Debug();

                    BundleRequest tmpRequeset = MutiResManager.AddRequeset(tmpMsg);


                    try
                    {
                        int tmpCount = tmpRequeset.bundleNode.Length;

                        for (int i = 0; i < tmpCount; i++)
                        {

                            BundleBackNode tmpNode = tmpRequeset.bundleNode[i];

                            string bundlFullName = ILoadManager.Instance.GetBundleRelateName(tmpNode.ScenceName, tmpNode.BundleName);

                            tmpNode.FullBundleName = bundlFullName;



                            MutiResManager.AddNodes(tmpNode);


                            RequesetBundle(tmpNode.ScenceName, tmpNode.BundleName, bundlFullName);



                        }
                    }
                    catch (System.Exception e)
                    {

                        Debug.LogError( "e =="+e);
                    }




                


                }
                break;

            case (ushort)AssetEvent.HunkScences:
                {

                    RequesetScenceMsg tmpMsg = (RequesetScenceMsg)msg;


                    ILoadManager.Instance.LoadAsset(tmpMsg.scenceName, tmpMsg.bundleName, tmpMsg.backDelegate);
 
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

            MutiResManager.BundleLoadFinish(bundle);

           // CallBack.CallBackRes(bundle);

          //    CallBack.Dispose(bundle);


        }

    }







    #endregion

    void Awake()
    {
        msgIds = new ushort[] {
            (ushort)AssetEvent.HunkMutiRes,

            (ushort)AssetEvent.HunkScences


        };

        RegistSelf(this,msgIds);

        
    }





    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
