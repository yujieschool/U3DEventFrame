using UnityEngine;
using System.Collections;

using U3DEventFrame;


namespace U3DEventFrame

{
    public class CheckBackMsg : MsgBase
    {


        public float delayTime;

        public float timerCount;



        public CheckBackMsg(ushort msgid)
        {
            this.msgId = msgid;

            this.delayTime = 5;

            this.timerCount = this.delayTime;


        }

        public bool ReduceTime(float timer)
        {

            this.timerCount -= timer;

            if (this.timerCount <= 0)
                return true;

            else
                return false;

        }


        public void Reset()
        {

            this.timerCount = this.delayTime;
        }

    }

}

