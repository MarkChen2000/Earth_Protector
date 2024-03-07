using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM_State
{
    Menu, Game, GameOver
}

public enum SFX
{
    Gun_Shoot, Meteor_Destroy, Earth_Destroy, Select, Select_Up, Select_Down
}

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public static AudioManager AudioManagerSin;

    [SerializeField] AudioSource BGMAudioSource, AudioSourcePrefab;

    [SerializeField] AudioClip BGM_MainMenu, BGM_Game, BGM_GameOver;

    [SerializeField] AudioClip[] SFX_Gun_Shoot, SFX_Meteor_Destroy, SFX_Earth_Destroy, SFX_Select;

    void Awake()
    {
        //Singleton
        if (AudioManagerSin != null && AudioManagerSin != this) Destroy(this);
        else AudioManagerSin = this;
    }

    public void StartBGM(BGM_State state)
    {
        switch ( state )
        {
            case BGM_State.Menu:
                BGMAudioSource.clip = BGM_MainMenu;
                break;
            case BGM_State.Game:
                BGMAudioSource.clip = BGM_Game;
                break;
            case BGM_State.GameOver:
                BGMAudioSource.clip = BGM_GameOver;
                break;
        }
        if ( BGMAudioSource.clip == null) {
            Debug.Log("There is no BGM can be played!");
            return;
        }
        BGMAudioSource.Play();
    }

    AudioSource audioSource;
    public void PlaySoundEffect(SFX sound, Transform spawnTrans)
    {
        AudioClip clip = null;
        switch ( sound )
        {
            case SFX.Gun_Shoot:
                if (CheckAndPlayClip(SFX_Gun_Shoot) == null) return;
                else clip = CheckAndPlayClip(SFX_Gun_Shoot);
                break;
            case SFX.Meteor_Destroy:
                if (CheckAndPlayClip(SFX_Meteor_Destroy) == null) return;
                else clip = CheckAndPlayClip(SFX_Meteor_Destroy);
                break;
            case SFX.Earth_Destroy:
                if (CheckAndPlayClip(SFX_Earth_Destroy) == null) return;
                else clip = CheckAndPlayClip(SFX_Earth_Destroy);
                break;
            case SFX.Select:
                if (CheckAndPlayClip(SFX_Select) == null) return;
                else clip = CheckAndPlayClip(SFX_Select);
                break;
        }

        audioSource = Instantiate(AudioSourcePrefab, Vector3.zero, Quaternion.identity, spawnTrans);
        audioSource.clip = clip;
        audioSource.volume = 1f;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    AudioClip CheckAndPlayClip(AudioClip[] clips)
    {
        if (clips.Length == 0) {
            Debug.Log("There is no audio clip of " + clips);
            return null;
        }
        else if (clips.Length > 1) {
            return clips[Random.Range(0, clips.Length)];
        }
        else {
            return clips[0];
        }
    }

}
