using UnityEngine;
using System.Collections;


namespace U3DEventFrame
{

    public abstract class AudioBase : MonoBase
    {

        public void RegistSelf(MonoBase mono, params ushort[] msgs)
        {
            AudioManager.instance.RegistMsg(mono, msgs);
        }



        public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
        {

            AudioManager.instance.UnRegistMsg(mono, msgs);

        }




        // all  son  send message 

        public void SendMsg(MsgBase msg)
        {

            AudioManager.instance.SendMsg(msg);
        }


        public ushort[] msgIds;
        void OnDestroy()
        {


            UnRegistSelf(this, msgIds);
        }




    }
}



