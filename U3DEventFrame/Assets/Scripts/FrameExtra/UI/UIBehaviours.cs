using UnityEngine;
using System.Collections;
using U3DEventFrame;

using UnityEngine.UI;

using UnityEngine.Events;

using UnityEngine.EventSystems;



public class UIBehaviours : MonoBehaviour{


  
 
    void Awake()
    {


        UIBase tmpBase = this.GetComponentInParent<UIBases>();

        if (tmpBase != null)
        {
            panelName = tmpBase.name;
           

        }




    }

    private string panelName = null;
    public string PanelName
    {

        get {

            return panelName;
        }

        set
        {
            panelName = value;

            UIManager.instance.RegistGameObject(panelName, this.name, gameObject);
        }
    }




    void OnDestroy()
    {

        if(panelName != null)
        UIManager.instance.UnRegistGameObject(panelName, name);


    }


  public  void AddButtonLisenter(UnityAction<GameObject> action)
    {
        Button btn = transform.GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.AddListener(delegate()
            {
                action(btn.gameObject);
            });

        }
     

    }



  public void AddButtonLisenter(UnityAction action)
  {
      Button btn = transform.GetComponent<Button>();

      if (btn != null)
      {

          btn.onClick.AddListener(action);
      }
      

  }





    public void AddButtonDownLisenter(UnityAction<BaseEventData> action)
    {

        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();

        if (trigger == null)
            trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerDown;

        entry.callback = new EventTrigger.TriggerEvent();

        entry.callback.AddListener(action);


        trigger.triggers.Add (entry);

    }

    public void AddButtonUpLisenter(UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();

        if (trigger == null)
            trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerUp;

        entry.callback = new EventTrigger.TriggerEvent();

        entry.callback.AddListener(action);


        trigger.triggers.Add(entry);
    }


  

    public void AddSliderLisenter(UnityAction<float> action)
    {
        Slider btn = transform.GetComponent<Slider>();

        if (btn != null)
            btn.onValueChanged.AddListener(action);
    }



    public void AddInputFiledLisenter(UnityAction<string> action)
    {
        InputField btn = transform.GetComponent<InputField>();


      
        if (btn != null)
            btn.onValueChanged.AddListener(action);
    }



    public void AddToggleLisenter(UnityAction<bool> action)
    {
        Toggle btn = transform.GetComponent<Toggle>();

        if (btn != null)
            btn.onValueChanged.AddListener(action);
    }




  


}
