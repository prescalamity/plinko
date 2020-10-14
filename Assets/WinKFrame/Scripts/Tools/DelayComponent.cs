using UnityEngine;
using System.Threading.Tasks;
using System;

public static class DelayComponent
{
    /// <summary>
    /// 延迟执行某函数
    /// </summary>
    /// <param name="second">多少毫秒（1s=1000ms）</param>
    /// <param name="action">函数</param>
    public static async void Delay(int second,Action action)
    {
        await Task.Delay(second);
        action();
    }
}
