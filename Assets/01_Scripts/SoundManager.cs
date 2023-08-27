using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    public enum EBgm
    {
        BGM_TITLE,
        BGM_GAME,
    }

    public enum ESfx
    {
        SFX_BUTTON,
        SFX_ENDING,
        SFX_TOKEN,
        SFX_BOTTLE,
        SFX_OPENDOOR,
        SFX_MissionClear
    }

    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;

    [SerializeField] AudioSource audioBgm;
    [SerializeField] AudioSource audioSfx;

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
    }

    public void PlayBGM(EBgm bgmIdx)
    {
        audioBgm.clip = bgms[(int)bgmIdx];

        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    public void PlaySFX(ESfx esfx)
    {
        audioSfx.PlayOneShot(sfxs[(int)esfx]);
    }
}
