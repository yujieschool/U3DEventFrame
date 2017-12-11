using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using  U3DEventFrame ;


public class JoyStick :  CharactorBase  {



    public override void ProcessEvent(MsgBase msg)
    {
        //throw new System.NotImplementedException();
    }


    MsgOneParam<MovingJoystick> joyMsg;
   
	// Use this for initialization
	void Start () {


        joyMsg = new MsgOneParam<MovingJoystick>((ushort)PlayerEvent.JoyStickCtr);


        EasyJoystick.On_JoystickMoveStart += EasyJoystick_On_JoystickMoveStart;

        EasyJoystick.On_JoystickMove += EasyJoystick_On_JoystickMove;

        EasyJoystick.On_JoystickMoveEnd += EasyJoystick_On_JoystickMoveEnd;



        EasyButton.On_ButtonPress += EasyButton_On_ButtonPress;

        
		
	}

    void EasyButton_On_ButtonPress(string buttonName)
    {
        //throw new System.NotImplementedException();

        Debug.Log("Button  press");
        joyMsg.msgId = (ushort)PlayerEvent.NormalAttack;
    
        SendMsg(joyMsg);
    }

    void EasyJoystick_On_JoystickMoveEnd(MovingJoystick move)
    {
        //throw new System.NotImplementedException();



        joyMsg.msgId = (ushort)PlayerEvent.StopRun;
        joyMsg.Data = move;

        SendMsg(joyMsg);


    }

    void EasyJoystick_On_JoystickMove(MovingJoystick move)
    {


       
     //   Debug.Log("Moving  coming");
        //throw new System.NotImplementedException();
        joyMsg.msgId = (ushort)PlayerEvent.JoyStickCtr;
        joyMsg.Data = move;

        SendMsg(joyMsg);
    }

    void EasyJoystick_On_JoystickMoveStart(MovingJoystick move)
    {

      //  Debug.Log("Moving  coming");

      //  joyMsg.Data = move;

     //   SendMsg(joyMsg);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
