using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip audioClip;
    private AudioSource audioSource;

    public float volume;
    public bool loop;

    public void SetSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        this.audioSource.clip = audioClip;
        this.audioSource.volume = volume;
        this.audioSource.loop = loop;
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop() { audioSource.Stop(); }

    public void SetLoop()
    {
        audioSource.loop = true;
    }
    
    public void SetLoopCancel()
    {
        audioSource.loop = false;
    }

}
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] sounds;

    private void Start()
    {
        for(int i = 0; i < sounds.Length; i++){
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + "=" +  sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void Play(string name)
    {
        for(int i =0; i < sounds.Length; i++)
        {
            if(name == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void Stop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void SetLoop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].SetLoop();
                return;
            }
        }
    }

    public void SetLoopCancel (string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].SetLoopCancel();
                return;
            }
        }
    }

    public void SetVolume(string name, float volume)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].SetVolume(volume);
                return;
            }
        }
    }
}
