using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacter : MonoBehaviour
{
    Animator m_Animator;
    public Transform head;
    public RectTransform targetRect;

    Vector3 mouse;
    bool isAtWall = false;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(targetRect, Input.mousePosition, Camera.main, out mouse);
    }


    void LateUpdate()
    {
        if (isAtWall)
        {
            head.forward = Vector3.Lerp(Vector3.Normalize(head.forward - mouse), head.forward, 0.5f);
        }
    }

    public void WalkToWall()
    {
        m_Animator.SetTrigger("WalkToWall");
    }

    public void WalkBack()
    {
        m_Animator.SetTrigger("WalkBack");
    }

    public void IsLooking(int i)
    {
        isAtWall = i == 1;
    }
}
