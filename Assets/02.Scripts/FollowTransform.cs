using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float speed = 5;
    private Vector3 currentVelocity;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _transform.position, ref currentVelocity, 0.1f, speed);
    }
}
