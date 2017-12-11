using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using U3DEventFrame;




public class FilledImagManager  
{


    public FilledImagManager( UIBases  tmpBase)
    {

        owerBase = tmpBase;
 

    }

    public UIBases owerBase;


    public FilledImage skillOne;

    public FilledImage skillTwo;

    public FilledImage skillThree;
    public void BigAttact(BaseEventData tmpData)
    {


        MsgBase tmpBase = ObjectPoolManager<MsgBase>.Instance.GetFreeObject();

        tmpBase.msgId = (ushort)PlayerEvent.BigAttack;


        owerBase.SendMsg(tmpBase);

        skillOne.BeginFilled();


    }

    public void BigAttactTwo(BaseEventData tmpData)
    {

     //   PlayerControl.Instance.Play("");


      //  AudioManager.instance.Playe();

        //npcManager.instacne.doSome();

       


        MsgBase tmpBase = ObjectPoolManager<MsgBase>.Instance.GetFreeObject();

        tmpBase.msgId = (ushort)PlayerEvent.BigAttack;



        owerBase.SendMsg(tmpBase);

        skillTwo.BeginFilled();


    }




    public void BigAttactThree(BaseEventData tmpData)
    {


        MsgBase tmpBase = ObjectPoolManager<MsgBase>.Instance.GetFreeObject();

        tmpBase.msgId = (ushort)PlayerEvent.BigAttack;



        owerBase.SendMsg(tmpBase);

        skillThree .BeginFilled();


    }
}
