using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] Sound;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sounds s in Sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.outputAudioMixerGroup = s.Output;

            s.source.clip = s.audioClip;

            s.source.volume = s.Volume;

            s.source.pitch = s.Pitch;

            s.source.loop = s.loop;
        }
    }

    public void Play(string AudioName)
    {
        Sounds s = Array.Find(Sound, sound => sound.Name == AudioName);
        if(s != null)
            s.source.Play();
    }

    // For Modifications
    public AudioSource FindSource(string AudioName)
    {
        Sounds s = Array.Find(Sound, sound => sound.Name == AudioName);

        return s.source;
    }
}