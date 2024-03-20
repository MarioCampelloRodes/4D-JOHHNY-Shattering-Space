using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIControl : MonoBehaviour
{
    public Image fadeScreen;
    public float fadeSpeed;

    private bool _shouldFade, _shouldUnfade;
    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldFade)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a >= 1f)
            {
                _shouldFade = false;
            }
        }

        if (_shouldUnfade)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a <= 0f)
            {
                _shouldUnfade = false;
            }
        }
    }

    public void FadeToBlack()
    {
        _shouldFade = true;
        _shouldUnfade = false;
    }

    public void FadeFromBlack()
    {
        _shouldFade = false;
        _shouldUnfade = true;
    }
}
