using UnityEngine;
using System.Collections;

using System;

namespace U3DEventFrame
{
    //  发送的时候     4  + 2  + proto   接收的时候   4+ 2+1 +proto
    public class NetMsgBase : MsgBase
    {

        public byte[] buffer;

        public NetMsgBase()
        {

        }
        public NetMsgBase(byte[] arr)
        {
            buffer = arr;

            this.msgId = BitConverter.ToUInt16(arr, 4);
        }

        //for  lua  send
        public NetMsgBase(ushort tmpMsgId, byte[] arr)
        {
            this.msgId = tmpMsgId;

            buffer = new byte[arr.Length + 6];

            Buffer.BlockCopy(arr, 0, buffer, 6, arr.Length);

            byte[] tmpLenght = BitConverter.GetBytes(arr.Length);

            Buffer.BlockCopy(tmpLenght, 0, buffer, 0, 4);


            byte[] tmpIds = BitConverter.GetBytes(this.msgId);

            Buffer.BlockCopy(tmpIds, 0, buffer, 4, 2);




        }

        public void DebugMsg()
        {

            return;
            int tmpInt = BitConverter.ToInt32(buffer, 0);

            Debug.Log("length==" + tmpInt);

            ushort tmpShort = BitConverter.ToUInt16(buffer, 4);

            Debug.Log("msgid ==" + tmpShort);


            Debug.Log("all length ==" + buffer.Length);

        }
        public override byte GetState()
        {
            return buffer[6];
        }


        public virtual byte[] GetProtoBuffer()
        {

            byte[] tmpByte = new byte[buffer.Length - 7];

            Buffer.BlockCopy(buffer, 7, tmpByte, 0, buffer.Length - 7);

            return tmpByte;
        }






        public override byte[] GetNetBytes()
        {
            return buffer;
        }

    }


}


