using UnityEngine;
using System.Collections;



namespace U3DEventFrame
{
    public abstract class MonoBase : MonoBehaviour
    {



        //	protected  DataBase dataInstance;
        public abstract void ProcessEvent(MsgBase msg);

    }

}



