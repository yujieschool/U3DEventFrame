

/****
 * 
 *   proto buffer switcher
 * 
 *****/
//#define   USE_PROTOBUFFER



/****
 * 
 *    MsgCenter  queue  switcher
 * 
 *****/

//#define USE_MutiMSGQueue

using UnityEngine;
using System.Collections;

using System.IO;


namespace  U3DEventFrame
{
	public class IPathTools 
	{



		public const string kAssetBundlesOutputPath = "AssetBundles";

   


        public static string GetAppPath()
		{

			string tmpPath = "";
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			{
				tmpPath = Application.dataPath;


			}
			else
			{
				tmpPath = Application.persistentDataPath;
			}


			return tmpPath;
		}

        public static string GetLuaFileStreamPath()
        {
            string tmpPath  = Application.streamingAssetsPath; ;



            return tmpPath;
        }





        public static string GetSqlitePath()
        {
            string tmpStr = GetSqlHead();


            if (Application.platform == RuntimePlatform.WindowsEditor ||
           Application.platform == RuntimePlatform.OSXEditor)
            {
                tmpStr = tmpStr + GetAssetStreamPath() ;
            }
            else
            {
                tmpStr = tmpStr + GetAssetPersistentPath();
            }


            return tmpStr;

        }

        public static string GetSqlHead()
        {
            string tmpStr = "";

#if UNITY_ANDROID

            tmpStr = "URI=file:";

#elif UNITY_STANDALONE_WIN

             tmpStr = "Data Source = ";

#elif  UNITY_IOS

              tmpStr = "Data Source = ";
#else
            tmpStr = GetRuntimeFolderSQLHead();

#endif

            return tmpStr;
        }


        public static string GetRuntimeFolderSQLHead()
        {
            if (pathTargetPlatform == 1)
            {

                return "Data Source = ";
            }
            else if (pathTargetPlatform == 2)
            {

                return "URI=file:";
            }
            else
            {

                return "Data Source = ";
            }
        }





        #region  FilePath







        /// <summary>
        /// art/scences/
        /// </summary>    AssetBundle.LoadFromFile
        /// <param name="path">  </param>
        /// <returns></returns>

        public static string GetAssetBundleFilePath(string path)
		{


            string tmpPath = "/" + IPathTools.kAssetBundlesOutputPath + "/" + IPathTools.GetRuntimeFolder() + "/" + path;

            if (Application.platform == RuntimePlatform.WindowsEditor ||
           Application.platform == RuntimePlatform.OSXEditor)
            {
                tmpPath = GetAssetStreamPath() + tmpPath;
            }
            else
            {
                tmpPath = GetAssetPersistentPath() + tmpPath;

  


            }

                //  tmpPath = ITools.FixedPath(tmpPath);

               
            return tmpPath;



        }


        public static string GetBundleFileHead()
        {
            string tmpStr = "";


            return tmpStr;
        }

        public static string GetAssetStreamPath()
        {
            string tmpStr = GetBundleFileHead();

            tmpStr = tmpStr + Application.streamingAssetsPath;

            return tmpStr;
        }

        public static string GetFileStreamPath()
        {
          string  tmpPath = Application.dataPath + "/StreamingAssets/";

            return tmpPath;
        }


        public static string GetAssetPersistentPath()
        {

            string tmpStr = GetBundleFileHead();

            tmpStr = tmpStr + Application.persistentDataPath;

            return tmpStr;
        }

        #endregion




        #region   WWWPath


        public static string GetAssetBundleWWWStreamPath(string path)
        {
            string tmpPath =  IPathTools.kAssetBundlesOutputPath + "/" + IPathTools.GetRuntimeFolder() + "/" + path;

            tmpPath = GetWWWStreamPath() + tmpPath;

            return tmpPath;
        }


        /// <summary>
        ///    www load    www 
        /// </summary>
        /// <returns></returns>
        public static string GetWWWFileHead()
        {

           
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.OSXEditor)
            {

                string tmpPath = "";
                tmpPath  = "file://";

                return tmpPath;
            }
            else
            {
                string tmpStr = "";
#if UNITY_ANDROID
                tmpStr = "jar:file://";

#elif UNITY_STANDALONE_WIN
			tmpStr = "file:///"  ;
#elif UNITY_IOS
			tmpStr = "file://"  ;
#else

                tmpStr = GetRuntimeFolderWWWHead();

#endif

                return tmpStr;
            }

        }

        public static string GetRuntimeFolderWWWHead()
        {
            if (pathTargetPlatform == 1)
            {

                return "file:///";
            }
            else if (pathTargetPlatform == 2)
            {

                return "jar:file://";
            }
            else
            {

                return "file://";
            }
        }




        public static string GetWWWStreamPath()
        {


            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.OSXEditor)
            {

                string tmpPath = "";
                tmpPath = GetWWWFileHead()+ Application.streamingAssetsPath+"/";

                return tmpPath;
            }
            else
            {
                string tmpStr = GetWWWFileHead();
#if UNITY_ANDROID
                tmpStr = tmpStr + Application.dataPath + "!/assets/";

#elif UNITY_IOS
			tmpStr =tmpStr + Application.dataPath + "/Raw/";
#elif  UNITY_STANDALONE_WIN
  tmpStr = tmpStr + Application.dataPath + "/StreamingAssets/";
#else


                tmpStr = tmpStr + Application.dataPath + GetRuntimeFolderAsestTail();
#endif

                return tmpStr;
            }




        }






        public static string GetRuntimeFolderAsestTail()
        {
            if (pathTargetPlatform == 1)
            {

                return "/StreamingAssets/";
            }
            else if (pathTargetPlatform == 2)
            {

                return "!/assets/";
            }
            else
            {

                return "/Raw/";
            }
        }

        public static string GetWWWPersistentPath()
        {
            string tmpStr = GetWWWFileHead() +Application.persistentDataPath;

             
            return tmpStr;
        }




        #endregion




        //各个平台 路径 进行一下 修改 

        public static string FixedPath(string  path)
		{

			path = path.Replace("\\", "/");



			return path;
		}



        /// <summary>
        /// 1 windows   2  android   3  ios 
        /// </summary>
        public static int pathTargetPlatform = 1;

        //   


        public static string GetSubRuntimeFolder()
        {

            if (pathTargetPlatform == 1)
            {

                return "Windows";
            }
            else if (pathTargetPlatform == 2)
            {

                return "Android";
            }
            else
            {

                return "iOS";
            }



        }

        public static string GetRuntimeFolder()
        {

#if UNITY_ANDROID

            return "Android";

#endif

#if UNITY_IPHONE
            return "iOS";

#endif

#if UNITY_STANDALONE_WIN

            return "Windows";

#else
        return      GetSubRuntimeFolder();
#endif



        }







        public static string GetPlatformFolderForAssetBundles(RuntimePlatform  platform)
		{



            switch (platform)
			{
			case  RuntimePlatform.Android:
				return "Android";
			case RuntimePlatform.IPhonePlayer:
				return "iOS";
			case RuntimePlatform.WebGLPlayer:
			case RuntimePlatform.WindowsWebPlayer:
				return "WebPlayer";
			case RuntimePlatform.WindowsEditor:

              
			case RuntimePlatform.WindowsPlayer:

				return "Windows";
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.OSXWebPlayer:
				return "OSX";
				// Add more build targets for your own.
				// If you add more targets, don't forget to add the same platforms to GetPlatformFolderForAssetBundles(RuntimePlatform) function.
			default:
				return null;
			}
		}









	}


}



