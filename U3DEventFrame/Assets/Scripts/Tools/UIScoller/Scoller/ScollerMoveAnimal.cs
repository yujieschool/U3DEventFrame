using UnityEngine;
using System.Collections;


using System.Collections.Generic;

using UnityEngine.UI;

public class ScollerMoveIterm : ScollerItemBase
{

    public delegate void ClickOn(int index);


    ClickOn btnClick;
    public RectTransform hidlePanel;


    public bool isHide;

    float parentHeight;
    float parentWidth;

    float hidePanelHeigth;
    float hidePanelWidth;


    float lerpSpeed = 0.35f;


    public bool isPlaying = false;


    byte animalState = 0;


    public void Reset()
    {

        //UnityEngine.Debug.Log("reset  coming "+cellIndex);
        isPlaying = false;
        isHide = true;

        animalState = 0;



    }

    public ScollerMoveIterm(RectTransform sonPanel, RectTransform itemRect,Button button, sbyte index, ClickOn click, ScollerDirect tmpDirect)
        : base(itemRect, index,tmpDirect)
    {

        isHide = true;
        hidlePanel = sonPanel;

       

        //UnityEngine.Debug.Log("ScollerMoveIterm  direct ==" + direct);

        hidlePanel.localScale = new Vector2(1, 0);
        parentHeight = itemRect.rect.height;
        parentWidth = itemRect.rect.width;

        hidePanelHeigth = hidlePanel.rect.height;
        hidePanelWidth = hidlePanel.rect.width;



        btnClick = new ClickOn(click);

     //   Button btn = itemRect.GetComponentInChildren<Button>();

        button.onClick.AddListener(BtnClick);


       // targetPos = cellTransform.localPosition;
    }


    public Vector3 GetCellLocalPosition()
    {

        return cellTransform.localPosition;
 
    }



    #region  RefushAniaml
    public void DismissCell()
    {

       

        isPlaying = true;

        animalState = 4;
     


      if (direct == ScollerDirect.Horizontal)
       {
           targetPos.x -= GetCellWidth();

      
       }
       else
       {

           targetPos.y += GetCellHeight();

       }
       
       

    }

    public void InsertCell()
    {

        animalState = 3;
        isPlaying = true;


        if (Direct == ScollerDirect.Vertical)
        {
            this.cellTransform.sizeDelta = new Vector2(this.cellTransform.sizeDelta.x,0);

        }
        else
        {
            this.cellTransform.sizeDelta = new Vector2(0, this.cellTransform.sizeDelta.y);
        }


    }

    public void InsertCellAnimal()
    {


        Vector2  tmpPos = new Vector2(GetCellWidth(),GetCellHeight()) ;

        this.cellTransform.sizeDelta = Vector2.Lerp(this.cellTransform.sizeDelta, new Vector2(GetCellWidth(),GetCellHeight()), lerpSpeed);


       // UnityEngine.Debug.Log("this.cellTransform.sizeDelta  ==" + this.cellTransform.sizeDelta);

    //    Mathf.Approximately(this.cellTransform.sizeDelta.x ,targetPos.x);

        float tmpXX = Mathf.Abs(this.cellTransform.sizeDelta.x - tmpPos.x);

        float tmpYY = Mathf.Abs(this.cellTransform.sizeDelta.y - tmpPos.y);

        if (tmpXX + tmpYY <0.1f)
        {
            animalState = 0;
            isPlaying = false;
        }

  
    }


