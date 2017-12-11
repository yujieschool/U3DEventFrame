using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;


public class Main : MonoBehaviour {


    void Awake()
    {
 
          gameObject.AddComponent<WWWHelper>();


    }
	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {


        System.GC.Collect();
		
	}
}
