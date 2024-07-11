using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScreen : MonoBehaviour
{
    public float scrollSpeed;
    public float startPos = -1150;
    RectTransform text;

    Vector2 currentPosition;
    bool scrolling = false;

    void Start()
    {
        currentPosition = new Vector2(0, startPos);

        text = transform.Find("Text").GetComponent<RectTransform>();
        text.anchoredPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (scrolling)
        {
            currentPosition.y += scrollSpeed;
            text.anchoredPosition = currentPosition;
        }
        
    }

    public void StartScrolling()
    {
        scrolling = true;
    }

    public void StopScrolling()
    {
        scrolling = false;
        currentPosition.y = startPos;
    }
}
