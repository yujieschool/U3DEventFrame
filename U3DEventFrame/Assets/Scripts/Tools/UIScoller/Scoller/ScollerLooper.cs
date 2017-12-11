using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.UI;


public class ScollerLooper
{


    public delegate void UpdateCellDelegate(int index, ScollerItemBase tmpBase);





    ScrollRect scoller;

    RectTransform content;


    List<ScollerItemBase> scolerIterms = null;


    ScollerDirect direct = ScollerDirect.Vertical;

    // v  leftTop   h leftButtom 
    Vector3 conner = Vector3.zero;




    ScollerAnimal scollerAnimal = null;

    UpdateCellDelegate updateCell;


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



    public ScollerDirect Direct
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

    public int cellCount;


    public int totalCellCount;


    public Vector2 curScolerPos = Vector2.zero;


    private Vector3 contentLocalPosition;

    public ScollerLooper(ScrollRect scol, RectTransform cont, int cout, ScollerAnimal animal, UpdateCellDelegate update)
    {
        scoller = scol;
        content = cont;

        updateCell = update;

        totalCellCount = cout;


        // Debug.Log("totalCellCount=="+ totalCellCount);

        contentLocalPosition = cont.localPosition;


        if (scol.horizontal)
        {
            Direct = ScollerDirect.Horizontal;

        }
        else
        {
            Direct = ScollerDirect.Vertical;

            //   connerTwo.y += content.sizeDelta.y;

        }



        if (animal != null)
        {
            scollerAnimal = animal;
            scolerIterms = animal.scolerIterms;

          

        }
        else
        {
           
            scollerAnimal = new ScollerAnimal(content,direct);


            scolerIterms = scollerAnimal.scolerIterms;


        }


       // cellCount = scolerIterms.Count;

        RefushCellCount();
        scollerAnimal.cellListen = new ScollerAnimal.CellChangeListen(UpdateContent);

        //




        ScollerItemBase tmpItem2 = FindCell(1);

        conner = cont.TransformPoint(Vector3.zero);

        //connerTwo = cont.TransformPoint(Vector3.zero);

       // scollerAnimal.Direct = Direct; 



        Vector3 tmpConor = cont.InverseTransformPoint(conner);



        scoller.onValueChanged.AddListener(ScolerValueChange);




    }



    public ScollerItemBase  RefushCell(int  index)
    {



        ScollerItemBase tmpCell = FindCell(index);


        return tmpCell;
    }








    public void UpdateAnimal()
    {
        if (scollerAnimal != null)
            scollerAnimal.Update();
    }





    /// <summary>
    ///   1 水平左   2  水平右  4，竖直上  3，竖直下  
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private byte RejugdeDirect(Vector2 pos, ScollerItemBase tmpCellOne)
    {
        if (Direct == ScollerDirect.Horizontal)
        {
            if (curScolerPos.x < pos.x)
            {
                if (pos.x - curScolerPos.x >= tmpCellOne.GetCellWidth())
                {
                    curScolerPos = pos;
                    return 0;
                }
                curScolerPos = pos;

                return 2;
            }
            else
            {
                if (curScolerPos.x - pos.x >= tmpCellOne.GetCellWidth())
                {
                    curScolerPos = pos;
                    return 0;
                }


                curScolerPos = pos;
                return 1;
            }

        }
        else if (Direct == ScollerDirect.Vertical)
        {


            if (curScolerPos.y <= pos.y)
            {

                if (pos.y - curScolerPos.y >= tmpCellOne.GetCellHeight())
                {

                    curScolerPos = pos;
                    return 0;
                }


                curScolerPos = pos;
                return 3;
            }
            else
            {

                if (curScolerPos.y - pos.y >= tmpCellOne.GetCellHeight())
                {

                    curScolerPos = pos;
                    return 0;
                }



                curScolerPos = pos;
                return 4;
            }
        }

        return 0;
    }

    private ScollerItemBase FindCell(int index)
    {
        for (int i = 0; i < scolerIterms.Count; i++)
        {

            if (scolerIterms[i].CellNumber == index)
            {
                return scolerIterms[i];
            }

        }

        return null;
    }





    private void ExchangeCellAndAnimal(int from, byte tmpDirect, ScollerItemBase lastItem)
    {

        ScollerItemBase fromItem = FindCell(from);




        fromItem.WillChangeCell(lastItem, tmpDirect);


        scollerAnimal.CellWillChange(fromItem, lastItem, tmpDirect);


        fromItem.ExChangeCell(lastItem, tmpDirect);


        scollerAnimal.CellExchange(fromItem, lastItem, tmpDirect);


        updateCell(fromItem.CellIndex, fromItem);

        fromItem.ChangeCellFinish(lastItem, tmpDirect);

        scollerAnimal.CellWillChangeFinish(fromItem, lastItem, tmpDirect);

    }


    public void UpdateContentReCorrect(ScollerItemBase  firstItem)
    {
        content.localPosition = contentLocalPosition;



      

        ScollerItemBase tmpLast = FindCell(1);
        tmpLast.CellLocalPosition = Vector3.zero;
        scollerAnimal.FollowFront(1);

        firstItem.FollowBack(tmpLast);

     
    }

