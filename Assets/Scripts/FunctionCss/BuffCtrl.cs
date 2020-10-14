using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCtrl : Inst<BuffCtrl>
{
    /// <summary>
    /// 是否收益翻倍
    /// </summary>
    bool x2 = false;
    /// <summary>
    /// 是否收益翻倍
    /// </summary>
    public bool X2 { get => x2; set => x2 = value; }
    /// <summary>
    /// 广告60收益倍率/老虎机入口移动倍率
    /// </summary>
    double multiple, moveMultiple = 1;
    /// <summary>
    /// 收益翻倍时间/老虎机移动时间
    /// </summary>
    int multipleTime, moveTime = 0;
    /// <summary>
    /// 广告增加弹力球的个数
    /// </summary>
    double elasticForceCount = 0;
    /// <summary>
    /// 掉落球数
    /// </summary>
    int ballRainCount = 0;
    /// <summary>
    /// 广告刷新金币或美金障碍数量
    /// </summary>
    int adsObsCount = 0;

    List<TableBuff> tableBuffs = new List<TableBuff>();
    int buffIndex = 0;
    TableBuff currentBuff = null;
    public void Init()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        tableBuffs = TableBuffList.inst.list;
        multiple = tableBuffs[0].Value;
        moveMultiple = tableBuffs[7].Value;
        multipleTime = tableBuffs[0].Time;
        moveTime = tableBuffs[7].Time;
        elasticForceCount = tableBuffs[6].Value;
        ballRainCount = (int)tableBuffs[3].Value;
        adsObsCount = (int)tableBuffs[1].Value;

        //DelegateCenter.startBuff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 获取广告翻倍倍率
    /// </summary>
    /// <returns></returns>
    public double GetMultiple()
    {
        return multiple;
    }
    /// <summary>
    /// 获取buff
    /// </summary>
    /// <returns></returns>
    public TableBuff GetBuff()
    {
        currentBuff = tableBuffs[buffIndex];
        buffIndex++;
        if (buffIndex == tableBuffs.Count)
        {
            buffIndex = 0;
        }
        return currentBuff;
    }
    /// <summary>
    /// 获取当前点击buff
    /// </summary>
    /// <returns></returns>
    public TableBuff GetCurrentBuff()
    {
        return currentBuff;
    }

    //-------------------------------以下是广告回调

    /// <summary>
    /// 广告收益翻倍
    /// </summary>
    public void AdsX2()
    {
        X2 = true;
        DelayComponent.Delay(multipleTime * 1000, () => {
            X2 = false;
        });
    }
    /// <summary>
    /// 广告加球弹力
    /// </summary>
    public void AdsBallElasticForce()
    {
        BallCtrl.Instance.SetSuperBallCount((int)elasticForceCount);
    }
    /// <summary>
    /// 广告老虎机入口变慢
    /// </summary>
    public void AdsMove()
    {
        MoveBucket.Instance.SetSpeed((float)moveMultiple);
        DelayComponent.Delay(moveTime * 1000, () => {
            MoveBucket.Instance.SetSpeed(1);
        });
    }
    /// <summary>
    /// 广告刷新全部黑色阻挡为金币美金
    /// </summary>
    public void AdsAllObs()
    {
        ObstacleManager.Instance.AllObsTo();
    }
    /// <summary>
    /// 广告掉落球
    /// </summary>
    public void AdsBallRain()
    {
        ScenePerformance.Instance.BallRain(ballRainCount);
    }
    /// <summary>
    /// 老虎机开始
    /// </summary>
    public void AdsLHJ()
    {
        DelegateCenter.slotMachineStart();
    }
    /// <summary>
    /// 广告加炸弹
    /// </summary>
    public void AdsBomb()
    {
        ObstacleManager.Instance.AddBomb();
    }
    /// <summary>
    /// 广告刷新数量金币美金障碍
    /// </summary>
    public void AdsUpdateObs()
    {
        ObstacleManager.Instance.ObsToCoinOrDollar(adsObsCount);
    }
    /// <summary>
    /// 广告
    /// </summary>
    public void Ads()
    {
        switch ((BuffType)currentBuff.Type)
        {
            case BuffType.Moneyx2:
                AdsX2();
                break;
            case BuffType.UpdateObs:
                AdsUpdateObs();
                break;
            case BuffType.SlotMachine:
                AdsLHJ();
                break;
            case BuffType.BallRain:
                AdsBallRain();
                break;
            case BuffType.CleanObs:
                AdsAllObs();
                break;
            case BuffType.Bomb:
                AdsBomb();
                break;
            case BuffType.ElasticForce:
                AdsBallElasticForce();
                break;
            case BuffType.SlotMachineSlowDown:
                AdsMove();
                break;
            default:
                break;
        }
        DelegateCenter.adsBuff();
    }
}
public enum BuffType
{
    /// <summary>
    /// 收益x2
    /// </summary>
    Moneyx2=1,
    /// <summary>
    /// 刷新阻挡
    /// </summary>
    UpdateObs,
    /// <summary>
    /// 老虎机
    /// </summary>
    SlotMachine,
    /// <summary>
    /// 球雨
    /// </summary>
    BallRain,
    /// <summary>
    /// 清除所有黑色障碍
    /// </summary>
    CleanObs,
    /// <summary>
    /// 炸弹
    /// </summary>
    Bomb,
    /// <summary>
    /// 弹力增强
    /// </summary>
    ElasticForce,
    /// <summary>
    /// 老虎机入口减速
    /// </summary>
    SlotMachineSlowDown
}