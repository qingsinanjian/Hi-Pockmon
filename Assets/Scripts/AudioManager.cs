using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicPlayer;
    public AudioSource affectPlayer;

    private void Awake()
    {
        instance = this;
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        if (musicPlayer.isPlaying) return;

        musicPlayer.clip = clip;
        musicPlayer.loop = loop;
        musicPlayer.Play();
    }

    public void PlayAffect(AudioClip clip, bool isStop)
    {
        if(clip == null) return;
        if (isStop)
        {
            affectPlayer.Stop();
        }
        else
        {
            affectPlayer.Play();
        }
    }
}
