using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public float fadingSpeed;

    CanvasGroup c;

    void Start()
    {
        c = GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());
    }


    public void StartGame()
    {
        StartCoroutine(FadeThenStart());
    }

    public void BackToMenu()
    {
        StartCoroutine(FadeThenMenu());
    }


    IEnumerator FadeIn()
    {
        for (float ft = 1f; ft >= -0.001f; ft -= fadingSpeed)
        {
            c.alpha = ft;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        for (float ft = 0f; ft <= 1.001f; ft += fadingSpeed)
        {
            c.alpha = ft;
            yield return null;
        }
    }

    IEnumerator FadeThenStart()
    {
        yield return StartCoroutine(FadeOut());
        GameManager.StartRound();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeThenMenu()
    {
        yield return StartCoroutine(FadeOut());
        GameManager.MainMenu();
        StartCoroutine(FadeIn());
    }
}
