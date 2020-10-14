using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DataTools : MonoBehaviour
{
    [MenuItem("Tools/清除本地数据", false, 0)]
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }

    //[MenuItem("DataTools/开启指引", false, 0)]
    //public static void OpenGuide()
    //{
    //    PlayerPrefs.SetInt("GuideOpen", 0);
    //}

    //[MenuItem("DataTools/关闭指引", false, 0)]
    //public static void CloseGuide()
    //{
    //    PlayerPrefs.SetInt("GuideOpen", 1);
    //}
}
