using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitsView : UIBase
{
    GameObject ReturnImage;
    GameObject ClaimDollar;
    GameObject NotGetFruitsNumber;
    Text yourFruitsText;
    Text NotGetFruitsNumbertext;

    void Start()
    {


        initiateFruitsView();

        yourFruitsText = transform.Find("YourFruits").GetComponentInChildren<Text>();
        NotGetFruitsNumbertext = NotGetFruitsNumber.transform.GetComponentInChildren<Text>();
        refreshFruitsViewPanel();
    }

    void initiateFruitsView()
    {
        ReturnImage = transform.Find("ReturnImage").gameObject;
        ClaimDollar = transform.Find("ClaimDollar").gameObject;
        NotGetFruitsNumber = transform.Find("NotGetFruitsNumber").gameObject;

        EventTriggerListener.Get(ReturnImage).onClick = ReturnBtn;
        EventTriggerListener.Get(ClaimDollar).onClick = ClaimDollarBtn;
    }

    public void refreshFruitsViewPanel()
    {
        int idIndex = 0;
        int number;
        int collectedNum=0;
        foreach (Text  textNum in transform.Find("FruitsPanel").GetComponentsInChildren<Text>())
        {
            if (idIndex >= FruitsModel.Instance.getFruitsCount()) break;

            number = FruitsModel.Instance.getFruitsNumberById(idIndex); 
            if (number > 0) {
                textNum.gameObject.transform.parent.Find("MaskImage").gameObject.SetActive(false);
                collectedNum++;
            }
            textNum.text = number.ToString();

            idIndex++;
        }

        yourFruitsText.text = "Your Fruits:  " + collectedNum + "/" + FruitsModel.Instance.getFruitsCount();


    }
   

    void ReturnBtn(GameObject obj)
    {
        Hide<FruitsView>();
    }


    void ClaimDollarBtn(GameObject obj)
    {
        if (FruitsModel.Instance.getNotGetFruitsIdCount()>0)
        {
            NotGetFruitsNumber.SetActive(true);
            NotGetFruitsNumbertext.text = StringUtils.StringSplit( "You still need "+ FruitsModel.Instance.getNotGetFruitsIdCount() + " type of fruits.");
            Invoke("hideNotGetFruitsNumber", GameControl.Instance.promptDelayTime);
        }
       
    }

    void hideNotGetFruitsNumber()
    {
        NotGetFruitsNumber.SetActive(false);
    }

}





