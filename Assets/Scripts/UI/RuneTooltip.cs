using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneTooltip : MonoBehaviour
{
    Text title;
    Text description;
    Text unlockText;
    Image unlockIcon;

    Color unlockedColor = new Color(1, 0.95f, 0.55f, 1);
    Color lockedColor = new Color(0.8f, 0.8f, 0.8f, 1);

    public Sprite iconLocked;
    public Sprite iconUnlocked;

    void Awake()
    {
        title = transform.Find("Header").Find("Title").GetComponent<Text>();
        description = transform.Find("Body").Find("Description").GetComponent<Text>();
        unlockText = transform.Find("Body").Find("UnlockCondition").GetComponent<Text>();
        unlockIcon = transform.Find("Body").Find("UnlockIcon").GetComponent<Image>();
    }

    public void SetValues(Ability a)
    {
        title.text = a.AbilityName;
        description.text = a.Description;
        unlockText.text = a.UnlockText;

        if (a.IsUnlocked)
        {
            unlockText.color = unlockedColor;
            unlockIcon.sprite = iconUnlocked;
            unlockIcon.color = unlockedColor;
        }
        else
        {
            unlockText.color = lockedColor;
            unlockIcon.sprite = iconLocked;
            unlockIcon.color = lockedColor;
        }
    }
    
}
