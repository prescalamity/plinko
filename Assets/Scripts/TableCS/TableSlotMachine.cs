using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableSlotMachine{
	/// <summary>
	/// id
	/// </summary>
	public int id;
	/// <summary>
	/// 描述
	/// </summary>
	public string Des;
	/// <summary>
	/// 权重
	/// </summary>
	public int Weigh;
	/// <summary>
	/// 图标
	/// </summary>
	public string Icon;
	/// <summary>
	/// 名字
	/// </summary>
	public string IconName;
	
}
[System.Serializable]
      public class TableSlotMachineList{
      	public List<TableSlotMachine> list;
          
          public TableSlotMachineList (){}
      	static TableSlotMachineList _inst;
      	public static TableSlotMachineList inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load("Table/table/TableSlotMachine");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<TableSlotMachineList> (ta.text);
      				} else {
      					_inst = new TableSlotMachineList ();
      				}
      			}
      			return _inst;
      		}
      	}
      }