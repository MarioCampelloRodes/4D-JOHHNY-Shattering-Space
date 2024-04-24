using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] sfx;
    //Referencias a la música del juego
    public AudioSource menuMusic, lvlOne, lvlTwo, lvlThree, lvlSelector, shopMusic, bossMusic, creditsMusic;

    //Hacemos el Singleton de este script
    public static AudioManager aMRef;

    private void Awake()
    {
        //Si la referencia del Singleton esta vacía
        if (aMRef == null)
            //La rrellenamos con todo el contenido de este código (para que todo sea accesible)
            aMRef = this;
    }

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level-1":
                lvlOne.Play();
                break;
            case "Level-2":
                lvlTwo.Play();
                break;
            case "Level-3":
                lvlThree.Play();
                break;
            case "LevelSelector":
                lvlSelector.Play();
                break;
            case "MainMenu":
                menuMusic.Play();
                break;
            case "Shop":
                shopMusic.Play();
                break;
            case "Credits":
                creditsMusic.Play();
                break;
            case "Boss":
                break;
            default:
                lvlThree.Play();
                break;
        }
    }

    //Método para reproducir los sonidos
    public void PlaySFX(int soundToPlay) //soundToPlay = sera el sonido número X del array que queremos reproducir
    {
        //Si ya estaba reproduciendo el sonido, lo paramos
        sfx[soundToPlay].Stop();
        //Alteramos un poco el sonido cada vez que se vaya a reproducir
        sfx[soundToPlay].pitch = Random.Range(.95f, 1.05f);
        //Reproducir el sonido pasado por parámetro
        sfx[soundToPlay].Play();
    }

    //Método para reproducir la música del Boss Final
    public void PlayBossMusic()
    {
        //Reproducimos la música del jefe
        bossMusic.Play();
    }
}