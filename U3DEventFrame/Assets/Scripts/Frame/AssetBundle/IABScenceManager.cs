using UnityEngine;
using System.Collections;


using System.Collections.Generic;

using System.IO;

namespace U3DEventFrame
{



	public class IABScenceManager
    {

	


		/// <summary>
		/// 读取资源 进度
		/// </summary>
		/// <param name="bundleName"></param>
		/// <param name="progress"></param>

	//	void LoadProgrocess(string bundleName, float progress)
//		{





	//	}


		public IABScenceManager(string scenName)
		{

			loadManager = new IABManager (scenName);
		}



		// private Dictionary<string, int> assetNumber = new Dictionary<string, int>();

		/// <summary>
		/// bundleName  bundlePath  这里面存的是assetbudnle包的真正相对路径
		/// </summary>
		private Dictionary<string, string> allAsset = new Dictionary<string, string>();





		public  void DebugAsset()
		{

			List<string>  keys = new List<string>();

            keys.AddRange(allAsset.Keys);

			for(int i =0  ; i < keys.Count  ;i++)
			{

			//	Debuger.Log(" key =="+ keys[i] +" BundleName =="+allAsset[keys[i]]);

				loadManager.DebuggerBundle(allAsset[keys[i]]);
			}



		}


      

		/// <summary>
		///  第一步读取 整个场景的配置文件
		/// </summary>
		/// <param name="path"></param>
		public void ReadConfiger(string fileName)
		{
			string path = IPathTools.GetAssetBundleFilePath(fileName);

      

            ReadConfig(path);


        }


		private void ReadConfig(string path)
		{

           // Debug.Log("configer path =="+ path);

			FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

			BinaryReader br = new BinaryReader(fs);

			int allCount = 0;

			allCount = br.ReadInt32();

          // Debug.Log("configer allCount ==" + allCount);

            for (int i = 0; i < allCount; i++)
			{
				string tmpKey = br.ReadString();



				string tmpValue = br.ReadString();


               // Debug.Log("tmpValue ==" + tmpValue);
                //ScenceOne/Materials.ld 

      

				allAsset.Add(tmpKey, tmpValue.ToLower());
			}



			br.Close();

			fs.Close();
		}


		public string GetBundleRelateName(string  bundleName)
		{

			if (allAsset.ContainsKey(bundleName))
			{
				return allAsset[bundleName] ;
			}
			else
			{
				return null ;
			}
		}

		/// <summary>
		/// 第二步    读取 单个包中 资源
		/// </summary>
		/// <param name="bundleName"></param>

		public void LoadAsset(string bundleName, LoaderProgrocess  progress,LoadAssetBundleCallBack  callBack)
		{

			if (allAsset.ContainsKey(bundleName))
			{

                 string  tmpValue = allAsset[bundleName] ;

               // Debug.Log("LoadAsset =="+ tmpValue);

				loadManager.LoadAssetBundle(allAsset[bundleName] ,progress,callBack);





			}
			else
			{
				Debuger.LogError("Donot contain budnle =="+bundleName);
			}

		}




        public void AddLoadFnishBackDelegate(string bundleName, LoaderProgrocess progress)
        {
            if (allAsset.ContainsKey(bundleName))
            {

                loadManager.AddLoadFnishBackDelegate(allAsset[bundleName],progress);
              

          
            }
            else
            {

                Debuger.LogError("IABManager no contain  bundle ==" + bundleName);
            }
        }




		public IEnumerator LoadAssetSys(string bundleName)
		{
           // Debug.Log("LoadAssetSys ScenceManager =="+bundleName);

            
            yield return  loadManager.LoadAssetBundles (bundleName);

		}

		/// <summary>
		/// 获取资源
		/// </summary>
		/// <param name="bundleName"> bundle name</param>
		/// <param name="resName"> 资源名称</param>
		/// <returns></returns>

