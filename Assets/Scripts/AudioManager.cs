using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistenSingleton<AudioManager>
{
    [SerializeField] AudioSource bgmPlayer;

    const float MIN_PITCH = 0.9f;
    const float MAX_PITCH = 1.1f;

    public void PlayBGM(AudioData audioData)
    {
        bgmPlayer.PlayOneShot(audioData.audioClip, audioData.volume);
    }

    public void PlayRandomBGM(AudioData audioData)
    {
        bgmPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlayBGM(audioData);
    }

    public void PlayBGMSFX(AudioData[] audioDatas)
    {
        PlayRandomBGM(audioDatas[Random.Range(0, audioDatas.Length)]);
    }
}
[System.Serializable] public class AudioData
{
    public AudioClip audioClip;
    public float volume;
}
