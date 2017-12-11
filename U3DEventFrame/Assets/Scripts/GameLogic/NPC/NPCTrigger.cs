using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using U3DEventFrame;

public class NPCTrigger : MonoBehaviour {



    void OnTriggerEnter(Collider  collider )
    {
        if(!IsTrigger)
        {
            if (collider.transform.tag == "Player")
            {

                IsTrigger = true;



                Initial();
                
                  
                 
            }
        }
      
         
    }

    bool IsTrigger = false;

    string[] triggerName = { "MonsterOne", "MonsterTwo", "MonsterThree", "MonsterFour" };


    public void Initial()
    {
        for (int i = 0; i < 4; i++)
        {

            Object tmpObj = Resources.Load("ScenceOne/NPC/" + triggerName[i]);


            GameObject tmpMonster = GameObject.Instantiate(tmpObj) as GameObject;

            Transform posTransform = transform.FindChild(triggerName[i]);


            tmpMonster.transform.parent = posTransform;

            tmpMonster.transform.localPosition = Vector3.zero;


            NPCCell tmpCell;

            if (i == 0)
            {
                tmpCell = tmpMonster.AddComponent<SkeletonAnimal>();

            }
            else
            {
                tmpCell = tmpMonster.AddComponent<SkeletonAnimal>();
            }

       

            NPCController.Instance.RegistGameObject(tmpMonster, tmpCell);



        }
    }
	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
