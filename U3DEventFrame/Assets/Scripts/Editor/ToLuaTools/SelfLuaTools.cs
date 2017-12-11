using UnityEngine;
using System.Collections;

using UnityEditor;

using System.Diagnostics;
using System.IO;

using System.Collections.Generic;
using U3DEventFrame;




public class SelfLuaTools 
{



    [MenuItem("LuaBuilder/Build Lua File (win)", false, 10)]

    public static void ProcessLuaJIT()
    {

        string frontPath = Application.dataPath;

        int tmpPos = frontPath.IndexOf("Assets");

        string rootPath = frontPath.Substring(0,tmpPos);


       
        Process proc = Process.Start(rootPath + "Itools/BuildLua/BuildLuaWithJit.bat");
        proc.WaitForExit();

        AssetDatabase.Refresh();
    }
    [MenuItem("LuaBuilder/Build Lua File (Android )", false, 20)]

    public static void ProcessLuaJITAndroid()
    {

        string frontPath = Application.dataPath;

        int tmpPos = frontPath.IndexOf("Assets");

        string rootPath = frontPath.Substring(0, tmpPos);



        Process proc = Process.Start(rootPath + "Itools/BuildLua/BuildLuaWithJitAndroid.bat");
        proc.WaitForExit();

        AssetDatabase.Refresh();
    }

    [MenuItem("LuaBuilder/Build Lua File (Mac)", false, 50)]

    public static void ProcessLuaLuaC()
    {

        string frontPath = Application.dataPath;

        int tmpPos = frontPath.IndexOf("Assets");

        string rootPath = frontPath.Substring(0, tmpPos);



		//Process proc = Process.Start("#!/bin/bash" ,rootPath + "Itools/BuildLua/TestLuaWithLuaC.sh");

//		string tmpargment = Application.dataPath + "/TestLuaWithLuaC.sh";
	//	UnityEngine.Debug.Log ("begin== "+tmpargment);



		//RunCommand2 (tmpargment ," ");
		//RunCommand ("TestLuaWithLuaC.sh",Application.dataPath+"/");
		//Process.Start ("/bin/bash",tmpargment);
//     	RunCommand ("bash",  Application.dataPath + "/TestLuaWithLuaC.sh");
		//Process process = Process.Start( "/bin/bash",rootPath + "Itools/BuildLua/BuildMacLua.sh");
//

		RunshCommond (rootPath + "Itools/BuildLua/BuildMacLua.sh");

		UnityEngine.Debug.Log ("Finsh ");
        AssetDatabase.Refresh();
    }



	[MenuItem("LuaBuilder/Build Lua File (IOS Auto)", false, 100)]

	public static  void ProcessLuaAutoForIOS()
	{

		ProcessLuaIOS ();

		ProcessLuaIOS64 ();

		GenZipFile ();

		GenFileMd5 ();
	}


	[MenuItem("LuaBuilder/Build Lua File (IOS)", false, 120)]

	public static void ProcessLuaIOS()
	{

		string frontPath = Application.dataPath;

		int tmpPos = frontPath.IndexOf("Assets");

		string rootPath = frontPath.Substring(0, tmpPos);



		//Process proc = Process.Start("#!/bin/bash" ,rootPath + "Itools/BuildLua/TestLuaWithLuaC.sh");

		//		string tmpargment = Application.dataPath + "/TestLuaWithLuaC.sh";
		//	UnityEngine.Debug.Log ("begin== "+tmpargment);



		//RunCommand2 (tmpargment ," ");
		//RunCommand ("TestLuaWithLuaC.sh",Application.dataPath+"/");
		//Process.Start ("/bin/bash",tmpargment);
		//     	RunCommand ("bash",  Application.dataPath + "/TestLuaWithLuaC.sh");
		//Process process = Process.Start( "/bin/bash",rootPath + "Itools/BuildLua/BuildIOSLua.sh");
		//

		RunshCommond (rootPath + "Itools/BuildLua/BuildIOSLua.sh");
		UnityEngine.Debug.Log ("Finsh ");
		//AssetDatabase.Refresh();
	}




	[MenuItem("LuaBuilder/Build Lua File (IOS_64)", false, 150)]

	public static void ProcessLuaIOS64()
	{

		string frontPath = Application.dataPath;

		int tmpPos = frontPath.IndexOf("Assets");

		string rootPath = frontPath.Substring(0, tmpPos);



		//Process proc = Process.Start("#!/bin/bash" ,rootPath + "Itools/BuildLua/TestLuaWithLuaC.sh");

		//		string tmpargment = Application.dataPath + "/TestLuaWithLuaC.sh";
		//	UnityEngine.Debug.Log ("begin== "+tmpargment);



		//RunCommand2 (tmpargment ," ");
		//RunCommand ("TestLuaWithLuaC.sh",Application.dataPath+"/");
		//Process.Start ("/bin/bash",tmpargment);
		//     	RunCommand ("bash",  Application.dataPath + "/TestLuaWithLuaC.sh");
		//Process process = Process.Start( "/bin/bash",rootPath + "Itools/BuildLua/BuildIOSLua_64.sh");
		//

		RunshCommond (rootPath + "Itools/BuildLua/BuildIOSLua_64.sh");


		UnityEngine.Debug.Log ("Finsh ");
		//AssetDatabase.Refresh();
	}






    [MenuItem("LuaBuilder/Gen Lua ProtoBuffer File", false,200)]

