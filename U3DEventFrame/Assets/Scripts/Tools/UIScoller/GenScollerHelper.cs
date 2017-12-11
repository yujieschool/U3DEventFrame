using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// 需要将panle 设置成 左上角对齐
/// </summary>
public class GenScollerHelper :MonoBehaviour  {

  
    private ScollerDirect direct ;


    public ScollerDirect ScollerDirect
    {
        get
        {

            return direct;
        }

        set
        {


            direct = value;

        }
    }

    private ScrollRect AddScollerRect(RectTransform scolelr)
    {
        ScrollRect tmpRect = scolelr.GetComponent<ScrollRect>();

        if (tmpRect == null)
        {
            tmpRect = scolelr.gameObject.AddComponent<ScrollRect>();
        }

        if (direct == ScollerDirect.Vertical)
        {
            tmpRect.vertical = true;
            tmpRect.horizontal = false;
        }
        else
        {
            tmpRect.vertical = false;
            tmpRect.horizontal = true;
        }


        return tmpRect;

    }


    private void LeftTop(RectTransform tmpRect)
    {
        tmpRect.anchorMin = new Vector2(0, 1);
        tmpRect.anchorMax = new Vector2(0, 1);

        tmpRect.pivot = new Vector2(0, 1);


    }








    RectTransform scollerTrans;


    RectTransform   scollerContent;




    bool isPlayingAnimal = false;

    int cellCount ;

    byte extraCell = 2;

    public int CellCount
    {

        get
        {
            return cellCount;
        }

        set
        {
            cellCount = value;
        }

    }




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

    //first
    private RectTransform  InitialGrid()
    {

        ScrollRect scolerRect = AddScollerRect(scollerTrans);

        GameObject tmpContent = new GameObject();

        tmpContent.name = "Grid";

        tmpContent.transform.parent = scollerTrans;


      //  tmpContent.transform.localScale = Vector3.one;

        RectTransform tmpRect = tmpContent.AddComponent<RectTransform>();
        LeftTop(tmpRect);
        

        scolerRect.content = tmpRect;
        tmpRect.anchoredPosition = Vector2.zero;

        
        tmpRect.sizeDelta = scollerTrans.sizeDelta;


        //Debug.Log("size delta ==" + scollerTrans.sizeDelta);

        return tmpRect;

    }



    // sencond  gen  scoller


    private void GenNormalContent(int index, GameObject  tmpObject )
    {

        tmpObject.transform.parent = scollerContent;
        tmpObject.transform.localScale = Vector3.one;
        tmpObject.transform.localRotation = Quaternion.identity;

        RectTransform  tmpRect  = tmpObject.GetComponent<RectTransform>();


        LeftTop(tmpRect);

        if (direct == ScollerDirect.Vertical)
        {
            tmpRect.localPosition = new Vector3(0, -tmpRect.sizeDelta.y * index, 0);

        }
        else
        {
            tmpRect.localPosition = new Vector3(tmpRect.sizeDelta.x*index,0 , 0);
        }

      


    }


    private float GenScollerContent(int index, GameObject tmpObject)
    {

        tmpObject.transform.parent = scollerContent;
        tmpObject.transform.localScale = Vector3.one;
        tmpObject.transform.localRotation = Quaternion.identity;

        RectTransform tmpRect = tmpObject.GetComponent<RectTransform>();


        LeftTop(tmpRect);


        float tmpHeight = 0;

        if (direct == ScollerDirect.Vertical)
        {
            tmpRect.localPosition = new Vector3(0, -tmpRect.sizeDelta.y * (index), 0);


            tmpHeight = tmpRect.sizeDelta.y;

        }
        else
        {
            tmpRect.localPosition = new Vector3(tmpRect.sizeDelta.x * (index ), 0, 0);


            tmpHeight = tmpRect.sizeDelta.x;
        }

        return tmpHeight;






    }

    private void GenNormalScoller(Object tmpCell,float  cellHeight )
    {


        for (int i = 1; i < cellCount; i++)
        {
            GameObject cell = Instantiate(tmpCell) as GameObject;

            GenNormalContent(i, cell);
        }



        if (direct == ScollerDirect.Vertical)
        {
            scollerContent.sizeDelta = new Vector3(scollerContent.sizeDelta.x, cellHeight * (cellCount), 0);

        }
        else
        {
            scollerContent.sizeDelta = new Vector3(cellHeight * cellCount, scollerContent.sizeDelta.y, 0);
        }


        

    }

    private void GenScoller(Object tmpCell, int tmpCellNumber)
    {

        float tmpHeight = 0;
        for (int i = 1; i < tmpCellNumber; i++)
        {

            GameObject cell = Instantiate(tmpCell) as GameObject;

            tmpHeight=  GenScollerContent(i,cell);


        }



        if (direct == ScollerDirect.Vertical)
        {
            scollerContent.sizeDelta = new Vector3(scollerContent.sizeDelta.x, tmpHeight * (tmpCellNumber - extraCell), 0);

        }
        else
        {
            scollerContent.sizeDelta = new Vector3(tmpHeight * (tmpCellNumber - extraCell), scollerContent.sizeDelta.y, 0);
        }




  
       

    }




