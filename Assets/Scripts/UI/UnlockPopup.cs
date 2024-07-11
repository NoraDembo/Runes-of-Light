using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPopup : MonoBehaviour
{
    public float activeAlpha = 0.8f;
    public float fade;
    public float showTime = 2;
    CanvasGroup group;
    Image displayIcon;

    float timer = 0;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
        displayIcon = transform.Find("UnlockPopupIcon").GetComponent<Image>();
    }

    void Update()
    {
        if (timer >= showTime && group.alpha > 0)
        {
            group.alpha -= fade * Time.deltaTime;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void Show(Sprite icon)
    {
        displayIcon.sprite = icon;
        timer = 0;
        group.alpha = activeAlpha;
    }
}
