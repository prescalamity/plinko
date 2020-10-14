using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class StringUtils
{
    private static StringBuilder stringBuilder = new StringBuilder();

    /// <summary>
    /// 字符串拼接
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public static string StringSplit(params object[] strs)
    {
        stringBuilder.Clear();
        for (int i = 0; i < strs.Length; i++)
        {
            stringBuilder.Append(strs[i]);
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// 格式的字符串拼接
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public static string StringAppendFormat(params object[] strs)
    {
        stringBuilder.Clear();
        switch (strs.Length)
        {
            case 2:
                stringBuilder.AppendFormat(strs[0].ToString(), strs[1].ToString());
                break;
            case 3:
                stringBuilder.AppendFormat(strs[0].ToString(), strs[1].ToString(), strs[2].ToString());
                break;
            case 4:
                stringBuilder.AppendFormat(strs[0].ToString(), strs[1].ToString(), strs[2].ToString(), strs[3].ToString());
                break;
            case 5:
                stringBuilder.AppendFormat(strs[0].ToString(), strs[1].ToString(), strs[2].ToString(), strs[3].ToString(), strs[4].ToString());
                break;
            case 6:
                stringBuilder.AppendFormat(strs[0].ToString(), strs[1].ToString(), strs[2].ToString(), strs[3].ToString(), strs[4].ToString(), strs[5].ToString());
                break;
            case 7:
                stringBuilder.AppendFormat(strs[0].ToString(), strs[1].ToString(), strs[2].ToString(), strs[3].ToString(), strs[4].ToString(), strs[5].ToString(), strs[6].ToString());
                break;
            case 8:
                stringBuilder.AppendFormat(strs[0].ToString(), strs[1].ToString(), strs[2].ToString(), strs[3].ToString(), strs[4].ToString(), strs[5].ToString(), strs[6].ToString(), strs[7].ToString());
                break;
            case 9:
                stringBuilder.AppendFormat(strs[0].ToString(), strs[1].ToString(), strs[2].ToString(), strs[3].ToString(), strs[4].ToString(), strs[5].ToString(), strs[6].ToString(), strs[7].ToString(), strs[8].ToString());
                break;
            default:
                break;
        }
        return stringBuilder.ToString();

    }


    /// <summary>
    /// 截取出表格被下划线或者，号隔开的值
    /// </summary>
    /// <param name="str"></param>
    /// <param name="cOne"></param>
    /// <param name="cTwo"></param>
    /// <param name="sOne"></param>
    /// <returns></returns>
    public static List<List<string>> StringSplitValue(string str, char cOne, char cTwo, string sOne)
    {
        List<List<string>> fs = new List<List<string>>();
        string[] ss = str.Split(cOne);
        UtilsCollections.ListDoSomething(ss, (st, index) =>
        {
            List<string> _s = new List<string>();
            string _str = st;
            if (_str.Contains(sOne))
            {
                _str = _str.Remove(_str.IndexOf(sOne), 1);
            }
            if (_str.Equals(""))
            {
                return;
            }
            string[] _ss = _str.Split(cTwo);
            foreach (var item in _ss)
            {
                _s.Add(item);
            }
            fs.Add(_s);
        });
        return fs;
    }

    /// <summary>
    /// 截取出表格被下划线或者，号隔开的值
    /// </summary>
    /// <param name="str"></param>
    /// <param name="cOne"></param>
    /// <param name="cTwo"></param>
    /// <returns></returns>
    public static List<string[]> StringSplitValue(string str, char cOne, char cTwo)
    {
        List<string[]> fs = new List<string[]>();
        string[] ss = str.Split(cOne);

        //Debug.LogError(str + "-----------" + cOne + "-----------" + cTwo+"-------"+ss.Length);
        UtilsCollections.ListDoSomething(ss, (st, index) =>
        {
            if (st.Equals(""))
            {
                return;
            }
            string[] _ss = st.Split(cTwo);
            List<string> _s = new List<string>();
            foreach (var item in _ss)
            {
                _s.Add(item);
            }
            fs.Add(_s.ToArray());
        });
        return fs;
    }

    /// <summary>
    /// 截取出表格被下划线或者，号隔开的值
    /// </summary>
    /// <param name="str"></param>
    /// <param name="cOne"></param>
    /// <param name="cTwo"></param>
    /// <returns></returns>
    public static List<List<TableValue>> StringSplitValue(string str, char cOne, char cTwo, char cThree)
    {
        List<List<TableValue>> fs = new List<List<TableValue>>();
        string[] ss = str.Split(cOne);

        UtilsCollections.ListDoSomething(ss, (st, index) =>
        {
            List<TableValue> vs = new List<TableValue>();
            if (st.Equals(""))
            {
                return;
            }
            string[] _ss = st.Split(cTwo);
            UtilsCollections.ListDoSomething(_ss, (_st, _index) =>
            {
                string[] _s = _st.Split(cThree);
                TableValue tableValue = new TableValue(int.Parse(_s[0]), float.Parse(_s[1]), int.Parse(_s[2]));
                vs.Add(tableValue);
            });
            fs.Add(vs);
        });
        return fs;
    }

    /// <summary>
    /// 截取出表格被下划线或者，号隔开的值
    /// </summary>
    /// <param name="str"></param>
    /// <param name="cOne"></param>
    /// <returns></returns>
    public static List<string> StringSplitValue(string str, char cOne)
    {
        List<string> fs = new List<string>();
        string[] ss = str.Split(cOne);
        UtilsCollections.ListDoSomething(ss, (st, index) =>
        {
            fs.Add(st);
        });
        return fs;
    }
    /// <summary>
    /// 秒转时间显示格式
    /// </summary>
    /// <param name="time">秒数</param>
    /// <param name="flag">是否只显示分秒（默认显示时分秒）</param>
    /// <returns></returns>
    public static string TimeText(int time, bool flag = false)
    {
        string text = "";
        if (flag)
        {
            text = string.Format("{0:D2}", time / 60 % 60)
                       + ":" + string.Format("{0:D2}", time % 60);
        }
        else
        {
            text = string.Format(string.Format("{0:D2}", time / 60 / 60 % 60) +
                ":" + string.Format("{0:D2}", time / 60 % 60)
                       + ":" + string.Format("{0:D2}", time % 60));
        }
        return text;
    }
    /// <summary>
    /// 切割字符串
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="c">切割符</param>
    /// <returns></returns>
    public static string[] Str(string str, char c)
    {
        return str.Split(c);
    }
}

public struct TableValue
{
    public int Type;
    public float Chance;
    public int Value;

    public TableValue(int type, float chance, int value)
    {
        Type = type;
        Chance = chance;
        Value = value;
    }
}
