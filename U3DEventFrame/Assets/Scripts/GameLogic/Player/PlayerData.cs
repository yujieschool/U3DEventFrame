using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{

    public PlayerData()
    {

        bloodCount = 100;
    }

    public void ReduceBlood(float tmpReduce)
    {
        bloodCount -= tmpReduce;

        if (bloodCount < 0)
        {

            bloodCount = 0;

        }

       // Debug.Log("blood  count=="+bloodCount);
    }

    private float bloodCount;

    public float BloodCount
    {
        get
        {
            return bloodCount;
        }
    }


}
