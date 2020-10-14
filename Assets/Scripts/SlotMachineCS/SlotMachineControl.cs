using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RewardEnum
{
    NoReward,
    Fruit,
    Coin,
    Ball,
    DollarOrCoin,
    DropBall,
    CoinReplace,
    Dollar
}

public class SlotMachineControl : MonoBehaviour
{

    int WeightTotal=1;

    /// <summary>
    /// 奖励项 在List中的下标ID
    /// </summary>
    int RewardId = 0;

    /// <summary>
    /// 水果奖励项 在List中的下标ID
    /// </summary>
    int FruitsRewardId = 0;

    /// <summary>
    /// 老虎机中各列分别要走的步数
    /// </summary>
    [HideInInspector]
    public int[] stepsTotalArray;

    /// <summary>
    /// 奖励项ID游标， 表示下一个所要显示的奖励项的ID
    /// </summary>
    [HideInInspector]
    public int[] nextRewardIdCursorArr;

    /// <summary>
    /// 老虎机中当前奖励项ID游标， 表示下一个所要显示的奖励项的ID
    /// </summary>
    [HideInInspector]
    public int[] CurrentRewardPositions;

    public Action launchSlotMachineAction;

    /// <summary>
    /// 当前未处理的进球个数
    /// </summary>
    int currentSlotMachineGoals = 0;

    bool isPlaying = false;

    GameObject RewardGotImageGO;

    Image RewardGotImage;

    /// <summary>
    /// 老虎机中能获得的奖励集合
    /// </summary>
    [HideInInspector]
    public int[] smRewardArray;

    void Awake()
    {
        stepsTotalArray = new int[3]{ 35, 28, 30 };
        nextRewardIdCursorArr = new int[3] { 4,4,4};
        CurrentRewardPositions = new int[3] { 1, 1, 1 };
        // 先水果再是其它奖励，减去没获奖和水果项
        smRewardArray = new int[TableFruitsList.inst.list.Count + TableSlotMachineList.inst.list.Count -1-1];
        //for (int i=0; i< smRewardArray.Length; i++) smRewardArray[i] = i;

        //初始化老虎机
        RewardGotImageGO = transform.Find("RewardGotImage").gameObject;
        RewardGotImage = RewardGotImageGO.GetComponent<Image>();
    }

    void Start()
    {
        for (int indexI = 0; indexI < TableSlotMachineList.inst.list.Count; indexI++)
        {
            WeightTotal += TableSlotMachineList.inst.list[indexI].Weigh;
        }


        DelegateCenter.slotMachineStart += ()=> {
            currentSlotMachineGoals++;
            slotMachineGoals();
        };

    }


    /// <summary>
    /// 处理老虎机进球
    /// </summary>
    void slotMachineGoals()
    {
        if (currentSlotMachineGoals>0 && !isPlaying)
        {
            //Debug.LogError("触发老虎机");
            isPlaying = true;
            RewardId = getRewardId();                    // 确定玩家所中奖项

            //RewardId = 1;
            //FruitsRewardId = 0;

            //print("AAAAAAAAAAAAA---->" + RewardId +"  AAAAAAAAA  "+FruitsRewardId);

            if (RewardId == 0){
                RewardGotImage.sprite = SpriteAtlasManager.Instance.GetSprite(TableFruitsList.inst.list[FruitsRewardId].FruitName); 
            }
            else if(RewardId != TableSlotMachineList.inst.list.Count - 1)
            {
                RewardGotImage.sprite = SpriteAtlasManager.Instance.GetSprite(TableSlotMachineList.inst.list[RewardId].IconName);
            }

            //计算老虎机转动步数以及真实的当前奖励位置
            for (int i = 0; i < stepsTotalArray.Length; i++) {
                stepsTotalArray[i] = RewardDistance(i); 

                CurrentRewardPositions[i] += stepsTotalArray[i];   //重置当前奖励位置
                if (CurrentRewardPositions[i] > (smRewardArray.Length - 1))
                    CurrentRewardPositions[i] -= smRewardArray.Length;

                stepsTotalArray[i] += smRewardArray.Length;
                //print("要走的步数：----------------" + stepsTotalArray[i]);
            }
            
            launchSlotMachineAction.Invoke();              // 启动老虎机
            currentSlotMachineGoals--;
        }
    }

