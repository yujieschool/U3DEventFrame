using UnityEngine;

using System.Collections;

using System.Collections.Generic;

public class WWWHelper : MonoBehaviour {


    static public WWWHelper Instance
    {

        get
        {
            return instance;
        }
    }

    static WWWHelper instance;

    Queue<WWWItermBase> downLoader;

    void Awake()
    {

        instance = this;
        isDownFinish = true;
        downLoader = new Queue<WWWItermBase>();
    }


    bool isDownFinish;
	// Use this for initialization
	void Start () {
	
	}


    public void AddWWWTask(WWWItermBase tmpBase)
    {

        downLoader.Enqueue(tmpBase);

        if (isDownFinish)
        {
            isDownFinish = false;
            StartCoroutine(Download());
        }

    }

    public IEnumerator Download()
    {

       
        while (downLoader.Count > 0)
        {

            WWWItermBase tmpBase = downLoader.Dequeue();

            yield return tmpBase.Download();


           

        }

        isDownFinish = true;
  
    }


	
	// Update is called once per frame
	void Update () {



      //  WWWHelper.instance.AddWWWTask();
       
	
	}
}
