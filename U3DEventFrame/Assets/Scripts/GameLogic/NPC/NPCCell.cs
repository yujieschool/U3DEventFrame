using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using  U3DEventFrame ;


public class NPCDataBase
{

    public delegate void DataBack(byte index);


    DataBack dataDelegate;

    public NPCDataBase( DataBack  tmpBack)
    {

        dataDelegate = tmpBack;

        bloodCount = 100;

        flownDistance = 10;

        attackDistance = 2;


    }

    private float bloodCount;

    public float BloodCout
    {
        get
        {
            return bloodCount;
        }
        set
        {

            bloodCount = value;

            if (bloodCount <= 0)
            {
                bloodCount = 0;
                dataDelegate(1);
            }
        }

    }     

    private float flownDistance;

    public float FlownDistance
    {
        get
        {
            return flownDistance;
        }
        set
        {

            flownDistance = value;
        }

    }

    private float attackDistance;

    public float AttackDistance
    {
        get
        {
            return attackDistance;
        }
        set
        {

            attackDistance = value;
        }

    }


    public void ReduceBlood( float  tmpCount)
    {

        BloodCout -= tmpCount;
    }
    

}


public  class NPCCell : MonoBehaviour {


    private ushort index;


    public ushort Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }


    public GameObject player;




  protected  bool CheckAttact(Transform player, Transform npc, float left, float forward)
    {
        Vector3 npcVector = (npc.position - player.position);

        float backForwad = Vector3.Dot(player.forward, npcVector);


        // forwad
        if (backForwad > 0)
        {



            float rightLeft = Vector3.Dot(player.right, npcVector);


        
            if (Mathf.Abs(rightLeft) <= left && backForwad < forward)
            {
                return true;
            }




        }


        return false;


    }


    public virtual void AttactedByPlayer()
    {

        if (CheckAttact(player.transform, transform, 2, 5))
        {
 

            
        }
 
    }

    public  virtual void CheckAttack()
    {

   
        if (player)
        {

            Vector3 deltaPos = player.transform.position - transform.position;

            float tmpDistance = deltaPos.magnitude;


            //Debug.Log("tmpDistance===" + tmpDistance);

            if (tmpDistance <   dataBase.FlownDistance)
            {

                if (tmpDistance <= dataBase.AttackDistance )
                {       

                  
                        fsmManager.ChangerState((byte)StateEnum.Attact);


                  

                }

                else
                {


                        moveControl.SimpleMove(deltaPos.normalized);


               
                          fsmManager.ChangerState(((byte)StateEnum.Run));
                        


                }




                transform.LookAt(player.transform.position);
            }

           
              
        }

    }

    public virtual void DataBackLogic(byte  index)
    {

        switch (index)
        {
            case 0:
                { }
                break;

            case 1:
                {
                    fsmManager.ChangerState((byte)StateEnum.Death);
                }
                break;

            case 2:
                { }
                break;

        }

 
    }

    public virtual void Initial()
    {

            
        moveControl = transform.GetComponent<CharacterController>();

        if (moveControl == null)
        {
             moveControl =  gameObject.AddComponent<CharacterController>();
        }




        fsmManager = new FSMManager((int)StateEnum.MaxValue);

        dataBase = new NPCDataBase(DataBackLogic);

       // Debug.Log("parent Initial");
    }

    protected FSMManager fsmManager;

    protected enum StateEnum
    {

        Idle = 0,

        Walk,

        Run,
        Hit,
        Attact,

        Death,
        MaxValue
    }

    protected CharacterController moveControl;

    protected NPCDataBase dataBase;

   public void Update()
    {


        CheckAttack();

        fsmManager.Update();

    }

}
