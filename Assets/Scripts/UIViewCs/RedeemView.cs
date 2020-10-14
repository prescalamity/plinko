using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedeemView : UIBase
{

    GameObject ReturnButton;
    GameObject StarItem;
    GameObject DollarItem;
    GameObject FruitsCashItem;
    GameObject StillNeedCOrD;
    Text StillNeedCOrDText;



    double RedeemLeastNeedCoinNum = 4000000d;
    double RedeemLeastNeedDollarNum = 100d;

    void Start()
    {

        initiateFruitsView();

        StillNeedCOrD = transform.Find("StillNeedCOrD").gameObject;
        StillNeedCOrDText = StillNeedCOrD.transform.GetComponentInChildren<Text>();

        string[] strs = StringUtils.Str(TableGameConfigList.inst.list[0].CoinTo, '_');
        RedeemLeastNeedCoinNum = double.Parse(strs[0]);

        strs = StringUtils.Str(TableGameConfigList.inst.list[0].DollarTo, '_');
        RedeemLeastNeedDollarNum = double.Parse(strs[0]);

        FruitsCashItem.transform.Find("NumberText").GetComponent<Text>().text =
            StringUtils.StringSplit("Fruits: ", FruitsModel.Instance.getHaveGotFruitsIdCount(), "/", FruitsModel.Instance.getFruitsCount());

        DollarItem.transform.Find("NumberText").GetComponent<Text>().text =
           StringUtils.StringSplit(GameControl.Instance.GetDollar().ToString());

        StarItem.transform.Find("NumberText").GetComponent<Text>().text =
           StringUtils.StringSplit(GameControl.Instance.GetCoin().ToString());
    }

    void initiateFruitsView()
    {
        ReturnButton = transform.Find("ReturnButton").gameObject;
        StarItem = transform.Find("StarItem").gameObject;
        DollarItem = transform.Find("DollarItem").gameObject;
        FruitsCashItem = transform.Find("FruitsCashItem").gameObject;

        EventTriggerListener.Get(ReturnButton).onClick = ReturnBtn;
        EventTriggerListener.Get(StarItem).onClick = StarItemBtn;
        EventTriggerListener.Get(DollarItem).onClick = DollarItemBtn;
        EventTriggerListener.Get(FruitsCashItem).onClick = FruitsCashItemBtn;

    }

    void ReturnBtn(GameObject obj)
    {

        Hide<RedeemView>();
    }

    void StarItemBtn(GameObject obj)
    {
        if (GameControl.Instance.GetCoin() < RedeemLeastNeedCoinNum)
        {
            StillNeedCOrDText.text = "You need " + (RedeemLeastNeedCoinNum - GameControl.Instance.GetCoin()) + " more gold coins.";
            StillNeedCOrD.SetActive(true);
            Invoke("hidePromptWindow", GameControl.Instance.promptDelayTime);
        }
        else
        {
            Show<TransactionsView>();
        }
        //Show<TransactionsView>();
    }

    void DollarItemBtn(GameObject obj)
    {
        if (GameControl.Instance.GetDollar() < RedeemLeastNeedDollarNum)
        {
            StillNeedCOrDText.text = "You need " + (RedeemLeastNeedDollarNum - GameControl.Instance.GetDollar()).ToString("0.00") + " more dollars.";
            StillNeedCOrD.SetActive(true);
            Invoke("hidePromptWindow", GameControl.Instance.promptDelayTime);
        }
        else
        {
            Show<TransactionsView>();
        }
    }

    void FruitsCashItemBtn(GameObject obj)
    {
        Show<FruitsView>();
    }

    void hidePromptWindow()
    {
        StillNeedCOrD.SetActive(false);
    }
}





