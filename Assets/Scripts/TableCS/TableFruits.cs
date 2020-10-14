using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableFruits{
	/// <summary>
	/// id
	/// </summary>
	public int id;
	/// <summary>
	/// 水果名
	/// </summary>
	public string FruitName;
	/// <summary>
	/// 描述
	/// </summary>
	public string Desc;
	
}
[System.Serializable]
      public class TableFruitsList{
      	public List<TableFruits> list;
          
          public TableFruitsList (){}
      	static TableFruitsList _inst;
      	public static TableFruitsList inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load("Table/table/TableFruits");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<TableFruitsList> (ta.text);
      				} else {
      					_inst = new TableFruitsList ();
      				}
      			}
      			return _inst;
      		}
      	}
      }