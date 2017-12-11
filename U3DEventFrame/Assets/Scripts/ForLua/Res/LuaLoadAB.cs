using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using U3DEventFrame;



using LuaInterface;



class BundleMutiResManager
{
    //bundleName  相对路径
    Dictionary<string, MutiCallBackNode> manager = null;

    public BundleMutiResManager()
    {
        manager = new Dictionary<string, MutiCallBackNode>();


    }

    public bool ContainsKey(string name)
    {
        return manager.ContainsKey(name);
    }

    public void AddBundle(string bundle, MutiCallBackNode tmpNode)
    {

        if (manager.ContainsKey(bundle))
        {
            MutiCallBackNode topNode = manager[bundle];

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

            MutiCallBackNode topNode = manager[bundle];

            // 挨个释放
            while (topNode.nextValue != null)
            {
                MutiCallBackNode curNode = topNode;

                topNode = topNode.nextValue;


                curNode.Dispose();
            }
            // 最后一个结点的释放
            topNode.Dispose();



            manager.Remove(bundle);
        }
    }

    public void LoadBundleFinish(string bundle)
    {
        if (manager.ContainsKey(bundle))
        {
            MutiCallBackNode topNode = manager[bundle];



        }

    }

    public void LoadResFinish(MutiCallBackNode node)
    {
 
    }




    public void CallBackLua(string bundle)
    {
        if (manager.ContainsKey(bundle))
        {
            MutiCallBackNode topNode = manager[bundle];

            do
            {
                if (topNode.isSingle)
                {



                    int tmpCount = topNode.resName.Length;
                    object[] results = new object[tmpCount];

                    for (int i = 0; i < tmpCount; i++)
                    {
                        results[i] = ILoadManager.Instance.GetSingleResources(topNode.scenceName, topNode.bundleName, topNode.resName[i]);
                    }

                    topNode.luaFunc.Call(topNode.scenceName, topNode.bundleName, topNode.resName, results);

                }
                else
                {
                    object[][] results = new object[topNode.resName.Length][];
                    for (int i = 0; i < topNode.resName.Length; i++)
                    {
                        results[i] = ILoadManager.Instance.GetMutiResources(topNode.scenceName, topNode.bundleName, topNode.resName[i]);
                    }


                    topNode.luaFunc.Call(topNode.scenceName, topNode.bundleName, topNode.resName, results);
                }
                topNode = topNode.nextValue;
            }
            while (topNode != null);



        }
        else
        {
            // Debuger.Log("extern contain bundle =="+bundle);
        }
    }





}







public class LuaLoadAB  {




        private static LuaLoadRes instance = null;
        public static LuaLoadRes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LuaLoadRes();
                }

                return instance;
            }

        }



        #region   LUALoadRes


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle"> bundle 相对路径</param>
        /// <param name="progress"></param>
        void LoaderProgrocess(string bundle, float progress)
        {

            //        Debug.Log("Lua  load progresss 22222222222222=="+ progress);

            if (progress >= 1.0f)
            {

                CallBack.CallBackLua(bundle);

                CallBack.Dispose(bundle);


            }

        }


        // ScenceName
        CallBackManager callBack = null;


        CallBackManager CallBack
        {

            get
            {
                if (callBack == null)
                {
                    callBack = new CallBackManager();
                }

                return callBack;
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
        public void GetResources(string scenceName, string bundleName, string res, bool single, LuaFunction luaFunc)
        {



            ///没有加载过
            if (!ILoadManager.Instance.IsLoadingAssetBundle(scenceName, bundleName))
            {



                ILoadManager.Instance.LoadAsset(scenceName, bundleName, LoaderProgrocess);

                string bundlFullName = ILoadManager.Instance.GetBundleRelateName(scenceName, bundleName);

                if (bundlFullName != null && luaFunc != null)
                {


                    CallBackNode tmpNode = new CallBackNode(scenceName, bundleName, res, luaFunc, single, null);
                    CallBack.AddBundle(bundlFullName, tmpNode);
                    // Debug.LogError("Get Multi Resources==" + bundleName + "res ==" + res);



                }
                else
                {
                    Debuger.LogWarning("doest not  contain bundle" + bundleName);
                }

            }
            //已经加载完成
            else if (ILoadManager.Instance.IsLoadingBundleFinish(scenceName, bundleName))
            {

                if (luaFunc != null)
                {

                    if (single)
                    {

                        luaFunc.Call(scenceName, bundleName, res, ILoadManager.Instance.GetSingleResources(scenceName, bundleName, res));
                    }
                    else
                    {
                        luaFunc.Call(scenceName, bundleName, res, ILoadManager.Instance.GetMutiResources(scenceName, bundleName, res));
                    }


                    luaFunc.Dispose();
                }





            }

            else
            {


                if (luaFunc != null)
                {
                    string bundlFullName = ILoadManager.Instance.GetBundleRelateName(scenceName, bundleName);

                    CallBackNode tmpNode = new CallBackNode(scenceName, bundleName, res, luaFunc, single, null);
                    CallBack.AddBundle(bundlFullName, tmpNode);


                    // Debug.LogError("Get Multi Resources==" + bundleName + "res ==" + res);
                }





            }
        }


        #endregion

        #region  Unload bundle  res
        /// <summary>
        /// 释放一个资源Object
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        /// <param name="res"></param>
        public void UnloadResObj(string scenceName, string bundleName, string resName)
        {


            ILoadManager.Instance.UnloadResObj(scenceName, bundleName, resName);
        }

        /// <summary>
        ///  释放一个assetbundle　里面所有的object
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        public void UnloadBundleObj(string scenceName, string bundleName)
        {
            ILoadManager.Instance.UnloadBundleResObj(scenceName, bundleName);
        }


        /// <summary>
        ///   释放一个场景　里面所有的object
        /// </summary>
        /// <param name="scenceName"></param>
        public void UnloadScenceObjes(string scenceName)
        {
            ILoadManager.Instance.UnloadAllResObj(scenceName);
        }


        public void UnLoadBundleAndRes(string scenceName, string bundleName)
        {

            ILoadManager.Instance.UnloadAssetBundleAndRes(scenceName, bundleName);
        }

        /// <summary>
        /// 　释放一个bundle
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        public void UnloadSingleBundle(string scenceName, string bundleName)
        {

            ILoadManager.Instance.UnloadAssetBundle(scenceName, bundleName);
        }

        /// <summary>
        ///  ///  卸载　一个场景下所有的  bundle 但是保留 Object
        /// </summary>
        /// <param name="scenceName"></param>
        public void UnloadScenceBundle(string scenceName)
        {
            ILoadManager.Instance.UnloadAllAssetBundle(scenceName);


        }
        /// <summary>
        /// 　释放一个场景中所有的　bundle 和 object
        /// </summary>
        /// <param name="scenceName"></param>
        public void UnloadAll(string scenceName)
        {
            ILoadManager.Instance.UnloadAllAssetBundleAndResObj(scenceName);


        }


        public void DebugBundle(string scenceName)
        {
            ILoadManager.Instance.DebugAllAssetBundle(scenceName);
        }

        #endregion













}
