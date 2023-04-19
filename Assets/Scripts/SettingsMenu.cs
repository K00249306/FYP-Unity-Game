using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer volumeMixer;

    // Sets the voulume of sound effects
    public void SetSFXVolume(float sfxVolume)
    {
        volumeMixer.SetFloat("sfxVolume", sfxVolume);
    }

    // Sets the voulume of music
    public void SetMusicVolume (float musicVolume)
    {
        volumeMixer.SetFloat("musicVolume", musicVolume);
    }
}
