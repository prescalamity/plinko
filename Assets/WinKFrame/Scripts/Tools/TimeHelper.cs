using System;
/// <summary>
/// 时间工具
/// </summary>
public static class TimeHelper
{
    private static readonly long epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
    /// <summary>
    /// 客户端时间
    /// </summary>
    /// <returns></returns>
    public static long ClientNow()
    {
        return (DateTime.UtcNow.Ticks - epoch) / 10000;
    }

    public static long ClientNowSeconds()
    {
        return (DateTime.UtcNow.Ticks - epoch) / 10000000;
    }

    public static long Now()
    {
        return ClientNow();
    }
    /// <summary>
    /// 是否过期
    /// </summary>
    /// <param name="startTime">开始时间</param>
    /// <param name="validityPeriod">有效期</param>
    /// <returns>false没过期true过期</returns>
    public static bool Expired(long startTime, int validityPeriod)
    {
        long timeDifference = ClientNowSeconds() - startTime;
        //DebugTool.print("timeDifference===>" + timeDifference);
        if (timeDifference < validityPeriod)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// 剩余秒数
    /// </summary>
    /// <param name="startTime">开始的时间戳</param>
    /// <param name="prescription">时效</param>
    /// <returns></returns>
    public static float TimeRemainingSeconds(long startTime, long prescription)
    {
        float time = prescription - (ClientNowSeconds() - startTime);
        if (time <= 0)
        {
            time = 0;
        }
        return time;
    }
    /// <summary>
    /// 剩余秒数文本格式
    /// </summary>
    /// <param name="startTime">开始时间戳</param>
    /// <param name="prescription">时效</param>
    /// <param name="flag">是否只显示分秒（默认显示时分秒）</param>
    /// <returns></returns>
    public static string TimeRemainingSecondsText(long startTime, long prescription, bool flag = false)
    {
        float time = TimeRemainingSeconds(startTime, prescription);
        string text = "";
        if (flag)
        {
            text = string.Format("{0:D2}", (int)time / 60 % 60)
                       + ":" + string.Format("{0:D2}", (int)time % 60);
        }
        else
        {
            text = string.Format(string.Format("{0:D2}", (int)time / 60 / 60 % 60) +
                ":" + string.Format("{0:D2}", (int)time / 60 % 60)
                       + ":" + string.Format("{0:D2}", (int)time % 60));
        }
        return text;
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
}