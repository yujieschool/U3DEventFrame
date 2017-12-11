using UnityEngine;
using System.Collections;
using UnityEngine.Events;

using UnityEngine.EventSystems;

//所有的UI  继承这个


namespace U3DEventFrame
{


    public abstract class UIBase : MonoBase
    {


        public void RegistSelf(MonoBase mono, params ushort[] msgs)
        {
            UIManager.instance.RegistMsg(mono, msgs);


        }


       


        public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
        {

            UIManager.instance.UnRegistMsg(mono, msgs);

        }





        // all  son  send message 

        public void SendMsg(MsgBase msg)
        {

            UIManager.instance.SendMsg(msg);
        }


        public ushort[] msgIds;
      public   void OnDestroy()
        {

            UIManager.instance.UnRegistPanelGameObject(this.name);
            if (msgIds != null)
                UnRegistSelf(this, msgIds);
        }




  

    }

}



