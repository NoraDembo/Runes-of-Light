using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    EnemyController controller;
    AudioSource m_Audio;

    Vector3 m_TargetDirection;
    Quaternion m_Rotation = Quaternion.identity;

    Vector3 m_CollisionNormal;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        controller = GetComponent<EnemyController>();
        m_Audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        m_TargetDirection = Vector3.Normalize(controller.attackTarget.transform.position - transform.position);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_TargetDirection, turnSpeed * Time.fixedDeltaTime, 0f);
        desiredForward.y = 0;
        m_Rotation = Quaternion.LookRotation(desiredForward.normalized);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Animator.deltaPosition);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 8)
        {
            m_Animator.SetTrigger("Bounce");
            m_Audio.Play();
            m_CollisionNormal = collision.GetContact(0).normal;
        }
    }

    public void Bounce()
    {
        m_Rotation = Quaternion.LookRotation(Vector3.Reflect(transform.forward, m_CollisionNormal));
    }
}
