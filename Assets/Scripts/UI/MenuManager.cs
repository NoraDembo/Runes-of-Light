using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    MenuCamera cam;

    public CanvasGroup runeScreen;
    public CanvasGroup statScreen;
    public CanvasGroup creditScreen;

    public Animator characterAnimator;

    CanvasGroup activeGroup;

    public float fadingSpeed;

    void Start()
    {
        cam = Camera.main.GetComponent<MenuCamera>();
    }

    void Update()
    {
        
    }

    public void ClickPlay()
    {
        GameManager.screenFader.StartGame();
    }

    public void ClickRunes()
    {
        runeScreen.GetComponent<RuneScreen>().LoadRunes();

        activeGroup = runeScreen;
        activeGroup.interactable = true;
        activeGroup.blocksRaycasts = true;
        StartCoroutine(FadeIn(activeGroup));
        characterAnimator.SetTrigger("WalkToWall");
        cam.SetPosition("sub");
    }

    public void ClickStats()
    {
        activeGroup = statScreen;
        activeGroup.interactable = true;
        activeGroup.blocksRaycasts = true;
        StartCoroutine(FadeIn(activeGroup));
        characterAnimator.SetTrigger("WalkToWall");
        cam.SetPosition("sub");
    }

    public void ClickCredits()
    {
        creditScreen.GetComponent<CreditScreen>().StartScrolling();

        activeGroup = creditScreen;
        activeGroup.interactable = true;
        activeGroup.blocksRaycasts = true;
        StartCoroutine(FadeIn(activeGroup));
        characterAnimator.SetTrigger("WalkToWall");
        cam.SetPosition("sub");
    }

    public void ClickMenu()
    {
        if(activeGroup == creditScreen)
        {
            creditScreen.GetComponent<CreditScreen>().StopScrolling();
        }

        activeGroup.interactable = false;
        activeGroup.blocksRaycasts = false;
        StartCoroutine(FadeOut(activeGroup));
        characterAnimator.SetTrigger("WalkBack");
        cam.SetPosition("main");
    }

    public void ClickQuit()
    {
        Application.Quit();
    }

    IEnumerator FadeIn(CanvasGroup c)
    {
        for (float ft = 0f; ft <= 1.001f; ft += fadingSpeed)
        {
            c.alpha = ft;
            yield return null;
        }
    }

    IEnumerator FadeOut(CanvasGroup c)
    {
        for (float ft = 1f; ft >= -0.001f; ft -= fadingSpeed)
        {
            c.alpha = ft;
            yield return null;
        }
    }
}
