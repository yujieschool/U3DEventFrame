
using System.Collections;
using UnityEngine;

using System;



namespace U3DEventFrame

{
/// <summary>
/// 资源加载类的接口
/// </summary>
public interface IResLoader
{
	/// <summary>
	/// 加载资源的入口函数
	/// </summary>
	/// <returns></returns>
	IEnumerator Load();

	/// <summary>
	/// 释放资源
	/// </summary>
	void Dispose();

	/// <summary>
	/// 返回资源加载进度
	/// </summary>
	float LoadingProgress
	{
		get;
	}
}

/// <summary>
/// 资源加载类中要使用的索引器父类
/// </summary>
public class ResIndexers : IDisposable
{
	/// <summary>
	/// 资源对象
	/// </summary>
	private AssetBundle ABRes;

	//private ResIndexers() : this(null)
	//{
	//}


		public void DebugAllRes()
		{
			string[] tmpAssetName= ABRes.GetAllAssetNames();

			for(int i =0 ; i < tmpAssetName.Length ; i++)
			{
				Debuger.Log("ABRes contain asset name =="+tmpAssetName[i]);
			}
		}

	/// <summary>
	/// 公开的构造方法，创建一个资源索引器
	/// </summary>
	/// <param name="abRes">资源对象</param>
	public ResIndexers(AssetBundle abRes)
	{
		ABRes = abRes;
	}



	/// <summary>
	/// 资源加载的方法
	/// </summary>
	/// <param name="resName"></param>
	/// <returns></returns>
	public UnityEngine.Object this[string resName]
	{
		get
		{

            //    Debug.Log("resName  =="+ resName);

            if ( this.ABRes == null || !this.ABRes.Contains( resName ) )
				{
					//Debuger.Log("ResIndexers =no res ="+resName);
					return null;
				}


            


            return ABRes.LoadAsset( resName );
            
		}
	}

    public IEnumerator  LoadResAsys(BundleInfo tmpNode)
    {

        if (this.ABRes != null)
        {

          

            if (tmpNode.isSingle)
            {
                AssetBundleRequest tmpRequest = ABRes.LoadAssetAsync(tmpNode.resName);
                yield return tmpRequest;

               // Debug.Log("tmpRequest resName== "+tmpNode.resName+"==="+tmpRequest.asset.name);


                //GameObject.Instantiate(tmpRequest.asset);
					tmpNode.AddReses(tmpRequest.asset)  ;


                    yield return null;
            }
            else
            {

                AssetBundleRequest tmpRequest = ABRes.LoadAssetWithSubAssetsAsync(tmpNode.resName) ;


                    yield return tmpRequest;

                    // Debug.Log("tmpRequest mutti resName== " + tmpNode.resName+"==="+tmpRequest.allAssets.Length);
                    tmpNode.AddReses(tmpRequest.allAssets);

                    yield return null;
            }



            
        }
        else
        {
            Debuger.Log("IResloader AB Not Cantain ==" + tmpNode.resName);
        }

    
    }



    /// <summary>
    /// 资源加载的方法
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object[] LoadResources(string  resName)
    {
      
            if (this.ABRes == null )
                return null;


          //  Debug.Log("LoadResources resName  ==" + resName);
            return ABRes.LoadAssetWithSubAssets(resName);

        
    }

        public void Dispose()
        {
            //生成的物体由全局类释放
			if(ABRes != null)
			{
				ABRes.Unload(false);
				
				ABRes = null;
				GC.SuppressFinalize(this);

				//Debug.LogError("disposed ");
			}
			else
			{

				Debug.Log("have disposed ==");
			}



            //   Resources.UnloadUnusedAssets();
        }



        /// <summary>
        /// 释放一个 assetbundle.load出来的　物体
        /// </summary>
        /// <param name="resObj"></param>
        public void UnloadRes(UnityEngine.Object resObj)
        {
            Resources.UnloadAsset(resObj);
        }


        /// <summary>
        /// 释放整个 assetbundle 
        /// </summary>
        /// <param name="isAll"> 是否将内存镜像　和　assetbundle.load出来的物体一起释放　一般设为fase</param>
        public void UnloadAssetBundle(bool isAll)
        {
            ABRes.Unload(isAll);
        }




    }


}