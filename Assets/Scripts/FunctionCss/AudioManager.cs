using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音乐音效的简单管理器
/// </summary>
[RequireComponent(typeof(AudioSource))]

public class AudioManager : Inst<AudioManager>
{
    /// <summary>
    /// 将声音放入字典中，方便管理
    /// </summary>
    private Dictionary<string, AudioClip> _soundDictionary;

    //背景音乐和音效的音频源
    private AudioSource[] audioSources;
    private AudioSource bgAudioSource;
    private AudioSource audioSourceEffect;

    public void Init()
    {
        gameObject.AddComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayBGAudio("BGM");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayBGAudio("ButtonClick");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayAudioEffect("Cannon");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayAudioEffect("Click");
        }
    }
    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        //加载资源存的所有音频资源
        LoadAudio();
        //获取音频源
        audioSources = this.GetComponents<AudioSource>();
        bgAudioSource = audioSources[0];
        bgAudioSource.playOnAwake = true;
        bgAudioSource.loop = true;
        audioSourceEffect = audioSources[1];
        audioSourceEffect.playOnAwake = false;
        audioSourceEffect.loop = false;
    }
    /// <summary>
    /// 加载声音资源
    /// </summary>
    void LoadAudio()
    {

        //初始化字典
        _soundDictionary = new Dictionary<string, AudioClip>();
        //本地加载 "Audios"文件夹名
        AudioClip[] audioArray = Resources.LoadAll<AudioClip>("Audios");
        //存放到字典
        foreach (AudioClip item in audioArray)
        {
            _soundDictionary.Add(item.name, item);
        }
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioName">名字</param>
    public void PlayBGAudio(string audioName)
    {
        if (_soundDictionary.ContainsKey(audioName))
        {
            bgAudioSource.clip = _soundDictionary[audioName];
            bgAudioSource.Play();
        }
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioEffectName">名字</param>
    public void PlayAudioEffect(string audioEffectName)
    {
        if (GameData.Sound != 0 && _soundDictionary.ContainsKey(audioEffectName))
        {
            //audioSourceEffect.clip = _soundDictionary[audioEffectName];
            //audioSourceEffect.Play();
            AudioClip clip = _soundDictionary[audioEffectName];
            AudioSource.PlayClipAtPoint(clip, Vector3.zero, GameData.Sound);
        }
    }
}