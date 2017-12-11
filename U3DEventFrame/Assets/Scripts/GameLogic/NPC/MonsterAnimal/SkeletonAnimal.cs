using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  U3DEventFrame ;



public class SkeletonAttact : FSMState
{

    Animator animator;


    public SkeletonAttact(Animator tmpAnimator)
    {
        animator = tmpAnimator;

  
    }

    float timeCount = 0.0f;

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();

        animator.SetInteger("StateIndex",5);

        Reset();

    }



    public void ReducePlayerBlood()
    {

      MsgOneParam<float>  tmpMsg=   ObjectPoolManager<MsgOneParam<float>>.Instance.GetFreeObject();


      tmpMsg.Data = 10;
      tmpMsg.msgId = (ushort)PlayerEvent.ReduceBlood;
    

      NPCController.Instance.SendMsg(tmpMsg);

      ObjectPoolManager<MsgOneParam<float>>.Instance.ReleaseObject(tmpMsg);
         
    }

    void Reset()
    {
        isLooping = true;

        timeCount = 0;
    }

    bool isLooping = false;

    public override void Update()
    {
        //base.Update();

        timeCount += Time.deltaTime;


     //   Debug.Log(" timeCount==" + timeCount +"11111===" + Time.deltaTime);


        if (timeCount > 0.12f)
        {

           

            if (isLooping)
            {
                isLooping = false;

              //  Debug.Log("attact coming");
                ReducePlayerBlood();
            }

          
        }

        if (timeCount > 0.25f)
        {

            Reset();
        }




    }
}



public class SkeletonDeth : FSMState
{

    Animator animator;
    public SkeletonDeth(Animator tmpAnimator)
    {
        animator = tmpAnimator;

    }

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();

        animator.SetInteger("StateIndex", 6);
    }




    public override void Update()
    {
        //base.Update();

    }
}




public class SkeletonRun : FSMState
{

    Animator animator;
    public SkeletonRun(Animator tmpAnimator)
    {
        animator = tmpAnimator;

    }

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();
        animator.SetInteger("StateIndex",3);

    }




    public override void Update()
    {
        //base.Update();

    }
}



public class SkeletonHit : FSMState
{

    Animator animator;
    public SkeletonHit(Animator tmpAnimator)
    {
        animator = tmpAnimator;

    }

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();

        animator.SetInteger("StateIndex", 4);
    }




    public override void Update()
    {
        //base.Update();

    }
}



public class SkeletonWalk : FSMState
{

    Animator animator;
    public SkeletonWalk(Animator tmpAnimator)
    {
        animator = tmpAnimator;

    }

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();

        animator.SetInteger("StateIndex", 2);
    }




    public override void Update()
    {
        //base.Update();

    }
}



public class SkeletonIdle : FSMState
{

    Animator animator;
    public SkeletonIdle(Animator tmpAnimator)
    {
        animator = tmpAnimator;

    }

    public override void OnEnter()
    {
        //throw new System.NotImplementedException();

        animator.SetInteger("StateIndex", 1);
    }




    public override void Update()
    {
        //base.Update();

    }
}



public class SkeletonAnimal : NPCCell {



    public override void Initial()
    {
        base.Initial();


        Debug.Log("Son initial");
    }



  	// Use this for initialization
	void Start () {


        Initial();
      
        Animator  animator = transform.GetComponent<Animator>();

        SkeletonIdle tmpIdle = new SkeletonIdle(animator);

        fsmManager.AddState(tmpIdle);



        SkeletonWalk tmpWalk = new SkeletonWalk(animator);

        fsmManager.AddState(tmpWalk);

        SkeletonRun tmpRun= new SkeletonRun(animator);

        fsmManager.AddState(tmpRun);


        SkeletonHit tmpHit = new SkeletonHit(animator);

        fsmManager.AddState(tmpHit);


        SkeletonAttact tmpAttact = new SkeletonAttact(animator);

        fsmManager.AddState(tmpAttact);

        SkeletonDeth tmpDeth = new SkeletonDeth(animator);

        fsmManager.AddState(tmpDeth);
		
	}

    void OnAnimatorMove()
    {
 
    }

  public override void AttactedByPlayer()
{
 	 base.AttactedByPlayer();
}

	// Update is called once per frame
  public 	void Update () {


        base.Update();

       
		
	}
}