    public void DismissCellAnimal()
    {

        if (Direct == ScollerDirect.Vertical)
        {
            this.cellTransform.sizeDelta = Vector2.Lerp(this.cellTransform.sizeDelta, new Vector2( GetCellWidth(),0), lerpSpeed);


           // float tmpXX = Mathf.Abs(this.cellTransform.sizeDelta.x - targetPos.x);

            float tmpYY = Mathf.Abs(this.cellTransform.sizeDelta.y );


           // UnityEngine.Debug.Log("  TMPYYYY  ===="+tmpYY);

            if ( tmpYY < 0.03f)
            {
                animalState = 0;
                isPlaying = false;
            }



        }
        else
        {
            this.cellTransform.sizeDelta = Vector2.Lerp(this.cellTransform.sizeDelta, new Vector2(0, GetCellHeight()), lerpSpeed);


            float tmpXX = Mathf.Abs(this.cellTransform.sizeDelta.x);

           // float tmpYY = Mathf.Abs(this.cellTransform.sizeDelta.y - targetPos.y);

            if (tmpXX < 0.03f)
            {
                animalState = 0;
                isPlaying = false;
            }




        }



      

    }


    #endregion


    public override float GetCellHeight()
    {
        if (isHide)
        {
            return base.GetCellHeight();
        }
        else
        {
            // return base.GetCellHeight();
            return base.GetCellHeight() + hidePanelHeigth;
        }

    }

    public override float GetCellWidth()
    {
        if (isHide)
        {
            return base.GetCellWidth();
        }
        else
        {

            return base.GetCellWidth() + hidePanelWidth;
        }
    }

    public void AddClickBack(ClickOn click)
    {
        btnClick = click;
    }


    public void BtnClick()
    {






        if (isPlaying)
        {
            //UnityEngine.Debug.Log("BtnClick 2222222 ==" + cellIndex);
            return;
        }

       


        //UnityEngine.Debug.Log("BtnClick11111  ==" + cellIndex);
        isHide = !isHide;

        isPlaying = true;

        animalState = 1;

        targetPos = cellTransform.localPosition;


        if (btnClick != null)
            btnClick(cellNumber);


    }



    public bool HiddenNoAnimal()
    {

     

        isPlaying = false;
        isHide = true;
        animalState = 0;
        if (direct == ScollerDirect.Vertical)
        {
            hidlePanel.localScale = new Vector2(1.0f, 0.0f);
            //hidlePanel.localPosition = new Vector2(0, -parentHeight / 2);



        }
        else
        {
            hidlePanel.localScale = new Vector2(0.0f, 1.0f);
            //hidlePanel.localPosition = new Vector2(-parentWidth / 2, 0);


        }

        return true;

    }

    public void  ShowPanleNoAnimal()
    {
      

        isHide = false;
        isPlaying = false;
        animalState = 0;
        if (direct == ScollerDirect.Vertical)
        {
            hidlePanel.localScale = new Vector2(1.0f, 1.0f);
            //hidlePanel.localPosition = new Vector2(12, 115);



        }
        else
        {
            hidlePanel.localScale = new Vector2(1.0f, 1.0f);
            //hidlePanel.localPosition = new Vector2(-parentWidth / 2 - hidePanelWidth / 2, 0);


        }
    }

    public override void ExChangeCell(ScollerItemBase toItem, byte direct)
    {
        base.ExChangeCell(toItem, direct);

      
    }


    public override void ChangeCellFinish(ScollerItemBase toItem, byte direct)
    {

        base.ChangeCellFinish(toItem,direct);

        Reset();

    }




