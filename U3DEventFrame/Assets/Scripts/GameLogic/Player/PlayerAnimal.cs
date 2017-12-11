using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using U3DEventFrame;


public class PlayerRun : FSMState
{


    AnimalStateBack stateBack;


    Animator animator;
    public PlayerRun(Animator  tmpAnimator,AnimalStateBack  tmpBack)
    {
        animator = tmpAnimator;

        stateBack = tmpBack;

    }

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();

        animator.SetInteger("MainState",2);

       
    }

}


public class PlayerIdle : FSMState
{

    Animator animator;

    AnimalStateBack stateBack;

    PlayerControl player;
    public PlayerIdle(Animator tmpAnimator,AnimalStateBack  tmpBack)
    {
        animator = tmpAnimator;

        stateBack = tmpBack;


    }

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();

        Debug.Log("Idle  coming");
        animator.SetInteger("MainState", 1);
    


        tmpCount = 5;
    


    }

    float tmpCount = 0;
    public override void Update()
    {
        //base.Update();

        tmpCount += Time.deltaTime;

        if (Mathf.FloorToInt(tmpCount) %8 ==0)
        {
            animator.SetFloat("BigAttact",0);
        }

        if (Mathf.FloorToInt(tmpCount) % 10 == 0)
        {

            //player.AnimalStateChange(3);

            //stateBack(3);

           animator.SetTrigger("LongIdle");

            
        }



    }
}


public class PlayerBigAttact : FSMState
{
     Animator animator;


     PlayerControl tmpControler;

    AnimalStateBack stateBack;
    public PlayerBigAttact(Animator tmpAnimator, AnimalStateBack tmpBack)
    {
        animator = tmpAnimator;

        stateBack = tmpBack;

    }

    public override void Update()
    {

        tmpCount += Time.deltaTime;

     //   Debug.Log("time count=="+ tmpCount);

        if (Mathf.FloorToInt(tmpCount) % 12 == 0)
        {

            animator.SetInteger("MainState", 0);
        }

        if (tmpCount > 15.2f)
        {

            stateBack(1);
        }


         animator.SetInteger("BigAttact", Mathf.FloorToInt(tmpCount));
        

    }

   


    float tmpCount = 0;
    public override void OnEnter()
    {
        //throw new System.NotImplementedException();

        animator.SetInteger("MainState", 6);



        tmpCount = animator.GetFloat("BigAttact");

        if (tmpCount < 10)
            tmpCount = 10;
    }
}

public class PlayerAttact : FSMState
{

    Animator animator;



    AnimalStateBack stateBack;
    public PlayerAttact(Animator tmpAnimator,AnimalStateBack  tmpBack)
    {
        animator = tmpAnimator;

        stateBack = tmpBack;

    }

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();


        Debug.Log("Attack  coming ");
        animator.SetInteger("MainState", 3);

        IsBegAttact = false;

        tmpCount = animator.GetFloat("BigAttact");


        if (tmpCount > 10)
        {

            IsBegAttact = true;
        }

        orginCount = tmpCount;


       // animator.SetInteger("BigAttact", Mathf.FloorToInt(tmpCount));
    }



   

   


    float tmpCount = 0;


    float orginCount = 0;


    bool IsBegAttact = false;
    
    public override void Update()
    {
        //base.Update();

        tmpCount += Time.deltaTime;

       // Debug.Log("time count"+ tmpCount);

        if (tmpCount-orginCount >  1.08f)
        {


            if (IsBegAttact)
            {
                stateBack(4);
            }
            else
            {
                animator.SetFloat("BigAttact", tmpCount);
                stateBack(1);
            }
        }



    }
}




