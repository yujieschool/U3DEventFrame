using UnityEngine;
using System.Collections;
using U3DEventFrame;

using UnityEngine.Events;

using UnityEngine.EventSystems;

public abstract class UIBases : UIBase
{



   public void Awake()
    {

        Debug.Log("regist awake  run"+ transform.name);

        panelName = transform.name;
        UIManager.instance.RegistGameObject(panelName, panelName, gameObject);
    }

    private string panelName;

    public void Initial(Transform  parentTransform)
    {

        panelName = parentTransform.name;
        UIManager.instance.RegistGameObject(panelName, panelName, gameObject);

        AdddBehaviours(parentTransform);
    }

    public void OnDestroy()
    {

        base.OnDestroy();
        UIManager.instance.UnRegistGameObject(panelName, panelName);

        UIManager.instance.UnRegistPanelGameObject(panelName);
    }
    /// <summary>
    ///   递归 加载  UIBehavrious
    /// </summary>//
    /// _n  表示 不加入 UIManager 里 编程不会用到
    /// _c  表示 犹如  背包系统的 多个 相同的 单元格的 管理方式 以 iterm 为单位
    /// <param name="root"></param>
    void AdddBehaviours(Transform root)
    {


        for (int i = 0; i < root.childCount; i++)
        {

            Transform tmpChild = root.GetChild(i);


            if (tmpChild.name.EndsWith("_c"))
            {

                 tmpChild.gameObject.AddComponent<UISubManager>();
            }
            else
            {
                if (!tmpChild.name.EndsWith("_n"))
                {
                  UIBehaviours  tmpBehaviours=  tmpChild.gameObject.AddComponent<UIBehaviours>();

                  tmpBehaviours.PanelName = panelName;
                }

                if (tmpChild.childCount > 0)
                {

                    AdddBehaviours(tmpChild);
                }
            }

 

        }
    }
    

    public void AddComponentToChild()
    {
        Transform[] childrens = GetComponentsInChildren<Transform>();

        for (int i = 1; i < childrens.Length; i++)
        {
            if (!childrens[i].name.EndsWith("_n"))
            {
                UIBehaviours behav = childrens[i].GetComponent<UIBehaviours>();
                if (behav == null)
                {

                    behav = childrens[i].gameObject.AddComponent<UIBehaviours>();
                    behav.PanelName = this.name;

                   
                }

            }
        }

    }


    #region  InitialPanel

    public Object LoadResources(string resPath)
    {


        Object tmpObj = Resources.Load(resPath);

        return tmpObj;


    }

    public GameObject InitialPanle(string resPath)
    {
        Object tmpObj = LoadResources(resPath);

        GameObject tmpGame = GameObject.Instantiate(tmpObj) as GameObject;


        GameObject parentObj = GameObject.FindGameObjectWithTag("MainCavas");

        tmpGame.transform.parent = parentObj.transform;
        return tmpGame;


    }

    public GameObject InitialPanle(Object tmpObj)
    {
       

        GameObject tmpGame = GameObject.Instantiate(tmpObj) as GameObject;


        return InitialGameObj(tmpGame);
         

    }

    public GameObject InitialGameObj(GameObject tmpGameObj)
    {
        tmpGameObj.name = tmpGameObj.name.Replace("(Clone)", "");


        GameObject parentObj = GameObject.FindGameObjectWithTag("MainCanvas");

        if (parentObj == null)
        {

            Debug.LogError("this canvase  not find");
            return null;
        }

        tmpGameObj.transform.SetParent(parentObj.transform, false);
        return tmpGameObj;
    }


    #endregion


    #region  SubManager

    public UISubManager GetSubManager(string  objName)
    {

        return   GetUIComponent<UISubManager>(objName);
         
    }



    #endregion


    #region  UIBehavrious



    public GameObject GetGameObject(string objName)
    {

        return UIManager.instance.GetGameObject(panelName, objName);
 
    }
    public T GetUIComponent<T>(string objName)
    {
        Debug.Log("objNanme=="+objName);
        return GetGameObject(objName).GetComponent<T>();

    }

    public T  AddComponetToChild<T>(string objName)  where T: Component
    {

        GameObject tmpObj = GetGameObject(objName);

        if(tmpObj != null)
        {
            return   tmpObj.AddComponent<T>();
        }

        return default(T);
    }


    public void AddButtonLisenter(string objName, UnityAction action)
    {

     
        UIBehaviours tmpBehavrours = GetUIComponent<UIBehaviours>(objName);

        Debug.Log("AddButtonLisenter  objName=="+objName);

        if (tmpBehavrours != null)
        tmpBehavrours.AddButtonLisenter(action);
    }


    public void AddButtonDownLisenter(string objName, UnityAction<BaseEventData> action)
    {
        UIBehaviours tmpBehavrours = GetUIComponent<UIBehaviours>(objName);

        if (tmpBehavrours != null)
        tmpBehavrours.AddButtonDownLisenter(action);
    }

    public void AddButtonUpLisenter(string objName, UnityAction<BaseEventData> action)
    {
        UIBehaviours tmpBehavrours = GetUIComponent<UIBehaviours>(objName);

        if (tmpBehavrours != null)
        tmpBehavrours.AddButtonUpLisenter(action);
    }


    public void AddSliderLisenter(string objName, UnityAction<float> action)
    {
        UIBehaviours tmpBehavrours = GetUIComponent<UIBehaviours>(objName);

        if (tmpBehavrours != null)
        tmpBehavrours.AddSliderLisenter(action);
    }

    public void AddInputFiledLisenter(string objName, UnityAction<string> action)
    {
        UIBehaviours tmpBehavrours = GetUIComponent<UIBehaviours>(objName);

        if (tmpBehavrours != null)
        tmpBehavrours.AddInputFiledLisenter(action);
    }


    public void AddToggleLisenter(string objName, UnityAction<bool> action)
    {
        UIBehaviours tmpBehavrours = GetUIComponent<UIBehaviours>(objName);

        if (tmpBehavrours != null)
        tmpBehavrours.AddToggleLisenter(action);
    }




    #endregion  



}
