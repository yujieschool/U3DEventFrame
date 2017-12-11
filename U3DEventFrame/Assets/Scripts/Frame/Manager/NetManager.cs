using UnityEngine;
using System.Collections;

using System.Collections.Generic ;

namespace U3DEventFrame
{

    public class NetManager : ManagerBase
    {





        // Use this for initialization
        void Awake()
        {



            instance = this;



        }




        // Update is called once per frame
        void Update()
        {



        }




        public void SendMsg(MsgBase msg)
        {

            if (msg.GetManager() == (int)ManagerID.NetManager)
            {


                ProcessEvent(msg);
            }
            else
            {
                MsgCenter.instance.SendToMsg(msg);

            }



         


        }


        public static NetManager instance;


    }

}