    public static void GenLuaProtoBuffer()
    {

          string frontPath = Application.dataPath;

        int tmpPos = frontPath.IndexOf("Assets");

        string rootPath = frontPath.Substring(0, tmpPos);
        

		Process proc = Process.Start(rootPath + "Itools/BuildLua/GenProtoBuffer.bat");
        proc.WaitForExit();



     //   string buildPath = Application.dataPath + "/Scripts/Editor/ToLuaTools/GenProtoBuffer.py";

        //"@C:/Python27/python.exe"
    //    RunPythonCmd(buildPath);


        AssetDatabase.Refresh();
    }


    [MenuItem("LuaBuilder/Gen Zip File", false, 250)]




    public static void GenZipFile()
    {

		#if  UNITY_EDITOR_WIN

		GenWinZipFile();
		#else

		GenMacZipFile();
		#endif

    }

	public static  void GenWinZipFile()
	{
		string frontPath = Application.dataPath;

		int tmpPos = frontPath.IndexOf("Assets");

		string rootPath = frontPath.Substring(0, tmpPos);
		Process proc = Process.Start(rootPath + "Itools/Zip/BuildZip.bat");
		proc.WaitForExit();



		//   string buildPath = Application.dataPath + "/Scripts/Editor/ToLuaTools/GenProtoBuffer.py";

		//"@C:/Python27/python.exe"
		//    RunPythonCmd(buildPath);


		//AssetDatabase.Refresh();
	}

	public static  void GenMacZipFile()
	{
		string frontPath = Application.dataPath;

		int tmpPos = frontPath.IndexOf("Assets");

		string rootPath = frontPath.Substring(0, tmpPos);



		//Process proc = Process.Start("#!/bin/bash" ,rootPath + "Itools/BuildLua/TestLuaWithLuaC.sh");

		//		string tmpargment = Application.dataPath + "/TestLuaWithLuaC.sh";
		//	UnityEngine.Debug.Log ("begin== "+tmpargment);



		//RunCommand2 (tmpargment ," ");
		//RunCommand ("TestLuaWithLuaC.sh",Application.dataPath+"/");
		//Process.Start ("/bin/bash",tmpargment);
		//     	RunCommand ("bash",  Application.dataPath + "/TestLuaWithLuaC.sh");
		//Process process = Process.Start( "/bin/bash",rootPath + "Itools/Zip/BuildZip.sh");
		//

		RunshCommond (rootPath + "Itools/Zip/BuildZip.sh");

		UnityEngine.Debug.Log ("Finsh ");
		//AssetDatabase.Refresh();
	}


	public static  void RunshCommond(string commondFile)
	{
		Process process = Process.Start( "/bin/bash",commondFile);
		//
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardError = true;

		process.StartInfo.RedirectStandardOutput = true;

		process.StartInfo.RedirectStandardInput = true;

		process.StartInfo.StandardOutputEncoding = System.Text.UTF8Encoding.UTF8;

		process.StartInfo.StandardErrorEncoding = System.Text.UTF8Encoding.UTF8;
		//
		process.Start ();
		string tmpOut = process.StandardOutput.ReadToEnd ();
		//

		string tmpError = process.StandardError.ReadToEnd ();

		process.WaitForExit ();

		UnityEngine.Debug.Log (tmpOut);


		UnityEngine.Debug.Log (tmpError);
	}





    [MenuItem("LuaBuilder/GenFileMD5", false, 300)]

    public static void GenFileMd5()
    {

        CreatFileList();
    }


  static   List<string> paths = new List<string>();
  static   List<string> files = new List<string>();

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void Recursive(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs)
        {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }



    public static void CreatFileList()
    {

        string resPath = Application.streamingAssetsPath + "/";
        string newFilePath = resPath +  GloableConfig.LuaFileListName;
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        paths.Clear(); files.Clear();
        Recursive(resPath);

        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(GloableConfig.VersionName +"|" + files.Count);
        for (int i = 0; i < files.Count; i++)
        {
            string file = files[i];
            string ext = Path.GetExtension(file);
            if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

            string md5 = Util.md5file(file);
            string value = file.Replace(resPath, string.Empty);
            sw.WriteLine(value + "|" + md5);
        }
        sw.Close(); fs.Close();

        UnityEngine.Debug.Log("create  md5 file  finish");
		AssetDatabase.Refresh();
    }




	[MenuItem("LuaBuilder/Clear Lua Special Charactor", false, 350)]

	public static void ProcessLuaSpecialCharactor()
	{

		string frontPath = Application.dataPath;

		int tmpPos = frontPath.IndexOf("Assets");

		string rootPath = frontPath.Substring(0, tmpPos);



		RunshCommond (rootPath + "Itools/doc2unix/ByPython.sh");


		UnityEngine.Debug.Log ("Finsh ");
		AssetDatabase.Refresh();
	}









    /// <summary>
    /// 
    /// </summary>
    /// <param name="cmd"> python  </param>
    /// <param name="args"> python file </param>

    private static void RunPythonCmd(string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "python";
        start.Arguments = args;
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.WindowStyle = ProcessWindowStyle.Normal;
        start.UseShellExecute = false;
        
        start.ErrorDialog = true;

        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = "";
                while (result != null)
                {

                    result = reader.ReadLine();
                  

                    UnityEngine.Debug.Log(result);
                }

            }
        }
    }



}