    public void DeleteCell()
    {

        cellCount -= 1;


       // Debug.Log("DeleteCell  ====="+cellCount);
        if (cellCount > 2)
        {


            RectTransform tmpCell0 = scollerContent.GetChild(0).GetComponent<RectTransform>();
            int findCellCount = IsNormalScoller(tmpCell0);

            if (findCellCount != -1)
            {
                IsPlayingAnimal = true;
            }
            else
            {
                IsPlayingAnimal = false;
            }
        }
        else
        {
            IsPlayingAnimal = false;
        }

    }


    /// <summary>
    /// 添加一个cell
    /// </summary>
    /// <param name="cellObj"></param>
    public void AddContent(Object  cellObj)
    {

        cellCount += 1;

        GameObject cell0 = Instantiate(cellObj) as GameObject;

        RectTransform cell0Rect = cell0.GetComponent<RectTransform>();


        if (cellCount > 2)
        {

            int findCellCount = IsNormalScoller(cell0Rect);




            if (findCellCount != -1)
            {

                GenScollerContent(cellCount-1, cell0);


                

              


                IsPlayingAnimal = true;


            }
            else
            {

                GenNormalContent(cellCount - 1, cell0);

 


            }
        }
        else
        {


            GenNormalContent( cellCount-1, cell0);
        }


 
    }


    private int  IsNormalScoller(RectTransform cell0Rect)
    {

       


        int  findCellCount = -1;
        if (direct == ScollerDirect.Vertical)
        {
            float tmpHeight = cell0Rect.sizeDelta.y * (cellCount - extraCell);


            if (tmpHeight > scollerContent.sizeDelta.y)
            {

               // IsNormal = false;

                for (int i = 2; i < cellCount; i++)
                {

                    tmpHeight = cell0Rect.sizeDelta.y * (i - extraCell);

                    if (tmpHeight > scollerContent.sizeDelta.y)
                    {

                        findCellCount = i;

                        break;

                    }
                }
            }

        }
        else
        {


            float tmpHeight = cell0Rect.sizeDelta.x * (cellCount - extraCell);


            if (tmpHeight > scollerContent.sizeDelta.x)
            {

                //IsNormal = false;


                for (int i = 2; i < cellCount; i++)
                {

                    tmpHeight = cell0Rect.sizeDelta.x * (i - extraCell);

                    if (tmpHeight > scollerContent.sizeDelta.x)
                    {

                        findCellCount = i;

                        break;

                    }
                }


            }



        }


        return findCellCount;



    }

    private void AutoBuilder(Object  tmpcell)
    {



        if (scollerContent == null)
        {
            scollerContent = InitialGrid();
        }
        else
        {
            for (int a = scollerContent.childCount - 1; a>-1; a--)
            {
                DestroyImmediate(scollerContent.GetChild(a).gameObject);
            }
            scollerContent.sizeDelta = scollerTrans.sizeDelta;
        }

        if (cellCount > 0)
        {
            GameObject cell0 = Instantiate(tmpcell) as GameObject;
            RectTransform cell0Rect = cell0.GetComponent<RectTransform>();

            if (cellCount > 2)
            {
                int findCellCount = IsNormalScoller(cell0Rect);

                if (findCellCount != -1)
                {
                    GenScollerContent(0, cell0);
                    GenScoller(tmpcell, findCellCount);

                    IsPlayingAnimal = true;
                }
                else
                {
                    GenNormalContent(0, cell0);

                    if (direct == ScollerDirect.Vertical)
                    {
                        GenNormalScoller(tmpcell, cell0Rect.sizeDelta.y);
                    }
                    else
                    {
                        GenNormalScoller(tmpcell, cell0Rect.sizeDelta.x);
                    }
                }
            }
            else
            {
                GenNormalContent(0, cell0);
                if (direct == ScollerDirect.Vertical)
                {
                    GenNormalScoller(tmpcell, cell0Rect.sizeDelta.y);
                }
                else
                {
                    GenNormalScoller(tmpcell, cell0Rect.sizeDelta.x);
                }
            }
        }
    }

    /// <summary>
    /// 初始化必备条件
    /// </summary>
    /// <param name="cellNumber"> 单元格的个数</param>
    /// <param name="direct"> 1  表示 竖直 2 表示横向</param>
    /// <param name="tmpObj"></param>
    public void  InitialChild(int cellNumber,int  tmpDirect ,Object  tmpObj)
    {

        cellCount = cellNumber;
        scollerTrans = gameObject.GetComponent<RectTransform>();

        LeftTop(scollerTrans);


        if (tmpDirect == 1)
        {
            direct = ScollerDirect.Vertical;
        }
        else
        {
            direct = ScollerDirect.Horizontal;
        }


        AutoBuilder(tmpObj);



    }

    void Start()
    {


        //Object  tmpObj=  Resources.Load("Scoller/Item0");

        //InitialChild(8,1,tmpObj);
         
          

    }
}
