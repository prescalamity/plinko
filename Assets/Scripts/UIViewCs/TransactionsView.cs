using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class TransactionsView : UIBase
{

    GameObject ReturnButton;
    GameObject FailedToSend;
    GameObject RedeemImage;
    GameObject SendCodeImage;
    GameObject TransactionsEmail;
    GameObject TransactionsPhone;

    InputField emailInputField;
    InputField emailInputFieldAgain;
    InputField phoneInputField;

    Text FailedToSendtext;

    // Start is called before the first frame update
    void Start()
    {
        initiateTransactionsView();

        emailInputField = TransactionsEmail.transform.Find("InputField").GetComponent<InputField>();
        emailInputFieldAgain = TransactionsEmail.transform.Find("InputFieldAgain").GetComponent<InputField>();
        phoneInputField = TransactionsPhone.transform.Find("PhoneNumber/InputField").GetComponent<InputField>();
        FailedToSendtext = FailedToSend.transform.GetComponentInChildren<Text>();
    }


    void initiateTransactionsView()
    {
        ReturnButton = transform.Find("ReturnButton").gameObject;
        FailedToSend = transform.Find("FailedToSend").gameObject;
        RedeemImage = transform.Find("TransactionsEmail/RedeemImage").gameObject;
        SendCodeImage = transform.Find("TransactionsPhone/SendCodeImage").gameObject;
        TransactionsEmail = transform.Find("TransactionsEmail").gameObject;
        TransactionsPhone = transform.Find("TransactionsPhone").gameObject;

        EventTriggerListener.Get(ReturnButton).onClick = ReturnBtn;
        EventTriggerListener.Get(RedeemImage).onClick = RedeemImageBtn;
        EventTriggerListener.Get(SendCodeImage).onClick = SendCodeImageBtn;
    }

    private void SendCodeImageBtn(GameObject go)
    {
        FailedToSendtext.text = " Failed to send the verification code! \n Please try again!";
        FailedToSend.SetActive(true);
        Invoke("hideFailedToSend", GameControl.Instance.promptDelayTime);
    }

    private void RedeemImageBtn(GameObject go)
    {
        if ( IsEmail(emailInputField.text) && emailInputField.text.Equals(emailInputFieldAgain.text))
        {
            TransactionsEmail.SetActive(false);
            TransactionsPhone.SetActive(true);
        }
        else
        {
            FailedToSendtext.text = "Please enter the correct and same email addresses.";
            FailedToSend.SetActive(true);
            Invoke("hideFailedToSend", GameControl.Instance.promptDelayTime);
        }
        
    }

    void ReturnBtn(GameObject obj)
    {
        Hide<TransactionsView>();

        emailInputField.text = null;
        emailInputFieldAgain.text = null;
        phoneInputField.text = null;

        TransactionsEmail.SetActive(true);
        TransactionsPhone.SetActive(false);
    }

    void hideFailedToSend()
    {
        FailedToSend.SetActive(false);  
    }

    /// <summary>
    /// 判断字符串是否为电子邮件
    /// </summary>
    /// <param name="EmailString"></param>
    /// <returns></returns>
    public bool IsEmail(string EmailString)
    {
        string strRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        if (Regex.IsMatch(EmailString, strRegex))
        {
            print(EmailString + "输入的电子邮件格式正确！");
            return true;
        }
        else
        {
            print(EmailString + "输入的电子邮件格式不正确！如(sina@sina.cn)。");
            return false;
        }

    }
}







