using UnityEngine;
using System.Collections;

using U3DEventFrame;
using System;

public class TestUIPanle_Zhou : UIBases
{
    public override void ProcessEvent(MsgBase msg)
    {
        switch (msg.msgId)
        {
            case (ushort)UIPanel_Zhou.Initial:
                break;

            case (ushort)UIPanel_Zhou.ChangeName:
                break;


            case (ushort)UIPanel_Zhou.ButtonClick:
                break;

            default:
                break;
        }




    }


    void Awake()
    {

        msgIds = new ushort[] {

            (ushort)UIPanel_Zhou.Initial,

             (ushort)UIPanel_Zhou.ChangeName,

              (ushort)UIPanel_Zhou.ButtonClick

        };

        RegistSelf(this, msgIds);
    }


    UIPanle_Zhou_Modle panel_Zhou_Modle;
    void Initial()
    {


        AddComponentToChild();

        GameObject tmpButton =  GetGameObject("Button");
        panel_Zhou_Modle = new UIPanle_Zhou_Modle(this,tmpButton);

         AddButtonLisenter("Button", panel_Zhou_Modle.ProcessButtonClick);
    }


    void Start()
    {

        Initial();
    }



    private void ReleaseBundle()
    {

       // ReleaseRes((ushort)AssetEvent.ReleaseBundleAndObject, "scenceName", "bundleName", "");
    }

    private void GetResoures()
    {
      //  this.GetRes(true, (ushort)AssetEvent.HunkRes, "scenceName", "bundleName", "resName", (ushort)PoleEvent.ReadData);


     //   this.GetMutiRes("scenceName", "bundleName", true, (ushort)PoleEvent.ReadData, "resName", "resName", "resName");
    }


    private void JumpNextView()
    {
      //  MsgBase tmpMsg = new MsgBase((ushort)PoleEvent.ReadData);
       // SendMsg(tmpMsg);

        //tmpMsg.ChangeEventId((ushort)PoleEvent.ReadData);

       // SendMsg(tmpMsg);


    }

}
