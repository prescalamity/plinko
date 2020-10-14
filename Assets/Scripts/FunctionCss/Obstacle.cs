using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    //[DisplayOnly]
    public int id = 0;
    //[DisplayOnly]
    public ObstacleType obstacleType = ObstacleType.Coin;
    //[DisplayOnly]
    public int hp = 0, currentHp = 0;
    Image image = null;
    /// <summary>
    /// 行数
    /// </summary>
    [Header("行数")]
    public int rowNumber = 0;
    double[] value = new double[6];
    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        List<TableObstacle> obstacles = TableObstacleList.inst.list;
        for (int i = 0; i < obstacles.Count; i++)
        {
            value[i] = obstacles[i].Rewad;
        }
    }
    public void Init(ObstacleType obstacleType, int id)
    {
        gameObject.SetActive(true);
        this.obstacleType = obstacleType;
        this.id = id;
        switch (obstacleType)
        {
            case ObstacleType.Coin:
            case ObstacleType.SeniorCoin:
                image.sprite = SpriteAtlasManager.Instance.GetSprite("yellowball@2x");
                break;
            case ObstacleType.Dollar:
            case ObstacleType.SeniorDollar:
                image.sprite = SpriteAtlasManager.Instance.GetSprite("greenball@2x");
                break;
            case ObstacleType.Obstacle:
                image.sprite = SpriteAtlasManager.Instance.GetSprite("home_blackball@2x");
                break;
            case ObstacleType.Prop:
                image.sprite = SpriteAtlasManager.Instance.GetSprite("home_top_Fruits_number@2x");
                break;
            default:
                break;
        }
        if (obstacleType != ObstacleType.Obstacle && currentHp == 0)
        {
            ObstacleManager.Instance.Hide(id);
        }
    }
    /// <summary>
    /// 设置血量
    /// </summary>
    /// <param name="hp">总血量</param>
    /// <param name="currentHp">当前血量</param>
    public void SetHp(int hp, int currentHp)
    {
        this.hp = hp;
        this.currentHp = currentHp;
        SetFade();
    }
    /// <summary>
    /// 设置透明度
    /// </summary>
    void SetFade()
    {
        if (hp == 0)
        {
            image.color = new Color(1, 1, 1, 1);
            return;
        }
        float value = (float)currentHp / hp;
        image.color = new Color(1, 1, 1, value);

        //if (obstacleType == ObstacleType.Dollar)
        //{
        //    image.color = new Color(1, 0, 1, value);
        //}
        //else
        //{
        //    image.color = new Color(1, 1, 1, value);
        //}
        //switch (obstacleType)
        //{
        //    case ObstacleType.Coin:
        //        image.color = new Color(1, 1, 1, value);
        //        break;
        //    case ObstacleType.Dollar:
        //        image.color = new Color(1, 0, 1, value);
        //        break;
        //    case ObstacleType.Obstacle:
        //        image.color = new Color(0, 1, 1, 1);
        //        break;
        //    case ObstacleType.SeniorCoin:
        //        image.color = new Color(0.5f, 0.5f, 1, value);
        //        break;
        //    case ObstacleType.SeniorDollar:
        //        image.color = new Color(0.5f, 0.5f, 0.5f, value);
        //        break;
        //    case ObstacleType.Prop:
        //        break;
        //    default:
        //        break;
        //}
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision();
    }
    double _value = 0;
    string str = "";
    /// <summary>
    /// 触发
    /// </summary>
    private void OnCollision()
    {
        _value = value[(int)obstacleType];
        if (BuffCtrl.Instance.X2)
        {
            _value *= BuffCtrl.Instance.GetMultiple();
        }
        if (hp != 0 && currentHp != 0)
        {
            currentHp -= 1;
            SetFade();
            if (currentHp == 0)
            {
                ObstacleManager.Instance.Hide(id);
            }
        }
        str = StringUtils.StringSplit("+", _value);
        switch (this.obstacleType)
        {
            case ObstacleType.Coin:
            case ObstacleType.SeniorCoin:
                DelegateCenter.addCoin(_value);
                TipsManager.Instance.Tips(str, transform,MoneyType.coin);
                break;
            case ObstacleType.Dollar:
            case ObstacleType.SeniorDollar:
                DelegateCenter.addDollar(_value);
                TipsManager.Instance.Tips(str, transform,MoneyType.dollar);
                break;
            case ObstacleType.Obstacle:
                break;
            case ObstacleType.Prop:
                if (currentHp==0)
                {
                    ObstacleManager.Instance.Bomb(id);
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 计算剩余生命值加钱
    /// </summary>
    public void CalculateHpAddMoney()
    {
        _value = value[(int)obstacleType];
        if (BuffCtrl.Instance.X2)
        {
            _value *= BuffCtrl.Instance.GetMultiple();
        }
        _value *= currentHp;
        str = StringUtils.StringSplit("+", _value);
        switch (this.obstacleType)
        {
            case ObstacleType.Coin:
            case ObstacleType.SeniorCoin:
                DelegateCenter.addCoin(_value);
                TipsManager.Instance.Tips(str, transform, MoneyType.coin);
                break;
            case ObstacleType.Dollar:
            case ObstacleType.SeniorDollar:
                DelegateCenter.addDollar(_value);
                TipsManager.Instance.Tips(str, transform, MoneyType.dollar);
                break;
            case ObstacleType.Obstacle:
                break;
            case ObstacleType.Prop:
                //ObstacleManager.Instance.Bomb(id);
                break;
            default:
                break;
        }
        ObstacleManager.Instance.Hide(id);
    }
}
/// <summary>
/// 障碍类型
/// </summary>
public enum ObstacleType
{
    /// <summary>
    /// 金币
    /// </summary>
    Coin,
    /// <summary>
    /// 美金
    /// </summary>
    Dollar,
    /// <summary>
    /// 障碍
    /// </summary>
    Obstacle,
    /// <summary>
    /// 高级金币
    /// </summary>
    SeniorCoin,
    /// <summary>
    /// 高级美金
    /// </summary>
    SeniorDollar,
    /// <summary>
    /// 道具
    /// </summary>
    Prop,
}
/// <summary>
///障碍保存对象
/// </summary>
public class _Obstacle
{
    /// <summary>
    /// id
    /// </summary>
    public int id;
    /// <summary>
    /// 障碍类型
    /// </summary>
    public ObstacleType obstacleType;
    /// <summary>
    /// 总血量
    /// </summary>
    public int hp;
    /// <summary>
    /// 当前血量
    /// </summary>
    public int currentHp;

    public _Obstacle()
    {
    }
}