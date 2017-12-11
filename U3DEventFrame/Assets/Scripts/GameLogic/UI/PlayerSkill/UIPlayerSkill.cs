using UnityEngine;
using System.Collections;

using U3DEventFrame;
using System;

using UnityEngine.EventSystems;


using UnityEngine.UI;


public class UIPlayerSkill : UIBases
{
    public override void ProcessEvent(MsgBase msg)
    {
        switch (msg.msgId)
        {
            case (ushort)UIPlayerEvent.ReduceBlood:
                {
 
                          MsgOneParam<float> tmpMsg = (MsgOneParam<float>)msg;

                         // Debug.Log("tmp=="+tmpMsg.Data);

                         // Debug.Log("bloodImage==" + bloodImage.name);
                          bloodImage.fillAmount = tmpMsg.Data / 100.0f;


                     
                    
                }
                break;

            default:
                break;
        }




    }


    void Awake()
    {

        base.Awake();
        msgIds = new ushort[] {
            (ushort)UIPlayerEvent.ReduceBlood
        };

        RegistSelf(this, msgIds);



    }



    FilledImagManager tmpManager;



    Image bloodImage;
    void Start()
    {

        AddComponentToChild();


        tmpManager = new FilledImagManager(this);


      tmpManager.skillOne= AddComponetToChild<FilledImage>("SkillOneChild");

      tmpManager.skillTwo=AddComponetToChild<FilledImage>("SkillTwoChild");


       tmpManager.skillThree= AddComponetToChild<FilledImage>("SkillThreeChild");


        AddButtonDownLisenter("SkillOne", tmpManager.BigAttact);
        AddButtonDownLisenter("SkillTwo", tmpManager.BigAttactTwo);
        AddButtonDownLisenter("SkillThree", tmpManager.BigAttactThree);




        bloodImage = GetUIComponent<Image>("Blood");
       
        

        
    }



}
