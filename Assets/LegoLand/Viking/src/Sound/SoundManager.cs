using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    protected SoundManager() { }

    public int numsOfSFXChnnel = 20;
    SoundChannel bgmChannel;
    Queue<SoundChannel> seQueue;

    public bool isPlayBGM;

    private void Awake()
    {
        gameObject.name = "Sound Manager";

        var bgmChannelObj = new GameObject();
        bgmChannel = bgmChannelObj.AddComponent<SoundChannel>();
        bgmChannel.Loop = true;
        bgmChannel.gameObject.transform.parent = transform;
        bgmChannel.name = "BGM Channel";

        seQueue = new Queue<SoundChannel>();
        for (int i = 0; i < numsOfSFXChnnel; i++)
        {
            var seChannelObj = new GameObject();
            var inst = seChannelObj.AddComponent<SoundChannel>();
            seQueue.Enqueue(inst);
            inst.transform.parent = transform;
            inst.gameObject.SetActive(false);
        }
    }

    public void PauseBGM()
    {
        bgmChannel.Pause();
    }

    public void StopBGM()
    {
        bgmChannel.Stop();
    }

    public void PlayUIAudio(AudioClip sfx)
    {
        if (seQueue.Count <= 0) return;
        var channel = seQueue.Dequeue();
        channel.audioClip = sfx;
        channel.gameObject.SetActive(true);
        channel.transform.parent = null;
        channel.transform.position = new Vector3(0,0,0);
    }

    public void PlayBGM(AudioClip bgm)
    {
        bgmChannel.audioClip = bgm;
        bgmChannel.Play();
    }

    public void PlaySE(AudioClip se, Vector3 position)
    {
        if (seQueue.Count <= 0) return;
        var channel = seQueue.Dequeue();
        channel.audioClip = se;
        channel.gameObject.SetActive(true);
        channel.transform.parent = null;
        channel.transform.position = position;
    }

    public void WithdrawChannel(SoundChannel channel)
    {
        seQueue.Enqueue(channel);
        channel.transform.parent = transform;
        channel.gameObject.SetActive(false);
    }
}
