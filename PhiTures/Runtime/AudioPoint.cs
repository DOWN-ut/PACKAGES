using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPoint : MonoBehaviour
{
    [SerializeField] private AudioElement[] clips;
    public AudioSource audioSource { get { return GetComponent<AudioSource>(); } }
    public bool isPlaying { get { return audioSource.isPlaying; } }
    public bool isComplete { get { return ( audioSource != null && clips != null ) && ( clips != null ? clips.Length > 0 : false ); } }
    public float pitch { get { return isComplete ? audioSource.pitch : 0; } }
    public void SetAudio(AudioElement ae ){ AudioElement.SetupAudioSource( audioSource , ae );  }
    public void SetVolume(float v ) {  audioSource.volume = v;  }
    public void SetPitch(float p ) { audioSource.pitch = p; }

    public bool Play (bool setAudio = true )
    {
        if(!isComplete) { return false; }

        AudioElement ae = GetAudio();
        if (setAudio) { SetAudio( ae ); }
        audioSource.PlayOneShot( ae );

        return true;
    }
    public bool Play (AudioClip a)
    {
        if (!isComplete) { return false; }

        audioSource.PlayOneShot( a );

        return true;
    }
    public bool Play (AudioElement a , bool setAudio = true )
    {
        if (!isComplete) { return false; }

        if (setAudio) { SetAudio( a); }
        audioSource.PlayOneShot( a );

        return true;
    }
    public bool Play (int i,bool setAudio = true)
    {
        if (!isComplete) { return false; }

        AudioElement ae = GetAudio(i);
        if (setAudio) { SetAudio( ae ); }
        audioSource.PlayOneShot( ae );

        return true;
    }

    public bool Stop ()
    {
        if (!isComplete) { return false; }

        audioSource.Stop();

        return true;
    }

    public AudioElement GetAudio ()
    {
        if (clips == null) { return AudioElement.NULL; }
        else if(clips.Length <= 0) { return AudioElement.NULL; }
        return AudioElement.GetRandom( clips );
    }
    public AudioElement GetAudio(int index )
    {
        if(clips == null) { return AudioElement.NULL; }
        else if (index < 0 || index > clips.Length) { return AudioElement.NULL; }
        return clips[index];
    }
}
