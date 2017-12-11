using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using U3DEventFrame;


public class CheckAssetData  {



	static bool dataInitOver = false;
	public  float process = 0;

    public delegate void LoadFinish();


    LoadFinish checkLoad;

    float  timeCount;

    public CheckAssetData(LoadFinish   loadFinish)
    {

        checkLoad = loadFinish;
    }


	bool   willCopyAsset= false ;
    /// <summary>
    ///  
    /// </summary>
    /// <returns></returns>
    public bool CheckIsCopyFile()
    {







        if (!File.Exists(IPathTools.GetAssetPersistentPath() + "/" + GloableConfig.LuaFileListName))
        {
             Debuger.Log("  not exist  111 ===");
            return true;
        }
        
        else 
        {


           // return false;



//            if (!File.Exists(srcFile))
//            {
//                Debug.Log("srcFile 333 file.txt ==="+srcFile);
//                return true;
//            }
//
//                FileStream fs = new FileStream(srcFile, FileMode.Open);
//            StreamReader sw = new StreamReader(fs);
//
//            string  srcArr = sw.ReadLine() ;
//
//            sw.Close();
//
//            fs.Close();
//
//            fs = new FileStream(disFile, FileMode.Open);
//            sw = new StreamReader(fs);
//
//            string disArr = sw.ReadLine();
//
//            Debuger.Log("srcArr ==="+ srcArr);
//            Debuger.Log("disArr ===" + disArr);
//
//            sw.Close();
//
//            fs.Close();
//
//
//            if (disArr != srcArr)
//            {
//                return true;
//            }
    
             



        }

        return false;
     

    }


	public IEnumerator LoadText()
	{

		string srcFile = IPathTools.GetAssetStreamPath()+"/" + GloableConfig.LuaFileListName;
		string disFile = IPathTools.GetAssetPersistentPath() + "/" + GloableConfig.LuaFileListName;

		Debuger.Log("srcFile 111 ==="+ srcFile);

		Debuger.Log("disFile 222 ===" + disFile);

		WWW www = new WWW(srcFile);
		yield return www;

		if (www.isDone)
		{

			string  tmpStr =www.text;

			int  tmpInt= tmpStr.IndexOf ("\n");


			string srcArr = tmpStr.Substring (0,tmpInt);


			FileStream	fs = new FileStream(disFile, FileMode.Open);
			StreamReader sw = new StreamReader(fs);

			string disArr = sw.ReadLine();

			Debuger.Log("srcArr ==="+ srcArr);
			Debuger.Log("disArr ===" + disArr);

			sw.Close();

			fs.Close();


			if (disArr != srcArr)
			{

				willCopyAsset = true;

			}




		}
	}

    //public IEnumerator OnExtractResource()
    //{
    //    timeCount = Time.realtimeSinceStartup;


    //    string dataPath = IPathTools.GetAssetPersistentPath() + "/";  //数据目录
    //    string resPath = IPathTools.GetWWWStreamPath() + "StreamingAssets.zip";
    //    WWW tmp = new WWW(resPath);


    //    yield return tmp;

    //    MemoryStream memStream = new MemoryStream(tmp.bytes.Length);

    //    FileStream output = new FileStream(dataPath, FileMode.Create);

    //    SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();

    //    DataOver();

    //}


    public IEnumerator OnExtractResource()
    {


		if (!File.Exists (IPathTools.GetAssetPersistentPath () + "/" + GloableConfig.LuaFileListName)) {
		
		
			willCopyAsset = true;
		} else {
		
			willCopyAsset = false;
			 
			//yield return LoadText ();
		}



		if (willCopyAsset)
		{
		
			timeCount = Time.realtimeSinceStartup;


			string zipPath = Application.temporaryCachePath + "/tempZip.zip";
			string dataPath = IPathTools.GetAssetPersistentPath() + "/";  //数据目录
			string resPath = IPathTools.GetWWWStreamPath() + "StreamingAssets.zip"; //游戏包资源目录
			Debuger.Log("dataPath  ==" + dataPath);
			Debuger.Log("resPath  ==" + resPath);

			WWW www = new WWW(resPath);
			yield return www;

			var data = www.bytes;
			File.WriteAllBytes(zipPath, data);

			try
			{
				ZipUtil.Unzip(zipPath, dataPath);
			}
			catch (System.Exception e)
			{
				Debuger.Log(" e ===" + e);
			}




			yield return null;

		
		}


        Debuger.Log("Finish  copy zip ==" );
        DataOver();

    }


    void DataOver()
	{
        process = 1;
        dataInitOver = true;

        float  tmpTime  = Time.realtimeSinceStartup - timeCount;

        Debuger.Log("zip  const time =="+ tmpTime);
        checkLoad();

    }
}
