using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTarget : MonoBehaviour
{

    public float lookAhead;
    public float z_Offset;

    public GameObject attatchTo;

    static Vector3 currentPosition;

    CinemachineVirtualCamera cam;

    private void Start()
    {
        cam = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = calculatePosition();
    }

    Vector3 calculatePosition()
    {
        Vector3 followPosition = attatchTo.transform.position + Vector3.forward * z_Offset;
        followPosition.y = 0;

        Vector3 desiredPosition = (followPosition + PlayerMovement.mousePosition) / 2;


        if (Vector3.Magnitude(desiredPosition - followPosition) > lookAhead)
        {
            currentPosition = followPosition + Vector3.Normalize(desiredPosition - followPosition) * lookAhead;
        }
        else
        {
            currentPosition = desiredPosition;
        }


        return currentPosition;
    }

}
