using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : UIBase
{

    AudioSource BGMsource;
    Toggle bgmToggle;

    GameObject ReturnButton;

    void Start()
    {

        initiateFruitsView();





    }


    void initiateFruitsView()
    {
        ReturnButton = transform.Find("ReturnButton").gameObject;
        BGMsource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        bgmToggle = transform.Find("BGMToggle").GetComponent<Toggle>();

        EventTriggerListener.Get(ReturnButton).onClick = ReturnBtn;
        bgmToggle.onValueChanged.AddListener(BgmSourceChange);

    }

    private void BgmSourceChange(bool arg0)
    {
        if (bgmToggle.isOn) {
            BGMsource.Play();
        }
        else
        {
            BGMsource.Pause();
        }
    }

    private void ReturnBtn(GameObject go)
    {
        Hide<SettingsView>();
    }
}




