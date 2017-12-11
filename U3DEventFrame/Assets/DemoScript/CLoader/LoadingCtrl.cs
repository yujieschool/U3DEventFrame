using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  loading  C  层
/// </summary>
public class LoadingCtrl 
{

    public UIBases monoBase;


    public LoadingCtrl(UIBases tmpBase)
    {

        monoBase = tmpBase;
    }



    private Object registPanel;

    public Object RegistPanle
    {

        get
        {
            return registPanel;
        }
        set
        {

            registPanel = value;
 
            // registPanel  = GameObject.Instantiate(value) as GameObject;
        }
    }


  public   void RegistBtn()
    {


        Debug.Log(RegistPanle.name);
        GameObject tmpGameObj = monoBase.InitialPanle(RegistPanle);

        Debug.Log(tmpGameObj.name);

 

        tmpGameObj.AddComponent<TeacherRegisting>();


    }

}
