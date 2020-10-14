using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 瓶子奖励管理
/// </summary>
public class BottleManager : Inst<BottleManager>
{
    /// <summary>
    /// 表数据
    /// </summary>
    List<TableBottle> tableBottles = new List<TableBottle>();
    List<BR> bRs = new List<BR>();
    int totalWeight = 0, weight = 0, _3Weight = 0;
    string _name = "BottleReward";
    /// <summary>
    /// 提示动画父级
    /// </summary>
    Transform tipss = null;
    public void Init(Transform transform)
    {
        tipss = transform;
        tableBottles = TableBottleList.inst.list;
        foreach (var item in tableBottles)
        {
            totalWeight += item.Weight;
            if (item.Type != 2 && item.Type != 5)
            {
                _3Weight += item.Weight;
            }
        }
        if (bRs.Count == 0)
        {
            if (GameData.BRs.Equals("") || GameData.BRs.Equals("[]"))
            {
                for (int i = 0; i < 4; i++)
                {
                    BR bR = GetNewBr();
                    bRs.Add(bR);
                }
            }
            else
            {
                bRs = JsonHelper.FromJson<List<BR>>(GameData.BRs);
            }
        }
        DelegateCenter.updateBottle(-1);
    }
    /// <summary>
    /// 获取新奖励
    /// </summary>
    /// <returns></returns>
    BR GetNewBr()
    {
        TableBottle br = GetRewardId();
        BR bR = new BR
        {
            id = br.id,
            bottleRewardType = (BottleRewardType)br.Type,
            count = br.Count,
        };
        if (bR.bottleRewardType == BottleRewardType.Fruit)
        {
            int id = FruitsModel.Instance.getFruitsRewardId();
            bR.fruitsId = id;
        }
        return bR;
    }
    /// <summary>
    /// 保存数据
    /// </summary>
    public void SaveData()
    {
        GameData.BRs = JsonHelper.ToJson(bRs);
    }
    /// <summary>
    /// 随机瓶子奖励
    /// </summary>
    /// <returns></returns>
    TableBottle GetRewardId()
    {
        TableBottle br = null;
        weight = 0;
        int index = 0;
        if (GameControl.Instance.IsLimit())
        {
            weight = 0;
            index = Random.Range(0, _3Weight);
            for (int i = 0; i < tableBottles.Count; i++)
            {
                TableBottle tableBottle = tableBottles[i];
                if (tableBottle.Type != 2 && tableBottle.Type != 5)
                {
                    weight += tableBottle.Weight;
                    if (index < weight)
                    {
                        br = tableBottle;
                        break;
                    }
                }
            }
        }
        else
        {
            weight = 0;
            index = Random.Range(0, totalWeight);
            for (int i = 0; i < tableBottles.Count; i++)
            {
                TableBottle tableBottle = tableBottles[i];
                weight += tableBottle.Weight;
                if (index < weight)
                {
                    br = tableBottle;
                    break;
                }
            }
        }
        return br;
    }
    /// <summary>
    /// 获取瓶子奖励对象
    /// </summary>
    /// <param name="pos">位置0123</param>
    /// <returns></returns>
    public BR GetBottleReward(int pos)
    {
        return bRs[pos];
    }
    /// <summary>
    /// 掉落瓶子奖励
    /// </summary>
    /// <param name="pos">瓶子位置0123</param>
    /// <param name="transform">碰撞的父级位置</param>
    public void Reward(int pos, Transform transform)
    {
        BR bR = GetBottleReward(pos);
        bRs[pos] = GetNewBr();//刷新奖励
        DelegateCenter.updateBottle(pos);
        PlayIconAnination(transform.localPosition.x);
        PlayTextAnination(bR, transform);
        switch (bR.bottleRewardType)
        {
            case BottleRewardType.Coin:
                DelegateCenter.addCoin(bR.count);
                break;
            case BottleRewardType.Dollar:
                DelegateCenter.addDollar(bR.count);
                break;
            case BottleRewardType.Fruit:
                FruitsModel.Instance.addFruits(bR.fruitsId, (int)bR.count);
                break;
            case BottleRewardType.Ball:
                DelegateCenter.addBall((int)bR.count);
                break;
            case BottleRewardType.DollarObstacle:
                ObstacleManager.Instance.AddDollarObstacle((int)bR.count);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 播放Inco动画
    /// </summary>
    /// <param name="x">本地坐标x轴</param>
    void PlayIconAnination(float x)
    {
        GameObject go = PoolManager.Instance.TakeOut(_name);
        if (go == null)
        {
            go = PrefabManager.Instance.New(_name);
            go.name = _name;
            go.transform.SetParent(tipss);
        }
        Image image = go.GetComponent<Image>();
        image.color = new Color(1, 1, 1, 1);
        go.transform.localScale = Vector3.zero;
        go.transform.localPosition = new Vector3(x, -510, 0);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(go.transform.DOScale(1, 0.5f));
        sequence.Append(image.DOFade(0, 0.5f));
        sequence.OnComplete(() =>
        {
            PoolManager.Instance.Enter(go);
        });
        sequence.PlayForward();
    }
    /// <summary>
    /// 播放数量动画
    /// </summary>
    /// <param name="tableBottle">奖励</param>
    /// <param name="transform">位置</param>
    void PlayTextAnination(BR br, Transform transform)
    {
        switch (br.bottleRewardType)
        {
            case BottleRewardType.Coin:
                TipsManager.Instance.Tips(StringUtils.StringSplit("+", br.count), transform, MoneyType.coin);
                break;
            case BottleRewardType.Dollar:
                TipsManager.Instance.Tips(StringUtils.StringSplit("+", br.count), transform, MoneyType.dollar);
                break;
            case BottleRewardType.Fruit:
                break;
            case BottleRewardType.Ball:
                break;
            case BottleRewardType.DollarObstacle:
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 修改指定瓶子奖励
    /// </summary>
    /// <param name="pos">瓶子位置0123</param>
    /// <param name="id">瓶子表id</param>
    public void ModifyObs(int pos, int id)
    {
        bRs[pos] = GetAppointBr(id);
        DelegateCenter.updateBottle(pos);
    }
    BR GetAppointBr(int id)
    {
        TableBottle br = tableBottles[id];
        BR bR = new BR
        {
            id = br.id,
            bottleRewardType = (BottleRewardType)br.Type,
            count = br.Count,
        };
        if (bR.bottleRewardType == BottleRewardType.Fruit)
        {
            int _id = FruitsModel.Instance.getFruitsRewardId();
            bR.fruitsId = _id;
        }
        return bR;
    }
}
/// <summary>
/// 瓶子奖励对象
/// </summary>
public class BR
{
    /// <summary>
    /// 表id
    /// </summary>
    public int id = 0;
    /// <summary>
    /// 奖励类型
    /// </summary>
    public BottleRewardType bottleRewardType;
    /// <summary>
    /// 奖励数量
    /// </summary>
    public double count = 0;
    /// <summary>
    /// 水果奖励id（仅限水果类型）
    /// </summary>
    public int fruitsId = 0;
}
public enum BottleRewardType
{
    /// <summary>
    /// 金币
    /// </summary>
    Coin = 1,
    /// <summary>
    /// 美金
    /// </summary>
    Dollar,
    /// <summary>
    /// 随机水果
    /// </summary>
    Fruit,
    /// <summary>
    /// 加球
    /// </summary>
    Ball,
    /// <summary>
    /// 刷新美金障碍
    /// </summary>
    DollarObstacle
}