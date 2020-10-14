using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableBottle{
	/// <summary>
	/// id
	/// </summary>
	public int id;
	/// <summary>
	/// 类型
	/// </summary>
	public int Type;
	/// <summary>
	/// 奖励数量
	/// </summary>
	public double Count;
	/// <summary>
	/// 权重
	/// </summary>
	public int Weight;
	
}
[System.Serializable]
      public class TableBottleList{
      	public List<TableBottle> list;
          
          public TableBottleList (){}
      	static TableBottleList _inst;
      	public static TableBottleList inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load("Table/table/TableBottle");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<TableBottleList> (ta.text);
      				} else {
      					_inst = new TableBottleList ();
      				}
      			}
      			return _inst;
      		}
      	}
      }