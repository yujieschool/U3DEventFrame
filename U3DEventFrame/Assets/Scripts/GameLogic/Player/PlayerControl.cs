using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using U3DEventFrame;



public  delegate  void  AnimalStateBack(int  index) ;




public class PlayerControl : CharactorBase
{

    public override void ProcessEvent(MsgBase msg)
    {
        //throw new System.NotImplementedException();

        switch (msg.msgId)
        {
            case (ushort)PlayerEvent.JoyStickCtr:
                {

                    MsgOneParam<MovingJoystick> tmpMsg = (MsgOneParam<MovingJoystick>)msg;

                    animManager.ChangerState((byte)AnimalEnum.Run);


                 
                    RunMove(tmpMsg.Data);

                }
                break;

            case (ushort)PlayerEvent.StopRun:
                {
                    animManager.ChangerState((byte)AnimalEnum.Idle);

 
                }

                break;


            case (ushort)PlayerEvent.ReduceBlood:
                {

                    MsgOneParam<float> tmpMsg = (MsgOneParam<float>)msg;


                    playerData.ReduceBlood( tmpMsg.Data);



                    MsgOneParam<float> bloodMsg = ObjectPoolManager<MsgOneParam<float>>.Instance.GetFreeObject();

                    bloodMsg.ChangeMsg((ushort)UIPlayerEvent.ReduceBlood, playerData.BloodCount);

                    SendMsg(bloodMsg);


                }


                break;

            case (ushort)PlayerEvent.BigAttack:
                {

                    Debug.Log("big  attack");
              

                    animManager.ChangerState((byte)AnimalEnum.BigAttack);

               
                     
                }
                break;

            case (ushort)PlayerEvent.NormalAttack:
                {

                    Debug.Log("recv   attack");
                    animManager.ChangerState((byte)AnimalEnum.Attact);
                }
                break;

            default :
                break;
        }



    }


    Vector3 moveDirector = Vector3.zero;

    Vector3 moveSpeed = Vector3.one *0.1f;

    PlayerData playerData;


    public void AnimalStateChange(int  index)
    {

        switch (index)
        {
            case 0:
                {
             
                }
                break;
                // free 
            case  1:
                {
                    animManager.ChangerState((byte)AnimalEnum.Idle);
                }
                break;

                // run
            case  2 :
                {
                    animManager.ChangerState((byte)AnimalEnum.Run);
                }
                break;

            case 3:
                {
                    animManager.ChangerState((byte)AnimalEnum.Attact);
                }
                break;

            case 4:
                {
                    animManager.ChangerState((byte)AnimalEnum.BigAttack);
                }
                break;

            default :
                break;
        }

 
    }




    public void RunMove(MovingJoystick joystick)
    {

        float speedX = Mathf.Abs(joystick.joystickAxis.x);

        float speedY = Mathf.Abs(joystick.joystickAxis.y);


        float tmpSpeed = Mathf.Sqrt(speedX * speedX + speedY * speedY);
        moveSpeed.y = 0;

        controlMove.SimpleMove(transform.forward * tmpSpeed *0.9f);


        MoveRoation(joystick.Axis2Angle()+90 );


    }

    public void MoveRoation( float   yAngle)
    {
        moveDirector.y = Mathf.LerpAngle(moveDirector.y,yAngle,  Time.deltaTime);

        transform.rotation = Quaternion.Euler(moveDirector);
    }

    FSMManager animManager;

    CharacterController controlMove;

    enum AnimalEnum
    {
          Idle =0 ,
          Run,
          Attact ,
        BigAttack,
         Walk,
          MaxValue
    }

    void Awake()
    {
        msgIds = new ushort[]
          {
              (ushort)PlayerEvent.JoyStickCtr,
              (ushort)PlayerEvent.StopRun,
              (ushort)PlayerEvent.ReduceBlood,
               (ushort)PlayerEvent.BigAttack,
               (ushort)PlayerEvent.NormalAttack
          };

        RegistSelf(this,msgIds);

        playerData = new PlayerData();
    }
    Animator animator;

    //  不使用动画片段的 位移 通过脚本控制
    void OnAnimatorMove()
    {

        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("baselayer.jumpattact"))
        // {
        //    //使用动画片段的位移 
        //     controlMove.Move(animator.deltaPosition);
        // }
        // else
        // {
        //    // controlMove.SimpleMove();
        // }


        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("baselayer.jumpattact"))
        //{

        //    controlMove.Move(animator.rootPosition);
        //}
        //else
        //{

        //    controlMove.SimpleMove(Vector3.forward);
        //}


    }


 
	// Use this for initialization
	void Start ()
    {


         

        controlMove = transform.GetComponent<CharacterController>();

       

        animator = transform.GetComponentInChildren<Animator>();

        animManager = new FSMManager((int)AnimalEnum.MaxValue);

        PlayerIdle tmpIdle = new PlayerIdle(animator, new AnimalStateBack(AnimalStateChange));

        animManager.AddState(tmpIdle);

        PlayerRun tmpRun = new PlayerRun(animator, new AnimalStateBack(AnimalStateChange));

        animManager.AddState(tmpRun);

        PlayerAttact tmpAttact = new PlayerAttact(animator, new AnimalStateBack(AnimalStateChange));

        animManager.AddState(tmpAttact);



        PlayerBigAttact bigAttact = new PlayerBigAttact(animator, new AnimalStateBack(AnimalStateChange));

        animManager.AddState(bigAttact);


        transform.localEulerAngles = new Vector3(0,90,0);


	}


   
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.A))
        {
            animManager.ChangerState((byte)AnimalEnum.Idle);
        }

        if (Input.GetKey(KeyCode.B))
        {
            animManager.ChangerState((byte)AnimalEnum.Run);
        }

        if (Input.GetKey(KeyCode.C))
        {
            animManager.ChangerState((byte)AnimalEnum.Attact);
        }

        animManager.Update();
		
	}
}
