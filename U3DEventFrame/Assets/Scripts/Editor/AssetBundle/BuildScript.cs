using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using U3DEventFrame ;


public class BuildScript
{





    static string PrecisionPlatform = "High";


    public static bool WriteFileBinary = true;


    public static void BuildAssetBundles()
	{
		// Choose the output path according to the build target.
		string outputPath = Path.Combine(IPathTools.kAssetBundlesOutputPath,  GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget) );


      //  outputPath=  Path.Combine(outputPath,PrecisionPlatform);
        if (!Directory.Exists(outputPath))
        {

            Debug.Log(outputPath);
            Directory.CreateDirectory(outputPath);
        }


        Debug.Log("outputPath ===" + outputPath);


        Debug.Log("active  ===" + EditorUserBuildSettings.activeBuildTarget);

     
        BuildPipeline.BuildAssetBundles(outputPath,BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget);


        string outputFolder = GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget);

        // Setup the source folder for assetbundles.
		var source = Path.Combine(Path.Combine(System.Environment.CurrentDirectory, IPathTools.kAssetBundlesOutputPath), outputFolder);
        if (!System.IO.Directory.Exists(source))
            Debug.Log("No assetBundle output folder, try to build the assetBundles first.");


		string distPath = Path.Combine(Application.streamingAssetsPath, IPathTools.kAssetBundlesOutputPath);
        distPath = Path.Combine(distPath, outputFolder);


		//distPath = IPathTools.FixedPath(distPath);

		IFileTools.CopyFolder(source, distPath);

        AssetDatabase.Refresh();

	}


    public static void ClearAssetBundle()
    {
		string outputPath = Path.Combine(Path.Combine(System.Environment.CurrentDirectory, IPathTools.kAssetBundlesOutputPath), GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget));
            


		outputPath = IPathTools.FixedPath(outputPath);

        Debug.Log(outputPath);
        if (Directory.Exists(outputPath))
        {

            FileUtil.DeleteFileOrDirectory(outputPath);
           // Directory.Delete(outputPath, true);

        }


		outputPath = Path.Combine(Application.streamingAssetsPath, IPathTools.kAssetBundlesOutputPath);

        outputPath= Path.Combine(outputPath, GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget));
		outputPath = IPathTools.FixedPath(outputPath);

        Debug.Log(outputPath);
        if (Directory.Exists(outputPath))
        {

            FileUtil.DeleteFileOrDirectory(outputPath);
      //      ITools.DeleteFolder(outputPath);

        }

        AssetDatabase.Refresh();
    }




    static string[] GetLevelsFromBuildSettings()
    {
        List<string> levels = new List<string>();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
        {
            if (EditorBuildSettings.scenes[i].enabled)
                levels.Add(EditorBuildSettings.scenes[i].path);
        }

        return levels.ToArray();
    }




	public static void BuildPlayer()
	{
		var outputPath = EditorUtility.SaveFolderPanel("Choose Location of the Built Game", "", "");
		if (outputPath.Length == 0)
			return;

		string[] levels = GetLevelsFromBuildSettings();
		if (levels.Length == 0)
		{
			Debug.Log("Nothing to build.");
			return;
		}

		string targetName = GetBuildTargetName(EditorUserBuildSettings.activeBuildTarget);
		if (targetName == null)
			return;

		// Build and copy AssetBundles.
		BuildScript.BuildAssetBundles();

		BuildOptions option = EditorUserBuildSettings.development ? BuildOptions.Development : BuildOptions.None;
		BuildPipeline.BuildPlayer(levels, outputPath + targetName, EditorUserBuildSettings.activeBuildTarget, option);
	}






    static void CopyAssetBundlesTo(string outputPath)
    {


        FileUtil.DeleteFileOrDirectory(Application.streamingAssetsPath);


		outputPath = IPathTools.FixedPath(outputPath);
        Directory.CreateDirectory(outputPath);




        string outputFolder = GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget);

        // Setup the source folder for assetbundles.
		var source = Path.Combine(Path.Combine(System.Environment.CurrentDirectory, IPathTools.kAssetBundlesOutputPath), outputFolder);
        if (!System.IO.Directory.Exists(source))
            Debug.Log("No assetBundle output folder, try to build the assetBundles first.");

        // Setup the destination folder for assetbundles.
        var destination = System.IO.Path.Combine(outputPath, outputFolder);

		destination = IPathTools.FixedPath(destination);
        if (System.IO.Directory.Exists(destination))
        {
         //   Directory.CreateDirectory(destination);
            FileUtil.DeleteFileOrDirectory(destination);
        }



        Directory.Move(source,destination);
  

     //   FileUtil.CopyFileOrDirectory(source, destination);
       // // Clear streaming assets folder.
       //FileUtil.DeleteFileOrDirectory(Application.streamingAssetsPath);
      

       // outputPath = ITools.FixedPath(outputPath);

       // Directory.CreateDirectory(outputPath);

       // string outputFolder = GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget);

       // // Setup the source folder for assetbundles.
       // var source = Path.Combine(Path.Combine(System.Environment.CurrentDirectory, ITools.kAssetBundlesOutputPath), outputFolder);
       // if (!System.IO.Directory.Exists(source))
       //     Debug.Log("No assetBundle output folder, try to build the assetBundles first.");

       // // Setup the destination folder for assetbundles.
       // var destination = System.IO.Path.Combine(outputPath, outputFolder);

       // if (System.IO.Directory.Exists(destination))
       //     FileUtil.DeleteFileOrDirectory(destination);

       // FileUtil.CopyFileOrDirectory(source, destination);
    }



    public static string GetBuildTargetName(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "/test.apk";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "/test.exe";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "/test.app";
            case BuildTarget.WebPlayer:
            case BuildTarget.WebPlayerStreamed:
                return "";
            // Add more build targets for your own.
            default:
                Debug.Log("Target not implemented.");
                return null;
        }
    }





    public static string GetPlatformFolderForAssetBundles(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "iOS";
            case BuildTarget.WebPlayer:
                return "WebPlayer";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
			return "Windows";
            // Add more build targets for your own.
            // If you add more targets, don't forget to add the same platforms to GetPlatformFolderForAssetBundles(RuntimePlatform) function.
            default:
                return null;
        }
    }






    public static void MarkAssetBundles()
    {

        string path = Application.streamingAssetsPath + "AssetBundles/Windows";

        Debug.Log(path);
    }



    #region   Record  AssetBundle



    public static void CopyRecorder()
    {

        //string highRecorder = Application.dataPath + "/Art/Scences/Highrecord.txt";

        //highRecorder = ITools.FixedPath(highRecorder);

        //string middleRecorder = Application.dataPath + "/Art/Scences/Middlerecord.txt";

        //middleRecorder = ITools.FixedPath(middleRecorder);
        //string lowRecorder = Application.dataPath + "/Art/Scences/Lowrecord.txt";
        //lowRecorder = ITools.FixedPath(lowRecorder);



        string path = Application.dataPath + "/Art/Scences/";

        DirectoryInfo dir = new DirectoryInfo(path);

        FileSystemInfo[] filesInfo = dir.GetFileSystemInfos();


		string dirPath = Path.Combine(Application.streamingAssetsPath, IPathTools.kAssetBundlesOutputPath);

        dirPath = Path.Combine(dirPath, GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget));


		dirPath = IPathTools.FixedPath(dirPath);

        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);


        for (int i = 0; i < filesInfo.Length; i++)
        {

            FileSystemInfo tmpFile = filesInfo[i];
            if (tmpFile is FileInfo)
            {
                if (tmpFile.Extension != ".meta")
                {
                    string filePath = dirPath + "/" + tmpFile.Name;

					filePath = IPathTools.FixedPath(filePath);

                    Debug.Log("copy file path==" + filePath);



                    File.Copy(tmpFile.FullName, filePath, true);
                }


              
            }
        }

        AssetDatabase.Refresh();


        //string dirPath = Application.streamingAssetsPath + "/" + ITools.kAssetBundlesOutputPath + "/" + ITools.GetPlatformFolderForAssetBundles(Application.platform) + "/" + "high/";

        //dirPath = ITools.FixedPath(dirPath);

        //if (!Directory.Exists(dirPath))
        //    Directory.CreateDirectory(dirPath);

        //dirPath += "Highrecord.txt";

        //File.Copy(highRecorder, dirPath, true);

        //dirPath = Application.streamingAssetsPath + "/" + ITools.kAssetBundlesOutputPath + "/" + ITools.GetPlatformFolderForAssetBundles(Application.platform) + "/" + "middle/";

        //dirPath = ITools.FixedPath(dirPath);

        //if (!Directory.Exists(dirPath))
        //    Directory.CreateDirectory(dirPath);

        //dirPath += "Middlerecord.txt";


        //File.Copy(middleRecorder, dirPath, true);


        //dirPath = Application.streamingAssetsPath + "/" + ITools.kAssetBundlesOutputPath + "/" + ITools.GetPlatformFolderForAssetBundles(Application.platform) + "/" + "low/";

        //dirPath = ITools.FixedPath(dirPath);


        //if (!Directory.Exists(dirPath))
        //    Directory.CreateDirectory(dirPath);
        //dirPath += "Lowrecord.txt";

        //File.Copy(lowRecorder, dirPath, true);





       


    }


    public static void  MarkAndRecordAssetBundle(string  fullPath)
    {

        
	

         

         string  textFileNmae = "Record.txt" ;
         string tmpPath = fullPath + textFileNmae;


         
        //Record.txt记录 assetbundle 相对路径　和包名
        FileStream fs = new FileStream(tmpPath, FileMode.OpenOrCreate);

     //   StreamWriter sw = new StreamWriter(fs);

        if(fullPath.Contains("High"))
        {

            PrecisionPlatform = "High"  ;
        }
        else if(fullPath.Contains("Middle"))
        {
             PrecisionPlatform = "Middle"  ;
        }
        else
        {

             PrecisionPlatform = "Low"  ;
        }


        Dictionary<string, string> readDict = new Dictionary<string, string>();

        ChangerHead(fullPath, readDict);



        if (BuildScript.WriteFileBinary)
        {
            //写入record.txt
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(readDict.Count);


            foreach (string key in readDict.Keys)
            {

                bw.Write(key);
                bw.Write(readDict[key]);
                // sw.WriteLine(key + "         " + readDict[key]);

            }




            bw.Flush();

            bw.Close();
        }
        else
        {
            //写入record.txt
            StreamWriter bw = new StreamWriter(fs);

            
            bw.WriteLine(readDict.Count);

          


            foreach (string key in readDict.Keys)
            {

                bw.Write(key);
                bw.Write("  ");
                bw.Write(readDict[key]);
                // sw.WriteLine(key + "         " + readDict[key]);

                bw.Write("\n");
            }




            bw.Flush();

            bw.Close();
        }


        fs.Close();


      //  Debug.Log("record file =="+ tmpPath);
        AssetDatabase.Refresh();

    }




    #endregion

    /// <summary>
    ///   full path  like    E:\\SVN\\ThirdMMO\\Assets\\Art\\Scences\\ScenceOne
    /// </summary>  
    /// <param name="fullPath"></param>
    public static void ChangerHead(string fullPath,Dictionary<string,string> theWriter)
    {





        /// replacePath  Assets\\Art\\Scences\\
      

        int tmpCount = fullPath.IndexOf("Assets");

        //int tmpCount2 = fullPath.IndexOf("Scences");

        //int tmpLength = tmpCount2 - tmpCount + "Scences".Length +1;

        int tmpLength = fullPath.Length;
        string replacePath = fullPath.Substring(tmpCount, tmpLength-tmpCount);

        //replacePath ==Assets/Art/Scences/
        //Debug.Log("replacePath 11 =="+ replacePath);
        
        DirectoryInfo dir = new DirectoryInfo(fullPath);

        //E:/SVN/3DFishTwo/Project/UluaFish/Assets/Art/Scences/ScencesOne
       // Debug.Log(fullPath);




  


        if (dir != null)
        {
            ListFiles(dir, replacePath, theWriter);
        }
        else
        {
            Debug.Log("this path  is not exit");
        }


        Debug.Log("mark finish");



    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="headPath">Assets/Art/Scences/ScencesOne</param>
    /// <returns></returns>
    public static string GetBundlePath(FileInfo file, string headPath)
    {



        string tmpPath = file.FullName;


        tmpPath = IPathTools.FixedPath(tmpPath);
       // Debug.Log("tmpPath 111=="+ tmpPath);

        headPath = IPathTools.FixedPath(headPath);
       // Debug.Log("headPath 111==" + headPath);

        int assetCount = tmpPath.IndexOf(headPath);


        //找到的
        assetCount += headPath.Length+1 ;
        int nameCount = tmpPath.LastIndexOf(file.Name);

        int tmpLenght = nameCount - assetCount ;



        int tmpCount = headPath.LastIndexOf("/");

        string  scencsHead = headPath.Substring(tmpCount + 1, headPath.Length - tmpCount - 1);

        //有路径的话 标记 全路径  
        if (tmpLenght > 0)
        {
            string subString = tmpPath.Substring(assetCount, tmpPath.Length - assetCount);

            string[] resulat = subString.Split("/".ToCharArray());

              return scencsHead+"/"+resulat[0];

       


        }
        else
        {


            return scencsHead;

        }






    }


    public static void ChangerAssetMark(FileInfo tmpFile ,string  markStr, Dictionary<string, string> theWriter)
    {


        string fullPath = tmpFile.FullName;



        int assetCount = fullPath.IndexOf("Assets");
        ///  
        string assetPath = fullPath.Substring(assetCount, fullPath.Length - assetCount);
        AssetImporter importer = AssetImporter.GetAtPath(assetPath);


        importer.assetBundleName = markStr;

       // Debug.Log("markStr  ===="+ markStr);

        if (PrecisionPlatform.Contains("High"))
        {
            importer.assetBundleVariant = "hd";
        }
        else if (PrecisionPlatform.Contains("Middle"))
        {
            importer.assetBundleVariant = "md";
        }
        else
        {
            importer.assetBundleVariant = "ld";
        }

        //对场景文件做标记
        if (tmpFile.Extension == ".unity")
        {
            importer.assetBundleVariant = "u3d";
        }




        string moduleName = "";

        string[] subMark = markStr.Split("/".ToCharArray());

        if (subMark.Length > 1)
        {
            moduleName = subMark[1];
        }
        else
        {
            moduleName = markStr;
        }

        string modlePath = markStr.ToLower() + "." + importer.assetBundleVariant;
        if (!theWriter.ContainsKey(moduleName))
        {
            theWriter.Add(moduleName, modlePath);

          //  Debug.Log("moduleName =="+ moduleName);

          //  Debug.Log("modlePath ==" + modlePath);
        }


     //   Debug.Log("assetPath ===="+ assetPath);
     //   AssetDatabase.ImportAsset(assetPath);
    }

    public static void ChangerMark(FileInfo tmpFile, string replacePath, Dictionary<string, string> theWriter)
    {




        if (tmpFile.Extension.ToLower() == ".meta" || tmpFile.Extension.ToLower() == ".ds_store")
        {

            Debug.Log(".meta file  is not  care==" + tmpFile.Name + "exten==" + tmpFile.Extension.ToLower());
            return;

        }

        if (tmpFile.Name.StartsWith("."))
            return;

        // Debug.Log("replacePath ==" + replacePath);
        string markStr = GetBundlePath(tmpFile, replacePath);


        //Debug.Log("markStr ==" + markStr);


        ChangerAssetMark(tmpFile,markStr,theWriter);


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info">//E:/SVN/3DFishTwo/Project/UluaFish/Assets/Art/Scences/ScencesOne</param>
    /// <param name="replacePath">Assets/Art/Scences/ScencesOne</param>
    /// <param name="theWriter"></param>
    public static void ListFiles(FileSystemInfo info, string replacePath ,Dictionary<string,string>  theWriter)
    {


        if (!info.Exists)
        {

            Debug.Log("is not exit");
            return;
        }

        DirectoryInfo dir = info as DirectoryInfo;
        if (dir == null)
        {

            Debug.Log("is not exit");
            return;

        }

        FileSystemInfo[] files = dir.GetFileSystemInfos();

        for (int i = 0; i < files.Length; i++)
        {

            EditorUtility.DisplayProgressBar("Mark", files[i].FullName, (float)i / files.Length);

            FileInfo file = files[i] as FileInfo;
            //是文件 
            if (file != null)
            {

                ChangerMark(file, replacePath,theWriter);

            }
            //对于子目录，进行递归调用
            else
            {
                ListFiles(files[i], replacePath, theWriter);
            }
        }


    } 

}
