using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 序列帧动画
/// </summary>
[RequireComponent(typeof(Image))]
public class UGUIAnimation : MonoBehaviour
{
    private Image ImageSource;
    private int mCurFrame = 0;
    private float mDelta = 0;
    [Header("帧率")]
    public float FPS = 60;
    [Header("图片集")]
    public List<Sprite> SpriteFrames;
    [Header("正在播放")]
    public bool IsPlaying = false;
    [Header("正播")]
    public bool Foward = true;
    [Header("自动播放")]
    public bool AutoPlay = false;
    [Header("循环")]
    public bool Loop = false;
    [Header("保持原来大小")]
    public bool Snap = true;

    public int FrameCount
    {
        get
        {
            return SpriteFrames.Count;
        }
    }

    void Awake()
    {
        ImageSource = GetComponent<Image>();

        if (AutoPlay)
        {
            Play();
        }
        else
        {
            IsPlaying = false;
        }
    }



    void Start()
    {

    }

    private void SetSprite(int idx)
    {
        ImageSource.sprite = SpriteFrames[idx];
        if (Snap)
        {
            ImageSource.SetNativeSize();
        }

    }

    public void Play()
    {

        IsPlaying = true;
        Foward = true;
    }

    public void PlayReverse()
    {
        IsPlaying = true;
        Foward = false;
        mCurFrame = SpriteFrames.Count;

    }

    void Update()
    {
        if (!IsPlaying || 0 == FrameCount)
        {
            return;
        }

        mDelta += Time.deltaTime;
        if (mDelta > 1 / FPS)
        {
            mDelta = 0;
            if (Foward)
            {
                mCurFrame++;
            }
            else
            {
                mCurFrame--;
            }

            if (mCurFrame >= FrameCount)
            {
                if (Loop)
                {
                    mCurFrame = 0;
                }
                else
                {
                    IsPlaying = false;
                    return;
                }
            }
            else if (mCurFrame < 0)
            {
                if (Loop)
                {
                    mCurFrame = FrameCount - 1;
                }
                else
                {
                    IsPlaying = false;
                    return;
                }
            }

            SetSprite(mCurFrame);
        }
    }

    public void Pause()
    {
        IsPlaying = false;
    }

    public void Resume()
    {
        if (!IsPlaying)
        {
            IsPlaying = true;
        }
    }

    public void Stop()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        IsPlaying = false;
    }

    public void Rewind()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        Play();
    }
}