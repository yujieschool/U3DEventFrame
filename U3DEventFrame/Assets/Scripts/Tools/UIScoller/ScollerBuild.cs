using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[ExecuteInEditMode]
[RequireComponent(typeof(ScollerManager))]
public class ScollerBuild : MonoBehaviour
{

    [SerializeField]
    private ScollerDirect direct = ScollerDirect.Vertical ;

    [SerializeField]
    public ScollerDirect ScollerDirect
    {
        get
        {

            return direct;
        }

        set
        {
         

            if (!Application.isPlaying)
            {
                direct = value;
                RefrushData();

                ScrollRect tmpRect = transform.GetComponent<ScrollRect>();

                if (direct == ScollerDirect.Horizontal)
                {
                    tmpRect.vertical = false;

                    tmpRect.horizontal = true;
                }
                  
                else
                {
                    tmpRect.vertical = true;

                    tmpRect.horizontal = false;
                }
            }
                
        }
    }

    private ScrollRect AddScollerRect(RectTransform scolelr)
    {
        ScrollRect tmpRect = scolelr.GetComponent<ScrollRect>();

        if (tmpRect == null)
        {
            tmpRect = scolelr.gameObject.AddComponent<ScrollRect>();
        }


        return tmpRect;

    }


    private void LeftTop(RectTransform  tmpRect)
    {
        tmpRect.anchorMin = new Vector2(0, 1);
        tmpRect.anchorMax = new Vector2(0, 1);

        tmpRect.pivot = new Vector2(0, 1);

       
    }

    private void RefrushData()
    {

        if (scollerTrans.childCount > 0)
        {
            RectTransform contentRect = scollerTrans.GetChild(0).GetComponent<RectTransform>();
            //  ScrollRect scolelr = scollerTrans.GetComponent<ScrollRect>();


            LeftTop(contentRect);


            float tmpY = scollerTrans.rect.yMax - scollerTrans.rect.yMin;


            float tmpX = scollerTrans.rect.xMax - scollerTrans.rect.xMin;


            

            contentRect.sizeDelta = new Vector2(tmpX, tmpY);

            Debug.Log("rect ==" + contentRect.sizeDelta);

       
            if (contentRect.childCount > 0)
            {
                RefushChild(contentRect);
            }
        }


     //   Debug.Log(contentRect.sizeDelta);


    }

    private void RefushChild(RectTransform  parent)
    {
        for (int i = 0; i < cellCount; i++)
        {
            RectTransform  tmpChild= parent.GetChild(i).GetComponent<RectTransform>();

            LeftTop(tmpChild);

         

            if (direct == ScollerDirect.Horizontal)
            {
              
                tmpChild.sizeDelta = new Vector2(parent.sizeDelta.x/(cellCount -2), parent.sizeDelta.y);
                tmpChild.anchoredPosition = new Vector2(parent.sizeDelta.x * (i-1) / (cellCount - 2), 0);

            }
            else if (direct == ScollerDirect.Vertical)
            {


                tmpChild.sizeDelta = new Vector2(parent.sizeDelta.x, parent.sizeDelta.y / (cellCount - 2));

                // tmpChild.sizeDelta = new Vector2(parent.sizeDelta.x , parent.sizeDelta.y/ (cellCount - 2));
                tmpChild.anchoredPosition = new Vector2(0, -parent.sizeDelta.y * (i-1) / (cellCount - 2));
            }
        }






    }

    private void AddContentChild(GameObject  parent)
    {

        RectTransform parentRect= parent.GetComponent<RectTransform>();
        for (int i = 0; i < cellCount; i++)
        {
            GameObject tmpChild = new GameObject();

            tmpChild.name = "Item" + i.ToString();
            tmpChild.transform.parent = parent.transform;
            RectTransform tmpRect= tmpChild.AddComponent<RectTransform>();

            tmpChild.transform.localScale = Vector3.one;
            tmpChild.AddComponent<Image>();

        }

        RefushChild(parentRect);
    }

    /// <summary>
    ///  第一步要建立一个scoller
    /// </summary>
    /// <param name="scolelr"></param>
    public  void  Builder(RectTransform scolelr)
    {

        ScrollRect scolerRect = AddScollerRect(scolelr);

        if (scolelr.childCount < 1)
        {

            GameObject tmpContent = new GameObject();

            tmpContent.name = "Grid";

            tmpContent.transform.parent = scolelr;


            tmpContent.transform.localScale = Vector3.one;

             RectTransform  tmpRect= tmpContent.AddComponent<RectTransform>();


            scolerRect.content = tmpRect;
            tmpRect.anchoredPosition = Vector2.zero;

            RefrushData();

            AddContentChild(tmpContent);

        }


    }


    RectTransform scollerTrans;

    private bool genMoveIterm = false;


    public bool GenMoveIterm
    {

        get
        {
            return genMoveIterm;
        }

        set
        {

            genMoveIterm = value;

            if (!Application.isPlaying && genMoveIterm)
            GenMoveCell();
        }

    }

    int cellCount = 5;

    private void GenMoveCell()
    {
        if (scollerTrans.childCount < 1)
        {
            Builder(scollerTrans);
        }


        Transform  grid = scollerTrans.GetChild(0);

        for (int i = 0; i < grid.childCount; i++)
        {

            Transform tmpCell = grid.GetChild(i);

            Transform sonPanel=  tmpCell.FindChild("SonPanel");

            if (sonPanel == null)
            {
                GameObject tmpSon = new GameObject();

                tmpSon.transform.parent = tmpCell;

                tmpSon.name = "SonPanel" ;
                RectTransform sonRect=    tmpSon.AddComponent<RectTransform>();

                tmpSon.AddComponent<Image>();

                LeftTop(sonRect);

                sonPanel = tmpSon.transform;



                GameObject tmpSonButton = new GameObject();

                tmpSonButton.transform.parent = tmpCell;

                

                tmpSonButton.name = "SonButton";

                tmpSonButton.AddComponent<Button>();
                RectTransform sonButtonRect = tmpSonButton.AddComponent<RectTransform>();


                



                LeftTop(sonButtonRect);

               

            }


            RectTransform  cellRect = tmpCell.GetComponent<RectTransform>();

           RectTransform  tmpSonRect = sonPanel.GetComponent<RectTransform>();

           tmpSonRect.sizeDelta = cellRect.sizeDelta;

        }


 
    }
    void Start()
    {
        if (!Application.isPlaying)
        {
            scollerTrans = gameObject.GetComponent<RectTransform>();
            Builder(scollerTrans);

         //   Debug.LogError("Start  scollerBuild");
        }

   
    }
}
