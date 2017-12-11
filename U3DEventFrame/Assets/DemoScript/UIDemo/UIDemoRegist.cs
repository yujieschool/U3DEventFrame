using UnityEngine;
using System.Collections;

using U3DEventFrame;
using System;


public enum UIDemoEvent
{
 
      DemoMsg =  ManagerID.UIManager+1 ,
    

      MaxValue


}


public class UIDemoRegist : UIBases
{
    public override void ProcessEvent(MsgBase msg)
    {
        switch (msg.msgId)
        {
            case (ushort)UIDemoEvent.DemoMsg:
                {


                    Debug.Log("c#   recv Msg");


                }
                break;

            default:
                break;
        }




    }


    void Awake()
    {

        msgIds = new ushort[] { (ushort)UIDemoEvent.DemoMsg };
  
        RegistSelf(this, msgIds);


      //  InitialRegist();

    }


    public void DonePress()
    {

        Debug.Log("BtnPress");

        MsgBase tmpBase = ObjectPoolManager<MsgBase>.Instance.GetFreeObject();


        tmpBase.ChangeEventId((ushort)UIDemoLoading.ShowPanel);

        SendMsg(tmpBase);

        ObjectPoolManager<MsgBase>.Instance.ReleaseObject(tmpBase);



        gameObject.SetActive(false);
    }

    public void InitialRegist()
    {


        Initial(transform);

        AddButtonLisenter("Done", DonePress);
           
    }


    private void ReleaseBundle()
    {
        //HunkAssetRes tmpMsg = ObjectPoolManager<HunkAssetRes>.Instance.GetFreeObject();

        //tmpMsg.ChangeReleaseBundleMsg((ushort)AssetEvent.ReleaseBundleAndObject, "scenceName", "bundleName");


        //SendMsg(tmpMsg);

        //ObjectPoolManager<HunkAssetRes>.Instance.ReleaseObject(tmpMsg);

    }

    private void GetResoures()
    {


        //        --- 申请多个bundle 里面多个资源

        //-- bundle 对应的名字   以下是二个bundle 包

        string[] bundle = {
                               "TestOne","TestThree"
                           };



        string[][] resName = new string[2][];

        //第一bundle 包里的资源名字
        resName[0] = new string[2] { "TestOne.prefab", "TestTwo.prefab" };
        //第二bundle 包里的
        // ------- -----------------------------这里面要加后缀 .prefab   .png----------TestTwo多个情况不用加----------
        resName[1] = new string[2] { "chooseLvl", "TestThree.prefab" };



        bool[][] singles = new bool[2][];

        //第一bundle 包里的资源 是单个资源还是多个资源true 表示单个
        singles[0] = new bool[2] { true, true };
        //第二bundle 包里的资源 是单个资源还是多个资源true 表示单个
        singles[1] = new bool[2] { false, true };

      //   AssetRequesetMsg tmpMsg =ObjectPoolManager<AssetRequesetMsg>.Instance.GetFreeObject();

     //   tmpMsg.ChangeEventMsg((ushort)AssetEvent.HunkMutiRes, (ushort)BackId, "ScenceName", bundle, resName, singles);

    
      //  SendMsg(tmpMsg);

     //   ObjectPoolManager<AssetRequesetMsg>.Instance.ReleaseObject(tmpMsg);
         
        

    }


    private void JumpNextView()
    {

      //  MsgBase  tmpMsg= ObjectPoolManager<MsgBase>.Instance.GetFreeObject();

     //   tmpMsg.ChangeEventId((ushort)PoleEvent.ReadData);

      //  SendMsg(tmpMsg);


       // ObjectPoolManager<MsgBase>.Instance.ReleaseObject(tmpMsg);

    }

}
