using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollowup : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 deltaVector;

    [SerializeField] float cameraSpeed = 0.125f;
    private Vector3 cameraVelocity = Vector3.zero;

    private void Start()
    {
        deltaVector = transform.position - player.position;
    }

    private void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, player.position + deltaVector, 0.9f);
        transform.position = Vector3.SmoothDamp(transform.position, player.position + deltaVector, ref cameraVelocity, cameraSpeed);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -25, 25), Mathf.Clamp(transform.position.y, 0, 40), transform.position.z);
    }
}
