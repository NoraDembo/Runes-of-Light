using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Transform mainPos;
    public Transform subPos;

    public float smoothing;
    Transform targetPos;

    void Start()
    {
        targetPos = mainPos;
    }


    void FixedUpdate()
    {
        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetPos.position, smoothing * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos.forward), smoothing * Time.deltaTime);
    }

    public void SetPosition(string pos)
    {
        switch (pos)
        {
            case "main":
                targetPos = mainPos;
                break;
            case "sub":
                targetPos = subPos;
                break;
        }
    }
}
