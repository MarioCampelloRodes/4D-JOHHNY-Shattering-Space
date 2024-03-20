using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource bgm, levelMusic, bossMusic;

    public static AudioManager aMRef;

    private void Awake()
    {
        if(aMRef == null)
        {
            aMRef = this;
        }

        bgm.Play();
    }
    
    public void PlaySFX(int soundToPlay)
    {
        sfx[soundToPlay].Stop();

        sfx[soundToPlay].pitch = Random.Range(0.9f, 1.1f);
        sfx[soundToPlay].Play();

        Debug.Log("Se está reproduciendo el sonido" + sfx[soundToPlay] + " con un pitch de " + sfx[soundToPlay].pitch);
    }
}
