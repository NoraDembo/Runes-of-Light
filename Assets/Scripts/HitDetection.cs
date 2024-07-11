using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    int counter;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().Hit();
            counter++;
        }
    }

    void OnEnable()
    {
        counter = 0;
    }

    void OnDisable()
    {
        GameManager.ReportMultiHit(counter);
    }
}