    public void UpdateAnimal()
    {

        //  UnityEngine.Debug.Log("UpdateAnimal rect==" + hidlePanel.rect.size);

        // UnityEngine.Debug.Log("UpdateAnimal=sizeDelta=" + hidlePanel.sizeDelta);

        if (isHide)
        {

            if (direct == ScollerDirect.Vertical)
            {
                hidlePanel.localScale = Vector2.Lerp(hidlePanel.localScale, new Vector2(1.0f, 0.0f), lerpSpeed);
                //hidlePanel.localPosition = Vector2.Lerp(hidlePanel.localPosition, new Vector2(0, -parentHeight / 2), lerpSpeed);

                //UnityEngine.Debug.Log("UpdateAnimal==" + isHide + "scal==" + hidlePanel.localScale.y);
                if (hidlePanel.localScale.y < 0.01f)
                {

                    HiddenNoAnimal();

             
                }
            }
            else
            {
                hidlePanel.localScale = Vector2.Lerp(hidlePanel.localScale, new Vector2(0.0f, 1.0f), lerpSpeed);
                //hidlePanel.localPosition = Vector2.Lerp(hidlePanel.localPosition, new Vector2(-parentWidth / 2, 0), lerpSpeed);

                // UnityEngine.Debug.Log("UpdateAnimal==" + isHide + "scal==" + hidlePanel.localScale.y);
                if (hidlePanel.localScale.x < 0.01f)
                {

                    HiddenNoAnimal();
                }
            }






        }
        else
        {

            if (direct == ScollerDirect.Vertical)
            {

                hidlePanel.localScale = Vector2.Lerp(hidlePanel.localScale, new Vector2(1.0f, 1.0f), lerpSpeed);
                //hidlePanel.localPosition = Vector2.Lerp(hidlePanel.localPosition, new Vector2(0, -parentHeight / 2 - hidePanelHeigth / 2), lerpSpeed);


                //UnityEngine.Debug.Log("UpdateAnimal==" + isHide + "scal==" + (1.0f - hidlePanel.localScale.y <0.1f));
                if (hidlePanel.localScale.y  >0.99f)
                {
                    ShowPanleNoAnimal();
                }
            }
            else
            {
                hidlePanel.localScale = Vector2.Lerp(hidlePanel.localScale, new Vector2(1.0f, 1.0f), lerpSpeed);
                //hidlePanel.localPosition = Vector2.Lerp(hidlePanel.localPosition, new Vector2(parentWidth / 2 - hidePanelWidth / 2, 0), lerpSpeed);

                // UnityEngine.Debug.Log("UpdateAnimal==" + isHide + "scal==" + hidlePanel.localScale.y);
                if (hidlePanel.localScale.x > 0.99f)
                {

                    ShowPanleNoAnimal();
                }

            }


        }


    }




    public void MoveToFrontPosition(ScollerMoveIterm frontItem)
    {
        Vector3 frontPos = frontItem.GetTargetPosition();

        isPlaying = true;


        //UnityEngine.Debug.Log("CaculateMovePosition  isPlaying" + cellIndex);
        animalState = 2;

        targetPos = frontPos;
    }




    public void CaculateMovePosition(ScollerItemBase  frontItem)
    {

       Vector3  frontPos= frontItem.GetTargetPosition();


   

       if (direct == ScollerDirect.Horizontal)
       {
           frontPos.x  += frontItem.GetCellWidth();

      
       }
       else
       {

           frontPos.y -= frontItem.GetCellHeight();

       }

       isPlaying = true;


       //UnityEngine.Debug.Log("CaculateMovePosition  isPlaying" + cellIndex);
       animalState = 2;

       targetPos = frontPos;


    }


  





    public void UpdatePosition(float speed)
    {




        cellTransform.localPosition = Vector2.Lerp(this.cellTransform.localPosition, targetPos, speed);


        if (direct == ScollerDirect.Vertical)
        {
            //UnityEngine.Debug.Log("targetPos.y ==" + targetPos.y);
           //UnityEngine.Debug.Log("cellTransform.localPosition.y.y ==" + cellTransform.localPosition.y);
           if (Mathf.Abs( Mathf.Abs(cellTransform.localPosition.y)-Mathf.Abs (targetPos.y)) < 0.01f)
            {


                //UnityEngine.Debug.Log("UpdateScale  false =="+cellIndex);
                animalState = 0;
                isPlaying = false;
            }
        }
        else
        {

            if (Mathf.Abs(Mathf.Abs(cellTransform.localPosition.x) - Mathf.Abs(targetPos.x)) < 0.01f)
            {

                //UnityEngine.Debug.Log("UpdateScale  false ==" + cellIndex);

                animalState = 0;
                isPlaying = false;
            }
        }




    }

    public void Update()
    {
        if (isPlaying)
        {

            //UnityEngine.Debug.Log("animalState===" + animalState);
            if (animalState == 1)
            {
                UpdateAnimal();
            }
            else if (animalState == 2)
            {
                UpdatePosition(lerpSpeed);
            }
            else if (animalState == 3)
            {

                InsertCellAnimal();
            }
            else if (animalState == 4)
            {

                DismissCellAnimal();
            }


            if (animalState == 0)
                isPlaying = false;


        }

    }



