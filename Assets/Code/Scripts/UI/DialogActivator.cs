using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    //Líneas del diálogo
    public string[] lines;
    //Para saber si el diálogo se puede activar o no
    private bool canActivate;
    private bool hasBeenActivated;
    //Sprite de diálogo del NPC
    public Sprite theNpcSprite;

    public bool activatesPursuit;
    public bool activatesBossBattle;

    //Si el jugador entra en la zona de Trigger puede activar el diálogo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Llamamos al método que muestra el diálogo y le pasamos las líneas concretas que contiene este objeto
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

    //Si el jugador sale de la zona de Trigger ya no puede activar le diálogo
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
