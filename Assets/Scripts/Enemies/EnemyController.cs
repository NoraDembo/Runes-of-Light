using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string enemyType;
    public int hp;
    public bool deadly;
    public bool bouncy;

    public GameObject deathEffect;
    public GameObject attackTarget;


    public void Hit()
    {
        hp--;

        if(hp <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        SpawnManager.spawnedEnemies--;
        GameManager.ReportKill(this);
        Destroy(gameObject);
    }

    public void SetDeadly(int i)
    {
        deadly = (i == 1);
    }
}

