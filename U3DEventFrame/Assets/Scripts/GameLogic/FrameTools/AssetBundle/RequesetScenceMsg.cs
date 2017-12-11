using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using U3DEventFrame;


public class RequesetScenceMsg : MsgBase
{

    public string scenceName;

    public string bundleName;

    public LoaderProgrocess backDelegate; 

    public void ChangeMsg(ushort msgid, string  tmpScence, string  tmpBundle , LoaderProgrocess  tmpBack)
    {

        this.msgId = msgid;

        this.bundleName = tmpBundle;

        this.scenceName = tmpScence;

        this.backDelegate = tmpBack;

 
    }


}
