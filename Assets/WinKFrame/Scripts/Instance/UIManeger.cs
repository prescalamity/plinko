using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManeger : Inst<UIManeger>
{
    List<GameObject> viewList = new List<GameObject>();
    List<GameObject> openList = new List<GameObject>();
    Transform parent = null;
    Canvas mask = null;

    int baseNumber = 1000;
    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Show<T>()
    {
        if (parent == null)
        {
            parent = GameObject.Find("Canvas").transform;
            mask = GameObject.Find("Mask").GetComponent<Canvas>();
        }
        string viewName = typeof(T).Name;
        string path = "UIView/" + viewName;
        GameObject view = GetView(viewName);
        if (view == null)
        {
            //Debug.LogError(path);
            view = Instantiate(Resources.Load<GameObject>(path), parent, false) as GameObject;
            view.name = viewName;
            viewList.Add(view);

        }
        //SetCanvas(view);
        if (HasView(viewName))
        {
            openList.Remove(view);
            openList.Add(view);
        }
        else
        {
            openList.Add(view);
        }
        ReorderCanvas();
        view.SetActive(true);
    }
    /// <summary>
    /// 是否有已经打开的面板
    /// </summary>
    /// <param name="viewName"></param>
    /// <returns></returns>
    bool HasView(string viewName)
    {
        foreach (var item in openList)
        {
            if (item.name == viewName)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Hide<T>()
    {
        string viewName = typeof(T).Name;
        GameObject view = GetView(viewName);
        view.SetActive(false);
        openList.Remove(view);
        ReorderCanvas();
    }
    /// <summary>
    /// 获取面板
    /// </summary>
    /// <param name="viewName"></param>
    /// <returns></returns>
    GameObject GetView(string viewName)
    {
        GameObject view = null;
        if (viewList.Count != 0)
        {
            foreach (var item in viewList)
            {
                if (item.name.Equals(viewName))
                {
                    view = item;
                }
            }
        }
        return view;
    }
    /// <summary>
    /// 关闭最上面
    /// </summary>
    void CloseTopView()
    {
        if (openList.Count == 1) return;
        openList[openList.Count - 1].SetActive(false);
        openList.RemoveAt(openList.Count - 1);
        //SetCanvas(openList[openList.Count - 1]);
        ReorderCanvas();
    }
    /// <summary>
    /// 设置层级
    /// </summary>
    /// <param name="view"></param>
    void SetCanvas(GameObject view)
    {
        if (openList.Count > 1)
        {
            mask.sortingOrder = openList.Count * baseNumber - 1;
        }
        else
        {
            mask.sortingOrder = 0;
        }
        view.GetComponent<Canvas>().sortingOrder = openList.Count * baseNumber;
    }
    /// <summary>
    /// 层级重新排序
    /// </summary>
    void ReorderCanvas()
    {
        for (int i = 0; i < openList.Count; i++)
        {
            openList[i].GetComponent<Canvas>().sortingOrder = (i + 1) * baseNumber;
            if (i == openList.Count - 1)
            {
                mask.sortingOrder = (i + 1) * baseNumber - 1;
            }
        }
        if (openList.Count == 1) mask.sortingOrder = 0;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTopView();
        }

    }
}
