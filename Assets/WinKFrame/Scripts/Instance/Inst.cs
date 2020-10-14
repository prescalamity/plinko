using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inst<T> : MonoBehaviour where T : Inst<T>
{
    static GameObject game = null;
    private static T _instance;
    static Dictionary<string, T> _instances = new Dictionary<string, T>();
    public static T Instance
    {
        get
        {
            _instances.TryGetValue(typeof(T).Name, out _instance);
            if (_instance == null)
            {
                game = GameObject.Find("Game");
                if (game == null)
                {
                    game = new GameObject("Game");
                }
                _instance = game.AddComponent<T>();
                _instances.Add(typeof(T).Name, _instance);
            }
            return _instance;
        }
    }
}
