using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreen : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateOptionsScreen();
        }
    }
    // Start is called before the first frame update
    public void DeactivateOptionsScreen()
    {
        gameObject.SetActive(false);
    }
}
