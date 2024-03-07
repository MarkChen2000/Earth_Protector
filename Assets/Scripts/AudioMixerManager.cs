using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer _AudioMixer;

    /*public void SetMasterVolume(bool enable, float level)
    {
        if ( enable == false ) _AudioMixer.SetFloat("MasterVolume", -80f);
        else _AudioMixer.SetFloat("MasterVolume",level);
    }*/

    public void SetBGMVolume(bool enable)
    {
        if (enable == false) _AudioMixer.SetFloat("BGMVolume", -80f);
        else  _AudioMixer.SetFloat("BGMVolume", 0f);
    }

    public void SetSFXVolume(bool enable)
    {
        if (enable == false) _AudioMixer.SetFloat("SFXVolume", -80f);
        else _AudioMixer.SetFloat("SFXVolume", 0f);
    }
}
