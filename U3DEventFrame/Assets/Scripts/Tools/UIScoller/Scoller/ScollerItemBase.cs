using UnityEngine;
using System.Collections;

using  System.Collections.Generic ;

/// <summary>
/// 注意 所有的 cell 对齐均采用 左上角对齐  
/// sizeDeta 都为 正值 就是宽高
/// </summary>
public class ScollerItemBase
{

    protected float cellWidth;

    protected float cellHeight;

    public RectTransform cellTransform;


    //记录当前第几个cell
    protected int cellIndex;

    //记录在数组的一个排序
    protected sbyte cellNumber;


    public sbyte CellNumber
    {

        get
        {
            return cellNumber;
        }
        set
        {
            cellNumber = value;
        }
    }

    public int CellIndex
    {

        get
        {
            return cellIndex;
        }
        set
        {
            cellIndex = value;
        }
    }




    public Dictionary<string, RectTransform> sonMember;



    public RectTransform this[string key]
    {

        get
        {

            return sonMember[key];
        }
    }


    public RectTransform GetMember(string key)
    {
 
        return  sonMember[key];
    }


    public void Dispose()
    {

        GameObject.Destroy(cellTransform.gameObject);

        cellTransform = null;
    }
    private void InitialSon(RectTransform  parent)
    {

       RectTransform[] allSon=   parent.GetComponentsInChildren<RectTransform>();

       sonMember = new Dictionary<string, RectTransform>();

       for (int i = 0; i < allSon.Length; i++)
       {
           if (!allSon[i].name.EndsWith("_n") )
           {

               if (!sonMember.ContainsKey(allSon[i].name))
               {

                   sonMember.Add(allSon[i].name, allSon[i]);
               }
                  
               else
               {

                   UnityEngine.Debug.Log("have contain key  please fixed ==" + allSon[i].name);
                   //Debuger.Log("have contain key  please fixed =="+allSon[i].name);
 
               }
           }
           
          

       }

    }


    

    public ScollerItemBase(RectTransform tmpRect, sbyte index,ScollerDirect  tmpDirect)
    {

        this.cellWidth = tmpRect.sizeDelta.x;

        this.cellHeight = tmpRect.sizeDelta.y;


        this.cellTransform = tmpRect;

        this.cellIndex = this.cellNumber = index;

        Direct = tmpDirect;


        InitialSon(tmpRect);


        targetPos = cellTransform.localPosition ;
    }


    public void SettingIndex( int  index)
    {

        this.cellIndex = index;
    }

    public void SettingNumber(sbyte nuber)
    {

        this.cellNumber = nuber;
    }

    protected Vector2 targetPos;


    public Vector3 GetTargetPosition()
    {

        return targetPos;
    }


    public Vector3 CellLocalPosition
    {

        get
        {
           return cellTransform.localPosition;
        }
        set
        {
          targetPos=  cellTransform.localPosition = value;
        }
    }






    public void FollowBack(ScollerItemBase  backItem)
    {
        Vector3  tmpPos=  backItem.CellLocalPosition;

        if (direct == ScollerDirect.Horizontal)
        {

            tmpPos.y -= GetCellHeight();
            CellLocalPosition = tmpPos;

        }
        else
        {
            tmpPos.y  +=  GetCellHeight();
            CellLocalPosition = tmpPos;
        }

    }

   public ScollerDirect direct;

    public ScollerDirect Direct
    {

        set
        {
            direct = value;
        }

        get
        {

            return direct;
        }
    }


    public void Debug()
    {


        //UnityEngine.Debug.Log("cell name ==" + cellTransform.name);
        //UnityEngine.Debug.Log("cellNumber ==" + cellNumber);

        //UnityEngine.Debug.Log("cellIndex ==" + cellIndex);


        //UnityEngine.Debug.Log("local position ==" + cellTransform.localPosition);
    }

    public ScollerItemBase()
    {

    }


    public virtual float GetCellWidth()
    {
        return cellWidth;
    }

    public virtual float GetCellHeight()
    {
        return cellHeight;
    }

    //
    public void FollowFront( ScollerItemBase frontItem)
    {

        Vector3 frontPos = frontItem.GetTargetPosition();




        if (direct == ScollerDirect.Horizontal)
        {
            frontPos.x += frontItem.GetCellWidth();


        }
        else
        {

            frontPos.y -= frontItem.GetCellHeight();

        }

        targetPos = cellTransform.localPosition = frontPos;


    }

    public void MoveTo(ScollerItemBase toItem)
    {

        Vector3 frontPos = toItem.GetTargetPosition();

        this.cellNumber = toItem.cellNumber;

         targetPos = cellTransform.localPosition = frontPos;
    }

    public virtual void WillChangeCell(ScollerItemBase toItem, byte direct)
    {

    }

    public virtual void ExChangeCell(ScollerItemBase toItem, byte direct)
    {
        //向右
        if (direct == 1)
        {

            cellNumber = (sbyte)(toItem.cellNumber - 1);

            cellIndex = toItem.cellIndex - 1;

            float tmpX = toItem.GetTargetPosition().x - GetCellWidth();  //Mathf.Abs(toItem.GetTargetPosition().x) - Mathf.Abs(cellWidth);
            cellTransform.localPosition = new Vector3(tmpX, toItem.GetTargetPosition().y, toItem.GetTargetPosition().z);

           


        }//　向左
        else if (direct == 2)
        {

            cellNumber = (sbyte)(toItem.cellNumber + 1);



            float tmpX = Mathf.Abs(toItem.GetTargetPosition().x) + Mathf.Abs(toItem.GetCellWidth());
            cellTransform.localPosition = new Vector3(tmpX, toItem.GetTargetPosition().y, toItem.GetTargetPosition().z);

            cellIndex = toItem.cellIndex + 1;

        }
        //　向下拖
        else if (direct == 3)
        {


            cellNumber = (sbyte)(toItem.cellNumber - 1);
            cellIndex = toItem.cellIndex - 1;


            float tmpY = GetCellHeight(); // Mathf.Abs(toItem.GetTargetPosition().y) - Mathf.Abs(cellHeight);
            cellTransform.localPosition = new Vector3(toItem.GetTargetPosition().x, toItem.GetTargetPosition().y + tmpY, toItem.GetTargetPosition().z);

    

        }
        //　向上拖
        else if (direct == 4)
        {


            cellNumber = (sbyte)(toItem.cellNumber + 1);

            cellIndex = toItem.cellIndex + 1;

            float tmpY =   Mathf.Abs(toItem.GetTargetPosition().y) + Mathf.Abs(toItem.GetCellHeight());
            cellTransform.localPosition = new Vector3(toItem.GetTargetPosition().x, -tmpY, toItem.GetTargetPosition().z);




        }


        targetPos = cellTransform.localPosition;


    }


    public virtual void ChangeCellFinish(ScollerItemBase toItem, byte direct)
    {


    }


}

