using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] sfx;
    //Referencias a la m�sica del juego
    public AudioSource menuMusic, lvlOne, lvlTwo, lvlThree, lvlSelector, shopMusic, bossMusic, creditsMusic;

    //Hacemos el Singleton de este script
    public static AudioManager aMRef;

    private void Awake()
    {
        //Si la referencia del Singleton esta vac�a
        if (aMRef == null)
            //La rrellenamos con todo el contenido de este c�digo (para que todo sea accesible)
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

    //M�todo para reproducir los sonidos
    public void PlaySFX(int soundToPlay) //soundToPlay = sera el sonido n�mero X del array que queremos reproducir
    {
        //Si ya estaba reproduciendo el sonido, lo paramos
        sfx[soundToPlay].Stop();
        //Alteramos un poco el sonido cada vez que se vaya a reproducir
        sfx[soundToPlay].pitch = Random.Range(.95f, 1.05f);
        //Reproducir el sonido pasado por par�metro
        sfx[soundToPlay].Play();
    }

    //M�todo para reproducir la m�sica del Boss Final
    public void PlayBossMusic()
    {
        //Reproducimos la m�sica del jefe
        bossMusic.Play();
    }
}