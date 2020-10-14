using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyTest : MonoBehaviour
{

    public void testasdas()
    {
        print("ddddddddddddd");
        DelegateCenter.slotMachineStart();
    }

    /*
    void Start()
    {

        Debug.Log("初始水果收集状态:，水果总类：" + FruitsModel.Instance.getFruitsCount() +
            "----->已获得：" + FruitsModel.Instance.getHaveGotFruitsIdCount() + 
            "----->未获得：" + FruitsModel.Instance.getNotGetFruitsIdCount());

        for (int i = 0; i < 1000; i++)
        {
            int RewardId = FruitsModel.Instance.getFruitsRewardId();

            Debug.Log("AAAAAA此次获得水果的ID=========================>"+RewardId);

            FruitsModel.Instance.addFruits(RewardId, 1);

            Debug.Log("初始水果收集状态:，水果总类：" + FruitsModel.Instance.getFruitsCount() + "----->已获得：" +
            FruitsModel.Instance.getHaveGotFruitsIdCount() + "----->未获得：" +
            FruitsModel.Instance.getNotGetFruitsIdCount());
        }

    }
    */

}




