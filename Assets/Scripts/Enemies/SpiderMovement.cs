using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float pounceDistance;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    EnemyController controller;

    AudioSource m_Audio;

    bool pouncing = false;

    float m_TargetDistance;
    Vector3 m_TargetDirection;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        controller = GetComponent<EnemyController>();
        m_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        m_TargetDistance = (controller.attackTarget.transform.position - transform.position).magnitude;
        m_TargetDirection = Vector3.Normalize(controller.attackTarget.transform.position - transform.position);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_TargetDirection, turnSpeed * Time.fixedDeltaTime, 0f);
        desiredForward.y = 0;
        m_Rotation = Quaternion.LookRotation(desiredForward.normalized);

        float targetAngle = Vector3.Angle(m_TargetDirection, transform.forward);
        if((m_TargetDistance <= pounceDistance) && (targetAngle < 1) && !pouncing)
        {
            pouncing = true;
            m_Animator.SetTrigger("Pounce");
            m_Audio.Play();
        }
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Animator.deltaPosition);

        if (!pouncing)
        {
            m_Rigidbody.MoveRotation(m_Rotation);
        }
    }

    public void StartPounce()
    {
        controller.deadly = true;
    }

    public void EndPounce()
    {
        pouncing = false;
        controller.deadly = false;
    }
}
