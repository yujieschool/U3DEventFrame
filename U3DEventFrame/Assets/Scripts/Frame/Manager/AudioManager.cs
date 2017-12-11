using UnityEngine;
using System.Collections;



namespace U3DEventFrame
{

    public class AudioManager : ManagerBase
    {


        void Awake()
        {
            instance = this;
        }



        public void SendMsg(MsgBase msg)
        {



            if (msg.GetManager() == (int)ManagerID.AudioManager)
            {


                ProcessEvent(msg);
            }
            else
            {
                MsgCenter.instance.SendToMsg(msg);

            }




        }


        public static AudioManager instance;


    }

}




