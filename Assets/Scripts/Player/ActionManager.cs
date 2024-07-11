using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    Animator m_Animator;
    GameObject m_WeaponHitbox;
    AbilityDisplayManager m_Display;
    AudioSource hitSound;

    public Ability[] abilities;

    bool stunned;

    void Start()
    {
        Physics.IgnoreLayerCollision(9, 10, false);

        m_Animator = GetComponent<Animator>();
        m_WeaponHitbox = GameObject.Find("SwordHitbox");
        m_WeaponHitbox.SetActive(false);
        hitSound = GetComponent<AudioSource>();

        abilities = GameManager.selectedAbilities;
        m_Display = GameObject.Find("AbilityIcons").GetComponent<AbilityDisplayManager>();


        for(int i = 0; i < abilities.Length; i++)
        {
            if(abilities[i] == AbilityManager.emptyAbility)
            {
                m_Display.DisableIcon(i);
            }
            else
            {
                m_Display.SetIcon(i, abilities[i].Icon);
                abilities[i].CooldownTimer = abilities[i].Cooldown;
            }
        }
    }

    void Update()
    {
        if (!stunned)
        {
            getInput();
        }

        for(int a = 0; a < abilities.Length; a++)
        {
            if (abilities[a] != AbilityManager.emptyAbility)
            {
                abilities[a].CoolDown(Time.deltaTime);
                m_Display.SetCooldown(a, abilities[a].CooldownTimer / abilities[a].Cooldown);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.layer == 9)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();
            if (enemy.deadly)
            {
                Die(hit.transform.position);
            }else if (enemy.bouncy)
            {
                Bounce();
                hitSound.Play();
            }


        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.layer == 9 && hit.GetComponent<EnemyController>().deadly)
        {
            Die(hit.transform.position);
        }
    }

    void getInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Animator.SetTrigger("Attack");
        }

        if (Input.GetMouseButtonDown(1) && abilities[0] != AbilityManager.emptyAbility)
        {
            abilities[0].UseAbility(m_Animator);
        }

        if (Input.GetKeyDown("1") && abilities[1] != AbilityManager.emptyAbility)
        {
            abilities[1].UseAbility(m_Animator);
        }

        if (Input.GetKeyDown("2") && abilities[2] != AbilityManager.emptyAbility)
        {
            abilities[2].UseAbility(m_Animator);
        }

        if (Input.GetKeyDown("3") && abilities[3] != AbilityManager.emptyAbility)
        {
            abilities[3].UseAbility(m_Animator);
        }

        if (Input.GetKeyDown("4") && abilities[4] != AbilityManager.emptyAbility)
        {
            abilities[4].UseAbility(m_Animator);
        }
    }


    public void SetSword(int a)
    {
        m_WeaponHitbox.SetActive(a == 1);
    }

    public void SetStun(int a) {
        stunned = (a == 1);
    }

    public void SetInvincible(int i)
    {
        Physics.IgnoreLayerCollision(9, 10, i==1);
    }

    public bool GetStun()
    {
        return stunned;
    }

    public void Bounce()
    {
        m_Animator.SetTrigger("Bounce");
    }

    public void Die(Vector3 causeDirection)
    {
        SetStun(1);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(causeDirection - transform.position));
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<PlayerMovement>().enabled = false;
        Physics.IgnoreLayerCollision(9, 10, true);
        m_Animator.SetTrigger("Die");
        GameObject.Find("DeathFlash").GetComponent<DeathFlashScript>().Flash();
        GameManager.GameOver();
    }

    public void InstantiateEffect(GameObject e)
    {
        GameObject effect = Instantiate(e, transform.position, transform.rotation);
    }
}


