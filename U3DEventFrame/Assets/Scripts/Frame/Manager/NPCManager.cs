using UnityEngine;
using System.Collections;


using System.Collections.Generic;


namespace U3DEventFrame
{


    //public class NPCData
    //{

    //    private GameObject npc;


    //    private byte  npcIIndex;


    //    public NPCData(GameObject  tmpObj)
    //    {

    //        npc = tmpObj;

             
    //    }

    //}

    public class NPCManager : ManagerBase
    {


        // Use this for initialization
        void Awake()
        {


            instance = this;
        }




        public void SendMsg(MsgBase msg)
        {



            if (msg.GetManager() == (int)ManagerID.NPCManager)
            {


                ProcessEvent(msg);
            }
            else
            {
                MsgCenter.instance.SendToMsg(msg);

            }




        }

        //public void RegistGameObject(string  tmpName, GameObject  tmpObj)
        //{

        //    if (!SonMembers.ContainsKey(tmpName))
        //    {
 
        //          //SonMembers.ad
        //    }

 
        //}


   
        //Dictionary<string, GameObject>  sonMembers = null;


        //public Dictionary<string, GameObject>  SonMembers
        //{
        //    get
        //    {
        //        if (sonMembers == null)
        //            sonMembers = new Dictionary<string, GameObject>();

        //        return sonMembers;
        //    }

        //}



        public static NPCManager instance;

    }

}


