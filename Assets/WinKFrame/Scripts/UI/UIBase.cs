using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public void Show<T>() where T : UIBase
    {
        //Debug.LogError(typeof(T).Name);
        UIManeger.Instance.Show<T>();
    }
    public void Hide<T>() where T : UIBase
    {
        UIManeger.Instance.Hide<T>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
