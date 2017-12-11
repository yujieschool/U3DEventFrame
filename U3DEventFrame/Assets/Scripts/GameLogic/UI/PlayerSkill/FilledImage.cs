using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;



public class FilledImage : MonoBehaviour {


    Image owerImage;
	// Use this for initialization
	void Start () 
    {

        owerImage = transform.GetComponent<Image>();

        isTrigger = false;
		
	}

    bool isTrigger;

    public void BeginFilled()
    {

        isTrigger = true;


        owerImage.fillAmount = 1;
    }

    float timeCount = 0;
	// Update is called once per frame
	void Update () {

        if (isTrigger)
        {

            owerImage.fillAmount -= Time.deltaTime;

            if (owerImage.fillAmount <= 0)
            {

                isTrigger = false;
            }
        }
		
	}
}
