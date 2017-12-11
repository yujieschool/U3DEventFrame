using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace U3DEventFrame
{
    public class AssetManager : ManagerBase
    {


        void Awake()
        {


            instance = this;



        }












        public void SendMsg(MsgBase msg)
        {



            if (msg.GetManager() == (int)ManagerID.AssetManager)
            {

                // Debug.Log("asset manager recv msg ==" +msg.msgId);

                ProcessEvent(msg);
            }
            else
            {
                MsgCenter.instance.SendToMsg(msg);

            }




        }

        public static AssetManager instance;


    }
}




