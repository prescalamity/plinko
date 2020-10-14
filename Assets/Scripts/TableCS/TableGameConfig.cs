using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableGameConfig{
	/// <summary>
	/// id
	/// </summary>
	public int id;
	/// <summary>
	/// 恢复球的CD（秒）
	/// </summary>
	public int BallTime;
	/// <summary>
	/// 球上限
	/// </summary>
	public int BallUpperLimit;
	/// <summary>
	/// 球初始数量
	/// </summary>
	public int BallInitialCount;
	/// <summary>
	/// 金币兑换美金
	/// </summary>
	public string CoinTo;
	/// <summary>
	/// 美金兑换美金
	/// </summary>
	public string DollarTo;
	/// <summary>
	/// 水果兑换美金
	/// </summary>
	public string FruitTo;
	/// <summary>
	/// 广告加球次数
	/// </summary>
	public int AdsAddCallCount;
	/// <summary>
	/// 一次广告加球数
	/// </summary>
	public int AdsAddCallNumber;
	/// <summary>
	/// 金币变高级金币时间
	/// </summary>
	public int SeniorCoinTime;
	/// <summary>
	/// 高级金币数量随机范围
	/// </summary>
	public string SeniorCoinCount;
	/// <summary>
	/// 美金变高级美金时间
	/// </summary>
	public int SeniorDollarTime;
	/// <summary>
	/// 高级美金数量随机范围
	/// </summary>
	public string SeniorDollarCount;
	/// <summary>
	/// 球默认弹力
	/// </summary>
	public double ElasticForce;
	/// <summary>
	/// 老虎机入口移动速度
	/// </summary>
	public int MoveSpeed;
	/// <summary>
	/// 极限美金
	/// </summary>
	public int LimitDollar;
	
}
[System.Serializable]
      public class TableGameConfigList{
      	public List<TableGameConfig> list;
          
          public TableGameConfigList (){}
      	static TableGameConfigList _inst;
      	public static TableGameConfigList inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load("Table/table/TableGameConfig");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<TableGameConfigList> (ta.text);
      				} else {
      					_inst = new TableGameConfigList ();
      				}
      			}
      			return _inst;
      		}
      	}
      }