using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsModel : Inst<FruitsModel>
{


    private List<int> notGetFruitsIdList = new List<int>();
    private List<int> haveGotFruitsIdList = new List<int>();

    private List<Fruits> FruitsList = new List<Fruits>();


    private void Awake()
    {
        if (GameData.CollectedFruits.Equals(""))
        {
            Fruits fruitLins;
            for (int i = 0; i < TableFruitsList.inst.list.Count; i++)
            {
                fruitLins = new Fruits();
                fruitLins.FruitsId = TableFruitsList.inst.list[i].id;
                fruitLins.FruitsNumber = 0;
                FruitsList.Add(fruitLins);
            }
        }
        else
        {
            FruitsList = JsonHelper.FromJson<List<Fruits>>(GameData.CollectedFruits);
        }

        for (int indexI = 0; indexI < FruitsList.Count; indexI++)
        {
            if (FruitsList[indexI].FruitsNumber == 0)
                notGetFruitsIdList.Add(FruitsList[indexI].FruitsId);
            else
                haveGotFruitsIdList.Add(FruitsList[indexI].FruitsId);
        }

    }

    /// <summary>
    /// 水果奖励项 在List中的下标ID
    /// </summary>
    /// <returns></returns>
    public int getFruitsRewardId()
    {
        int FruitId = 0;

        int randomId = Random.Range(0, TableFruitsList.inst.list.Count);

        if (randomId < notGetFruitsIdList.Count && notGetFruitsIdList.Count > 1)
        {  // 从未获得水果中随机选取一个水果Id
            FruitId = notGetFruitsIdList[randomId];
            notGetFruitsIdList.RemoveAt(randomId);
            haveGotFruitsIdList.Add(FruitId);
        }
        else
        {  // 从已获得水果中随机选取一个水果Id
            FruitId = haveGotFruitsIdList[UnityEngine.Random.Range(0, haveGotFruitsIdList.Count)];
        }

        return FruitId;
    }


    public void addFruits(int FruitsId, int number)
    {
        for(int i=0; i<FruitsList.Count; i++)
        {
            if (FruitsList[i].FruitsId == FruitsId)
            {
                FruitsList[i].FruitsNumber += number;
                break;
            }
        }
        DelegateCenter.updateFruit();
    }

    /// <summary>
    /// 返回玩家的水果收集进度
    /// </summary>
    /// <returns></returns>
    public string getRateOfProgress()
    {
        return StringUtils.StringSplit(haveGotFruitsIdList.Count,"/",FruitsList.Count);
    }

    /// <summary>
    /// 返回玩家 已收集 的水果种类总数
    /// </summary>
    /// <returns></returns>
    public int getHaveGotFruitsIdCount()
    {
        return haveGotFruitsIdList.Count;
    }

    /// <summary>
    /// 返回玩家 未收集 的水果种类总数
    /// </summary>
    /// <returns></returns>
    public int getNotGetFruitsIdCount()
    {
        return notGetFruitsIdList.Count;
    }

    /// <summary>
    /// 获取水果种类总数
    /// </summary>
    /// <returns></returns>
    public int getFruitsCount()
    {
        return FruitsList.Count;
    }

    /// <summary>
    /// 根据水果Id获取该Id的水果数量
    /// </summary>
    /// <param name="FruitsId"></param>
    /// <returns></returns>
    public int getFruitsNumberById(int FruitsId)
    {
        return FruitsList[FruitsId].FruitsNumber;
    }

    /// <summary>
    /// 将玩家收集的水果数据持久化保存
    /// </summary>
    public void saveFruitsData()
    {
        string fruitsDataString = JsonHelper.ToJson(FruitsList);
        GameData.CollectedFruits = fruitsDataString;
    }


}



public class Fruits
{
    public int FruitsId = 0;

    public int FruitsNumber = 0;

}






