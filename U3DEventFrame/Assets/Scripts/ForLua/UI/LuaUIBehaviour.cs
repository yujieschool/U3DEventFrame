using UnityEngine;
using System.Collections;
using UnityEngine.UI;


using LuaInterface;
using UnityEngine.Events;

using UnityEngine.EventSystems;


public class LuaUIBehaviour : MonoBehaviour
{

    // public delegate void CallFunc(GameObject tmp);

    // public CallFunc AwakeFunc = null;

    public string panelName;
    void Awake()
    {
        
        // if (AwakeFunc != null)
        //      AwakeFunc.Invoke(gameObject);
        if (panelName == null || panelName == "")
        {
            try
            {
                panelName = GetComponentInParent<LuaUIPanel>().name;

            }
            catch (System.Exception e)
            {

            }
        }
        CallMethod("LUIManager", "RegistGameObject", gameObject, panelName);
    }


    public void RegistSelft()
    {
        // if (AwakeFunc != null)
        //     AwakeFunc.Invoke(gameObject);
    }

    public void AddButtonLisenter(LuaFunction action)
    {



        Button btn = transform.GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();

            UnityAction tmpAction = delegate()
            {

                Debug.Log("123");

                action.Call(gameObject);
                
            };


            btn.onClick.AddListener(tmpAction);
        }

    }


    public void AddButtonLisenter(LuaFunction action, object obj)
    {


        Button btn = transform.GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(delegate ()
            {

                action.Call(gameObject, obj);
            });
        }

    }

    public void AddButtonLisenter(LuaFunction action, object obj1, object obj2)
    {


        Button btn = transform.GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(delegate ()
            {

                action.Call(gameObject, obj1, obj2);
            });
        }

    }



    public void AddButtonDownLisenter(LuaFunction action)
    {

        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();

        if (trigger == null)
            trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerDown;

        entry.callback = new EventTrigger.TriggerEvent();

        entry.callback.AddListener(

            delegate (BaseEventData tmpData)
            {
                action.Call(gameObject, tmpData);
            });


        trigger.triggers.Add(entry);

    }

    public void AddButtonUpLisenter(LuaFunction action)
    {
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();

        if (trigger == null)
            trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerUp;

        entry.callback = new EventTrigger.TriggerEvent();

        entry.callback.AddListener(

     delegate (BaseEventData tmpData)
     {
         action.Call(gameObject, tmpData);


     });


        trigger.triggers.Add(entry);
    }




    public void AddSliderLisenter(LuaFunction action)
    {
        Slider btn = transform.GetComponent<Slider>();

        if (btn != null)
            btn.onValueChanged.AddListener(delegate (float tmpFloat)
            {

                action.Call(gameObject, tmpFloat);
            });
    }



    public void AddInputFiledLisenter(LuaFunction action,LuaFunction endEditaction)
    {
        InputField btn = transform.GetComponent<InputField>();


        if (btn != null)
        {
#if UNITY_5_5
            btn.onValueChanged.AddListener(delegate (string tmpFloat)
            {

                action.Call(gameObject, tmpFloat);
            });
            if(endEditaction != null){
                btn.onEndEdit.AddListener(delegate (string tmpFloat)
                {

                    endEditaction.Call(gameObject, tmpFloat);
                });
            }
#else
            btn.onValueChange.AddListener(delegate (string tmpFloat)
            {

                action.Call(gameObject, tmpFloat);
            });
#endif 
        }

    }



    public void AddToggleLisenter(LuaFunction action)
    {
        Toggle btn = transform.GetComponent<Toggle>();

        if (btn != null)
            btn.onValueChanged.AddListener(delegate (bool tmpBool)
            {
                action.Call(gameObject, tmpBool);
            });
    }


    public void AddDropDownLisenter(LuaFunction action)
    {
        Dropdown btn = transform.GetComponent<Dropdown>();

        if (btn != null)
            btn.onValueChanged.AddListener(delegate (int tmpInt)
            {
                action.Call(gameObject, tmpInt);
            });



    }





    public RectTransform GetTransform()
    {
        RectTransform tran = GetComponent<RectTransform>();
        if (tran == null)
        {
            tran = gameObject.AddComponent<RectTransform>();
        }

        return tran;
    }

    public Image SetImageSprite(Sprite sprite)
    {
        Image img = GetComponent<Image>();
        if (img == null)
        {
            img = gameObject.AddComponent<Image>();
        }
        img.sprite = sprite;

        return img;
    }

    public Button GetButton()
    {
        Button btn = GetComponent<Button>();
        if (btn == null)
        {
            btn = gameObject.AddComponent<Button>();
        }
        return btn;
    }

	public Slider GetSlider()
	{
		Slider btn = GetComponent<Slider>();
		if (btn == null)
		{
			btn = gameObject.AddComponent<Slider>();
		}
		return btn;
	}


    public Button SetButtonSprites(params Sprite[] sprites)
    {
        Button btn = GetComponent<Button>();
        if (btn == null)
        {
            btn = gameObject.AddComponent<Button>();
        }


        btn.transition = Selectable.Transition.SpriteSwap;

        if (sprites.Length > 0)
        {
            if (sprites[0] != null)
                SetImageSprite(sprites[0]);
            SpriteState ss = new SpriteState();
            if (sprites.Length == 2)
            {
                if (sprites[1] != null)
                    ss.pressedSprite = sprites[1];

            }
            else if (sprites.Length == 3)
            {
                if (sprites[1] != null)
                    ss.pressedSprite = sprites[1];
                if (sprites[2] != null)
                    ss.disabledSprite = sprites[2];

            }
            else if (sprites.Length == 4)
            {
                if (sprites[1] != null)
                    ss.highlightedSprite = sprites[1];
                if (sprites[2] != null)
                    ss.pressedSprite = sprites[2];
                if (sprites[3] != null)
                    ss.disabledSprite = sprites[3];
            }

            btn.spriteState = ss;
        }

        return btn;
    }

    public Text GetText()
    {
        Text tex = GetComponent<Text>();
        if (tex == null)
        {
            tex = gameObject.AddComponent<Text>();
        }

        return tex;
    }

    public Image GetImage()
    {
        Image img = GetComponent<Image>();
        if (img == null)
        {
            img = gameObject.AddComponent<Image>();
        }

        return img;
    }


    public void SetInteractable(bool value)
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.interactable = value;
        }
    }

    public  Color GetTextColor(Text text)
    {
        return text.color;
    }

    public  void SetTextColor(Text text, Color color)
    {
        text.color = color;
        
    }

	public int GetTextWidth(string str)
	{
		Text tex = GetComponent<Text>();
		if (tex) {
			Font font = tex.font;
			int fontsize = tex.fontSize;
			font.RequestCharactersInTexture(str, fontsize, FontStyle.Normal);
			CharacterInfo characterInfo;
			int width = 0;
			for(int i = 0; i < str.Length; i++){
				font.GetCharacterInfo(str[i], out characterInfo, fontsize);
				width += characterInfo.advance;
			}
			
			return width;
		}
		return 0;
	}



  
    /// <summary>
    /// 执行Lua方法
    /// </summary>
    protected void CallMethod(string moudle, string func, GameObject args, string panelName)
    {
        //if (!LuaController.Instance.isInit) return null;

        string funcName = moudle + "." + func;
        LuaClient.Instance.CallFuncWithGameObjectName(funcName, args, panelName);
    }

    //-----------------------------------------------------------------
    protected void OnDestroy()
    {
        if (GetComponent<Button>()!=null)
        {
            GetComponent<Button>().onClick.RemoveAllListeners();

        }
    }
}
