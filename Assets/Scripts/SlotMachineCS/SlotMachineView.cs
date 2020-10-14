using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class SlotMachineView : MonoBehaviour
{
    
    int slotMachineLaneId;   //老虎机中的随机数的列数Id

    int stepsTotal;
    float deltaDistance = 0f;
    float moveTime = 0.033f;

    public GameObject UpFlagImage;
    public GameObject DownFlagImage;

    Image ThisMoveImage;

    SlotMachineControl slotMachineControl;
    //Text testText;
    //Tweener tweener = null;
    void Start()
    {
        slotMachineControl = transform.GetComponentInParent<SlotMachineControl>();
        ThisMoveImage = gameObject.GetComponent<Image>();

        slotMachineControl.launchSlotMachineAction += launchSlotMachine;

        switch (transform.parent.parent.gameObject.name)
        {
            case "GunDongLieImage": slotMachineLaneId = 0;break;
            case "GunDongLieImage (1)": slotMachineLaneId = 1; break;
            case "GunDongLieImage (2)": slotMachineLaneId = 2; break;
            default: break;
        }

        stepsTotal = slotMachineControl.stepsTotalArray[slotMachineLaneId];

        deltaDistance = (DownFlagImage.transform.localPosition.y - UpFlagImage.transform.localPosition.y) / 4;

        //testText = transform.GetComponentInChildren<Text>();

        //tweener = transform.DOLocalMove(new Vector3(0, transform.localPosition.y + deltaDistance, 0), 1f).SetLoops(-1, LoopType.Incremental).OnComplete(() =>
        //{
        //    Debug.Log("CCCCCCCCCC" + deltaDistance + "CCCCCCC" + transform.localPosition);
        //    if (transform.localPosition.y == DownFlagImage.transform.localPosition.y)
        //    {
        //        transform.localPosition = UpFlagImage.transform.localPosition;
        //    }
        //});

    }

    void launchSlotMachine()
    {
        stepsTotal = slotMachineControl.stepsTotalArray[slotMachineLaneId];
        moveOneByOne();
    }

    void moveOneByOne()
    {
        
        if (stepsTotal !=0) {
            
        
            transform.DOLocalMove(new Vector3(0, transform.localPosition.y + deltaDistance, 0), moveTime).SetEase(Ease.Linear).OnComplete(()=> {
                stepsTotal--;

                if (transform.localPosition.y == DownFlagImage.transform.localPosition.y)
                {
                    //transform.localPosition = new Vector3(0, UpFlagImage.transform.localPosition.y + deltaDistance, 0);
                    transform.localPosition = UpFlagImage.transform.localPosition;                                                 // 返回顶部

                    ThisMoveImage.sprite = slotMachineControl.AccordingCurserToSprite(slotMachineLaneId);        // 更新图片

                    slotMachineControl.nextRewardIdCursorArr[slotMachineLaneId]++;                                           // 下一个元素游标后移一

                    if (slotMachineControl.nextRewardIdCursorArr[slotMachineLaneId] >= slotMachineControl.smRewardArray.Length)
                        slotMachineControl.nextRewardIdCursorArr[slotMachineLaneId] = 0;

                }

                if (stepsTotal > 0) moveOneByOne();
                else slotMachineControl.animationOver();
            });

        }

    }


    //private void Play()
    //{
    //    if (transform.localPosition.y == DownFlagImage.transform.localPosition.y)
    //    {
    //        transform.localPosition = new Vector3(0, 130);
    //    }
    //    else
    //    {
    //        Debug.Log("SSSSSSSSSS");
    //        //y = y - _y;
    //        transform.DOPlayForward();
    //    }
    //}

}
