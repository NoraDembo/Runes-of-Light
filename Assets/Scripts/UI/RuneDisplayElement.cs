using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneDisplayElement : MonoBehaviour
{
    Ability refAbility;
    Image runeIcon;
    

    public GameObject tooltipPrefab;

    Color unlockedColor = new Color(1, 0.95f, 0.55f, 1);
    Color lockedColor = new Color(0.5f, 0.5f, 0.5f, 1);

    GameObject tooltip;


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
        tooltip = Instantiate(tooltipPrefab);
        tooltip.transform.SetParent(transform, false);

        tooltip.GetComponent<RuneTooltip>().SetValues(refAbility);
    }

    public void DestroyTooltip()
    {
        Destroy(tooltip);
    }

}