    public void UpdateContentHead()
    {
     





        ScollerItemBase tmpLast = FindCell(0);



        if (tmpLast.CellLocalPosition != Vector3.zero)
        {
            content.localPosition = contentLocalPosition;

           // Debug.Log("coming  1111111111111" + tmpLast.CellLocalPosition);
            tmpLast.CellLocalPosition = Vector3.zero;
            scollerAnimal.FollowFront(0);
        }



        //if (Direct == ScollerDirect.Vertical)
        //{
        //    if (tmpLast.CellLocalPosition!=  Vector3.zero)
        //    {
        //        tmpLast.CellLocalPosition = Vector3.zero;
        //        scollerAnimal.FollowFront(0);
        //    }
        //}
        //else
        //{

        //    if (tmpLast.CellLocalPosition.x != 0)
        //    {
        //        tmpLast.CellLocalPosition = Vector3.zero;
        //        scollerAnimal.FollowFront(0);
        //    }

        //}

 

       // firstItem.FollowBack(tmpLast);


    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="toIndex"></param>
    /// <param name="tmpDirect">   1    水平右   2  水平左  4，竖直上  3，竖直下  </param>
    private void ExchangeCell(int from, int toIndex, byte tmpDirect)
    {




        switch (tmpDirect)
        {

            //  1    水平右
            case 1:
                {

                    ScollerItemBase tmpLast = FindCell(toIndex);
                    if (tmpLast.CellIndex <= 0)
                    {
                        Debug.Log(" 1111111111111111111 =="+toIndex);
                        UpdateContentHead();
                        return;
                    }
   
                   


                    ExchangeCellAndAnimal(from, tmpDirect, tmpLast);

                    UpdateContent();

                }
                break;

            case 2:
                {



                    ScollerItemBase tmpLast = FindCell(toIndex);
                    if (tmpLast.CellIndex >= totalCellCount - 1)
                    {

                        UpdateContent();
                        return;

                    }
                      



                    ExchangeCellAndAnimal(from, tmpDirect, tmpLast);



                    UpdateContent();


                }
                break;
            //   3，竖直下
            case 3:
                {


                    ScollerItemBase tmpLast = FindCell(toIndex);


                   // Debug.Log("tmpLast.cellIndex 333333==2222=" + tmpLast.CellIndex + "==from==" + from + "==toIndex ==" + toIndex);



                    if (tmpLast.CellIndex <= 0)
                    {
                        UpdateContentHead();
                       
                        return;
                    }
  

                    ExchangeCellAndAnimal(from, tmpDirect, tmpLast);

                    UpdateContent();


                }
                break;
            //4，竖直上
            case 4:
                {

                    ScollerItemBase tmpLast = FindCell(toIndex);


                    if (tmpLast.CellIndex >= totalCellCount - 1)
                    {
                        UpdateContent();
                        return;
                    }
                       


                    ExchangeCellAndAnimal(from, tmpDirect, tmpLast);
                    UpdateContent();


                }
                break;

        }














    }


    private void UpdateContent()
    {

        ScollerItemBase tmpItem = FindCell(cellCount - 1);

         // Debug.Log(" last tmpItem.cellIndex== " + tmpItem.cellIndex);

        if (Direct == ScollerDirect.Horizontal)
        {
            float tmpWidth = Mathf.Abs(tmpItem.cellTransform.localPosition.x) + tmpItem.GetCellWidth();

            content.sizeDelta = new Vector2(tmpWidth, content.sizeDelta.y);
        }
        else
        {
            float tmpHeigth = Mathf.Abs(tmpItem.cellTransform.localPosition.y) + tmpItem.GetCellHeight();

            content.sizeDelta = new Vector2(content.sizeDelta.x, tmpHeigth);

        }

    }

    public void RefushCellCount()
    {
        cellCount = scolerIterms.Count;
    }

    private void ScolerValueChange(Vector2 pos)
    {


        if (!IsPlayingAnimal)
            return;

        RefushCellCount();
        Vector3 contPos = content.InverseTransformPoint(conner);


        ScollerItemBase tmpItem = FindCell(1);

        byte tmpDirect = RejugdeDirect(contPos, tmpItem);




        if (tmpDirect == 0 )
            return;



        switch (tmpDirect)
        {

            //    1  水平右
            case 1:
                {



                    float tmpFloat = Mathf.Abs(contPos.x - tmpItem.GetTargetPosition().x);


                    ScollerItemBase iterm0 = FindCell(0);

                    if (tmpFloat >= iterm0.GetCellWidth() * 0.5f && contPos.x < tmpItem.GetTargetPosition().x)
                    {

                        ExchangeCell(cellCount - 1, 0, tmpDirect);


                    }


                   // UpdateContent();



                }
                break;
            //2 水平左
            case 2:
                {


                    float tmpFloat = Mathf.Abs(tmpItem.GetTargetPosition().x - contPos.x);


                    if (tmpFloat >= tmpItem.GetCellWidth()*0.5f && contPos.x > tmpItem.GetTargetPosition().x)
                    {

                        ExchangeCell(0, cellCount - 1, tmpDirect);


                    }

                  //  UpdateContent();
                }
                break;
            // 4，向上拖
            case 4:
                {



                    float tmpFloat = Mathf.Abs(tmpItem.GetTargetPosition().y - contPos.y);


                    if ((tmpFloat >= tmpItem.GetCellHeight() * 0.5f) && (tmpItem.GetTargetPosition().y > contPos.y))
                    {
                        ExchangeCell(0, cellCount - 1, tmpDirect);



                    }

                //    UpdateContent();


                }
                break;
            //3，向下拖
            case 3:
                {



                    float tmpFloat = Mathf.Abs(contPos.y - tmpItem.GetTargetPosition().y);





                    ScollerItemBase iterm0 = FindCell(0);
                    if (tmpFloat >= iterm0.GetCellHeight() * 0.8f && tmpItem.GetTargetPosition().y < contPos.y)
                    {

                        ExchangeCell(cellCount - 1, 0, tmpDirect);


                    }


                //    UpdateContent();

                }
                break;

        }
    }


}