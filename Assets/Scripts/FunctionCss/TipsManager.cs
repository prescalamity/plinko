using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TooSimpleFramework.UI;
public class TipsManager : Inst<TipsManager>
{
    string _name = "Tips";
    /// <summary>
    ///Text父级
    /// </summary>
    Transform tipss = null;
    public void Init(Transform transform)
    {
        tipss = transform;
    }

    public void Tips( string des, Transform transform,MoneyType moneyType)
    {
        GameObject go = PoolManager.Instance.TakeOut(_name);
        if (go == null)
        {
            go = PrefabManager.Instance.New(_name);
            go.name = _name;
            go.transform.SetParent(tipss);
        }
        go.transform.localScale = Vector3.zero;
        go.transform.localPosition = transform.localPosition;
        Text text = go.GetComponent<Text>();
        text.text = des;
        OutlineEx outlineEx = go.GetComponent<OutlineEx>();
        switch (moneyType)
        {
            case MoneyType.coin:
                outlineEx.OutlineColor = new Color32(255, 250, 117, 255);
                break;
            case MoneyType.dollar:
                outlineEx.OutlineColor = new Color32(62, 249, 186, 255);
                break;
            default:
                break;
        }
        outlineEx.Init();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(go.transform.DOLocalMoveY(transform.localPosition.y + 100, .5f));
        sequence.Join(go.transform.DOScale(3, .5f));
        sequence.OnComplete(() =>
        {
            PoolManager.Instance.Enter(go);
        });
        sequence.PlayForward();
    }
}
