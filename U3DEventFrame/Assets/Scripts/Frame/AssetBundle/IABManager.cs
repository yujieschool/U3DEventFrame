using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace U3DEventFrame
{


	public delegate void LoadAssetBundleCallBack( string scneName, string  bundleName) ;



	public class IABManager  {



		public class AssetObj
		{

			public List<Object> objs;



			public AssetObj(params  Object[] tmpObj)
			{
				objs = new List<Object>();
				objs.AddRange(tmpObj);
			}


            public void ReleaseGameObject()
            {

                int tmpCount = objs.Count;
                for (int i = 0; i < tmpCount; i++)
                {

                    objs[i] = null;
                }
                objs.Clear();
            }

			public void ReleaseObj()
			{


                int tmpCount = objs.Count;
                for (int i = 0; i < tmpCount; i++)
                    {
                       
                              if(objs[i].GetType() != typeof(UnityEngine.GameObject))
                            Resources.UnloadAsset(objs[i]);
                    }
    

                objs.Clear();

            }
		}

		public class AssetResObj
		{
			// resources  name
			public Dictionary<string, AssetObj> resObjs;


			public AssetResObj(string name ,AssetObj  tmp)
			{
				resObjs = new Dictionary<string, AssetObj>();

				resObjs.Add(name,tmp);


			}

			public void AddResObj(string  name, AssetObj  tmpObj)
			{

                if (!resObjs.ContainsKey(name))
				resObjs.Add(name,tmpObj);

			}

			public void ReleaseAllResObj()
			{
				List<string> keys = new List<string>();


                keys.AddRange(resObjs.Keys);

  
                 

				for (int i = 0; i < keys.Count; i++)
				{
					ReleaseResObj(keys[i]);
				}

                resObjs.Clear();


            }

            public void ReleaseAllGameObject()
            {
                List<string> keys = new List<string>();


                keys.AddRange(resObjs.Keys);



                for (int i = 0; i < keys.Count; i++)
                {
                    ReleaseGameObject(keys[i]);
                }

                resObjs.Clear();
            }
            public void ReleaseGameObject(string name)
            {
                if (resObjs.ContainsKey(name))
                {
                    AssetObj tmpObj = resObjs[name];


                    tmpObj.ReleaseGameObject();

                    resObjs.Remove(name);
                }
                else
                {
                    Debuger.Log("release Object name is not exist==" + name);
                }
            }

			public void ReleaseResObj(string name)
			{

                if (resObjs.ContainsKey(name))
                {
                    AssetObj tmpObj = resObjs[name];


                    tmpObj.ReleaseObj();

                    resObjs.Remove(name);
                }
                else
                {
                    Debuger.Log("release Object name is not exist=="+name);
                }

			}




			public List<Object> GetResObj(string name)
			{

				if (resObjs.ContainsKey(name))
				{
					AssetObj tmpObj = resObjs[name];

					return tmpObj.objs;
				}
				else
				{

					return null;
				}

			}


		}





		//like :file:///E:/SVN/3DFish/Project/Fish/Assets/StreamingAssets/AssetBundles/Windows/Windows



		//  AssetBundle manifesetAssetBundle;

		string  scenceName  ;
	
		public IABManager(string scenName)
		{

            if(loadHelper == null)
            loadHelper = new Dictionary<string, IABRelationManager>();

            if(loadObj == null)
            loadObj = new Dictionary<string, AssetResObj>();

            scenceName = scenName;
		//	Instance = this;



	
		}

        // assetbundle  内存镜像
        Dictionary<string, IABRelationManager> loadHelper = null;

        //bundle Name    
        Dictionary<string, AssetResObj> loadObj = null;






        public bool  IsLoadingFinish(string bundleName)
		{
			if(loadHelper.ContainsKey(bundleName))
			{
				IABRelationManager loader = loadHelper[bundleName];

				return loader.IsLoadingFinish();
			}
			else
			{
				Debuger.Log("IABManager no contain  bundle =="+bundleName);

				return  false ;
			}

		}

        public bool IsLoadingAssetBundle(string bundleName)
        {
            if (!loadHelper.ContainsKey(bundleName))
            {

                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddLoadFnishBackDelegate(string bundleName,LoaderProgrocess progress)
        {
            if (loadHelper.ContainsKey(bundleName))
            {
                IABRelationManager loader = loadHelper[bundleName];

                 loader.AddLoadFnishBackDelegate(progress);
            }
            else
            {

                Debuger.Log("IABManager no contain  bundle ==" + bundleName);
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bundleName"> 为相对路径 like ==ScenceOne/Prefeb.ld  </param>
		/// <param name="referName"></param>
		/// <param name="progress"></param>
		public void LoadAssetBundle(string bundleName ,LoaderProgrocess  progress ,LoadAssetBundleCallBack callBack)
		{


			if (!loadHelper.ContainsKey(bundleName))
			{

				IABRelationManager loader = new IABRelationManager();

				loader.Initial(progress);

				loader.SetBundleName(bundleName);

				loadHelper.Add(bundleName, loader);



              //  Debug.Log("CALL BACK =="+ bundleName);

				//让 ILoadManager去加载	public IEnumerator LoadAssetBundles(string  bundleName)
				callBack(scenceName,bundleName);
				//StartCoroutine(LoadAssetBundles(bundleName));

				//    StartCoroutine(loader.LoadAssetBundle());





			}
			else
			{

				//progress(bundleName,1);
				Debuger.LogError("IABManager  have contain bundleName =="+bundleName);
			}
		}



		public string[] GetDependences(string bundleName)
		{
			 
			if(IABManifestLoader.Instance.IsLoadFinish())
			{
				return  IABManifestLoader.Instance.GetDepedences(bundleName);
			}

			else
			{
				Debuger.Log("IABManifestLoader is not finish");
				return null;
			}


		}


		/// <summary>
		///  last  release  this   all assetbundle   dependence this
		/// </summary>
		public void UnloadManifeset()
		{
			IABManifestLoader.Instance.UnloadManifeset();
			//manifest.UnloadManifeset();

		}







		/// <summary>
		/// 
		/// </summary>
		/// <param name="bundleName"> like ==ScenceOne\Prefeb.ld  </param>
		/// <param name="referName"></param>
		/// <param name="progress"></param>
		public IEnumerator LoadAssetBundleDependences(string bundleName, string referName, LoaderProgrocess progress)
		{

           
            if (!loadHelper.ContainsKey(bundleName))
			{



             //   Debuger.Log("bundle =="+ referName +"==dependec=="+bundleName);


                IABRelationManager loader = new IABRelationManager();

				loader.Initial(progress);

				loader.SetBundleName(bundleName);

				if (referName != null)
				{

					loader.AddReferrence(referName);
				}

				loadHelper.Add(bundleName, loader);



				yield return LoadAssetBundles(bundleName);
				//     StartCoroutine(LoadAssetBundles(bundleName));

				//    StartCoroutine(loader.LoadAssetBundle());





			}
			else
			{
				if (referName != null)
				{
					IABRelationManager loader = loadHelper[bundleName];


					loader.AddReferrence(referName);
				}

			}
		}

		/// <summary>
		/// Loads the asset bundles.
		/// </summary>
		/// <returns>The asset bundles.</returns>
		/// <param name="bundleName">Bundle name. 相对路径</param>
		public IEnumerator LoadAssetBundles(string  bundleName)
		{

         //  Debug.Log("IABManager load  manifest== " + bundleName);

            while (!IABManifestLoader.Instance.IsLoadFinish())
			{

                //Debug.Log("IABManager  load  manifest3332== " + bundleName);


                yield return  null; //IABManifestLoader.Instance.LoadManifeset();
			}




          // Debug.Log("load bundle  == " + bundleName);


            IABRelationManager loader = loadHelper[bundleName];



			string[]  dependences = GetDependences(bundleName);


            loader.SetDependences(dependences);
            //scenceone/materials.ld

            int  tmpDeferCount = dependences.Length ;
            for (int i = 0; i < tmpDeferCount; i++)
			{

				yield  return    LoadAssetBundleDependences(dependences[i], bundleName, loader.GetProgress());

			}

           // Debug.Log("load  IABManager  manifest3333== " + bundleName);

            if (loader.IsUnLoadAssetBundle())
            {

               // Debug.Log("load  IABManager  444444444444== " + bundleName);


                yield return loader.LoadAssetBundle();
            }

           




		}





        #region  由下层提供 API



        public void DebuggerBundle(string bundleName)
        {
            if (loadHelper.ContainsKey(bundleName))
            {
                IABRelationManager loader = loadHelper[bundleName];
                loader.DebuggerAsset();
            }
            else
            {
              //  Debuger.Log("IABManager no contain  bundle ==" + bundleName);

            }
        }




        #endregion






        public IEnumerator GetResAsys(string bundleName, BundleInfo tmpInfo)
        {

            bool hasFind = false;
            ///缓存中已经加载了Object
            if (loadObj.ContainsKey(bundleName))
            {

                //Debug.Log("get  from memory =="+ resName);
                AssetResObj tmpRes = loadObj[bundleName];

                List<Object> tmpObj = tmpRes.GetResObj(tmpInfo.resName);
                if (tmpObj != null)
                {
                    tmpInfo.AddReses(tmpObj.ToArray());


                    tmpInfo.ReleaseObj();
                    hasFind = true;
                }




            } // 没有找到 继续向下


            if (!hasFind &&loadHelper.ContainsKey(bundleName))
            {
 
                IABRelationManager loader = loadHelper[bundleName];

                yield return loader.GetResAsys(tmpInfo)
                    
                    ;
                AssetObj tmpAssetObj = new AssetObj(tmpInfo.resObj);

                //是否已经包含bundle　
                if (loadObj.ContainsKey(bundleName))
                {
                    AssetResObj tmpRes = loadObj[bundleName];


                    tmpRes.AddResObj(tmpInfo.resName, tmpAssetObj);


                }
                else
                {

                    AssetResObj tmpRes = new AssetResObj(tmpInfo.resName, tmpAssetObj);
                    loadObj.Add(bundleName, tmpRes);
                }


                tmpInfo.ReleaseObj();

            }


        }



        /// <summary>
        /// Gets the single resource.
        /// </summary>
        /// <returns>The single resource.</returns>
        /// <param name="bundleName">Bundle name. 相对路径</param>
        /// <param name="resName">Res name.</param>

        public Object GetSingleResource(string bundleName, string  resName)
		{
            ///缓存中已经加载了Object
			if (loadObj.ContainsKey(bundleName))
			{

                //Debug.Log("get  from memory =="+ resName);
				AssetResObj tmpRes = loadObj[bundleName];

				List<Object> tmpObj = tmpRes.GetResObj(resName);
				if (tmpObj != null)
				{
					return tmpObj[0];
				}




			} // 没有找到 继续向下

			if (loadHelper.ContainsKey(bundleName))

			{

				//  Debug.Log("single  load manager =="+ bundleName);

				IABRelationManager loader = loadHelper[bundleName];

				Object tmpObj = loader.GetSingleResource(resName);
				AssetObj tmpAssetObj = new AssetObj(tmpObj);

               //是否已经包含bundle　
				if (loadObj.ContainsKey(bundleName))
				{
					AssetResObj tmpRes = loadObj[bundleName];


					tmpRes.AddResObj( resName, tmpAssetObj);


				}
				else
				{

					AssetResObj tmpRes = new AssetResObj(resName, tmpAssetObj);
					loadObj.Add(bundleName, tmpRes);
				}







				return tmpObj;
			}
			else
			{
				return null;
			}


		}




		public Object[] GetMutiResources(string bundleName, string resName)
		{

			if (loadObj.ContainsKey(bundleName))
			{
				AssetResObj tmpRes = loadObj[bundleName];

				List<Object> tmpObj = tmpRes.GetResObj(resName);
				if (tmpObj != null)
				{
					return tmpObj.ToArray();
				}



			}





			if (loadHelper.ContainsKey(bundleName))
			{

				IABRelationManager loader = loadHelper[bundleName];

				Object[] tmpObjs = loader.GetAllResources(resName);
				AssetObj tmpAssetObj = new AssetObj(tmpObjs);


				if (loadObj.ContainsKey(bundleName))
				{
					AssetResObj tmpRes = loadObj[bundleName];


					tmpRes.AddResObj(resName, tmpAssetObj);


				}
				else
				{


					AssetResObj tmpRes = new AssetResObj(resName, tmpAssetObj);
					loadObj.Add(bundleName, tmpRes);

				}







				return tmpObjs;
			}
			else
			{
				return null;
			}


		}


        /// <summary>
        /// 删除 assetbundle.load　出来没用的Object
        /// </summary>
        /// <param name="tmpObj"></param>
        public void DisposeResObj(string bundleName, string resName)
        {
            if (loadObj.ContainsKey(bundleName))
            {
                AssetResObj tmpObj = loadObj[bundleName];

                tmpObj.ReleaseResObj(resName);


            }
        }

        /// <summary>
        /// 释放 Gameobject 类的
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="resName"></param>
        public void DisposeResGameObject(string bundleName, string resName)
        {
            if (loadObj.ContainsKey(bundleName))
            {
                AssetResObj tmpObj = loadObj[bundleName];

                tmpObj.ReleaseGameObject(resName);


            }
        }


		//删除包 加载的  assetbundle.load 出来的内存
		public void DisposeResObj(string bundleName)
		{
			if (loadObj.ContainsKey(bundleName))
			{
				AssetResObj tmpObj = loadObj[bundleName];

				tmpObj.ReleaseAllResObj();


			}

			Resources.UnloadUnusedAssets();
		}



        public void DisposeAllObjs()
        {

            List<string> keys = new List<string>();

            keys.AddRange(loadObj.Keys);

            for (int i = 0; i < loadObj.Count; i++)
            {


                //删除Object
                DisposeResObj(keys[i]);


            }

            loadObj.Clear();
        }


        /// <summary>
        ///   删除 bundle　镜像
        /// </summary>
		public void DisposeAllBundle()
		{



			List<string> keys = new List<string>();
            keys.AddRange(loadHelper.Keys);

			for (int i = 0; i < loadHelper.Count; i++)
			{
				IABRelationManager loader = loadHelper[keys[i]];

				loader.Dispose();

                //删除Object
				//DisposeResObj(keys[i]);


			}

			loadHelper.Clear();

		}


        /// <summary>
        ///   删除 bundle　镜像
        /// </summary>
        public void DisposeAllBundleAndReses()
        {



            DisposeAllObjs();

            List<string> keys = new List<string>();

            keys.AddRange(loadHelper.Keys);


            for (int i = 0; i < loadHelper.Count; i++)
            {
                IABRelationManager loader = loadHelper[keys[i]];

                loader.Dispose();

                loader = null;
                //删除Object
                // DisposeResObj(keys[i]);


            }

            loadHelper.Clear();

        }




        public void DisposeBundleAndRes(string bundleName)
        {
            if (loadHelper.ContainsKey(bundleName))
            {

                IABRelationManager loader = loadHelper[bundleName];

                List<string> dependences = loader.GetDependences();

                if (dependences != null)
                {
                    //删除该包 所依赖的包的关系
                    for (int i = 0; i < dependences.Count; i++)
                    {
                        if (loadHelper.ContainsKey(dependences[i]))
                        {

                            loader.RemoveDependence(bundleName);
                            IABRelationManager denpence = loadHelper[dependences[i]];

                            if (denpence.RemoveReferrence(bundleName))
                            {
                                //没有依赖关系了　就删除了
                                DisposeBundle(denpence.GetBundleName());



                            }

                        }

                    }
                }

                if (loader.GetReferrence() != null)
                {
                    //删除该包
                    if (loader.GetReferrence().Count <= 0)
                    {


                        loader.Dispose();
                        loadHelper.Remove(bundleName);

                        loader = null;

                        //删除包 加载的  assetbundle.load 出来的内存
                        DisposeResObj(bundleName);
                    }
                }



            }
        }

        // remove  single bundle
        public void DisposeBundle(string bundleName)
		{

			if (loadHelper.ContainsKey(bundleName))
			{

				IABRelationManager loader = loadHelper[bundleName];

				List<string>  dependences = loader.GetDependences();

                if (dependences != null)
                {
                    //删除该包 所依赖的包的关系
                    for (int i = 0; i < dependences.Count; i++)
                    {
                        if (loadHelper.ContainsKey(dependences[i]))
                        {
                            loader.RemoveDependence(bundleName);

                            IABRelationManager denpence = loadHelper[dependences[i]];

                            if (denpence.RemoveReferrence(bundleName))
                            {
                                //没有依赖关系了　就删除了
                                string tmpName = denpence.GetBundleName();
                          
                                   
                                 DisposeBundle(tmpName);
                               
                                    
                   

                            }

                        }

                    }
                }

                if (loader.GetReferrence() != null)
                {
                    //删除该包
                    if (loader.GetReferrence().Count <= 0)
                    {


                        loader.Dispose();
                        loadHelper.Remove(bundleName);

                        loader = null;


                        
                        //删除包 加载的  assetbundle.load 出来的内存
                        //	DisposeResObj(bundleName);
                    }
                }



			}

		}







    }

}

