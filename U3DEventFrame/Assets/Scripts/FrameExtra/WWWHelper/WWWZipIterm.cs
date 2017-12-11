using UnityEngine;
using System.Collections;



using System.IO;

using U3DEventFrame;

public class WWWZipIterm : WWWItermBase
{


    private string outPutPath;

    public string OutPutPath
    {
        get
        {
            return outPutPath;
        }
        set
        {
            outPutPath = value;
        }
    }


    public override void FinishDownLoad(WWW tmpWWW)
    {

        string zipPath = Application.temporaryCachePath + "/tempZip.zip";

        if (OutPutPath == null)
        {

            OutPutPath = IPathTools.GetAssetPersistentPath() + "/";  //数据目录
        }


        var data = tmpWWW.bytes;
        File.WriteAllBytes(zipPath, data);

        try
        {
            ZipUtil.Unzip(zipPath, outPutPath);
        }
        catch (System.Exception e)
        {
            Debuger.Log(" e ===" + e);
        }
    }


    public override void DownLoadError(WWW tmpWWW,WWWItermBase  tmpBase)
    {
        //base.DownLoadError(tmpWWW);

        WWWHelper.Instance.AddWWWTask(tmpBase);
    }


}