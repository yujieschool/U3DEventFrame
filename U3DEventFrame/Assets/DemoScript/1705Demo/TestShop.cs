using UnityEngine;
using System.Collections;

using U3DEventFrame;
using System;


public class MsgTransform : MsgBase

{
    public Transform myTransform;

    public MsgTransform()
    {
 
    }

    public MsgTransform( Transform  tmpTras , ushort msgid)
    {

        ChangMsg(tmpTras,msgid);

 
    }

    public void ChangMsg(Transform tmpTras, ushort msgid)
    {
        this.myTransform = tmpTras;

        this.msgId = msgid;
 
    }
}



public class TestShop : UIBases
{
    public override void ProcessEvent(MsgBase msg)
    {
        switch (msg.msgId)
        {
            case (ushort)TestBagEvents.Initial:
                {
                    Debug.Log("Shop  Initial  coming1!!");
                }
                break;

            case (ushort)TestShopEvents.GetResources:
                {


                    AssetResponseMsg tmpMsg = (AssetResponseMsg)msg;

                    //  Resource.load ()
                  UnityEngine.Object[]  objs=    tmpMsg.GetBundleRes("Loading", "Loading.prefab");


                   GameObject  loadObj= InitialPanle(objs[0]);


                   Initial(loadObj.transform);


                   AddButtonLisenter("Regist", ClickRegist);



                   objs = tmpMsg.GetBundleRes("Registing", "Registing.prefab");


                   GameObject registObj = InitialPanle(objs[0]);


                  // GameObject  loadObj = GameObject.Instantiate(objs[0]) as GameObject;



                  // Debug.Log(loadObj.name+"======================");
                    
                }
                break;

            default:
                break;
        }




    }


    public void ClickRegist()
    {

        Debug.Log("  click   ");

    }

    void Awake()
    {

        msgIds = new ushort[] {

            (ushort)TestShopEvents.Initial,
            (ushort)TestShopEvents.GetResources,
            (ushort)TestBagEvents.Initial

        };

        RegistSelf(this, msgIds);



    }


    void Start()
    {

        GetResoures();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           // MsgTransform tmpMsg = new MsgTransform(transform, (ushort)TestBagEvents.Initial);

          //  SendMsg(tmpMsg);

            MsgTransform  tmpMsg=    ObjectPoolManager<MsgTransform>.Instance.GetFreeObject();

            tmpMsg.ChangMsg(transform, (ushort)AllPlayerEvents.Initial);


            SendMsg(tmpMsg);

            ObjectPoolManager<MsgTransform>.Instance.ReleaseObject(tmpMsg);


        }
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
                               "Loading","Registing"
                           };



        string[][] resName = new string[2][];

        //第一bundle 包里的资源名字
        resName[0] = new string[1] { "Loading.prefab" };
        //第二bundle 包里的
        // ------- -----------------------------这里面要加后缀 .prefab   .png----------TestTwo多个情况不用加----------
        resName[1] = new string[1] { "Registing.prefab" };  // button_endless



        bool[][] singles = new bool[2][];

        //第一bundle 包里的资源 是单个资源还是多个资源true 表示单个
        singles[0] = new bool[1] { true };
        //第二bundle 包里的资源 是单个资源还是多个资源true 表示单个
        singles[1] = new bool[1] { true };



        AssetRequesetMsg tmpMsg = ObjectPoolManager<AssetRequesetMsg>.Instance.GetFreeObject();

        tmpMsg.ChangeEventMsg((ushort)AssetEvent.HunkMutiRes, (ushort)TestShopEvents.GetResources, "UIScence", bundle, resName, singles);


        Debug.Log(" back Id  ==" +(ushort)TestShopEvents.GetResources);
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