    private void HiddenPanel()
    {
        hidlePanel.gameObject.SetActive(false);
    }

    private void ShowPanel()
    {
        //hidlePanel.gameObject.SetActive(true);
    }




}







public class ScollerMoveAnimal : ScollerAnimal
{


    byte playCount = 0;



    byte animalState = 0;

    int dismissCellIndex = 0;
    private void LeftTop(RectTransform tmpRect)
    {

        tmpRect.anchoredPosition = Vector3.zero;
        tmpRect.anchorMin = new Vector2(0, 1);
        tmpRect.anchorMax = new Vector2(0, 1);

        tmpRect.pivot = new Vector2(0, 1);


    }





    public ScollerMoveIterm  AddChild(int index, RectTransform content)
    {


        RectTransform tmpChild = content.GetChild(index).GetComponent<RectTransform>();


        RectTransform sonPanel = tmpChild.FindChild("SonPanel").GetComponent<RectTransform>();

        LeftTop(sonPanel);


        Button btn = tmpChild.FindChild("SonButton").GetComponent<Button>();

        ScollerMoveIterm tmpIterm = new ScollerMoveIterm(sonPanel, tmpChild, btn, (sbyte)index, ButtonClick, direct);

        scolerIterms.Add(tmpIterm);


        return tmpIterm;


    }


    public ScollerMoveAnimal(RectTransform content, ScollerDirect direct):base(direct)
    {


        scolerIterms = new List<ScollerItemBase>();


        for (int i = 0; i < content.childCount; i++)
        { 

            AddChild(i,content);
        }



    }



    #region  RefrushCell


    public void RefrushCellOrder(int  insertIndex ,ScollerMoveIterm  inserIterm)
    {

        //先找到插入的位置
        ScollerItemBase  frontItem =  FindCellByIndex(insertIndex);

        int tmpIndex = frontItem.CellIndex;
        sbyte tmpCellNub = frontItem.CellNumber;

        //更改其它 cell index
        for (int i = 0; i < scolerIterms.Count; i++)
        {
 
              ScollerItemBase  tmpIterm = scolerIterms[i];


              if (tmpIterm.CellIndex >= insertIndex)
              {
                  tmpIterm.CellNumber += 1;
                  tmpIterm.CellIndex += 1;
              }
              
        }

        // 将最后一个cell 移动要要插入的位置
            inserIterm.MoveTo(frontItem);

            inserIterm.CellNumber = tmpCellNub;
            inserIterm.CellIndex = tmpIndex;



            ChangeBackCellPos(tmpCellNub);
            //for (int i = 0; i < scolerIterms.Count; i++)
            //{

            //    ScollerMoveIterm tmpIterm = (ScollerMoveIterm)scolerIterms[i];

            //    Debug.Log("tmpIterm.cell index===" + tmpIterm.CellIndex + "==cell number==" + tmpIterm.CellNumber);
            //    if (tmpIterm.CellIndex > inserIterm.CellIndex)
            //    {

            //        ScollerItemBase tmpFront = FindCellByIndex(tmpIterm.CellIndex - 1);
            //        tmpIterm.CaculateMovePosition(tmpFront);
            //    }

            //}


         // inserIterm.CellLocalPosition = inserIterm.GetTargetPosition();

         // inserIterm.cellIndex = frontItem.cellIndex + 1;



    }

    public void ReoderContentCell(RectTransform content,int inserIndex)
    {


        if (isPlaying)
            return;
        int index = content.childCount - 1;
       ScollerMoveIterm  tmpItem=  AddChild(index, content);

     //  tmpItem.SettingIndex(lastItem.cellIndex+1);


       RefrushCellOrder(inserIndex,tmpItem);

       tmpItem.InsertCell();





       isPlaying = true;

       animalState = 2;

    }


