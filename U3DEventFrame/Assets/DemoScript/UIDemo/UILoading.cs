using UnityEngine;
using System.Collections;

using U3DEventFrame;
using System;


using  UnityEngine.EventSystems;




public class LoadingModle

{

    public string useNmae;

    public string passWorld;

 

}



public class UILoading : UIBases
{
    public override void ProcessEvent(MsgBase msg)
    {
        switch (msg.msgId)
        {
            case (ushort)UIDemoLoading.GetResources:
                {

                    AssetResponseMsg tmpMsg = (AssetResponseMsg)msg;

                      //Resources.load
                     UnityEngine.Object[]  tmpObjs=   tmpMsg.GetBundleRes("Loading", "Loading.prefab");


                     GameObject  tmpGameObj =  InitialPanle(tmpObjs[0]);


                     loadingCtr.LoadingObj = tmpGameObj;

                     Initial(tmpGameObj.transform);

                     InitialLogic();

                  
                    

                     UnityEngine.Object[] tmpRejest = tmpMsg.GetBundleRes("Regist", "Regist.prefab");

                     loadingCtr.SettingObj(tmpRejest[0]);


 
                }
                break;

        case (ushort)UIDemoLoading.Initial:
                {
 
                }
                break;


        case (ushort)UIDemoLoading.ShowPanel:
                {


                    Debug.Log("Show panel  recv");
                    loadingCtr.ShowPanle();
                }
                break;

            default:
                break;
        }




    }


    public void InitialLogic()
    {
        AddButtonLisenter("Registing",loadingCtr.RegistBtnPress);


        

    }


    UILoadingCtrl loadingCtr;



    LoadingModle modleClass;


    void Awake()
    {

        msgIds = new ushort[] {

             (ushort)UIDemoLoading.GetResources ,

               (ushort)UIDemoLoading.Initial ,

                 (ushort)UIDemoLoading.ShowPanel

        };

        RegistSelf(this, msgIds);

        //C 层 
        loadingCtr = new UILoadingCtrl( this);

        // M 层 
        modleClass = new LoadingModle();


        GetResoures();



    }


    private void ReleaseBundle()
    {
        //HunkAssetRes tmpMsg = ObjectPoolManager<HunkAssetRes>.Instance.GetFreeObject();

        //tmpMsg.ChangeReleaseBundleMsg((ushort)AssetEvent.ReleaseBundleAndObject, "scenceName", "bundleName");


        //SendMsg(tmpMsg);

        //ObjectPoolManager<HunkAssetRes>.Instance.ReleaseObject(tmpMsg);

    }


    ////父类10010

    //virtual void TmpFunc()
    //{


    //    MyStruct tmpStruct;

    //    tmpStruct.tmpInt = 10;


    //    MyStruct tmpTwo = tmpStruct;




    //    tmpTwo.tmpInt = 20;

    //    tmpStruct.tmpInt = 30;


    //   //  tmpTwo.tmpInt?
    ////  


    //    //  tmpTwo   tmpStruct ?


    //   //   tmpStruct.tmpInt  
    //}

    ////子类 10020
    //override void TmpFunc()
    //{ }


    //public struct MyStruct
    //{

    //   public int tmpInt = 5;
    //}



    

    private void GetResoures()
    {


        //        --- 申请多个bundle 里面多个资源

        //-- bundle 对应的名字   以下是二个bundle 包

        string[] bundle = {
                               "Loading","Regist"
                           };



        string[][] resName = new string[2][];

        //第一bundle 包里的资源名字
        resName[0] = new string[1] { "Loading.prefab" };
        //第二bundle 包里的
        // ------- -----------------------------这里面要加后缀 .prefab   .png----------TestTwo多个情况不用加----------
        resName[1] = new string[1] { "Regist.prefab" };



        bool[][] singles = new bool[2][];

        //第一bundle 包里的资源 是单个资源还是多个资源true 表示单个
        singles[0] = new bool[1] { true };
        //第二bundle 包里的资源 是单个资源还是多个资源true 表示单个
        singles[1] = new bool[1] { true };

        AssetRequesetMsg tmpMsg = ObjectPoolManager<AssetRequesetMsg>.Instance.GetFreeObject();

        tmpMsg.ChangeEventMsg((ushort)AssetEvent.HunkMutiRes, (ushort)UIDemoLoading.GetResources, "Demo", bundle, resName, singles);


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

}
