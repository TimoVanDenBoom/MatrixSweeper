using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsScript : MonoBehaviour
{
    public AudioClip menuMouseDownClip;
    public AudioClip menuMouseOverClip;
    public AudioClip mouseOverRowClip;
    public AudioClip mouseUpRowClip;
    public AudioClip mouseDownRowClip;
    public AudioClip mouseOverOperationClip;
    public AudioClip mouseUpAdditionClip;
    public AudioClip mouseDownOperationClip;
    public AudioClip mouseUpSubtractionClip;

    private List<AudioSource> SoundEffects = new List<AudioSource>();

    public static float soundEffectsVolume = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, menuMouseDownClip));
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, menuMouseOverClip));
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, mouseOverRowClip));
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, mouseUpRowClip));
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, mouseDownRowClip));
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, mouseOverOperationClip));
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, mouseUpAdditionClip));
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, mouseDownOperationClip));
        SoundEffects.Add(AddAudio(false, false, soundEffectsVolume, mouseUpSubtractionClip));

        transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
    }

    public void playMenuMouseDownSound()
    {
        SoundEffects[0].Play();
    }

    public void playMenuMouseOverSound()
    {
        SoundEffects[1].Play();
    }

    public void playMouseOverRowClip()
    {
        SoundEffects[2].Play();
    }

    public void playMouseUpRowClip()
    {
        SoundEffects[3].Play();
    }

    public void playMouseDownRowClip()
    {
        SoundEffects[4].Play();
    }

    public void playMouseOverOperationClip()
    {
        SoundEffects[5].Play();
    }

    public void playMouseUpAdditionClip()
    {
        SoundEffects[6].Play();
    }

    public void playMouseDownOperationClip()
    {
        SoundEffects[7].Play();
    }

    public void playMouseUpSubtractionClip()
    {
        SoundEffects[8].Play();
    }

    public void setSoundEffectsVolume(float volume)
    {
        soundEffectsVolume = volume;
        foreach (AudioSource fx in SoundEffects)
        {
            fx.volume = volume;
        }
    }
    public void setSpecificSoundEffectsVolume(float volume, int fx)
    {
        SoundEffects[fx].volume = volume;
    }

    public AudioSource AddAudio(bool loop, bool playAwake, float vol, AudioClip clip)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }
}
