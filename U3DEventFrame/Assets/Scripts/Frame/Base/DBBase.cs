using UnityEngine;
using System.Collections;


namespace U3DEventFrame
{

    public abstract class DBBase : MonoBase
    {

        public void RegistSelf(MonoBase mono, params ushort[] msgs)
        {
            DBManager.instance.RegistMsg(mono, msgs);
        }



        public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
        {

            DBManager.instance.UnRegistMsg(mono, msgs);

        }





        // all  son  send message 

        public void SendMsg(MsgBase msg)
        {

            DBManager.instance.SendMsg(msg);
        }



        public ushort[] msgIds;
        void OnDestroy()
        {

            if (msgIds != null)
                UnRegistSelf(this, msgIds);
        }



    }

}


