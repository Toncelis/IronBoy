using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] float xSpeed;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.right * xSpeed;
    }
}
