using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject objectToSwitch;

    public Sprite downSprite, upSprite;

    private bool _activateSwitch;

    public GameObject infoPanel;

    private SpriteRenderer _sPRef;

    private PlayerController _pCRef;
    // Start is called before the first frame update
    void Start()
    {
        _sPRef = GetComponent<SpriteRenderer>();

        _pCRef = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _pCRef.canInteract)
        {
            if(objectToSwitch.GetComponent<Door>().isActive == false)
            {
                objectToSwitch.GetComponent<Door>().ActivateObject();
                objectToSwitch.GetComponent<Door>().isActive = true;
            }
            else if (objectToSwitch.GetComponent<Door>().isActive == true)
            {
                objectToSwitch.GetComponent<Door>().DeactivateObject();
                objectToSwitch.GetComponent<Door>().isActive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            infoPanel.SetActive(true);
            collision.GetComponent<PlayerController>().canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            infoPanel.SetActive(false);
            collision.GetComponent<PlayerController>().canInteract = false;
        }
    }
}
