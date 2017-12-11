using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using U3DEventFrame;


public class NPCController : NPCBase {


    public override void ProcessEvent(MsgBase msg)
    {
        //throw new System.NotImplementedException();

        switch (msg.msgId)
        {
            case (ushort)0:
                {

 
                }
                break;
            default:
                break;
        }
    }



    private ushort npcCount;


    public  void   RegistGameObject(GameObject  tmpObj,NPCCell  tmpBase)
    {

      //  if (!SonMembers.ContainsKey(npcCount))
        {

            tmpBase.player = Player;
            tmpBase.Index = npcCount;


            SonMembers.Add(tmpBase);

            //SonMembers.Add(npcCount, tmpBase);

            npcCount++;
        }

    }

    public void CheckPlayerAttacked()
    {

        for (int i = 0; i < SonMembers.Count; i++)
        {
            SonMembers[i].AttactedByPlayer();
        }
        //foreach (var item in SonMembers.Keys)
        //{

        //    SonMembers[item].AttactedByPlayer();
        //}


    }

    List<NPCCell> sonMembers = null;

    public List<NPCCell> SonMembers
    {
        get
        {

            if (sonMembers == null)
                sonMembers = new List<NPCCell>();

            return sonMembers;
 
        }
 
    }

    //Dictionary<ushort, NPCCell> sonMembers = null;


    //public Dictionary<ushort, NPCCell> SonMembers
    //{
    //    get
    //    {
    //        if (sonMembers == null)
    //            sonMembers = new Dictionary<ushort, NPCCell>();

    //        return sonMembers;
    //    }

    //}


    public static NPCController Instance;

    private GameObject player = null;

    public GameObject Player
    {

        get
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");

              //  Debug.Log(" player =="+player.name);
            }

            return player;
        }

    }

    void Awake()
    {

        Instance = this;

        msgIds = new ushort[] {  (ushort)NPCEvent.Initial   };

        RegistSelf(this,msgIds) ;

       
    }

	// Use this for initialization
	void Start () {

        npcCount = 0;
      
		
	}
	
	// Update is called once per frame
	void Update () {

      //  Debug.Log("222222222222");
	}
}
