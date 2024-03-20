using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateObject()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }
    public void DeactivateObject()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }

}