		public Object GetSingleResource(string bundleName, string resName)
		{

			if (allAsset.ContainsKey(bundleName))
			{
				
				return   loadManager.GetSingleResource(allAsset[bundleName], resName);

			}
			else
			{
                
				Debuger.LogError("IABLoadManager  GetSingleResources   no key  bundleName=="+bundleName +"==resName=="+resName);
				return null;
			}
		}
        public IEnumerator GetResAsys(string bundleName, BundleInfo bundInfo)
        {

            if (allAsset.ContainsKey(bundleName))
            {

               // yield return null;
               yield return loadManager.GetResAsys(allAsset[bundleName], bundInfo);

            }
            else
            {

                Debuger.Log("IABLoadManager  GetSingleResources   no key  bundleName==" + bundleName + "==resName==" +bundInfo.resName);
               // yield return null;
            }
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="bundleName"> bundle name</param>
		/// <param name="resName">资源名称</param>
		/// <returns></returns>

		public Object[] GetMutiResources(string bundleName, string resName)
		{
			if (allAsset.ContainsKey(bundleName))
			{

				return loadManager.GetMutiResources(allAsset[bundleName], resName);

			}
			else
			{
				return null;
			}
		}







		/// <summary>
		/// 卸载 Object
		/// </summary>
		/// <param name="name"></param>
		public void DisposeResObj(string bundleName,string  res)
		{
			if (allAsset.ContainsKey(bundleName))
			{
                lock (loadManager)
                {
                    loadManager.DisposeResObj(allAsset[bundleName], res);
                }

				

			}
		}
        /// <summary>
        /// 卸载  bundle 里面　所有load出来的Object
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name=""></param>

        public void DisposeBundleReses(string bundleName)
        {
            if (allAsset.ContainsKey(bundleName))
            {

                lock (loadManager)
                {
                    loadManager.DisposeResObj(allAsset[bundleName]);
                }
               

            }
        }

        /// <summary>
        /// 卸载  所有bundle 里面　所有load出来的Object
        /// </summary>
        public void DisposeAllResObj()
        {
            lock (loadManager)
            {
                loadManager.DisposeAllObjs();
            }

           
        }


        public void DisposeBundleAndRes(string bundleName)
        {
            if (allAsset.ContainsKey(bundleName))
            {
                lock (loadManager)
                {
                    loadManager.DisposeBundleAndRes(allAsset[bundleName]);
                }

              

            }
        }

        /// <summary>
        ///  卸载　一个bundle
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="bundle"></param>
        public void DisposeBundle(string bundleName)
        {

            if (allAsset.ContainsKey(bundleName))
            {

                lock (loadManager)
                {
                    loadManager.DisposeBundle(allAsset[bundleName]);
                }
                

            }
        }

        /// <summary>
        ///  卸载　一个场景下所有的  bundle 但是保留 Object
        /// </summary>
        public void DisposeAllBunlde()
        {
            lock (loadManager)
            {
                loadManager.DisposeAllBundle();
            }
           
            
        }

        /// <summary>
        /// 把bundle　和　object　全部释放
        /// </summary>
		public void DisposeAll()
		{
            lock (loadManager)
            {
                loadManager.DisposeAllBundleAndReses();

                allAsset.Clear();
            }

		}



	   public bool IsLoadingFinish(string bundleName)
		{
            if (allAsset.ContainsKey(bundleName))
            {
                return loadManager.IsLoadingFinish(allAsset[bundleName]);
            }
            else
            {
                Debuger.LogError("is not contain bundle ==" + bundleName);

                return false;
            } 
		}

        public bool IsLoadingAssetBundel(string bundleName)
        {
            if (allAsset.ContainsKey(bundleName))
            {
                return loadManager.IsLoadingAssetBundle(allAsset[bundleName]);
            }
            else
            {
                Debuger.LogError("is not contain bundle ==" + bundleName);

                return false;
            }
        }




        private IABManager loadManager;





	}


}

