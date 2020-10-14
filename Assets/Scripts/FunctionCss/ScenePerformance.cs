using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePerformance : Inst<ScenePerformance>
{
    TableGameConfig config;
    WaitForSeconds coin, dollar;
    int coinMin, coinMax, dollarMin, dollarMax;
    GameObject ball;
    public void Init()
    {
        config = TableGameConfigList.inst.list[0];
        coin = new WaitForSeconds(config.SeniorCoinTime);
        dollar = new WaitForSeconds(config.SeniorDollarTime);
        //coin = new WaitForSeconds(5);
        //dollar = new WaitForSeconds(3);
        string[] strs = StringUtils.Str(config.SeniorCoinCount, '_');
        coinMin = int.Parse(strs[0]);
        coinMax = int.Parse(strs[1]);
        strs = StringUtils.Str(config.SeniorDollarCount, '_');
        dollarMin = int.Parse(strs[0]);
        dollarMax = int.Parse(strs[1]);

        StartCoroutine(StartSeniorCoin());
        StartCoroutine(StartSeniorDollar());
    }
    // Start is called before the first frame update
    void Start()
    {
        DelegateCenter.randomDropBall += BallRain;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 开始变高级金币
    /// </summary>
    /// <returns></returns>
    IEnumerator StartSeniorCoin()
    {
        yield return coin;
        int count = Random.Range(coinMin, coinMax);
        if (count != 0) ObstacleManager.Instance.UpgradCoin(count);
        StartCoroutine(StartSeniorCoin());
    }
    /// <summary>
    /// 开始变高级美金
    /// </summary>
    /// <returns></returns>
    IEnumerator StartSeniorDollar()
    {
        yield return dollar;
        int count = Random.Range(dollarMin, dollarMax);
        if (count != 0) ObstacleManager.Instance.UpgradDollar(count);
        StartCoroutine(StartSeniorDollar());
    }
    /// <summary>
    /// 球雨
    /// </summary>
    /// <param name="count"></param>
    public void BallRain(int count)
    {
        for (int i = 0; i < count; i++)
        {
            DelayComponent.Delay(100 * i, () =>
            {
                BallCtrl.Instance.FallBall(Random.Range(-350, 351));
            });
        }
    }
}
