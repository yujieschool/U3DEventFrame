using UnityEngine;
using System.Collections;

namespace U3DEventFrame
{
    public class DBManager : ManagerBase
    {

        // Use this for initialization
        void Awake()
        {


            instance = this;
        }




        public void SendMsg(MsgBase msg)
        {



            if (msg.GetManager() == (int)ManagerID.DBManager)
            {


                ProcessEvent(msg);
            }
            else
            {
                MsgCenter.instance.SendToMsg(msg);

            }




        }



        public static DBManager instance;
    }

}