    /// <summary>
    /// 计算 当前奖励游标 与 目标奖励游标的距离
    /// </summary>
    /// <param name="nextRICIndex"> 当前游标列</param>
    /// <returns></returns>
    int RewardDistance(int slotMachineLaneId)
    {
        int TheDistance;
        int TargetPos;

        if (RewardId == 0) {
            TargetPos = FruitsRewardId;
        } 
        else if (RewardId == TableSlotMachineList.inst.list.Count - 1){  // 没有获得奖励时
            if (slotMachineLaneId == 2) {
                return nextRewardIdCursorArr[0] + stepsTotalArray[0] 
                     - nextRewardIdCursorArr[2] + 4;   //确保在不中奖时，老虎机不显示中将状态
            }
            else
                return UnityEngine.Random.Range(5,10);
        }
        else{
            TargetPos = TableFruitsList.inst.list.Count + RewardId - 1;
        }

        //print("目标位置：-----  " + TargetPos);
        //print("当前位置：-----  " + CurrentRewardPositions[slotMachineLaneId]);

        if (CurrentRewardPositions[slotMachineLaneId] > TargetPos)  
            TheDistance = smRewardArray.Length - CurrentRewardPositions[slotMachineLaneId] + TargetPos;   //当前位置大于目标位置
        else
            TheDistance = TargetPos - CurrentRewardPositions[slotMachineLaneId];

        return  TheDistance;//smRewardArray.Length +
    }


    /// <summary>
    /// 获得奖励项在List中的下标ID
    /// </summary>
    /// <returns></returns>
    private int getRewardId()
    {
        int rewardIdLins=0;

        int randomWeight = UnityEngine.Random.Range(1, WeightTotal + 1);

        List<int> dollarRewardIdList = new List<int>();

        // 如果超过绿币上限，去除绿币奖项
        if (GameControl.Instance.IsLimit())  dollarRewardIdList = getDollarRewardIdList();

        for (int indexI = 0; indexI < TableSlotMachineList.inst.list.Count; indexI++)
        {
            //跳过绿币奖励项
            if (dollarRewardIdList.Count > 0 && indexI == dollarRewardIdList[0])
            {
                if (randomWeight > TableSlotMachineList.inst.list[indexI].Weigh)
                    randomWeight -= TableSlotMachineList.inst.list[indexI].Weigh;
                dollarRewardIdList.RemoveAt(0);
                continue;
            }

            if (randomWeight > TableSlotMachineList.inst.list[indexI].Weigh)
            {
                randomWeight -= TableSlotMachineList.inst.list[indexI].Weigh;
            }
            else
            {
                rewardIdLins = indexI;
                break;
            }

        }

        if (rewardIdLins == 0)   // 如果奖励为水果，刷新水果的奖励Id
        {
            FruitsRewardId = FruitsModel.Instance.getFruitsRewardId();
        }

        return rewardIdLins;
    }

    List<int> getDollarRewardIdList()
    {
        List<int> resulte = new List<int>();
        for (int iIndex = 0; iIndex < TableRewardsList.inst.list.Count; iIndex++)
        {
            if (TableRewardsList.inst.list[iIndex].RewardType == 7) resulte.Add(iIndex);
        }
        return resulte;
    }

