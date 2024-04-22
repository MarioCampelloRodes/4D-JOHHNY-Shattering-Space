using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] sfx;
    //Referencias a la m�sica del juego
    public AudioSource menuMusic, lvlOne, lvlTwo, lvlThree, lvlSelector, shopMusic, bossMusic;

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
                Debug.Log("Se est� reproduciendo la Soundtrack " + lvlOne.ToString());
                break;
            case "Level-2":
                lvlTwo.Play();
                Debug.Log("Se est� reproduciendo la Soundtrack " + lvlTwo.ToString());
                break;
            case "Level-3":
                lvlThree.Play();
                Debug.Log("Se est� reproduciendo la Soundtrack " + lvlThree.ToString());
                break;
            case "LevelSelector":
                lvlSelector.Play();
                Debug.Log("Se est� reproduciendo la Soundtrack " + lvlSelector.ToString());
                break;
            case "MainMenu":
                menuMusic.Play();
                Debug.Log("Se est� reproduciendo la Soundtrack " + menuMusic.ToString());
                break;
            case "Shop":
                shopMusic.Play();
                Debug.Log("Se est� reproduciendo la Soundtrack " + shopMusic.ToString());
                break;
            default:
                lvlThree.Play();
                Debug.Log("Se est� reproduciendo la Soundtrack " + lvlThree.ToString());
                break;
        }
    }

    //M�todo para reproducir los sonidos
    public void PlaySFX(int soundToPlay) //soundToPlay = sera el sonido n�mero X del array que queremos reproducir
    {
        //Si ya estaba reproduciendo el sonido, lo paramos
        sfx[soundToPlay].Stop();
        //Alteramos un poco el sonido cada vez que se vaya a reproducir
        sfx[soundToPlay].pitch = Random.Range(.9f, 1.1f);
        //Reproducir el sonido pasado por par�metro
        sfx[soundToPlay].Play();
    }

    //M�todo para reproducir la m�sica del Boss Final
    public void PlayBossMusic()
    {
        //Reproducimos la m�sica del jefe
        bossMusic.Play();
        Debug.Log("Se est� reproduciendo la Soundtrack " + bossMusic.ToString());
    }
}