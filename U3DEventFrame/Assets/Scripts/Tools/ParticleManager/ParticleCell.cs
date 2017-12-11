using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCell : MonoBehaviour {


    ParticleSystem[] cells;
	// Use this for initialization
	void Start () {
		

        cells = transform.GetComponentsInChildren<ParticleSystem>();
	}

    public void Play()
    {
        for (int i = 0; i < cells.Length; i++)
        {

            cells[i].Play(true);
            
        }
    }

    public void Stop()
    {

        for (int i = 0; i < cells.Length; i++)
        {

            cells[i].Stop();

        }

    }
	
	// Update is called once per frame
	void Update () {

         if(Input.GetKeyDown(KeyCode.E))
         {
              Play();
         }
		
	}
}
