
using System.IO ;
using System.Collections.Generic ;
using  UnityEngine ;


using System.Collections;

namespace  U3DEventFrame
{
	
	
	public class ILoadManager : MonoBehaviour
	{
		
		
		public static ILoadManager Instance;

		//一个场景　一个 manager
		private Dictionary<string, IABScenceManager> loadManagers = new Dictionary<string, IABScenceManager>();
		
		
		//加截　
		public void  LoadCallBack(string scenceName ,string bundleName)
		{

          //  Debug.Log("LoadCallBack  111=="+ scenceName);

         //   Debuger.Log("Begin  load  bundlename==" + bundleName);

            if (loadManagers.ContainsKey (scenceName)) 
			{



              //  Debug.Log("LoadCallBack  3333==" + scenceName);
                IABScenceManager tmpManager = loadManagers[scenceName];

                StartCoroutine(tmpManager.LoadAssetSys(bundleName));
				
			}

          //  Debug.Log("LoadCallBack  222==" + scenceName);

        }
		
		void Awake()
		{
			
			
			Instance = this;


        //    Debug.Log("Iload Manager  load  Mainifeset  =============2222222222222");
			StartCoroutine( IABManifestLoader.Instance.LoadManifeset());
			
			
		}






        /// <summary>
        /// E:/SVN/3DFishTwo/Project/UluaFish/Assets/StreamingAssets/AssetBundles/Android/ScenceTwoRecord.txt
        /// </summary>
        /// <param name="scenceName"></param>
        public void ReadConfiger(string scenceName)
		{
			
		
                IABScenceManager tmpManager = new IABScenceManager(scenceName);
				
				//Debug.Log("scenceName=="+ scenceName);
				tmpManager.ReadConfiger(scenceName+"Record.txt");
				
				loadManagers.Add(scenceName, tmpManager);
				
		
			
		}


		/// <summary>
		/// Loads the asset.  加载场景资源
		/// </summary>
		/// <param name="scenceName">Scence name.</param>
		/// <param name="bundleName">Bundle name.</param>
		/// <param name="progress">Progress.</param>
		public void LoadAsset(string scenceName ,string bundleName,LoaderProgrocess progress)
		{

           // Debug.Log("bundleName=="+ bundleName);
			if (!loadManagers.ContainsKey (scenceName)) 
			{
				
				ReadConfiger(scenceName);
				
			}

            IABScenceManager tmpManager = loadManagers[scenceName];
			tmpManager.LoadAsset(bundleName, progress, LoadCallBack);
			
			
			
		}






        public void AddLoadFinishBackDelegate(string scenceName,string bundleName, LoaderProgrocess progress)
        {

            if (loadManagers.ContainsKey(scenceName))
            {

                IABScenceManager tmpManager = loadManagers[scenceName];
                tmpManager.AddLoadFnishBackDelegate(bundleName, progress);

            }
            else
            {
                Debuger.Log("IloadManager  GetSingleResources   no key ==" + scenceName + "==BundleNamke==" + bundleName);

            }
   
        }



       /// <summary>
       /// 加载单个res
       /// </summary>
       /// <param name="scenceName"></param>
       /// <param name="bundleName"></param>
       /// <param name="resName"></param>
       /// <returns></returns>
		public Object  GetSingleResources(string scenceName,string bundleName, string resName)
		{
			if (loadManagers.ContainsKey (scenceName)) {

                IABScenceManager tmpManager = loadManagers [scenceName];
				return tmpManager.GetSingleResource (bundleName, resName);
			} else {
				
				Debuger.Log("IloadManager  GetSingleResources   no key =="+scenceName +"==BundleNamke=="+bundleName);
				return null ;
			}
			
		}



        public void    GetResAsys(BundleInfo  bundleInfo )
        {
            if (loadManagers.ContainsKey(bundleInfo.ScenceName))
            {

                IABScenceManager tmpManager = loadManagers[bundleInfo.ScenceName];

                StartCoroutine(tmpManager.GetResAsys(bundleInfo.BundleName, bundleInfo));
            }
            else
            {

                Debuger.Log("IloadManager  GetSingleResources   no key ==" + bundleInfo.ScenceName + "==BundleNamke==" + bundleInfo.bundleName);
              
            }

        }



		
        /// <summary>
        /// 加载　多个reses
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
		public Object[]    GetMutiResources(string scenceName,string bundleName, string resName)
		{
			if (loadManagers.ContainsKey (scenceName)) {

                IABScenceManager tmpManager = loadManagers [scenceName];
				return tmpManager.GetMutiResources (bundleName, resName);
			} else {
				
				return null ;
			}
		}



        #region    释放　资源


        /// <summary>
        /// 释放一个资源Object
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        /// <param name="res"></param>
        public void UnloadResObj(string scenceName, string bundleName, string res)
        {
            if (loadManagers.ContainsKey(scenceName))
            {
                IABScenceManager tmpManager = loadManagers[scenceName];

                tmpManager.DisposeResObj(bundleName, res);
            }

        }

