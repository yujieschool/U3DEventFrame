using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;

using UnityEngine.Events;

using U3DEventFrame;


public class UISubManager : MonoBehaviour 
{

    Dictionary<string, GameObject> sonMember;


    private string panelName = null;
    public string PanelName
    {

        get
        {

            return panelName;
        }

        set
        {
            panelName = value;

            UIManager.instance.RegistGameObject(panelName, transform.name, gameObject);
            AddComponentToChild();
        }
    }




    void Awake()
    {


        UIBase tmpBase = this.GetComponentInParent<UIBases>();

        sonMember = new Dictionary<string, GameObject>();
        if (tmpBase != null)
        {

            PanelName = tmpBase.name;
        }
         
     


     
        


         
    }

    public void AddComponentToChild()
    {
        Transform[] childrens = GetComponentsInChildren<Transform>();

        for (int i = 1; i < childrens.Length; i++)
        {
            if (!childrens[i].name.EndsWith("_n"))
            {

                sonMember.Add(transform.name,gameObject);

            }
        }

    }


    public GameObject GetChildGameObject(string  tmpName)
    {
        if (sonMember.ContainsKey(tmpName))
        {
             return  sonMember[tmpName];
        }

        return null;
 
    }






    public void AddButtonLisenter(string tmpName, UnityAction action)
    {

      GameObject tmpGame =   GetChildGameObject(tmpName);

      Button btn = tmpGame.GetComponent<Button>();

        if (btn != null)
            btn.onClick.AddListener(action);

    }



    public void AddButtonDownLisenter(string  tmpName,UnityAction<BaseEventData> action)
    {


        GameObject tmpGame = GetChildGameObject(tmpName);

        EventTrigger trigger = tmpGame.GetComponent<EventTrigger>();

        if (trigger == null)
            trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerDown;

        entry.callback = new EventTrigger.TriggerEvent();

        entry.callback.AddListener(action);


        trigger.triggers.Add(entry);

    }

    public void AddButtonUpLisenter(string  tmpName,UnityAction<BaseEventData> action)
    {

        GameObject tmpGame = GetChildGameObject(tmpName);


        EventTrigger trigger = tmpGame.GetComponent<EventTrigger>();

        if (trigger == null)
            trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerUp;

        entry.callback = new EventTrigger.TriggerEvent();

        entry.callback.AddListener(action);


        trigger.triggers.Add(entry);
    }




    public void AddSliderLisenter(string  tmpName,UnityAction<float> action)
    {

        GameObject tmpGame = GetChildGameObject(tmpName);


        Slider btn = tmpGame.GetComponent<Slider>();

        if (btn != null)
            btn.onValueChanged.AddListener(action);
    }



    public void AddInputFiledLisenter(string  tmpName,UnityAction<string> action)
    {

        GameObject tmpGame = GetChildGameObject(tmpName);


        InputField btn = tmpGame.GetComponent<InputField>();



        if (btn != null)
            btn.onValueChanged.AddListener(action);
    }



    public void AddToggleLisenter(string  tmpName,UnityAction<bool> action)
    {

        GameObject tmpGame = GetChildGameObject(tmpName);


        Toggle btn = tmpGame.GetComponent<Toggle>();

        if (btn != null)
            btn.onValueChanged.AddListener(action);
    }

    

      

}
