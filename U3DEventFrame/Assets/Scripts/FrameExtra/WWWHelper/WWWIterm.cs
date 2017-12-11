using UnityEngine;
using System.Collections;



public class WWWItermBase
{

    private string url;

    public string Url
    {
        get
        {
            return url;
        }
        set
        {
            url = value;
        }
    }

    public IEnumerator Download()
    {
        BeginDownLoad();
        WWW www = new WWW(Url);
        yield return www;

        if (www.isDone)
        {
            FinishDownLoad(www);
        }
        else
        {

            DownLoadError(www,this);
        }
     
    }
    public virtual void BeginDownLoad()
    {


    }

    public virtual void FinishDownLoad(WWW tmpWWW)
    {

    }

    public virtual void DownLoadError(WWW tmpWWW,WWWItermBase tmpBase )
    {
 
    }


}


