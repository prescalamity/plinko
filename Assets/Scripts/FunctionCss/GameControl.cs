using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏控制器
/// </summary>
public class GameControl : Inst<GameControl>
{
    /// <summary>
    /// 提示窗口的延迟消失时间
    /// </summary>
    public float promptDelayTime = 2f;
    /// <summary>
    /// 金币
    /// </summary>
    double coin = 0;
    /// <summary>
    /// 美金
    /// </summary>
    float dollar = 0;
    /// <summary>
    /// 球个数
    /// </summary>
    int ballCount = 0;
    /// <summary>
    /// 极限美金
    /// </summary>
    int limitDollar = 0;
    /// <summary>
    /// 是否超过极限
    /// </summary>
    bool isLimit = false;
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if (GameData.NewGame == 0)
        {
            GameData.BallCount = TableGameConfigList.inst.list[0].BallInitialCount;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        coin = double.Parse(GameData.Coin);
        dollar = GameData.Dollar;
        ballCount = GameData.BallCount;
        limitDollar = TableGameConfigList.inst.list[0].LimitDollar;
        //limitDollar = 10;
        isLimit = dollar >= limitDollar;

        DelegateCenter.addCoin += AddCoin;
        DelegateCenter.addDollar += AddDollar;
        DelegateCenter.addBall += AddBall;

        DelegateCenter.upDateMoney(MoneyType.coin);
        DelegateCenter.upDateMoney(MoneyType.dollar);
        DelegateCenter.updateBallCount(ballCount);
        DebugTool.AddButton("收益翻倍60秒", () =>
        {
            BuffCtrl.Instance.AdsX2();
        });
        DebugTool.AddButton("弹力增强", () =>
        {
            BuffCtrl.Instance.AdsBallElasticForce();
        });
        DebugTool.AddButton("球雨20", () =>
        {
            //ScenePerformance.Instance.BallRain(20);
            BuffCtrl.Instance.AdsBallRain();
        });
        DebugTool.AddButton("老虎机入口变慢", () =>
        {
            BuffCtrl.Instance.AdsMove();
        });
        DebugTool.AddButton("刷新20个金币或美金", () =>
        {
            BuffCtrl.Instance.AdsUpdateObs();
        });
        DebugTool.AddButton("加炸弹", () =>
        {
            ObstacleManager.Instance.AddBomb();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)//退后台保存数据
        {
            GameData.Coin = coin.ToString();
            GameData.Dollar = dollar;
            GameData.BallCount = ballCount;

            BottleManager.Instance.SaveData();
            ObstacleManager.Instance.SaveData();
        }
    }
    /// <summary>
    /// 加减金币
    /// </summary>
    /// <param name="coin">正负金币数</param>
    void AddCoin(double coin)
    {
        this.coin += coin;
        DelegateCenter.upDateMoney(MoneyType.coin);
    }
    /// <summary>
    /// 加减美金
    /// </summary>
    /// <param name="dollar">正负美金数</param>
    void AddDollar(double dollar)
    {
        this.dollar += (float)dollar;
        isLimit = this.dollar >= limitDollar;
        DelegateCenter.upDateMoney(MoneyType.dollar);
    }
    /// <summary>
    /// 加减球
    /// </summary>
    /// <param name="count"></param>
    void AddBall(int count)
    {
        this.ballCount += count;
        DelegateCenter.updateBallCount(ballCount);
        if (count > 0)
        {
            BallCtrl.Instance.StopTime();
        }
    }
    /// <summary>
    /// 获取货币描述
    /// </summary>
    /// <param name="moneyType">货币类型</param>
    /// <returns></returns>
    public string GetMoneyDes(MoneyType moneyType)
    {
        string str = "";
        switch (moneyType)
        {
            case MoneyType.coin:
                str = string.Format("{0:N0}", coin);
                break;
            case MoneyType.dollar:
                str = dollar.ToString("F2");
                break;
            default:
                break;
        }
        return str;
    }
    /// <summary>
    /// 获取球数量
    /// </summary>
    /// <returns></returns>
    public int GetBallCount()
    {
        return ballCount;
    }
    /// <summary>
    /// 获取金币
    /// </summary>
    /// <returns></returns>
    public double GetCoin()
    {
        return coin;
    }
    /// <summary>
    /// 获取美金
    /// </summary>
    /// <returns></returns>
    public float GetDollar()
    {
        return dollar;
    }
    /// <summary>
    /// 是否超过极限（true不再出美金相关）
    /// </summary>
    /// <returns></returns>
    public bool IsLimit()
    {
        return isLimit;
    }
}
