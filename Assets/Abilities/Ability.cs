using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityRune", menuName = "AbilityRune")]
public class Ability : ScriptableObject
{

    #region Fields

    [SerializeField]
    private Sprite rune;

    [SerializeField]
    private string abilityName;

    [SerializeField]
    private string description;

    [SerializeField]
    private string unlockCondition;

    [SerializeField]
    private int unlockValue;

    [SerializeField]
    private string secondaryUnlockValue;

    [SerializeField]
    private bool isUnlocked;

    [SerializeField]
    private string unlockText;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private float cooldownTimer;

    #endregion

    #region CONSTRUCTOR

    #endregion

    #region Properties

    public Sprite Icon
    {
        get { return rune; }
        set { rune = value; }
    }

    public string AbilityName
    {
        get { return abilityName; }
        set { abilityName = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string UnlockCondition
    {
        get { return unlockCondition; }
        set { unlockCondition = value; }
    }

    public int UnlockValue
    {
        get { return unlockValue;  }
        set { unlockValue = value;  }
    }

    public string SecondaryUnlockValue
    {
        get { return secondaryUnlockValue; }
        set { secondaryUnlockValue = value; }
    }

    public bool IsUnlocked
    {
        get { return isUnlocked; }
        set { isUnlocked = value; }
    }

    public string UnlockText
    {
        get { return unlockText; }
        set { unlockText = value; }
    }

    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }

    public float CooldownTimer
    {
        get { return cooldownTimer; }
        set { cooldownTimer = value; }
    }

    #endregion

    #region Methods

    public void UseAbility(Animator a)
    {
        if (cooldownTimer >= cooldown)
        {
            cooldownTimer = 0;
            a.SetTrigger(abilityName);
        }

    }

    public void CoolDown(float deltaTime)
    {
        if(cooldownTimer < cooldown)
        {
            cooldownTimer += deltaTime;
        }
    }

    #endregion

}