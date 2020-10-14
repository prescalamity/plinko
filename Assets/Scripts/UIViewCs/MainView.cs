using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainView : UIBase
{
    GameObject uis;//ui父级
    GameObject obstacles;//障碍父级
    GameObject bottles;//瓶子父级
    GameObject balls;//球父级
    RectTransform parentRectTrans;
    GameObject ballDropBtn;//球掉落按钮
    Transform tipss;//提示父级

    GameObject menuBtn;//菜单按钮
    GameObject soundBtn;//声音按钮
    GameObject fruitsBtn;//水果按钮
    GameObject addBallBtn;//加球按钮
    GameObject coinBtn;//金币按钮
    GameObject dollarBtn;//美金按钮
    Text fruitsCountText;//水果收集显示
    Text ballCountText;//球数量显示
    Text coinText;//金币显示
    Text dollarText;//美金显示

    Text countDown;//球恢复倒计时

    GameObject buffBtn;//buff按钮
    Image buffIcon;//buffIcon
    Text buffDes;//buff描述
    Text buffCountDown;//buff倒计时

    Image soundBtnImage;

    List<Image> bottleIcons = new List<Image>();//4个瓶子Icon
    List<Text> bottleCounts = new List<Text>();//4个瓶子奖励数量
    string textDes = "";//用于赋值
    int ballCount = 0;//用于判断能否掉球

    int delayTime = 60;
    //int delayTime = 3;
    float targetTime = 60, time = 0, _time = 0;
    bool isTime = false;
    // Start is called before the first frame update
    void Start()
    {
        GoInit();
        SetSoundBtnImage();
        ObstacleManager.Instance.Init(obstacles);

        DelegateCenter.upDateMoney += UpdateMoney;
        DelegateCenter.updateBallCount += UpdateBallCount;
        DelegateCenter.updateBottle += UpdateBottle;
        DelegateCenter.adsBuff += EndBuff;
        DelegateCenter.updateFruit += SetFruitDes;

        AudioManager.Instance.Init();
        GameControl.Instance.Init();
        BottleManager.Instance.Init(tipss);
        TipsManager.Instance.Init(tipss);
        BallCtrl.Instance.Init(countDown, balls.transform);
        ScenePerformance.Instance.Init();
        BuffCtrl.Instance.Init();

        //StartBuff();
        DelayComponent.Delay(10, () => { StartBuff(); SetFruitDes(); });
    }
    void GoInit()
    {
        uis = transform.Find("UIs").gameObject;
        obstacles = transform.Find("Obstacles").gameObject;
        bottles = transform.Find("Buckets").gameObject;
        balls = transform.Find("Balls").gameObject;
        parentRectTrans = balls.GetComponent<RectTransform>();
        ballDropBtn = uis.transform.Find("BallDropBtn").gameObject;
        EventTriggerListener.Get(ballDropBtn).onClick = BallDropBtnOnClick;
        tipss = uis.transform.Find("Tipss");

        menuBtn = uis.transform.Find("MenuBtn").gameObject;
        soundBtn = uis.transform.Find("SoundBtn").gameObject;
        soundBtnImage = soundBtn.GetComponent<Image>();
        fruitsBtn = uis.transform.Find("FruitsBtn").gameObject;
        addBallBtn = uis.transform.Find("BallCount/AddBallBtn").gameObject;
        coinBtn = uis.transform.Find("CoinBtn").gameObject;
        dollarBtn = uis.transform.Find("DollarBtn").gameObject;
        fruitsCountText = fruitsBtn.transform.Find("FruitsCountText").GetComponent<Text>();
        ballCountText = uis.transform.Find("BallCount/BallCountText").GetComponent<Text>();
        coinText = coinBtn.transform.Find("CoinText").GetComponent<Text>();
        dollarText = dollarBtn.transform.Find("DollarText").GetComponent<Text>();

        EventTriggerListener.Get(menuBtn).onClick = MenuBtnOnClick;
        EventTriggerListener.Get(soundBtn).onClick = SoundBtnOnClick;
        EventTriggerListener.Get(fruitsBtn).onClick = FruitsBtnOnClick;
        EventTriggerListener.Get(addBallBtn).onClick = AddBallBtnOnClick;
        EventTriggerListener.Get(coinBtn).onClick = OpenExchangeOnClick;
        EventTriggerListener.Get(dollarBtn).onClick = OpenExchangeOnClick;

        countDown = uis.transform.Find("BallCount/Countdown").GetComponent<Text>();

        for (int i = 1; i < 5; i++)
        {
            Transform tra = bottles.transform.Find("Bucket" + i);
            Image image = tra.Find("Bottom" + i + "/Icon").GetComponent<Image>();
            Text text = tra.Find("Bottom" + i + "/CountText").GetComponent<Text>();
            bottleIcons.Add(image);
            bottleCounts.Add(text);
        }

        buffBtn = uis.transform.Find("BuffBtn").gameObject;
        buffIcon = buffBtn.GetComponent<Image>();
        buffDes = buffBtn.transform.Find("Des").GetComponent<Text>();
        buffCountDown = buffBtn.transform.Find("CountDown").GetComponent<Text>();
        EventTriggerListener.Get(buffBtn).onClick = BuffBtnOnClick;
    }
    /// <summary>
    /// 开始buff按钮动画循环
    /// </summary>
    void StartBuff()
    {
        TableBuff buff = BuffCtrl.Instance.GetBuff();
        buffIcon.sprite = SpriteAtlasManager.Instance.GetSprite(buff.Icon, "AdsBallView");
        //buffIcon.sprite = SpriteAtlasManager.Instance.GetSprite("home_ic_increase@2x");
        buffDes.text = buff.BuffName;
        time = targetTime;
        _time = 0;
        buffCountDown.text = StringUtils.StringSplit(time, "s");
        buffBtn.transform.DOLocalMoveX(313, 0.5f).OnComplete(() =>
        {
            isTime = true;
        });
        Debug.Log("开始buff");
    }
    /// <summary>
    /// 结束buff倒计时
    /// </summary>
    void EndBuff()
    {
        isTime = false;
        buffBtn.transform.DOLocalMoveX(595, 0.5f).OnComplete(() =>
        {
            DelayComponent.Delay(delayTime * 1000, () =>
            {
                StartBuff();
            });
        });
        Debug.Log("结束buff");
    }
    // Update is called once per frame
    void Update()
    {
        if (isTime)
        {
            _time += Time.deltaTime;
            if (_time >= 1)
            {
                _time = 0;
                time--;
                buffCountDown.text = StringUtils.StringSplit(time, "s");
                if (time == 0)
                {
                    EndBuff();
                }
            }
        }
    }
    /// <summary>
    /// 球掉落按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void BallDropBtnOnClick(GameObject gameObject)
    {
        ballCount = GameControl.Instance.GetBallCount();
        if (ballCount > 0)
        {
            Vector2 vecMouse = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTrans, Input.mousePosition, Camera.main, out vecMouse);
            float x = vecMouse.x;
            BallCtrl.Instance.FallBall(x);
            DelegateCenter.addBall(-1);
            BallCtrl.Instance.RecoveryBall();
        }
        else
        {
            Show<AdsBallView>();
        }
    }
    /// <summary>
    /// 菜单按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void MenuBtnOnClick(GameObject gameObject)
    {
        Show<MenuView>();
    }
    /// <summary>
    /// 声音按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void SoundBtnOnClick(GameObject gameObject)
    {
        if (GameData.Sound == 0)
        {
            GameData.Sound = 1;//开启声音
        }
        else
        {
            GameData.Sound = 0;//关闭声音
        }
        SetSoundBtnImage();
    }
    /// <summary>
    /// 更换声音按钮贴图
    /// </summary>
    void SetSoundBtnImage()
    {
        if (GameData.Sound==1)
        {
            soundBtnImage.sprite = SpriteAtlasManager.Instance.GetSprite("home_ic_voice_on@2x");
        }
        else
        {
            soundBtnImage.sprite = SpriteAtlasManager.Instance.GetSprite("home_ic_voice_off@2x");
        }
    }
    /// <summary>
    /// 水果按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void FruitsBtnOnClick(GameObject gameObject)
    {
        Show<FruitsView>();
    }
    /// <summary>
    /// 加球按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void AddBallBtnOnClick(GameObject gameObject)
    {
        Show<AdsBallView>();
    }
    /// <summary>
    /// 打开兑换面板事件
    /// </summary>
    /// <param name="gameObject"></param>
    void OpenExchangeOnClick(GameObject gameObject)
    {
        Show<RedeemView>();
    }
    /// <summary>
    /// buff按钮事件
    /// </summary>
    /// <param name="gameObject"></param>
    void BuffBtnOnClick(GameObject gameObject)
    {
        Show<AdsBuffView>();
    }
    /// <summary>
    /// 更新货币
    /// </summary>
    /// <param name="moneyType">货币类型</param>
    void UpdateMoney(MoneyType moneyType)
    {
        textDes = GameControl.Instance.GetMoneyDes(moneyType);
        switch (moneyType)
        {
            case MoneyType.coin:
                coinText.text = textDes;
                break;
            case MoneyType.dollar:
                dollarText.text = textDes;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 更新球个数
    /// </summary>
    /// <param name="ballCount">球数量</param>
    void UpdateBallCount(int ballCount)
    {
        ballCountText.text = ballCount.ToString();
    }
    Image _image = null;
    Text _text = null;
    Sprite _sprite = null;
    /// <summary>
    /// 更新瓶子奖励
    /// </summary>
    /// <param name="pos">-1更行全部，0123</param>
    void UpdateBottle(int pos)
    {
        if (pos == -1)
        {
            for (int i = 0; i < bottleCounts.Count; i++)
            {
                SetBoottle(i);
            }
        }
        else
        {
            SetBoottle(pos);
        }
    }
    Sprite _Icon;
    /// <summary>
    /// 设置瓶子奖励
    /// </summary>
    /// <param name="pos"></param>
    void SetBoottle(int pos)
    {
        BR br = BottleManager.Instance.GetBottleReward(pos);
        _image = bottleIcons[pos];
        _text = bottleCounts[pos];
        switch (br.bottleRewardType)
        {
            case BottleRewardType.Coin:
                _Icon = SpriteAtlasManager.Instance.GetSprite("yellowball@2x");
                break;
            case BottleRewardType.Dollar:
                _Icon = SpriteAtlasManager.Instance.GetSprite("greenball@2x");
                break;
            case BottleRewardType.Fruit:
                _Icon = SpriteAtlasManager.Instance.GetSprite("home_top_Fruits_number@2x");
                break;
            case BottleRewardType.Ball:
                _Icon = SpriteAtlasManager.Instance.GetSprite("redball@2x");
                break;
            case BottleRewardType.DollarObstacle:
                _Icon = SpriteAtlasManager.Instance.GetSprite("home_blackball@2x");
                break;
            default:
                break;
        }
        _sprite = _Icon;
        _image.sprite = _sprite;
        _text.text = (br.count).ToString();
    }
    /// <summary>
    /// 更新水果收集进度
    /// </summary>
    void SetFruitDes()
    {
        string fruitDes = FruitsModel.Instance.getRateOfProgress();
        fruitsCountText.text = fruitDes;
    }
}
