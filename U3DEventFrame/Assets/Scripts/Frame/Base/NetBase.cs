using UnityEngine;
using System.Collections;

//所有的NetBase  继承这个



namespace U3DEventFrame
{
    public abstract class NetBase : MonoBase
    {






        public void RegistSelf(MonoBase mono, params ushort[] msgs)
        {
            NetManager.instance.RegistMsg(mono, msgs);


        }



        public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
        {

            NetManager.instance.UnRegistMsg(mono, msgs);

        }





        // all  son  send message 

        public void SendMsg(MsgBase msg)
        {

            NetManager.instance.SendMsg(msg);
        }


        public ushort[] msgIds;
        void OnDestroy()
        {

            if (msgIds != null)
                UnRegistSelf(this, msgIds);
        }


        HunkAssetRes hunkRes = null;

        HunkAssetRes HunkAssetMsg
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




