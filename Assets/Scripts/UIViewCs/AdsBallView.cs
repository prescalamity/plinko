using UnityEngine;
using UnityEngine.UI;

public class AdsBallView : UIBase
{
    string numberDes = "Get <color=#FFF20A>{0}</color> balls for free!";
    string countDes = "Today`s free chance(s):{0}";
    int _number, _count = 0;
    Text number, count;
    GameObject freeBtn;//免费按钮
    GameObject closeBtn;//关闭按钮
    // Start is called before the first frame update
    void Start()
    {
        TableGameConfig config = TableGameConfigList.inst.list[0];
        _number = config.AdsAddCallNumber;
        _count = config.AdsAddCallCount;
        number = transform.Find("Bg/NumberDes").GetComponent<Text>();
        count = transform.Find("Bg/Count").GetComponent<Text>();
        number.text = string.Format(numberDes, _number);
        count.text = string.Format(countDes, _count);

        freeBtn = transform.Find("Bg/FreeBtn").gameObject;
        closeBtn = transform.Find("Bg/CloseBtn").gameObject;
        EventTriggerListener.Get(freeBtn).onClick += FreeBtnOnClick;
        EventTriggerListener.Get(closeBtn).onClick += CloseBtnOnClick;
    }
    /// <summary>
    /// 免费按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void FreeBtnOnClick(GameObject gameObject)
    {
        //TODO 广告
        DelegateCenter.addBall(_count);
        Hide<AdsBallView>();
    }
    /// <summary>
    /// 关闭按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void CloseBtnOnClick(GameObject gameObject)
    {
        Hide<AdsBallView>();
    }
}
