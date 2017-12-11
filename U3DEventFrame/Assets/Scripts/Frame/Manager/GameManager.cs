using UnityEngine;
using System.Collections;


namespace U3DEventFrame
{

    public class GameManager : ManagerBase
    {
        void Awake()
        {
            instance = this;
        }

        public void SendMsg(MsgBase msg)
        {



            if (msg.GetManager() == (int)ManagerID.GameManager)
            {


                ProcessEvent(msg);
            }
            else
            {
                MsgCenter.instance.SendToMsg(msg);

            }


        }



        public static GameManager instance;





    }

}


