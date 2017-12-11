using UnityEngine;
using System.Collections;

public class LitMap : MonoBehaviour {


    private Terrain terrain;

    private GameObject target;

    private RectTransform selftRectTrans;
    private Vector2 parentSize;
	// Use this for initialization
	void Start () {


        target = GameObject.FindWithTag("Player");

        terrain = GameObject.FindWithTag("Terrain").GetComponent<Terrain>();
	
        RectTransform      parentTransform =  transform.parent.GetComponent<RectTransform>();

        parentSize = parentTransform.rect.size;



        Debug.Log("parentSize  ==" + terrain.terrainData.size);


        selftRectTrans = transform.GetComponent<RectTransform>();

	}
	
	// Update is called once per frame
	void Update () {

        if (target && terrain)
        {
            transform.rotation = Quaternion.Euler(0,0,180-target.transform.rotation.eulerAngles.y);


            float tmpXX = target.transform.position.x / terrain.terrainData.size.x;

            float tmpYY = target.transform.position.z / terrain.terrainData.size.z;



            tmpXX = parentSize.x * tmpXX;

            tmpYY = parentSize.y * tmpYY;


            selftRectTrans.anchoredPosition3D = new Vector3(tmpXX, tmpYY, 0);


        }
	
	}
}
