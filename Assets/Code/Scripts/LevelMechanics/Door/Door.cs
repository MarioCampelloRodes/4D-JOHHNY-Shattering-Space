using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isActive = false;

    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateObject()
    {
        _anim.SetTrigger("Close");
    }
    public void DeactivateObject()
    {
        _anim.SetTrigger("Open");
    }

}
