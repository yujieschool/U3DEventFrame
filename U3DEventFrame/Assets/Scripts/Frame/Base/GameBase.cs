using UnityEngine;
using System.Collections;



namespace U3DEventFrame
{

    public abstract class GameBase : MonoBase
    {

     

            public void RegistSelf(MonoBase mono, params ushort[] msgs)
            {
                GameManager.instance.RegistMsg(mono, msgs);


            }



            public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
            {

            GameManager.instance.UnRegistMsg(mono, msgs);

            }





            // all  son  send message 

            public void SendMsg(MsgBase msg)
            {

               GameManager.instance.SendMsg(msg);
            }


            public ushort[] msgIds;
            void OnDestroy()
            {

                if (msgIds != null)
                    UnRegistSelf(this, msgIds);
            }


        HunkAssetRes hunkRes = null;

    public    HunkAssetRes HunkAssetMsg
        {
            get
            {
                if (hunkRes == null)
                {
                    hunkRes = new HunkAssetRes();
                }

                return hunkRes;

            }
        }


        public void GetRes(bool single, ushort msgId, string scence, string bundle, string tmpRes, ushort backId)
        {
            HunkAssetMsg.ChangeHunkAssetMsg(single, msgId, scence, bundle, tmpRes, backId);

            SendMsg(HunkAssetMsg);
        }


        public void ReleaseRes(ushort msgId, string scence, string bundle, string resName)
        {
            HunkAssetMsg.ChangeReleaseResMsg(msgId, scence, bundle, resName);

            SendMsg(HunkAssetMsg);
        }


    }



}
