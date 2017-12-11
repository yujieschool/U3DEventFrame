using UnityEngine;
using System.Collections;





namespace U3DEventFrame
{


    public class CharaterManager : ManagerBase
    {


        void Awake()
        {
            instance = this;


            // Debug.Log("character manager");
        }





        public void SendMsg(MsgBase msg)
        {



            if (msg.GetManager() == (int)ManagerID.CharatorManager)
            {


                ProcessEvent(msg);
            }
            else
            {
                MsgCenter.instance.SendToMsg(msg);

            }




        }






        public static CharaterManager instance;



    }

}