    private int animationCounter = 0;
    public void animationOver()
    {
        animationCounter++;
        if (animationCounter == 12)
        {
            animationCounter = 0;

            //DelegateCenter.slotMachineEnd(RewardId);
            //if(RewardId==0)
            //    Debug.Log("所获奖励ID："+RewardId+";   奖励类型："+ (RewardEnum)TableRewardsList.inst.list[RewardId].RewardType+
            //        "；  水果ID："+FruitsRewardId+"； 水果名："+TableFruitsList.inst.list[FruitsRewardId].FruitName);
            //else
            //    Debug.Log("所获奖励ID：" + RewardId + ";   奖励类型：" + (RewardEnum)TableRewardsList.inst.list[RewardId].RewardType);

            //print("游标位置：" + nextRewardIdCursorArr[0] + "," + nextRewardIdCursorArr[1] + "," + nextRewardIdCursorArr[2]);
            //print("真实位置：" + CurrentRewardPositions[0]+ "," + CurrentRewardPositions[1] + "," + CurrentRewardPositions[2]);

            playerGetReward();      // 将所获奖励给玩家

            if (RewardId != TableSlotMachineList.inst.list.Count - 1)  //如果玩家获得奖励，弹出所获奖励图
            {
                rewardAnimation();
            }
            else  // 判断后续是否还有中球
            {
                Invoke("SlotMachineNeededAgain", 1f);
            }
          
        }
    }

    private void playerGetReward()
    {
        int rewardType = TableRewardsList.inst.list[RewardId].RewardType;
        switch ((RewardEnum)rewardType)
        {
            case RewardEnum.NoReward: break;
            case RewardEnum.Fruit:
                addFruit();
                break;
            case RewardEnum.Coin:
                DelegateCenter.addCoin(TableRewardsList.inst.list[RewardId].Number);
                break;
            case RewardEnum.Ball:
                DelegateCenter.addBall(TableRewardsList.inst.list[RewardId].Number);
                break;
            case RewardEnum.DollarOrCoin:
                addCOrDollar();
                break;
            case RewardEnum.DropBall:
                DelegateCenter.randomDropBall?.Invoke(TableRewardsList.inst.list[RewardId].Number);
                break;
            case RewardEnum.CoinReplace:
                DelegateCenter.coinReplaceAllBlock?.Invoke();
                break;
            case RewardEnum.Dollar:
                DelegateCenter.addDollar(TableRewardsList.inst.list[RewardId].Number);
                break;
            default: break;
        }
    }

    /// <summary>
    /// 刷新金币或美金阻挡
    /// </summary>
    private void addCOrDollar()
    {
        DelegateCenter.addCoinOrDollarBlock?.Invoke(TableRewardsList.inst.list[RewardId].Number);
    }

    /// <summary>
    /// 增加水果
    /// </summary>
    private void addFruit()
    {
        FruitsModel.Instance.addFruits(FruitsRewardId,1);
    }

    /// <summary>
    /// 老虎机结束后弹出奖励动画
    /// </summary>
    private void rewardAnimation()
    {
        RewardGotImageGO.SetActive(true);

        RewardGotImage.transform.DOBlendableScaleBy(Vector3.one, 2f).OnComplete(()=> {
            RewardGotImage.transform.localScale = Vector3.zero;
        });

        RewardGotImage.transform.DOBlendableLocalRotateBy(new Vector3(0f, 0f, 360f), 2f, RotateMode.LocalAxisAdd).OnComplete(()=>{
            RewardGotImage.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            RewardGotImageGO.SetActive(false);

            SlotMachineNeededAgain();
        });

    }

    void SlotMachineNeededAgain()
    {
        isPlaying = false;
        Debug.Log("老虎机动画事件已经结束！");
        slotMachineGoals();                     // 判断后续是否还有中球
    }

    /// <summary>
    /// 根据老虎机中当前游标获取对应图标
    /// </summary>
    /// <param name="nextRICIndex"></param>
    /// <returns></returns>
    public Sprite AccordingCurserToSprite(int slotMachineLaneId)
    {
        //print("aaaaaaaa----->"+nextRewardIdCursorArr[slotMachineLaneId]);
        if (nextRewardIdCursorArr[slotMachineLaneId] < TableFruitsList.inst.list.Count)
        {
            return SpriteAtlasManager.Instance.GetSprite(TableFruitsList.inst.list[nextRewardIdCursorArr[slotMachineLaneId]].FruitName);
        }
        else
        {
            return SpriteAtlasManager.Instance.GetSprite(TableSlotMachineList.inst.list[ nextRewardIdCursorArr[slotMachineLaneId] - TableFruitsList.inst.list.Count + 1 ].IconName);
        }
    }


}






