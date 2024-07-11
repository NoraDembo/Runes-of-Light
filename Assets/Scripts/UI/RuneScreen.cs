using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneScreen : MonoBehaviour
{
    public GameObject runePrefab;
    GameObject runeList;
    GameObject description;

    public Image[] selectedAbilityIcons;
    int activeSlot;

    void Start()
    {
        runeList = transform.Find("Rune List").gameObject;
        description = transform.Find("DescriptionBox").gameObject;

        LoadRunes();
    }

    public void SelectSlot(int i)
    {
        activeSlot = i;
    }

    public void WriteRuneIntoSlot(Ability a)
    {
        int previousSlot = FindAlreadyEquippedRune(a);

        if(previousSlot != -1)
        {
            GameManager.selectedAbilities[previousSlot] = GameManager.selectedAbilities[activeSlot];
            selectedAbilityIcons[previousSlot].sprite = GameManager.selectedAbilities[activeSlot].Icon;
            selectedAbilityIcons[previousSlot].color = new Color(1f, 0.95f, 0.55f, 1f);
        }

        GameManager.selectedAbilities[activeSlot] = a;
        selectedAbilityIcons[activeSlot].sprite = a.Icon;
        selectedAbilityIcons[activeSlot].color = new Color(1f, 0.95f, 0.55f, 1f);
    }

    int FindAlreadyEquippedRune(Ability a)
    {
        for(int i = 0; i <= 4; i++)
        {
            if(GameManager.selectedAbilities[i] == a)
            {
                return i;
            }
        }

        return -1;
    }

    public void LoadRunes()
    {

        for (int i = 0; i < runeList.transform.childCount; i++)
        {
            Destroy(runeList.transform.GetChild(i).gameObject);
        }

        foreach (Ability a in AbilityManager.allAbilities)
        {
            GameObject entry = Instantiate(runePrefab);
            entry.transform.SetParent(runeList.transform, false);

            RuneListElement rle = entry.GetComponent<RuneListElement>();
            rle.SetValues(a);
            rle.descriptionElement = description;
            rle.runeScreen = this;
        }

        for (int i = 0; i < GameManager.selectedAbilities.Length; i++)
        {
            selectedAbilityIcons[i].sprite = GameManager.selectedAbilities[i].Icon;
            selectedAbilityIcons[i].color = new Color(1f, 0.95f, 0.55f, 1f);
        }
    }
}