    /// <summary>
    /// 前面已经实例化了一个cell 在最后  现在修正 index  number
    /// </summary>
    /// <param name="content"></param>
    public void AddCellToLastCellNumber(RectTransform content)
    {
       // ScollerItemBase tmpLastIndex = FindLastIndexCell();

        ScollerItemBase tmpLastNumber = FindLastIndexCell();


        int tmpIndex = tmpLastNumber.CellIndex ;

        sbyte tmpNuber = tmpLastNumber.CellNumber;


        ScollerMoveIterm tmpAdd  = AddChild(content.childCount - 1, content);


        for (int i = 0; i < scolerIterms.Count; i++)
        {
            ScollerItemBase tmpIterm = scolerIterms[i];

            if (tmpIterm.CellIndex > tmpIndex)
            {
                tmpIterm.CellIndex += 1;
            }

        }

        tmpAdd.CellNumber = (sbyte)(tmpNuber + 1);

        tmpAdd.CellIndex = tmpIndex+1;






    }

    /// <summary>
    /// 删除掉
    /// </summary>
    /// <param name="index"></param>
    public void DeleteContent(int  index)
    {


        if (isPlaying)
            return;


        ScollerMoveIterm frontItem =  (ScollerMoveIterm)FindCellByIndex(index);


        frontItem.DismissCell();




        ChangeBackCellPos(frontItem.CellNumber);



        isPlaying = true;


        animalState = 3;
       

        dismissCellIndex = index;

       // DeleteChild(index);


      
    }




    #endregion




    public void ClickBtnByIndex(int index)
    {
        for (int j = 0; j < scolerIterms.Count; j++)
        {

            if (scolerIterms[j].CellNumber == index)
            {
                (scolerIterms[j] as ScollerMoveIterm).BtnClick();
            }
        }
    }

    public void ButtonClick(int index)
    {
        
        PlayAnimal(index);

      


    }


   private void ChangeBackCellPos(int  cellNumber)
    {
        isPlaying = true;


        int tmpMax = scolerIterms.Count-1 - cellNumber;


        for (int i = 1; i <= tmpMax; i++)
        {

            for (int j = 0; j < scolerIterms.Count; j++)
            {

                if (scolerIterms[j].CellNumber - cellNumber == i)
                {
                    ScollerMoveIterm tmpItem = (ScollerMoveIterm)scolerIterms[j];



                    ScollerMoveIterm tmpFront = (ScollerMoveIterm)FindCellByNumber(tmpItem.CellNumber - 1);

                    if (tmpFront != null)
                    {



                        tmpItem.CaculateMovePosition(tmpFront);

                        // tmpItem.CaculateTargetPosition(currentOpeCell.hidlePanel.sizeDelta, currentOpeCell.isHide);
                    }
                }
            }

        }




 
    }






    public void PlayAnimal(int index)
    {


        animalState = 1;

        ChangeBackCellPos(index);





    }

    public override void CellWillChange(ScollerItemBase from, ScollerItemBase to, byte direct)
    {
        base.CellWillChange(from, to, direct);


        ScollerMoveIterm tmpFront = (ScollerMoveIterm)from;
        bool hidden = tmpFront.HiddenNoAnimal();


        //if (hidden)
        //{

           
        //    ChangeBackCellPos(tmpFront.cellNumber);

        //}
    }







    public override void Update()
    {



        if (isPlaying)
        {


            playCount = 0;
            for (int i = 0; i < scolerIterms.Count; i++)
            {
                ScollerMoveIterm tmpItem = (ScollerMoveIterm)scolerIterms[i];

                tmpItem.Update();

                if (tmpItem.isPlaying)
                    playCount += 1;


            }


          // Debug.Log("animal isPlaying==" + playCount);
            cellListen();
            if (playCount == 0)
            {

                isPlaying = false;

                if (animalState == 3)
                {

                    DeleteChild(dismissCellIndex);

                }
                else if(animalState ==4)
                {

                     
                }
            }
                
        }

    }


}

