using UnityEngine;
using System.Collections;

using U3DEventFrame;
using LuaInterface;

using System;

//上层接收消息
public class AssetResponseMsg : MsgBase, IDisposable
{
    private BundleRequest response;

    public AssetResponseMsg(BundleRequest tmpRequeset)
    {

        this.msgId = tmpRequeset.BackId;
        this.response = tmpRequeset;

    }

    public string[] GetBundleName()
    {

        return response.requesetInfo.bundles;

    }

    public void DebugBundle()
    {

        response.DebugBundle();
    }

    public UnityEngine.Object[] GetBundleResByIndex(int index,int resIndex)
    {

        return response.GetBundleObjs(index, resIndex);
    }

    public UnityEngine.Object[] GetBundleRes(string bundleName, string resName)
    {
        return response.GetBundleObjs(bundleName, resName);
    }


    public void Dispose()
    {
        response.Dispose();

        response = null;
    }


}

//上层发送消息

public class AssetRequesetMsg : MsgBase
{



    public AssetRequesetInfo requesetInfo;


     [NoToLua]
    public int GetVarayArrayLenth(int[] col, int index)
    {
        int tmpInt = 0;

        for (int i = 0; i < index; i++)
        {
            tmpInt += col[i];
        }

        return tmpInt;
    }
     [NoToLua]
    public T[][] ChangeTwoArray<T>(T[] tmpArray,int row , int[] col)
    {
        T[][] resName = new T[row][];




        int tmpIndex = 0;
        for (int i = 0; i < row; i++)
        {

            tmpIndex = GetVarayArrayLenth(col, i);


            resName[i] = new T[col[i]];
            for (int j = 0; j < col[i]; j++)
            {
              // Debug.Log("j ===" + (tmpIndex + j));
                resName[i][j] = tmpArray[tmpIndex + j];
            }
        }


            return resName;
    }


     [NoToLua]
     public AssetRequesetMsg()
     {
 
     }


    //for  lua
     public AssetRequesetMsg(ushort msgid, ushort backid, string scence, string[] bundle, string[] res, bool[] resSingle, int[] col)
    {


        //bool[][] resSingles = new bool[3][];

        //resSingles[0] = new bool[] { true, true };
        //resSingles[1] = new bool[] { true, true };

        //resSingles[2] = new bool[] { false };


        //Debug.Log("msgid==" + msgid);

       // Debug.Log("msgid=222=" + (int)AssetEvent.HunkMutiRes );
        this.msgId = msgid;
        
      string[][] resName = ChangeTwoArray<string>(res,bundle.Length ,col);


       bool[][] resBool = ChangeTwoArray<bool>(resSingle,bundle.Length,col);
      



      requesetInfo = new AssetRequesetInfo(backid, scence, bundle, resName, resBool);
         

    }


    [NoToLua]
    public AssetRequesetMsg(ushort msgid, ushort backid, string scence, string[] bundle, string[][] res, bool[][] resSingles)
    {
        this.msgId = msgid;

        requesetInfo = new AssetRequesetInfo(backid, scence, bundle, res, resSingles);
    }


    public void ChangeEventMsg(ushort msgid, ushort backid, string scence, string[] bundle, string[][] res, bool[][] resSingles)
    {
        this.msgId = msgid;

        requesetInfo = new AssetRequesetInfo(backid, scence, bundle, res, resSingles);

    }



}
