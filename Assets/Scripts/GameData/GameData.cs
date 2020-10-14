using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    /// <summary>
    /// 金币
    /// </summary>
    public static string Coin {
        get
        {
            return PlayerPrefs.GetString("Coin", "0");
        }
        set
        {
            PlayerPrefs.SetString("Coin", value);
        }
    }
    /// <summary>
    /// 美金
    /// </summary>
    public static float Dollar
    {
        get
        {
            return PlayerPrefs.GetFloat("Dollar", 0);
        }
        set
        {
            PlayerPrefs.SetFloat("Dollar", value);
        }
    }
    /// <summary>
    /// 障碍布局
    /// </summary>
    public static string Obstacles
    {
        get
        {
            return PlayerPrefs.GetString("Obstacles", "");
        }
        set
        {
            PlayerPrefs.SetString("Obstacles", value);
        }
    }
    /// <summary>
    /// 球个数
    /// </summary>
    public static int BallCount
    {
        get
        {
            return PlayerPrefs.GetInt("BallCount", 30);
        }
        set
        {
            PlayerPrefs.SetInt("BallCount", value);
        }
    }
    /// <summary>
    /// 瓶子奖励
    /// </summary>
    public static string BRs
    {
        get
        {
            return PlayerPrefs.GetString("BRs", "");
        }
        set
        {
            PlayerPrefs.SetString("BRs", value);
        }
    }
    /// <summary>
    /// 玩家已收集的水果数据
    /// </summary>
    public static string CollectedFruits
    {
        get
        {
            return PlayerPrefs.GetString("CollectedFruits", "");
        }
        set
        {
            PlayerPrefs.SetString("CollectedFruits", value);
        }
    }
    /// <summary>
    /// 是否新游戏0否1是
    /// </summary>
    public static int NewGame
    {
        get
        {
            return PlayerPrefs.GetInt("NewGame", 1);
        }
        set
        {
            PlayerPrefs.SetInt("NewGame", value);
        }
    }
    /// <summary>
    /// 是否开启声音0否1是
    /// </summary>
    public static int Sound
    {
        get
        {
            return PlayerPrefs.GetInt("Sound", 1);
        }
        set
        {
            PlayerPrefs.SetInt("Sound", value);
        }
    }
}
