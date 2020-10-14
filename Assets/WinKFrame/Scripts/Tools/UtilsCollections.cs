using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UtilsCollections
{
    public static bool isBreak = false;
    #region 遍历执行事件
    /// <summary>
    /// 遍历列表并且执行事件
    /// </summary>
    /// <param name="list">List.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static void ListDoSomething<T>(IList<T> list, Action<T, int> action)
    {
        isBreak = false;
        if (list == null || action == null)
        {
            return;
        }
        int len = list.Count;
        for (int i = 0; i < len; i++)
        {
            action(list[i], i);
            if (isBreak)
            {
                break;
            }
        }
    }

    /// <summary>
    /// 遍历数组并且执行事件
    /// </summary>
    /// <param name="list">List.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static void ListDoSomething<T>(T[] list, Action<T, int> action)
    {
        isBreak = false;
        if (list == null || action == null)
        {
            return;
        }
        int len = list.Length;
        for (int i = 0; i < len; i++)
        {
            action(list[i], i);
            if (isBreak)
            {
                break;
            }
        }
    }

    public static void Break()
    {
        isBreak = true;
    }

    /// <summary>
    /// 遍历字典并且执行事件
    /// </summary>
    /// <param name="dic">Dic.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T1">The 1st type parameter.</typeparam>
    /// <typeparam name="T2">The 2nd type parameter.</typeparam>
    public static void DicDoSomething<T1, T2>(Dictionary<T1, T2> dic, Action<T1, T2> action)
    {
        isBreak = false;
        if (dic == null || action == null)
        {
            return;
        }
        foreach (var kv in dic)
        {
            action(kv.Key, kv.Value);
            if (isBreak)
            {
                break;
            }
        }
    }


    #endregion
    #region 获取元素
    /// <summary>
    /// 获得数组对应索引元素
    /// </summary>
    /// <returns>The index.</returns>
    /// <param name="arr">Arr.</param>
    /// <param name="index">Index.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T GetByIndex<T>(T[] arr, int index)
    {
        if (arr != null && index >= 0 && arr.Length > index)
        {
            return arr[index];
        }
        return default(T);
    }
    /// <summary>
    /// 获得列表对应索引元素
    /// </summary>
    /// <returns>The index.</returns>
    /// <param name="arr">Arr.</param>
    /// <param name="index">Index.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T GetByIndex<T>(IList<T> list, int index)
    {
        if (list != null && index >= 0 && list.Count > index)
        {
            return list[index];
        }
        return default(T);
    }
    /// <summary>
    /// 获得字典对应Key的值
    /// </summary>
    /// <returns>The by key.</returns>
    /// <param name="dic">Dic.</param>
    /// <param name="key">Key.</param>
    /// <typeparam name="T1">The 1st type parameter.</typeparam>
    /// <typeparam name="T2">The 2nd type parameter.</typeparam>
    public static T2 GetByKey<T1, T2>(IDictionary<T1, T2> dic, T1 key)
    {
        if (dic != null && key != null)
        {
            T2 v = default(T2);
            dic.TryGetValue(key, out v);
            return v;
        }
        return default(T2);
    }
    #endregion

    public static List<T2> DicToList<T1, T2>(Dictionary<T1, T2> dic)
    {
        List<T2> list = new List<T2>();
        DicDoSomething(dic, (k, v) =>
        {
            list.Add(v);
        });
        return list;
    }
    /// <summary>
    /// 获得由0开始的有序整数数组
    /// </summary>
    /// <returns>The sort int array.</returns>
    /// <param name="len">Length.</param>
    public static int[] GetSortIntArray(int len)
    {
        len = Mathf.Max(0, len);
        int[] ret = new int[len];
        for (int i = 0; i < len; i++)
        {
            ret[i] = i;
        }
        return ret;
    }

    internal static void ListDoSomething<T>(T moneyRewards, Action<T, int> p)
    {
        throw new NotImplementedException();
    }
}
