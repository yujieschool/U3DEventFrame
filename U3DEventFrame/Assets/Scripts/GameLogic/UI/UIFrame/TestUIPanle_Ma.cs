using UnityEngine;
using System.Collections;

using U3DEventFrame;
using System;

public class TestUIPanle_Ma : UIBases
{
    public override void ProcessEvent(MsgBase msg)
    {
        switch (msg.msgId)
        {
            case (ushort)UIPanel_Ma.Initial:

                {
                    panle_Ma_Modle.ProcessButtonClick();
                }
                break;


            case (ushort)UIPanel_Ma.Loading:
                {
                    panle_Ma_Modle.ProcessButtonClick();
                }
                break;


            case (ushort)UIPanel_Ma.Regist:
                {
                    panle_Ma_Modle.ProcessButtonClick();
                }
                break;

            default:
                break;
        }




    }


    void Awake()
    {

        msgIds = new ushort[] {

            (ushort)UIPanel_Ma.Initial,

             (ushort)UIPanel_Ma.Loading,

             (ushort)UIPanel_Ma.Regist
        };

        RegistSelf(this, msgIds);
    }



    UIPanle_Ma_Modle panle_Ma_Modle;


    
    void  Initial()
    {

        AddComponentToChild();



       GameObject  tmpButton= GetGameObject("Button");
       panle_Ma_Modle = new UIPanle_Ma_Modle(tmpButton);

        AddButtonLisenter("Button", panle_Ma_Modle.ProcessButtonClick);

    }

    void Start()
    {

        Initial();
    }




}
