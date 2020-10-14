using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCtrl : Inst<BallCtrl>
{
    string countDownDes = "Next in {0}";
    Text countDown;
    int cd = 0;//球恢复cd
    int upperLimit = 0;//球上限
    int ballCount = 0;//球数量
    float _time = 0;//计时用
    int totalTime = 0;
    bool isTime = false;//倒计时状态机

    int superBallCount = 0;//弹力增强数量
    Transform balls;
    float force, _force = 0;//默认弹力和增强弹力

   
    public void Init(Text text, Transform transform)
    {
        balls = transform;
        countDown = text;
        countDown.text = "";
        countDown.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        TableGameConfig config = TableGameConfigList.inst.list[0];
        TableBuff buff = TableBuffList.inst.list[6];//弹力增强buff
        cd = config.BallTime;
        //cd = 10;
        upperLimit = config.BallUpperLimit;
        force = (float)config.ElasticForce;
        _force = (float)buff.ElasticForce;

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
                totalTime--;
                if (totalTime == 0)//恢复一颗球
                {
                    DelegateCenter.addBall(1);
                    isTime = false;
                    RecoveryBall();
                }
                else
                {
                    string str = TimeHelper.TimeText(totalTime, true);
                    countDown.text = string.Format(countDownDes, str);
                }
            }
        }
    }
    /// <summary>
    /// 恢复球
    /// </summary>
    public void RecoveryBall()
    {
        if (isTime) return;
        ballCount = GameControl.Instance.GetBallCount();
        if (ballCount >= upperLimit)
        {
            countDown.gameObject.SetActive(false);
        }
        else
        {
            StartTime();
            countDown.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 开始计时
    /// </summary>
    void StartTime()
    {
        _time = 0;
        totalTime = cd;
        isTime = true;
        string str = TimeHelper.TimeText(totalTime, true);
        countDown.text = string.Format(countDownDes, str);
    }
    /// <summary>
    /// 停止计时恢复球数量（超过计时恢复上限的话）
    /// </summary>
    public void StopTime()
    {
        ballCount = GameControl.Instance.GetBallCount();
        if (ballCount >= upperLimit)
        {
            isTime = false;
            countDown.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 设置弹力增强数量
    /// </summary>
    /// <param name="count">数量</param>
    public void SetSuperBallCount(int count)
    {
        superBallCount = count;
    }
    GameObject ball;
    /// <summary>
    /// 落下球
    /// </summary>
    /// <param name="x"></param>
    public void FallBall(float x)
    {
        ball = PrefabManager.Instance.New("Ball");
        ball.transform.SetParent(balls.transform);
        ball.transform.localScale = Vector3.one;
        ball.transform.localPosition = new Vector3(x, 750, 0);
        Ball _ball = ball.GetComponent<Ball>();
        if (superBallCount > 0)
        {
            _ball.SetElasticForce(_force);
            superBallCount--;
        }
        else
        {
            _ball.SetElasticForce(force);
        }
        _ball.IsAdd = true;

    }
}
