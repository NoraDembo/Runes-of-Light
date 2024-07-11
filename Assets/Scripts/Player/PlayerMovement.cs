using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float turnSpeed = 20f;
    public float turnThreshold = 0.3f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    GameObject m_ChestBone;

    public static Vector3 mousePosition;

    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Quaternion m_ChestRotation = Quaternion.identity;
    Vector3 m_MouseDirection;
    Vector3 m_FaceDirection;

    ActionManager m_ActionManager;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_ChestBone = GameObject.Find("/Character/Armature/Root/Waist/ChestRotator");
        m_ActionManager = GetComponent<ActionManager>();
    }

    private void Update()
    {

        //find mouse position on ground plane and direction from player to mouse
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, 100, LayerMask.GetMask("Ground")))
        {
            mousePosition = hit.point;
            Vector3 tempDir = mousePosition - transform.position;
            if (tempDir.magnitude > turnThreshold)
            {
                m_MouseDirection = tempDir;
                m_MouseDirection.Normalize();
            }
            
        }

        
        //Calculate move direction
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isMoving = (horizontal != 0 || vertical != 0);
        m_Animator.SetBool("IsMoving", isMoving);

        //Set Facing of the GameObject
        if (!m_ActionManager.GetStun())
        {
            if (isMoving)
            {
                m_Movement.Set(horizontal, 0f, vertical);
                m_Movement.Normalize();

                float moveAngle = Vector3.SignedAngle(m_Movement, m_MouseDirection, Vector3.up);
                m_Animator.SetFloat("MoveAngle", moveAngle);
                m_FaceDirection = m_Movement;

            }
            else
            {
                m_FaceDirection = m_MouseDirection;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_ActionManager.GetStun())
        {
            m_ChestRotation = Quaternion.LookRotation(Vector3.RotateTowards(m_ChestBone.transform.forward, m_MouseDirection, turnSpeed * Time.deltaTime, 0f));

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_FaceDirection, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);
        }
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    private void LateUpdate()
    {
        m_ChestBone.transform.rotation = m_ChestRotation;
    }

    public void RotateToMouse()
    {
        m_Rotation = Quaternion.LookRotation(m_MouseDirection);
        m_Movement = m_MouseDirection;
    }
}
