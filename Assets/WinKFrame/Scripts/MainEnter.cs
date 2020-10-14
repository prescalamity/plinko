using UnityEngine;

public class MainEnter :MonoBehaviour
{
    /// <summary>
    /// 速度最快
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void StartWinKFrame()
    {
        //Debug.LogError("主入口");
        //GameObject.Find("Canvas").AddComponent<TestView>();
    }
    /// <summary>
    /// 速度第二
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void StartWinKFrame1()
    {
        //Debug.LogError("主入口1");
        //UIManeger.Instance.Show<MainView>();
    }
    /// <summary>
    /// 速度最后
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    static void StartWinKFrame2()
    {
        //Debug.LogError("主入口2");
        //GameObject.Find("Canvas").AddComponent<TestView>();
    }
}
