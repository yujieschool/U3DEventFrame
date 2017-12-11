using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using U3DEventFrame;

public class MsgOneParam<T> : MsgBase
{

    private T data;

    public T Data
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }

    public MsgOneParam()
    {

       // Debug.Log("coming ===");
        this.msgId = 0;
         data = default(T);
    }
    public MsgOneParam(ushort  tmpMsgId,T tmpData)
    {
        data = tmpData;

        this.msgId = tmpMsgId;
    }

    public MsgOneParam(ushort tmpMsgId)
    {

       // Debug.Log("coming =111==");
        this.msgId = tmpMsgId;

        data = default(T);
    }

    public void ChangeMsg(ushort msg, T  tmpData)
    {
        data = tmpData;

        msgId = msg;
 
    }

}


public class MsgTwoParam<T,U> : MsgBase
{

    private T data;

    public T Data
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }

    private  U dataTwo ;
    public U DataTwo
    {
        get
        {
            return dataTwo;
        }

        set
        {
            dataTwo = value;
        }
    }

    public MsgTwoParam(ushort tmpMsgId,T tmpData, U tmpTwo)
    {
        data = tmpData;

        dataTwo = tmpTwo ;

        this.msgId = tmpMsgId;
    }

}

