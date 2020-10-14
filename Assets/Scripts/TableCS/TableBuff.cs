using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableBuff{
	/// <summary>
	/// id
	/// </summary>
	public int id;
	/// <summary>
	/// buff类型
	/// </summary>
	public int Type;
	/// <summary>
	/// 有效时间
	/// </summary>
	public int Time;
	/// <summary>
	/// 值
	/// </summary>
	public double Value;
	/// <summary>
	/// 权重
	/// </summary>
	public string Weight;
	/// <summary>
	/// 弹力
	/// </summary>
	public double ElasticForce;
	/// <summary>
	/// buff名字
	/// </summary>
	public string BuffName;
	/// <summary>
	/// Buff描述
	/// </summary>
	public string BuffDes;
	/// <summary>
	/// 图标
	/// </summary>
	public string Icon;
	
}
[System.Serializable]
      public class TableBuffList{
      	public List<TableBuff> list;
          
          public TableBuffList (){}
      	static TableBuffList _inst;
      	public static TableBuffList inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load("Table/table/TableBuff");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<TableBuffList> (ta.text);
      				} else {
      					_inst = new TableBuffList ();
      				}
      			}
      			return _inst;
      		}
      	}
      }