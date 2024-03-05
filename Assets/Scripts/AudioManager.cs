using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
    Shoot, Destroy, Select
}

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public static AudioManager AudioManagerSin;

    [SerializeField] AudioSource AudioSourcePrefab;
 
    [SerializeField] AudioClip[] Clips_Shoot, Clips_Destroy;

    void Awake()
    {
        //Singleton
        if (AudioManagerSin != null && AudioManagerSin != this) Destroy(this);
        else AudioManagerSin = this;
    }

    AudioSource audioSource;
    public void PlaySoundEffect(SFX sound, Transform spawnTrans)
    {
        AudioClip clip = null;
        switch ( sound )
        {
            case SFX.Shoot:
                if (Clips_Shoot ==null) {
                    Debug.Log("There is no audio clip of shoot!");
                    return;
                }
                if (Clips_Shoot.Length > 1) {
                    clip = Clips_Shoot[Random.Range(0, Clips_Shoot.Length)];
                }
                else {
                    clip = Clips_Shoot[0];
                }
                break;
        }

        audioSource = Instantiate(AudioSourcePrefab, Vector3.zero, Quaternion.identity, spawnTrans);
        audioSource.clip = clip;
        audioSource.volume = 1f;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

}
