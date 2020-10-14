using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableRewards{
	/// <summary>
	/// id
	/// </summary>
	public int id;
	/// <summary>
	/// 类型
	/// </summary>
	public int RewardType;
	/// <summary>
	/// 数量
	/// </summary>
	public int Number;
	
}
[System.Serializable]
      public class TableRewardsList{
      	public List<TableRewards> list;
          
          public TableRewardsList (){}
      	static TableRewardsList _inst;
      	public static TableRewardsList inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load("Table/table/TableRewards");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<TableRewardsList> (ta.text);
      				} else {
      					_inst = new TableRewardsList ();
      				}
      			}
      			return _inst;
      		}
      	}
      }