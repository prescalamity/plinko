using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdsBuffView : UIBase
{
    GameObject closeBtn;
    GameObject adsBtn;
    Image icon;
    Text title, des ;
    TableBuff buff = null;
    // Start is called before the first frame update
    void Awake()
    {
        closeBtn = transform.Find("Bg/CloseBtn").gameObject;
        adsBtn = transform.Find("Bg/FreeBtn").gameObject;

        EventTriggerListener.Get(closeBtn).onClick = CloseBtnOnClick;
        EventTriggerListener.Get(adsBtn).onClick = AdsBtnOnClick;

        icon = transform.Find("Bg/Image").GetComponent<Image>();

        title = transform.Find("Bg/Title").GetComponent<Text>();
        des = transform.Find("Bg/Des").GetComponent<Text>();
    }
    private void OnEnable()
    {
        buff = BuffCtrl.Instance.GetCurrentBuff();
        Debug.LogError("----" + buff == null + "       " + buff.Icon);
        icon.sprite = SpriteAtlasManager.Instance.GetSprite(buff.Icon, "AdsBallView");
        title.text = buff.BuffName;
        des.text = buff.BuffDes;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 关闭按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void CloseBtnOnClick(GameObject gameObject)
    {
        Hide<AdsBuffView>();
    }
    /// <summary>
    /// 广告按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void AdsBtnOnClick(GameObject gameObject)
    {
        BuffCtrl.Instance.Ads();
        Hide<AdsBuffView>();
    }
}
