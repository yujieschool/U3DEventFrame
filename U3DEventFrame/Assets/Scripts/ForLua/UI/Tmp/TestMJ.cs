using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMJ : MonoBehaviour {



	// Use this for initialization
	void Start () {

       // GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(1, 0));


        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, 0));;
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}
