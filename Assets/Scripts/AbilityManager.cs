using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Ability[] AbilititesInput;
    public static Ability[] allAbilities;

    public Ability emptyAbilityInput;
    public static Ability emptyAbility;

    UnlockPopup popup;

    void Awake()
    {
        allAbilities = AbilititesInput;
        emptyAbility = emptyAbilityInput;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.ingame)
        {
            CheckForUnlocks();
        }
    }

    public static Ability GetAbilityByName(string abilityName)
    {
        if(abilityName == "Empty")
        {
            return emptyAbility;
        }

        for (int i = 0; i < allAbilities.Length; i++)
        {
            if (allAbilities[i].AbilityName == abilityName)
            {
                return allAbilities[i];
            }
        }

        return emptyAbility; //fallback
    }

    void CheckForUnlocks()
    {
        foreach(Ability a in allAbilities)
        {
            if (!a.IsUnlocked)
            {

                bool unlockSuccessful = false;

                switch (a.UnlockCondition)
                {
                    case "Kills":
                        if(GameManager.sessionKills >= a.UnlockValue)
                        {
                            unlockSuccessful = true;
                        }
                        break;
                    case "MultiHit":
                        if(GameManager.sessionLargestMultiHit >= a.UnlockValue)
                        {
                            unlockSuccessful = true;
                        }
                        break;
                    case "Time":
                        if(Time.timeSinceLevelLoad >= a.UnlockValue)
                        {
                            unlockSuccessful = true;
                        }
                        break;
                    case "SpecificKills":
                        if (GameManager.GetKillCountByName(a.SecondaryUnlockValue) >= a.UnlockValue)
                        {
                            unlockSuccessful = true;
                        }
                        break;
                }

                if (unlockSuccessful)
                {
                    a.IsUnlocked = true;
                    GameManager.unlockedAbilities.Add(a);
                    popup = GameObject.Find("UnlockPopup").GetComponent<UnlockPopup>();
                    popup.Show(a.Icon);
                }
            }
        }
    }
}
