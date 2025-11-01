using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource saveSource;
    [SerializeField] AudioSource damageSource;
    [SerializeField] AudioSource deathSource;
    [SerializeField] List<AudioClip> saveClips = new List<AudioClip>() ;
    [SerializeField] List<AudioClip> deathClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> damageClips = new List<AudioClip>();

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    private void Awake()
    {
        if (instance == null) 
        {
          instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        mixer.SetFloat(VolumeSliders.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSliders.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }


    public void SaveSFX()
    {
        
        AudioClip clip = saveClips[Random.Range(0, saveClips.Count)];
        saveSource.PlayOneShot(clip);

    }

    public void DamageSFX()
    {

        AudioClip clip = damageClips[Random.Range(0, damageClips.Count)];
        damageSource.PlayOneShot(clip);

    }

    public void DeathSFX()
    {

        AudioClip clip = deathClips[Random.Range(0, deathClips.Count)];
        deathSource.PlayOneShot(clip);

    }
}
