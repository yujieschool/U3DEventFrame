using UnityEngine;
using System.Collections;

using System.Collections.Generic;


namespace U3DEventFrame
{
	
	public class IABRelationManager  
	{

		IABLoader assetLoader;


		private string bundleName;

		private byte   loadState  ;





		/// <summary>
		/// 
		///        ---->           yy      ---->  
		///     xx  referBundle              depedenceBundle      zz
		///       ---- >           AA      --- >
		/// 
		/// 
		///       yy and   aa     depedenceBundle  zz
		///       
		///       yy  and   aa    referBundle   xx
		/// 
		/// </summary>
		//记录所有被相关联的包
		List<string> depedenceBundle  =null;





		//记录所有相关联的包
		List<string> referBundle = null;


        List<string> ReferBundle
        {
            get
            {
                if (referBundle == null)
                {
                    referBundle = new List<string>();
                }

                return referBundle;

            }
        }


        List<string> DepedenceBundle
        {
             get
            {
                if (depedenceBundle == null)
                {
                    depedenceBundle = new List<string>();
                }
                return depedenceBundle;
            }
        }

        LoaderProgrocess delegateProgrocess;



        /// <summary>
        ///   loadState  ０　表示未加载　１　表示正在加载　　２　表示加载完成
        /// </summary>
        /// <returns></returns>
		public bool IsLoadingFinish()
		{
            if (loadState == 2)
            {

                return true;

            }
            else
            {
                return false;
            }
			
		}

        public bool IsUnLoadAssetBundle()
        {
            if (loadState == 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }




		public void  BundleLoadFinish(string bundleName)
		{
            loadState = 2 ;
		}


		//初始化
		public void Initial( LoaderProgrocess  progress)
		{

			delegateProgrocess = new LoaderProgrocess( progress);
            loadState = 0 ;


          


            assetLoader = new IABLoader(progress,BundleLoadFinish);

		}




	    public LoaderProgrocess GetProgress()
		{

			return delegateProgrocess;
		}










		public string GetBundleName()
		{

			return bundleName;
		}

		public void SetBundleName(string name)
		{
			bundleName = name;

			assetLoader.SetBundleName(name);



		//	string bundlePath = IPathTools.GetAssetBundleFilePath(bundleName);


           // Debug.Log("bundlePath =="+ bundlePath);
		
			assetLoader.LoadResources(bundleName);



		}




        #region   Refer



        public void AddReferrence(string bundleName)
        {
            ReferBundle.Add(bundleName);

        }


        public List<string> GetReferrence()
        {
            return ReferBundle;
        }


        public bool RemoveReferrence(string bundleName)
        {
           
        

               int  tmpCount =  ReferBundle.Count ;
               for (int i = 0; i < tmpCount; i++)
                {
                    if (bundleName.Equals(referBundle[i]))
                    {
                        ReferBundle.RemoveAt(i);

                        break;
                    }
                }

                if (ReferBundle.Count <= 0)
                {
                   // Dispose();

                    return true;
                }

            


            return false;

        }
        #endregion

        #region    dependence


        public void SetDependences(string[] dependence)

		{
			if (dependence.Length >0)
			{



				DepedenceBundle.AddRange(dependence);
			}


		}
		public List<string> GetDependences()
		{


			return DepedenceBundle;
		}

		public void RemoveDependence(string bundleName)
		{



            int dependeceCount = DepedenceBundle.Count;
            for (int i = 0; i < dependeceCount; i++)
                {
                    if (DepedenceBundle[i] == bundleName)
                    {
                        DepedenceBundle.RemoveAt(i);

                        break;
                    }
                }
            


		}


        #endregion





        #region   同下层提供API



        public IEnumerator LoadAssetBundle()
        {

            loadState = 1;

            yield return assetLoader.CommonLoad();

        }


        public void AddLoadFnishBackDelegate(LoaderProgrocess  progress)
        {
            assetLoader.AddDelegate(progress);
        }


        /// <summary>
        ///异步加载 res
        /// </summary>
        /// <param name="tmpBndle"></param>
        /// <returns></returns>
        public IEnumerator GetResAsys(BundleInfo  tmpInfo)
        {
            yield return assetLoader.GetResAsys(tmpInfo);
 
        }

        //load  single  resourece

        public Object GetSingleResource(string name)

        {


            return assetLoader.GetResource(name);
        }
        //load  muti  resourece

        public Object[] GetAllResources(string name)
        {

            return assetLoader.GetAllResources(name);
        }

        /// <summary>
        /// 释放一个资源
        /// </summary>
        /// <param name="tmpObj"></param>
        public void DisposeSingleRes(Object tmpObj)
        {
            assetLoader.UnloadBundleRes(tmpObj);
        }

        /// <summary>
        /// 释放一个包
        /// </summary>
        public void Dispose()
        {

            loadState = 0;
            RemoveDependence(bundleName);

            if (assetLoader != null)
            {
                assetLoader.commonDispose();
                assetLoader = null;
            }
           

          

            depedenceBundle = null;

            referBundle = null;
        }


        public void DebuggerAsset()
        {
            if (assetLoader != null)
            {
                assetLoader.DebuggerLoader();
            }
            else
            {
               // Debuger.Log("assetLoader  is null");
            }


        }




        #endregion




    }

}

