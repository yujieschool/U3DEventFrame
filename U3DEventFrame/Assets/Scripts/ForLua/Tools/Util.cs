using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Util {

	private static List<string> luaPaths = new List<string>();



	/// <summary>
	/// 获取Lua路径
	/// </summary>
	/// <param name="fileName"></param>
	/// <returns></returns>
	public static string SearchLuaPath(string fileName) {
		string filePath = fileName;
		for (int i = 0; i < luaPaths.Count; i++) {
			filePath = luaPaths[i] + fileName;
			if (File.Exists(filePath)) {
				return filePath;
			}
		}
		return filePath;
	}
	
	/// <summary>
	/// 添加的Lua路径
	/// </summary>
	/// <param name="path"></param>
	public static void AddLuaPath(string path) {
		if (!luaPaths.Contains(path)) {
			if (!path.EndsWith("/")) {
				path += "/";
			}
			luaPaths.Add(path);
		}
	}

    public static void Log(string str) {
        Debuger.Log(str);
    }

    public static void LogWarning(string str) {
        //Debuger.LogWarning(str);
    }

    public static void LogError(string str) {
        Debuger.LogError(str);
    }


  

    /// <summary>
    /// 取得数据存放目录
    /// </summary>
    public static string DataPath
    {
        get
        {
            string game = AppConst.AppName.ToLower();
            if (Application.isMobilePlatform)
            {
                return Application.persistentDataPath + "/" + game + "/";
            }
            if (AppConst.DebugMode)
            {
                return Application.dataPath + "/" + AppConst.AssetDir + "/";
            }
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                int i = Application.dataPath.LastIndexOf('/');
                return Application.dataPath.Substring(0, i + 1) + game + "/";
            }
            return "c:/" + game + "/";
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static void GetChildByTag(Transform root, string targetTag,ref Transform target)
    {
        for (int i = 0; i < root.childCount; i++)
        {
            if (root.GetChild(i).tag == targetTag)
            {
                target = root.GetChild(i);
                return;
            }
            else
            {
                GetChildByTag(root.GetChild(i),targetTag,ref target);
            }
        }
    }

    public static void GetChildByName(Transform root, string targetName, ref Transform target)
    {
        for (int i = 0; i < root.childCount; i++)
        {
            if (root.GetChild(i).name == targetName)
            {
                target = root.GetChild(i);
                return;
            }
            else
            {
                GetChildByName(root.GetChild(i), targetName, ref target);
            }
        }
    }
    /// <summary>
    /// 计算文件的MD5值
    /// </summary>
    public static string md5file(string file) {
		try {

           
			FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None);
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] retVal = md5.ComputeHash(fs);
			fs.Close();
			
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < retVal.Length; i++) {
				sb.Append(retVal[i].ToString("x2"));
			}
			return sb.ToString();
		} catch (Exception ex) {

            //Debug.Log("file ==" + file);

            FileStream fs = new FileStream(file, FileMode.Open,FileAccess.Read,FileShare.None);
            //Debug.Log("fs =="+ fs.Length);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);

            //Debug.Log("retVal ==" + retVal.Length);
            fs.Close();


			throw new Exception("md5file() fail, error:" + ex.Message);
		}
	}

}