        /// <summary>
        ///  释放一个assetbundle　里面所有的object
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>

        public void UnloadBundleResObj(string scenceName, string bundleName)
        {
            if (loadManagers.ContainsKey(scenceName))
            {
                IABScenceManager tmpManager = loadManagers[scenceName];

                tmpManager.DisposeBundleReses(bundleName);
            }

        }


        /// <summary>
        ///   释放一个场景　里面所有的object
        /// </summary>
        /// <param name="scenceName"></param>

        public void UnloadAllResObj(string scenceName)
        {

            if (loadManagers.ContainsKey(scenceName))
            {
                IABScenceManager tmpManager = loadManagers[scenceName];

                tmpManager.DisposeAllResObj();
            }
        }


        /// <summary>
        /// 释放 bundle 和 包里 所有的物体
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        public void UnloadAssetBundleAndRes(string scenceName, string bundleName)
        {
            if (loadManagers.ContainsKey(scenceName))
            {

                IABScenceManager tmpManager = loadManagers[scenceName];
                tmpManager.DisposeBundleAndRes(bundleName);
            }

        }

        /// <summary>
        /// 　释放一个bundle
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        public void UnloadAssetBundle(string scenceName ,string bundleName)
		{
			if (loadManagers.ContainsKey (scenceName)) {

                IABScenceManager tmpManager = loadManagers [scenceName];
				tmpManager.DisposeBundle (bundleName);
			}

		}


        /// <summary>
        ///  ///  卸载　一个场景下所有的  bundle 但是保留 Object
        /// </summary>
        /// <param name="scenceName"></param>

        public void UnloadAllAssetBundle(string scenceName)
        {

            if (loadManagers.ContainsKey(scenceName))
            {

                IABScenceManager tmpManager = loadManagers[scenceName];
                tmpManager.DisposeAllBunlde();

                

                System.GC.Collect();
            }
        }

        /// <summary>
        /// 　释放一个场景中所有的　bundle 和 object
        /// </summary>
        /// <param name="scenceName"></param>

        public void UnloadAllAssetBundleAndResObj(string scenceName)
        {

            if (loadManagers.ContainsKey(scenceName))
            {

                IABScenceManager tmpManager = loadManagers[scenceName];
                tmpManager.DisposeAll();

                loadManagers.Remove(scenceName);

                System.GC.Collect();
            }
        }



        #endregion



        #region  Debug　信息

        public void  DebugAllAssetBundle(string scenceName)
		{
			if(Debuger.EnableLog)
			{
				if (loadManagers.ContainsKey (scenceName)) {

                    IABScenceManager tmpManager = loadManagers [scenceName];
					
					tmpManager.DebugAsset();
					
				}
				else
				{
					Debuger.Log("no contain key=="+scenceName);
				}
			}
			
		}


        #endregion



        public string GetBundleRelateName(string scenceName, string bundleName)
        {

            if (!loadManagers.ContainsKey(scenceName))
            {

                ReadConfiger(scenceName);

            }


            IABScenceManager tmpManager = loadManagers[scenceName];

            if (tmpManager != null)
            {
                return tmpManager.GetBundleRelateName(bundleName);
            }
            else
            {
                return null;
            }


        }



        /// <summary>
        /// 判断是否　加载完成
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public bool IsLoadingBundleFinish(string scenceName, string bundleName)
        {



            bool tmpBool = loadManagers.ContainsKey(scenceName);

            if (tmpBool)
            {

                IABScenceManager tmpManager = loadManagers[scenceName];
                return tmpManager.IsLoadingFinish(bundleName);
            }
            else
            {
                Debuger.Log("this scnece not contain bundle==" + bundleName);

            }

            return false;

        }

        /// <summary>
        /// 判断　是否已经加载进来了
        /// </summary>
        /// <param name="scenceName"></param>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public bool IsLoadingAssetBundle(string scenceName, string bundleName)
        {


            if (loadManagers.ContainsKey(scenceName))
            {

                IABScenceManager tmpManager = loadManagers[scenceName];
                return tmpManager.IsLoadingAssetBundel(bundleName);
            }
            else
            {
                Debuger.Log("this scnece not contain bundle==" + bundleName);
                return false;
            }
        }



        //public IEnumerator LoadAssetSys(string scenceName, string bundleName)
        //{

        //    // Debug.Log("LoadAssetSys==" + scenceName);

        //    IABScenceManager tmpManager = loadManagers[scenceName];
        //    if (tmpManager != null)
        //    {

        //        //  Debug.Log("LoadAssetSys=22=" + bundleName);
        //        yield return tmpManager.LoadAssetSys(bundleName);
        //    }
        //    else
        //    {
        //        Debuger.Log("Manager is not  exit" + scenceName);
        //    }

        //}




    }
}
