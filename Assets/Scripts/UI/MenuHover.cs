using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHover : MonoBehaviour
{
    public Color tintColor;
    public Image targetIcon;


    public void Tint()
    {
        targetIcon.color = tintColor;
    }

    public void Untint()
    {
        targetIcon.color = Color.white;
    }
}
