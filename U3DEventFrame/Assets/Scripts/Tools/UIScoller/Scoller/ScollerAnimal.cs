using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ScollerAnimal
{
    //void PlayAnimal(int index);


    public ScollerDirect direct;
    public ScollerDirect Direct
    {

        set
        {

            for (int i = 0; i < scolerIterms.Count; i++)
            {
                ScollerItemBase tmpIterm = scolerIterms[i];

                tmpIterm.Direct = value;
            }
        }
    }




    public ScollerAnimal(ScollerDirect  tmpDirect)
    {
        direct = tmpDirect;

    }


    //private bool isPlaying;

    //public bool IsPlaying
    //{
    //    get
    //    {
    //        return isPlaying;
    //    }
    //    set
    //    {
 
    //    }

    //}


    public ScollerAnimal(RectTransform content,ScollerDirect  tmpDirect)
    {

        scolerIterms = new List<ScollerItemBase>();


        direct = tmpDirect;

        float tmpHeight = 0.0f;

        for (int i = 0; i < content.childCount; i++)
        {
            RectTransform tmpChild = content.GetChild(i).GetComponent<RectTransform>();
            ScollerItemBase tmpItem = new ScollerItemBase(tmpChild, (sbyte)i,direct);

            //    Debug.Log("tmpIterm-=="+ tmpIterm.cellPos+"==i=="+i);
            scolerIterms.Add(tmpItem);

            if (direct == ScollerDirect.Vertical)
            {

                tmpHeight += tmpChild.sizeDelta.y;
            }
            else
            {
                tmpHeight += tmpChild.sizeDelta.x;
            }

        }


           if (direct == ScollerDirect.Vertical)
            {

                if (tmpHeight < content.sizeDelta.y)
                {

                    isPlaying = false;

                }
                else
                {
                    isPlaying = true;
                }
            }
            else
            {
                if (tmpHeight < content.sizeDelta.x)
                {

                    isPlaying = false;

                }
                else
                {
                    isPlaying = true;
                }
            }

       


    }



    public ScollerItemBase FindLastIndexCell()
    {

        int tmpIndex = 0;

        int tmpFinde = 0;
        for (int i = 0; i < scolerIterms.Count; i++)
        {

            if (scolerIterms[i].CellIndex > tmpIndex)
            {

                tmpFinde = i;

                tmpIndex = scolerIterms[i].CellIndex;
            }

        }

        return scolerIterms[tmpFinde];
    }


    public ScollerItemBase FindLastNumberCell()
    {

        int tmpIndex = 0;

        int tmpFinde = 0;
        for (int i = 0; i < scolerIterms.Count; i++)
        {

            if (scolerIterms[i].CellNumber > tmpIndex)
            {

                tmpFinde = i;

                tmpIndex = scolerIterms[i].CellNumber;
            }

        }

        return scolerIterms[tmpFinde];
    }




    public ScollerItemBase FindCellByNumber(int index)
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


    public ScollerItemBase FindCellByIndex(int index)
    {
        for (int i = 0; i < scolerIterms.Count; i++)
        {

            if (scolerIterms[i].CellIndex == index)
            {
                return scolerIterms[i];
            }

        }

        return null;
    }


    /// <summary>
    /// 删除以后对cellnumber 进行改正
    /// </summary>
    private void FixedCellNumber()
    {

        int findCell = -1;
        for (int i = 0; i < scolerIterms.Count; i++)
        {


          ScollerItemBase  tmpIterm=    FindCellByNumber(i);


          if (tmpIterm == null)
          {

              findCell =i ;

          }

        }


        if (findCell != -1)
        {
            for (int i = 0; i < scolerIterms.Count; i++)
            {

                ScollerItemBase tmpItem = scolerIterms[i];

                if (tmpItem.CellNumber > findCell)
                {

                    tmpItem.CellNumber -= 1;
                }
            }
        }




    }

    public virtual void DeleteChild(int  index)
    {


        int deleteNumber = -1;


        for (int i = 0; i < scolerIterms.Count; i++)
        {

            if (scolerIterms[i].CellIndex == index)
            {


                deleteNumber = scolerIterms[i].CellNumber;

                scolerIterms[i].Dispose();
                 scolerIterms.RemoveAt(i);

                 break;
            }

        }


        for (int i = 0; i < scolerIterms.Count; i++)
        {

            if (scolerIterms[i].CellNumber > deleteNumber)
            {

                scolerIterms[i].CellNumber -= 1;
            }


            if (scolerIterms[i].CellIndex > index)
            {

                scolerIterms[i].CellIndex -= 1;
            }


        }



        //for (int i = 0; i < scolerIterms.Count; i++)
        //{

        //    if (scolerIterms[i].CellIndex > index)
        //    {

        //        scolerIterms[i].CellIndex -= 1;
        //    }

        //}


    }






    public void FollowFront(int  cellnumber)
    {
        for (int i = 0; i < scolerIterms.Count; i++)
        {

            if (scolerIterms[i].CellNumber > cellnumber)
            {
                ScollerItemBase tmpItem = scolerIterms[i];



                ScollerItemBase tmpFront = FindCellByNumber(tmpItem.CellNumber - 1);

                if (tmpFront != null)
                {



                    tmpItem.FollowFront(tmpFront);

                    // tmpItem.CaculateTargetPosition(currentOpeCell.hidlePanel.sizeDelta, currentOpeCell.isHide);
                }


            }

        }
    }

    public List<ScollerItemBase> scolerIterms;

    public virtual void Update()
    {

    }


    public virtual void CellWillChange(ScollerItemBase from, ScollerItemBase to, byte direct)
    {

    }



    public virtual void CellExchange(ScollerItemBase from, ScollerItemBase to, byte direct)
    {

        if (direct == 1 || direct == 3)
        {

            for (int i = 0; i < scolerIterms.Count; i++)
            {
                ScollerItemBase tmpItem = scolerIterms[i];
                tmpItem.CellNumber += 1;

               // tmpItem.cellTransform.name = "Item" + tmpItem.cellNumber +"==index=="+tmpItem.cellIndex;
            }
        }
        else if (direct == 2 || direct == 4)
        {


            for (int i = 0; i < scolerIterms.Count; i++)
            {
                ScollerItemBase tmpItem = scolerIterms[i];
                tmpItem.CellNumber -= 1;


               // tmpItem.cellTransform.name = "Item" + tmpItem.cellNumber + "==index==" + tmpItem.cellIndex  ;
            }

        }


    }


    public virtual void CellWillChangeFinish(ScollerItemBase from, ScollerItemBase to, byte direct)
    {

    }



    public delegate void CellChangeListen();


    public CellChangeListen cellListen;

    public void AddCellChangeListen(CellChangeListen listen)
    {
        this.cellListen = listen;
    }


    protected bool isPlaying = false;


    public bool PlayingAnimal
    {

        get
        {

            return isPlaying;
        }

        set
        {

            isPlaying = value;
        }
    }
}