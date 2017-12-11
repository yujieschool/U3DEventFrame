using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingCtrl  {



    UnityEngine.Object registObj;


    UIBases tmpMono;


    GameObject loadigObj;

    public GameObject LoadingObj
    {
        get
        {
            return loadigObj;
        }

        set
        {
            loadigObj = value;

        }
    }

    public UILoadingCtrl(UIBases  tmpBase)

    {

        tmpMono = tmpBase;
 
          
    }

    public void ShowPanle()
    {

        loadigObj.SetActive(true);

        loadigObj.transform.SetAsLastSibling();
    }


    public  void  RegistBtnPress()
    {
          Debug.Log("BtnDone=="+registObj.name);


          LoadingObj.SetActive(false);

       GameObject  tmpGameObj=    tmpMono.InitialPanle(registObj);


        //tmpMono.Initial(tmpGameObj.transform);


        tmpGameObj.AddComponent<UIDemoRegist>();


    }

    public void  SettingObj(Object   tmpObj)
    {

        registObj = tmpObj ;


    }


}
