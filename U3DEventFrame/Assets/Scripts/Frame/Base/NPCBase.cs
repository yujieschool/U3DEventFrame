using UnityEngine;
using System.Collections;


namespace U3DEventFrame
{

    public abstract class NPCBase : MonoBase
    {

        public void RegistSelf(MonoBase mono, params ushort[] msgs)
        {

            //Debug.Log("Regist ==" + mono.name);

            //for (int i = 0; i < msgs.Length; i++)
            //{
            //    Debug.Log("Regist msg  ==" + msgs[i]);
            //}
                NPCManager.instance.RegistMsg(mono, msgs);
        }



        public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
        {

            NPCManager.instance.UnRegistMsg(mono, msgs);

        }





        // all  son  send message 

        public void SendMsg(MsgBase msg)
        {

            NPCManager.instance.SendMsg(msg);
        }



        public ushort[] msgIds;
        void OnDestroy()
        {

            if (msgIds != null)
                UnRegistSelf(this, msgIds);
        }


     //   HunkAssetRes hunkRes = null;

     //public   HunkAssetRes HunkAssetMsg
     //   {
     //       get
     //       {
     //           if (hunkRes == null)
     //           {
     //               hunkRes = new HunkAssetRes();
     //           }

     //           return hunkRes;

     //       }
     //   }



     //   public void GetRes(bool single, ushort msgId, string scence, string bundle, string tmpRes, ushort backId)
     //   {
     //       HunkAssetMsg.ChangeHunkAssetMsg(single, msgId, scence, bundle, tmpRes, backId);

     //       SendMsg(HunkAssetMsg);
     //   }


        //public void ReleaseRes(ushort msgId, string scence, string bundle, string resName)
        //{
        //    HunkAssetMsg.ChangeReleaseResMsg(msgId, scence, bundle, resName);

        //    SendMsg(HunkAssetMsg);
        //}


    }

}


