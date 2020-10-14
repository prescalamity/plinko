using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Inst<PoolManager>
{
    List<GameObject> pools = new List<GameObject>();
    Dictionary<string, List<GameObject>> poolLists = new Dictionary<string, List<GameObject>>();
    /// <summary>
    /// 入池
    /// </summary>
    /// <param name="go"></param>
    public void Enter(GameObject go)
    {
        go.SetActive(false);
        poolLists.TryGetValue(go.name, out pools);
        if (pools == null)
        {
            pools = new List<GameObject>();
        }
        else
        {
            pools = poolLists[go.name];
        }
        pools.Add(go);
        poolLists[go.name] = pools;
    }
    /// <summary>
    /// 取出
    /// </summary>
    /// <param name="name">对象名字</param>
    /// <returns></returns>
    public GameObject TakeOut(string name)
    {
        GameObject go = null;
        poolLists.TryGetValue(name, out pools);
        if (pools != null && pools.Count > 0)
        {
            GameObject item = poolLists[name][0];
            go = item;
            go.SetActive(true);
            poolLists[name].Remove(item);
        }
        return go;
    }

    bool HaveKey(string name)
    {
        bool isHave = false;
        foreach (var item in poolLists)
        {

        }
        return isHave;
    }
}
