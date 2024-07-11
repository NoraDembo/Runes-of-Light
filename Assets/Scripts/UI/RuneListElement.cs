using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneListElement : MonoBehaviour
{
    Ability refAbility;

    Image runeIcon;

    public GameObject descriptionElement;
    public RuneScreen runeScreen;

    Color unlockedColor = new Color(1, 0.95f, 0.55f, 1);
    Color lockedColor = new Color(0.5f, 0.5f, 0.5f, 1);

    void Awake()
    {
        runeIcon = transform.Find("Icon").GetComponent<Image>();

    }

    public void SetValues(Ability a)
    {
        refAbility = a;

        runeIcon.sprite = refAbility.Icon;


        if (refAbility.IsUnlocked)
        {
            runeIcon.color = unlockedColor;
        }
        else
        {
            runeIcon.color = lockedColor;
        }
    }

    public void ShowTooltip()
    {
        descriptionElement.GetComponent<RuneTooltip>().SetValues(refAbility);
        descriptionElement.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void HideTooltip()
    {
        descriptionElement.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void WriteRune()
    {
        if (refAbility.IsUnlocked)
        {
            runeScreen.WriteRuneIntoSlot(refAbility);
        }
    }
}
