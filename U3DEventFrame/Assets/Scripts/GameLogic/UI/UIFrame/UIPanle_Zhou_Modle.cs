using UnityEngine;

using System.Collections;

using U3DEventFrame;


//
//  NewBehaviourScript#FILEEXTENSION#
//  #PROJECTNAME#
//
//  Created by #SMARTDEVELOPERS# on #CREATIONDATE#.
//
//

public class UIPanle_Zhou_Modle  
{


    GameObject buttonObj;


    TestUIPanle_Zhou  zhouMono ;

    public UIPanle_Zhou_Modle(TestUIPanle_Zhou tmpMono, GameObject button)
    {


        zhouMono = tmpMono;

        buttonObj = button;

    }

    bool isActive = true;

    public void ProcessButtonClick()
    {

        if (isActive)
        {
            buttonObj.SetActive(false);

            isActive = false;



            MsgBase tmpBase = new MsgBase((ushort)UIPanel_Ma.Loading);
            zhouMono.SendMsg(tmpBase);
        }
        else
        {
            buttonObj.SetActive(true);

            isActive = true;

            MsgBase tmpBase = new MsgBase((ushort)UIPanel_Ma.Regist);
            zhouMono.SendMsg(tmpBase);
        }


         

       
       

    }

}
