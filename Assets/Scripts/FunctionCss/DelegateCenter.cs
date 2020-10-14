/// <summary>
/// 公共委托管理中心  --仅适用简单项目
/// </summary>
public class DelegateCenter
{
    /// <summary>
    /// 加金币委托
    /// </summary>
    /// <param name="coin"></param>
    public delegate void AddCoin(double coin);
    /// <summary>
    /// 加金币
    /// </summary>
    public static AddCoin addCoin;
    /// <summary>
    /// 加美金委托
    /// </summary>
    /// <param name="coin"></param>
    public delegate void AddDollar(double dollar);
    /// <summary>
    /// 加美金
    /// </summary>
    public static AddDollar addDollar;
    /// <summary>
    /// 加球委托
    /// </summary>
    /// <param name="count"></param>
    public delegate void AddBall(int count);
    /// <summary>
    /// 加球
    /// </summary>
    public static AddBall addBall;
    /// <summary>
    /// 老虎机开始委托
    /// </summary>
    public delegate void SlotMachineStart();
    /// <summary>
    /// 老虎机开始
    /// </summary>
    public static SlotMachineStart slotMachineStart;
    /// <summary>
    /// 老虎机结束委托
    /// </summary>
    /// <param name="id">奖励id</param>
    public delegate void SlotMachineEnd(int id);
    /// <summary>
    /// 老虎机结束
    /// </summary>
    public static SlotMachineEnd slotMachineEnd;
    /// <summary>
    /// 更新货币委托
    /// </summary>
    public delegate void UpDateMoney(MoneyType moneyType);
    /// <summary>
    /// 更新货币
    /// </summary>
    public static UpDateMoney upDateMoney;
    /// <summary>
    /// 更新球个数委托
    /// </summary>
    /// <param name="ballCount">球数量</param>
    public delegate void UpdateBallCount(int ballCount);
    /// <summary>
    /// 更新球个数
    /// </summary>
    public static UpdateBallCount updateBallCount;
    /// <summary>
    /// 第一个参数中，1表示金币，2表示绿币，第二个参数表示增加的数量
    /// </summary>
    /// <param name="number"></param>
    public delegate void AddCoinOrDollarBlock(int number);
    /// <summary>
    /// 第一个参数中，1表示金币，2表示绿币，第二个参数表示增加的数量
    /// </summary>
    public static AddCoinOrDollarBlock addCoinOrDollarBlock;
    /// <summary>
    /// 随机掉落圆球
    /// </summary>
    /// <param name="number"></param>
    public delegate void DropBallRandom(int number);
    /// <summary>
    /// 随机掉落圆球
    /// </summary>
    public static DropBallRandom randomDropBall;
    /// <summary>
    /// 将所有阻挡替换为金币
    /// </summary>
    public delegate void CoinReplaceAllBlock();
    /// <summary>
    /// 将所有阻挡替换为金币
    /// </summary>
    public static CoinReplaceAllBlock coinReplaceAllBlock;
    /// <summary>
    /// 更新瓶子奖励委托
    /// </summary>
    /// <param name="pos">位置0123，-1更行全部</param>
    public delegate void UpdateBottle(int pos);
    /// <summary>
    /// 更新瓶子奖励
    /// </summary>
    public static UpdateBottle updateBottle;
    /// <summary>
    /// 看buff广告委托
    /// </summary>
    public delegate void AdsBuff();
    /// <summary>
    /// 看广告buff
    /// </summary>
    public static AdsBuff adsBuff;
    /// <summary>
    /// 更新水果委托
    /// </summary>
    public delegate void UpdateFruit();
    /// <summary>
    /// 更新水果
    /// </summary>
    public static UpdateFruit updateFruit;
}
public enum MoneyType
{
    /// <summary>
    /// 金币
    /// </summary>
    coin,
    /// <summary>
    /// 美金
    /// </summary>
    dollar
}