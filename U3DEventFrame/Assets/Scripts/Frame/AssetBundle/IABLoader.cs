

using UnityEngine;
using System.Collections;



namespace U3DEventFrame

{


	public delegate void LoaderProgrocess(string bundle, float progress);


	public delegate void LoadFinish(string bundle);

	/// <summary>
	/// AssetsBundle加载类，在后台随时加载游戏资源
	/// <para>
	/// 每个游戏资源的加载，应在进入游戏房间便开始，这样玩家感觉不到资源加载的速度延迟
	/// </para>
	/// <para>
	/// 实测每5M的本地资源，加载时间约1秒钟
	/// </para>
	/// </summary>
	public class IABLoader
	{




		public IABLoader(LoaderProgrocess loadDelegate ,LoadFinish  tmploadFinish)
		{

           // Debug.Log("LoaderProgrocess   === LoaderProgrocess");

            loadPorgress = new LoaderProgrocess(loadDelegate);
			loadPorgress = loadDelegate;

			this.loadFinish = tmploadFinish;

		}


        public void AddDelegate(LoaderProgrocess loadDelegate)
        {

            loadPorgress += loadDelegate;
        }

		private string bundleName;

        /// <summary>
        /// 资源加载路径
        /// </summary>
       // private string commonResourcesPath = "";


        /// <summary>
        /// 资源加载进度
        /// </summary>
        private float commonResourcesDone = 0f;

        /// <summary>
        /// 加载资源用的WWW对象
        /// </summary>
       // private WWW commonData;

        //  private ushort evnentID;

        private LoaderProgrocess loadPorgress = null;

        private LoadFinish loadFinish = null;

        public ResIndexers commonRes;


        public void SetBundleName(string name)
		{
			bundleName = name;
		}

		#region 加载资源
		/// <summary>
		/// 加载公共资源
		/// </summary>

		/// <summary>
		/// 开始载入，在Unity3d环境中用StartCoroutine()来开始一个资源的载入
		/// 
		/// commonResourcesPath ==file:///E:/SVN/3DFish/Project/Fish/Assets/StreamingAssets/AssetBundles/Windows/scenceone/prefeb.ld
		/// </summary>
		/// <returns></returns>
		public IEnumerator CommonLoad()
		{


			commonResourcesDone = 0;



            string bundlePath = IPathTools.GetAssetBundleFilePath(bundleName);

          //  Debuger.Log("bundlePath =="+ bundlePath);
            //判断persistent是否存在
            if (!IFileTools.IsExistFile(bundlePath))
            {

                bundlePath = IPathTools.GetAssetBundleWWWStreamPath(bundleName);

				WWW bundle = WWW.LoadFromCacheOrDownload(bundlePath, FrameTools.VersionId);

                yield return bundle;



                if (!string.IsNullOrEmpty(bundle.error))
                {


                    Debuger.Log(bundle.error);

                  //  yield break;
                }
                else
                {
                   // Debug.Log("manifestPath =777777=" + bundle.progress);

                    commonResourcesDone = 1.0f;


                    commonRes = new ResIndexers(bundle.assetBundle);

                    //    Debug.Log("commonResourcesPath load finish= 2222=" + tmpRequest.assetBundle);


                    this.loadFinish(bundleName);
                    loadPorgress(bundleName, commonResourcesDone);
                }

              

            }
            else
            {
                AssetBundleCreateRequest tmpRequest = AssetBundle.LoadFromFileAsync(bundlePath);

                yield return tmpRequest;



                if (tmpRequest.assetBundle != null)
                {


                    commonResourcesDone = 1.0f;
                    //  Debug.Log("commonResourcesPath load finish= 2222=" + bundleName);

                   

                    commonRes = new ResIndexers(tmpRequest.assetBundle);

                    //    Debug.Log("commonResourcesPath load finish= 2222=" + tmpRequest.assetBundle);


                    this.loadFinish(bundleName);
                    loadPorgress(bundleName, commonResourcesDone);
                }
                else
                {
                    Debuger.LogError(" bundleName is not finish ==" + bundleName);

                  //  yield break;

                }

            }





		}



		public bool IsFree()
		{

			if (commonResourcesDone <= 0 || commonResourcesDone >= 1)
				return true;

			else
				return false;
		}

		/// <summary>
		/// 资源加载进度
		/// </summary>
		public float commonLoadingProgress
		{
			get
			{

				return commonResourcesDone ;
			}
		}




		public void DebuggerLoader()
		{
			if(commonRes != null)
			{
                Debuger.LogError(" bundle name =="+bundleName +"==contain :");

                commonRes.DebugAllRes();

			}
			else
			{
				//Debuger.Log("commonRes  is null  bundleName =="+bundleName);
			}

		}


        /// <summary>
        ///异步加载 res
        /// </summary>
        /// <param name="tmpBndle"></param>
        /// <returns></returns>
        public IEnumerator GetResAsys(BundleInfo tmpBndle)
        {


         //   yield return null;
            yield return commonRes.LoadResAsys(tmpBndle);

        }



		public Object GetResource(string name)
		{
			

			return   commonRes[name];

		}

		public Object[] GetAllResources(string name)
		{

			return commonRes.LoadResources(name);
		}


		/// <summary>
		/// all path  "file:///E:\\SVN\\ThirdMMO\\Assets\\StreamingAssets\\AssetBundles\\Windows\\scenceone\\loading.hd";
		/// </summary>
		/// <param name="path"> scenceone\\loading.hd </param>
		/// <param name="msgId"></param>

		public void LoadResources(string path)
		{


		//	commonResourcesPath = path;


			if (commonRes != null)
			{
				commonDispose();

			}
			else
			{
				Resources.UnloadUnusedAssets();
			}



		}
        /// <summary>
        /// 释放资源
        /// </summary>
        public void commonDispose()
        {



			//Debug.LogError("assetbundle =="+ bundleName);

			if(commonRes != null)
			{
				commonRes.Dispose();
				
				commonRes = null;

                bundleName = null;

                loadFinish = null;

                loadPorgress = null;



            }


        }



        // 卸载资源
        public void UnloadBundleRes(UnityEngine.Object tmpObj)
        {

            commonRes.UnloadRes(tmpObj);
        }


        #endregion


    }

}
