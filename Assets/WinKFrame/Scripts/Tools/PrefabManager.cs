using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Inst<PrefabManager>
{
    public GameObject New(string name)
    {
        GameObject _go = PoolManager.Instance.TakeOut(name);
        if (_go == null)
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/" + name);
            if (go==null)
            {
                Debug.LogError("没有" + name + "预制体！！！");
                return null;
            }
            _go = GameObject.Instantiate<GameObject>(go);
            _go.name = name;
        }
        return _go;
    }
}
