using UnityEngine;
using System.Collections;
using System ;

using System.Net;




namespace U3DEventFrame
{
    public class MsgBase
    {
        // 0---65535
        public ushort msgId;


     
        public MsgBase(ushort tmpid)
        {
            msgId = tmpid;
        }


        public void ChangeEventId(ushort  tmpid)
        {
            msgId = tmpid;
        }

		public MsgBase(byte[] arr)
		{

		}




		public virtual byte[] GetNetBytes()
		{
			return null ;
		}

        public virtual byte GetState()
        {
            return  127;
        }

        public int GetManager()
        {
            int tmpid = msgId / FrameTools.MsgSpan;

            return (tmpid * FrameTools.MsgSpan);

        }





        public MsgBase()
        {
            msgId = 0;
        }
    }




}


























