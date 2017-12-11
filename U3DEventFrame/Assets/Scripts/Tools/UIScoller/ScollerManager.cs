using UnityEngine;
using System.Collections;


using UnityEngine.UI;


using System.Collections.Generic;

public enum ScollerDirect
{

    Horizontal,

    Vertical,

    Both
}





[RequireComponent(typeof(ScrollRect))]
public class ScollerManager : MonoBehaviour {



    public  delegate   void   CellChangeData(int  index ,ScollerItemBase  cell) ;
    ScollerLooper looper = null;


  
    public int cellToatalCount = 20;

    private ScollerDirect directer;
    public ScollerDirect Directer
    {
        get
        {
            return directer;
        }
        set
        {

            directer = value;
            looper.Direct = value;

            sollerAnimal.Direct = value;


        }
    }


    bool isPlayingAnimal = false;

   

    public bool IsPlayingAnimal
    {

        get
        {
            return isPlayingAnimal;
        }

        set
        {
            isPlayingAnimal = value;
        }

    }



    /// <summary>
    ///  更新ｃｅｌｌ　
    /// </summary>
    /// <param name="index"></param>
    /// <param name="tmpCell"></param>
    void UpdateCell(int index, ScollerItemBase  tmpCell)
    {
        //UnityEngine.Debug.LogError("index ============ " + index);
       tmpCell.cellTransform.name = "Iterm==" + tmpCell.CellNumber +"==index=="+index;

        //Debug.Log(index + "===" + tmpCell["SonButton"].name);

        if (cellChanger != null)
        cellChanger(index,tmpCell);
       
    }

    CellChangeData    cellChanger  = null;







    public ScollerItemBase  RefrushCell(int index)
    {


        return looper.RefushCell(index);
 
    }

    public void ClickCell(int index)
    {
        sollerAnimal.ClickBtnByIndex(index);
    }

    ScollerMoveAnimal sollerAnimal;

    void Initial()
    {

        ScrollRect rect = transform.GetComponent<ScrollRect>();

        RectTransform content = transform.GetChild(0).GetComponent<RectTransform>();

      
        if (rect.vertical)
        {
            directer = ScollerDirect.Vertical;
        }
        else
        {
            directer = ScollerDirect.Horizontal;
        }

        content.localPosition = Vector3.zero;
        content.localScale = Vector3.one;
        content.localRotation = Quaternion.identity;

        sollerAnimal = new ScollerMoveAnimal(content, directer);
        
        looper = new ScollerLooper(rect, content, cellToatalCount, sollerAnimal, UpdateCell);

       looper.IsPlayingAnimal = IsPlayingAnimal;

        
    }


    private void LeftTop(RectTransform tmpRect)
    {
        tmpRect.anchorMin = new Vector2(0, 1);
        tmpRect.anchorMax = new Vector2(0, 1);

        tmpRect.pivot = new Vector2(0, 1);


    }


    RectTransform content = null;
    public void AddChild(GameObject  tmpCell)
    {
        if (content == null)
        {
            content = transform.GetChild(0).GetComponent<RectTransform>();
        }


        tmpCell.transform.parent = content;
        
    }

    public void FinishInitial()
    {

        Initial();
    }

    void Awake()
    {
       
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="cellDelegate"></param>
    public void InitialScoller(CellChangeData cellDelegate)
    {



        cellChanger = cellDelegate;


    }






    public void DeleteCell( int  cellIndex,Object cellObj)
    {

        if (genScollerHelp != null)
        {
            if (cellToatalCount <= 0 || sollerAnimal.PlayingAnimal)
                return;


            ScollerItemBase tmpDeleteCell = sollerAnimal.FindCellByIndex(cellIndex);

            if (tmpDeleteCell == null)
                return;
             

            genScollerHelp.DeleteCell();

            IsPlayingAnimal = genScollerHelp.IsPlayingAnimal;

            cellToatalCount -= 1;


            if (cellToatalCount <= 0)
                return;

            ///如果还是无限滚动在最后增加一个单元格  如果不是了  就删除单元格
            if (!IsPlayingAnimal)
            {

                sollerAnimal.DeleteContent(cellIndex);
            }
            else
            {

               

                //先在最后添加一个
                genScollerHelp.AddContent(cellObj);


                RectTransform content = transform.GetChild(0).GetComponent<RectTransform>();
                sollerAnimal.AddCellToLastCellNumber(content);
                // 当前的删除掉
                sollerAnimal.DeleteContent(tmpDeleteCell.CellIndex);
            }

            looper.RefushCellCount();
        }

    }


    public void AddCell( int  cellIndex, Object  tmpObj)
    {

        if (genScollerHelp != null)
        {
            if (sollerAnimal.PlayingAnimal)
                return;


            ScollerItemBase findAddCell = sollerAnimal.FindCellByIndex(cellIndex);

            if (findAddCell == null)
                return;
             




            genScollerHelp.AddContent(tmpObj);

            IsPlayingAnimal = genScollerHelp.IsPlayingAnimal;


            cellToatalCount += 1;

            RectTransform content = transform.GetChild(0).GetComponent<RectTransform>();
            sollerAnimal.ReoderContentCell(content, cellIndex);

            looper.RefushCellCount();

        }

 

    }



    GenScollerHelper genScollerHelp = null;




    /// <summary>
    /// 初始化必备条件
    /// </summary>
    /// <param name="cellNumber"> 单元格的个数</param>
    /// <param name="direct"> 1  表示 竖直 2 表示横向</param>
    /// <param name="tmpObj"></param>
    public void InitialChild(int cellNumber, int tmpDirect, Object tmpObj ,CellChangeData  cellDelegate)
    {

        genScollerHelp = gameObject.GetComponent<GenScollerHelper>() == null?gameObject.AddComponent<GenScollerHelper>(): gameObject.GetComponent<GenScollerHelper>();

       

        genScollerHelp.InitialChild(cellNumber, tmpDirect, tmpObj);


        IsPlayingAnimal = genScollerHelp.IsPlayingAnimal;
        cellToatalCount = cellNumber;


        InitialScoller(cellDelegate);

        Initial();
 
    }

    // Use this for initialization
    void Start () {

        //Initial();

        Object tmpObj = Resources.Load("Scoller/Item0");

        InitialChild(20, 1, tmpObj, null);



    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(500, 100, 200, 100), "AddContent"))
        {
            Object tmpObj = Resources.Load("Scoller/Item0");
            AddCell(1, tmpObj);
        }

        if (GUI.Button(new Rect(500, 300, 200, 100), "DeleteCell"))
        {
            Object tmpObj = Resources.Load("Scoller/Item0");
            DeleteCell(1, tmpObj);
        }
    }

    // Update is called once per frame
    void Update () {

        if(looper != null)
        looper.UpdateAnimal();


    }
}
