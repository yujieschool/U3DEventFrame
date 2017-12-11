using UnityEngine;

using System.Collections;


//
//  UIPanle_Ma_Modle#FILEEXTENSION#
//  #PROJECTNAME#
//
//  Created by #SMARTDEVELOPERS# on #CREATIONDATE#.
//
//

public class UIPanle_Ma_Modle  
{


    GameObject buttonObj;


    public UIPanle_Ma_Modle(GameObject button)
    {

        buttonObj = button;

    }

    bool isActive = true;

    public void ProcessButtonClick()
    {

        if (isActive)
        {
            buttonObj.SetActive(false);

            isActive = false;
        }
        else
        {
            buttonObj.SetActive(true);

            isActive = true;
        }

       
       

    }
	


}
