using UnityEngine;
using System.Collections;

public class FollowMonster : MonoBehaviour {



    private Transform target;

    public float smoothTime = 0.01f;

    private Vector3 cameraVelocity = Vector3.zero;

    public Vector3 Distance;

	// Use this for initialization
	void Start () {

        target = GameObject.FindWithTag("Player").transform;
	
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        if (target)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + Distance, ref  cameraVelocity, smoothTime);
        }
	}


}
