using UnityEngine;
using UnityEditor;
using System.Collections;

using System.IO;

using System;



public class AssetbundlesMenuItems
{


    [MenuItem("ITools/AssetBundles/Copy StreamingPath To PersistentPath")]

    public static void CopyToPersistentPath()
    {

        string persistentPath = Application.persistentDataPath;

        string streamPath = Application.streamingAssetsPath ;
        Debug.Log("persistentPath==" + persistentPath);
        if (Directory.Exists(persistentPath))
        {

            FileUtil.CopyFileOrDirectory(streamPath, persistentPath);
            // Directory.Delete(outputPath, true);

        }
    }




    [MenuItem("ITools/AssetBundles/Clear PersistentPath")]

    public static void ClearPersistentPath()
    {

        string persistentPath = Application.persistentDataPath;


        Debug.Log("persistentPath=="+persistentPath);
        if (Directory.Exists(persistentPath))
        {

            FileUtil.DeleteFileOrDirectory(persistentPath);
            // Directory.Delete(outputPath, true);

        }
    }


    [MenuItem("ITools/AssetBundles/CopyConfigerToStreamAssets")]

    public static void ReadConfiger()
    {

   
    

        BuildScript.CopyRecorder();

    }

    const string kSimulateAssetBundlesMenu = "ITools/AssetBundles/WriteBinary";

    [MenuItem(kSimulateAssetBundlesMenu,false ,0)]
    public static void ToggleSimulateAssetBundle()
    {
        BuildScript.WriteFileBinary = !BuildScript.WriteFileBinary;


        Debug.Log("false " + BuildScript.WriteFileBinary);
    }

    [MenuItem(kSimulateAssetBundlesMenu, true,0)]
    public static bool ToggleSimulateAssetBundleValidate()
    {
     
        Menu.SetChecked(kSimulateAssetBundlesMenu, BuildScript.WriteFileBinary);

        Debug.Log("true "+ BuildScript.WriteFileBinary);
        return true;
    }


    [MenuItem("ITools/AssetBundles/MarkAsset", false, 100)]
    public static void MarkAssetBundle()
    {
        AssetDatabase.StartAssetEditing();
        AssetDatabase.RemoveUnusedAssetBundleNames();


        string path = Application.dataPath + "/Art/Scences/";

        DirectoryInfo dir = new DirectoryInfo(path);

        FileSystemInfo[] filesInfo = dir.GetFileSystemInfos();

        

        for (int i = 0; i < filesInfo.Length; i++)
        {
            EditorUtility.DisplayProgressBar("Mark", "Mark", (float)i/filesInfo.Length);

            FileSystemInfo tmpFile = filesInfo[i];
            if (tmpFile is DirectoryInfo)
            {
                string tmpPath = Path.Combine(path, tmpFile.Name);


                 //Debug.Log("tmpPath==="+tmpPath); // ScencesOne 具体到某一个场景文件夹
                BuildScript.MarkAndRecordAssetBundle(tmpPath);
            }
        }

        EditorUtility.DisplayProgressBar("Mark", "Mark", 1.0f);


        EditorUtility.ClearProgressBar();

        AssetDatabase.StopAssetEditing();

    }



    [MenuItem("ITools/AssetBundles/Build AssetBundles", false, 100)]
    public static void BuildAssetBundle()
    {
       


        BuildScript.BuildAssetBundles();

        BuildScript.CopyRecorder();
    }







  
    public static void BuildAssetBundles()
    {
      //  string path = EditorUtility.OpenFolderPanel("select path", "Assets/Art/Scences/", "");

        AssetDatabase.RemoveUnusedAssetBundleNames();


        string path =  Application.dataPath + "/Art/Scences/";

        DirectoryInfo dir = new DirectoryInfo(path);

        FileSystemInfo[] filesInfo = dir.GetFileSystemInfos();


        for (int i = 0; i < filesInfo.Length; i++)
        {

            FileSystemInfo tmpFile = filesInfo[i];
            if (tmpFile is DirectoryInfo)
            {
                string tmpPath = Path.Combine(path, tmpFile.Name);


               // Debug.Log(tmpPath);
                BuildScript.MarkAndRecordAssetBundle(tmpPath);
            }
        }


        //Debug.Log(path);
        //BuildScript.MarkAndRecordAssetBundle(path);


        //path = Application.dataPath + "/Art/Scences/Middle";

        //BuildScript.MarkAndRecordAssetBundle(path);


        //path = Application.dataPath + "/Art/Scences/Low";

        //BuildScript.MarkAndRecordAssetBundle(path);

       

        BuildScript.BuildAssetBundles();

        BuildScript.CopyRecorder();
    }


    //[MenuItem ("AssetBundles/Build AssetBundles",false,100)]
    //static public void BuildAssetBundles ()
    //{
    //    BuildScript.BuildAssetBundles();
    //}



    [MenuItem("ITools/AssetBundles/ClearAssetBundle", false, 100)]
    static void ClearAssetBundles()
    {

        BuildScript.ClearAssetBundle();
    }






    [MenuItem("ITools/AssetBundles/Build Player", false, 200)]
	static void BuildPlayer ()
	{
		BuildScript.BuildPlayer();
	}





}
