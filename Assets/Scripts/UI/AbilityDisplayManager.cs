using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplayManager : MonoBehaviour
{
    public CanvasGroup[] abilityIcons;

    Color readyColor = new Color(1, 0.95f, 0.55f, 1);

    Image[] iconBorder;
    Image[] iconIcon;
    SpriteRenderer[] ingameRune;
    Light[] ingameLight;

    private void Awake()
    {
        iconBorder = new Image[abilityIcons.Length];
        iconIcon = new Image[abilityIcons.Length];
        ingameRune = new SpriteRenderer[abilityIcons.Length];
        ingameLight = new Light[abilityIcons.Length];

        for(int i = 0; i<5; i++)
        {
            iconBorder[i] = abilityIcons[i].transform.Find("Border").GetComponent<Image>();
            iconIcon[i] = abilityIcons[i].transform.Find("Icon").GetComponent<Image>();
            ingameRune[i] = GameObject.Find("Rune" + i).GetComponent<SpriteRenderer>();
            ingameLight[i] = GameObject.Find("Rune" + i).GetComponent<Light>();
        }
    }

    public void DisableIcon(int icon)
    {
        abilityIcons[icon].alpha = 0.5f;
        ingameLight[icon].enabled = false;
    }

    public void SetIcon(int ability, Sprite icon)
    {
        iconIcon[ability].sprite = icon;
        iconIcon[ability].enabled = true;
        ingameRune[ability].sprite = icon;
    }

    public void SetCooldown(int ability, float alpha)
    {
        if(alpha < 1)
        {
            iconBorder[ability].color = new Color(1, 1, 1, alpha);
            iconIcon[ability].color = new Color(1, 1, 1, alpha);
            ingameRune[ability].color = new Color(0.3f, 0.3f, 0.3f, 1f);
            ingameLight[ability].enabled = false;
        }
        else
        {
            iconBorder[ability].color = readyColor;
            iconIcon[ability].color = readyColor;
            ingameRune[ability].color = readyColor;
            ingameLight[ability].enabled = true;
        }
    }
}
