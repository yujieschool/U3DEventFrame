using UnityEngine;
using System.Collections;

using U3DEventFrame;
using System;

using UnityEngine.SceneManagement;

public class TeacherLoading : UIBases
{
    public override void ProcessEvent(MsgBase msg)
    {
        switch (msg.msgId)
        {
            case (ushort)UIPlayerEvent.TeacherLoading:
                {


                    AssetResponseMsg respones = (AssetResponseMsg)msg;

                    //Resources.load 代替
                    UnityEngine.Object[] tmpObj = respones.GetBundleRes("Loading", "Loading.prefab");




                    GameObject   tmpGame=  InitialPanle(tmpObj[0]);
                    this.Initial(tmpGame.transform.parent);
                    InitialLogic();

            

                    //regist
                    UnityEngine.Object[] tmpRegist = respones.GetBundleRes("Registing", "Registing.prefab");




                    logicCtr.RegistPanle = tmpRegist[0];
                 //   UnityEngine.Object[] tmpLevel = respones.GetBundleRes("Scences", "Test.unity");



                   
                   
                   // logicCtr.RegistPanle = tmpRegist[0];
                 
 
                }
                break;

            default:
                break;
        }




    }


    public void InitialLogic()
    {

        
        AddButtonLisenter("Regist", logicCtr.RegistBtn);
    }




    void Awake()
    {

        msgIds = new ushort[] {
            (ushort)UIPlayerEvent.TeacherLoading,

            (ushort)UIPlayerEvent.ReduceBlood
        };

        RegistSelf(this, msgIds);


    }


    LoadingCtrl logicCtr;
    void Start()
    {
        logicCtr = new LoadingCtrl(this);
        GetResoures();
    }


    private void ReleaseBundle()
    {

        HunkAssetRes tmpMsg = ObjectPoolManager<HunkAssetRes>.Instance.GetFreeObject();

        tmpMsg.ChangeReleaseBundleMsg((ushort)AssetEvent.ReleaseBundleAndObject, "UIScence", "Loading");


        SendMsg(tmpMsg);

        ObjectPoolManager<HunkAssetRes>.Instance.ReleaseObject(tmpMsg);



    }

    private void GetResoures()
    {


        //        --- 申请多个bundle 里面多个资源

        //-- bundle 对应的名字   以下是二个bundle 包

        string[] bundle = {
                               "Loading","Registing"
                           };



        string[][] resName = new string[2][];

        //第一bundle 包里的资源名字
        resName[0] = new string[2] { "Loading.prefab", "Bg_Map02.png" };

        //第二个 bundle包里的资源名字
        resName[1] = new string[1] { "Registing.prefab" };

        bool[][] singles = new bool[2][];
    
        //第一bundle 包里的资源 是单个资源还是多个资源 true 表示单个
        singles[0] = new bool[2] { true,true };

        //第二个 bundle 包里的资源 是单个资源还是多个资源 true 表示单个
        singles[1] = new bool[1] { true };
  
        AssetRequesetMsg tmpMsg = ObjectPoolManager<AssetRequesetMsg>.Instance.GetFreeObject();

        tmpMsg.ChangeEventMsg((ushort)AssetEvent.HunkMutiRes, (ushort)UIPlayerEvent.TeacherLoading, "UIScence", bundle, resName, singles);


        SendMsg(tmpMsg);

        ObjectPoolManager<AssetRequesetMsg>.Instance.ReleaseObject(tmpMsg);
         
        





    }


    private void JumpNextView()
    {

      //  MsgBase  tmpMsg= ObjectPoolManager<MsgBase>.Instance.GetFreeObject();

     //   tmpMsg.ChangeEventId((ushort)PoleEvent.ReadData);

      //  SendMsg(tmpMsg);


       // ObjectPoolManager<MsgBase>.Instance.ReleaseObject(tmpMsg);

    }


    void Update()
    {
 
    }

}
