using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    //L�neas del di�logo
    public string[] lines;
    //Para saber si el di�logo se puede activar o no
    private bool canActivate;
    private bool hasBeenActivated;
    //Sprite de di�logo del NPC
    public Sprite theNpcSprite;

    public bool activatesPursuit;
    public bool activatesBossBattle;

    //Si el jugador entra en la zona de Trigger puede activar el di�logo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Llamamos al m�todo que muestra el di�logo y le pasamos las l�neas concretas que contiene este objeto
        if (collision.CompareTag("Player") && !hasBeenActivated)
        {
            DialogManager.instance.ShowDialog(lines, theNpcSprite);

            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (activatesPursuit)
            {
                GameObject.Find("PursuitTrigger").GetComponent<LevelOnePursuitTrigger>().SpawnPursuit();
            }
            if (activatesBossBattle)
            {
                DialogManager.instance.activatesBossBattle = true;
            }
        }
           
    }

    //Si el jugador sale de la zona de Trigger ya no puede activar le di�logo
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasBeenActivated = true;

            if(activatesPursuit)
                GameObject.FindWithTag("Pursuit").GetComponent<LevelOnePursuit>().speed = 3.65f;
        }
    }
}
