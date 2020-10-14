using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableObstacle{
	/// <summary>
	/// id
	/// </summary>
	public int id;
	/// <summary>
	/// 障碍类型
	/// </summary>
	public int Type;
	/// <summary>
	/// 描述
	/// </summary>
	public string Des;
	/// <summary>
	/// 权重
	/// </summary>
	public int Weight;
	/// <summary>
	/// 血量
	/// </summary>
	public int Hp;
	/// <summary>
	/// 奖励
	/// </summary>
	public double Rewad;
	/// <summary>
	/// 随机最小时间
	/// </summary>
	public int MixTime;
	/// <summary>
	/// 随机最大时间
	/// </summary>
	public int MaxTime;
	/// <summary>
	/// 随机最小数量
	/// </summary>
	public int MixCount;
	/// <summary>
	/// 随机最大数量
	/// </summary>
	public int MaxCount;
	/// <summary>
	/// 场上最多数量
	/// </summary>
	public int Max;
	
}
[System.Serializable]
      public class TableObstacleList{
      	public List<TableObstacle> list;
          
          public TableObstacleList (){}
      	static TableObstacleList _inst;
      	public static TableObstacleList inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load("Table/table/TableObstacle");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<TableObstacleList> (ta.text);
      				} else {
      					_inst = new TableObstacleList ();
      				}
      			}
      			return _inst;
      		}
      	}
      }