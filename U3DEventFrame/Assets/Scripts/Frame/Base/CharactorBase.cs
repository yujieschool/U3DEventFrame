using UnityEngine;
using System.Collections;

namespace U3DEventFrame
{
    public abstract class CharactorBase : MonoBase
    {

        public void RegistSelf(MonoBase mono, params ushort[] msgs)
        {
            CharaterManager.instance.RegistMsg(mono, msgs);
        }



        public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
        {

            CharaterManager.instance.UnRegistMsg(mono, msgs);

        }





        // all  son  send message 

        public void SendMsg(MsgBase msg)
        {

            CharaterManager.instance.SendMsg(msg);
        }


        public ushort[] msgIds;
        void OnDestroy()
        {

            if (msgIds != null)
                UnRegistSelf(this, msgIds);
        }






    }

}
