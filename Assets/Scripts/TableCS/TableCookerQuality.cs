using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableCookerQuality{
	/// <summary>
	/// 厨师品质
	/// </summary>
	public int CookerQuality;
	/// <summary>
	/// 厨师品质系数
	/// </summary>
	public float CookerCoefficient;
	/// <summary>
	/// 名称
	/// </summary>
	public string CookerQualityName;
	/// <summary>
	/// 吞噬提供厨师经验
	/// </summary>
	public int CookerProvideEXP;
	/// <summary>
	/// 品质解锁等级（厨师）
	/// </summary>
	public int UnLockLevel;
	/// <summary>
	/// 升品消耗钻石
	/// </summary>
	public int LevelUpUse;
	
}
[System.Serializable]
      public class TableCookerQualityList{
      	public List<TableCookerQuality> list;
          
          public TableCookerQualityList (){}
      	static TableCookerQualityList _inst;
      	public static TableCookerQualityList inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load("Table/table/TableCookerQuality");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<TableCookerQualityList> (ta.text);
      				} else {
      					_inst = new TableCookerQualityList ();
      				}
      			}
      			return _inst;
      		}
      	}
      }