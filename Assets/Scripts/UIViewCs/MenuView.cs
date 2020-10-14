using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : UIBase
{
    GameObject ReturnButton;
    GameObject RedeemItem;
    GameObject FruitsItem;
    GameObject SettingsItem;
    GameObject ContactUsItem;

    void Start()
    {
        initiateMenuView();
       
        EventTriggerListener.Get(ReturnButton).onClick = ReturnBtn;
        EventTriggerListener.Get(RedeemItem).onClick = RedeemItemButton;
        EventTriggerListener.Get(FruitsItem).onClick = FruitsItemButton;
        EventTriggerListener.Get(SettingsItem).onClick = SettingsItemButton;
        EventTriggerListener.Get(ContactUsItem).onClick = ContactUsItemButton;
    }

    void initiateMenuView()
    {
        ReturnButton = transform.Find("ReturnButton").gameObject;
        RedeemItem = transform.Find("RedeemItem").gameObject;
        FruitsItem = transform.Find("FruitsItem").gameObject;
        SettingsItem = transform.Find("SettingsItem").gameObject;
        ContactUsItem = transform.Find("ContactUsItem").gameObject;
    }

    void ReturnBtn(GameObject obj)
    {
        Hide <MenuView> ();
    }

    /// <summary>
    /// 提现按钮
    /// </summary>
    /// <param name="obj"></param>
    void RedeemItemButton(GameObject obj)
    {
        Show<RedeemView>();
        Debug.Log("提现，，，");
    }

    /// <summary>
    /// Fruits Item Button
    /// </summary>
    /// <param name="obj"></param>
    void FruitsItemButton(GameObject obj)
    {
        //sHide<MenuView>();
        Show<FruitsView>();
    }
    void SettingsItemButton(GameObject obj)
    {
        Show<SettingsView>();
        //Debug.Log("设置按钮，，，");
    }
    void ContactUsItemButton(GameObject obj)
    {
        Debug.Log("联系我们，，，");
    }



}


