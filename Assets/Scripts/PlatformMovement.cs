using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    //Can be integrated with PlatformController once finished.

    [SerializeField] float accel = 1f;
    private float _speed = 0f;

    private void Update()
    {
        _speed += accel * Time.deltaTime * 0.5f;
        transform.position += Vector3.left * (_speed * Time.deltaTime);
        _speed += accel * Time.deltaTime * 0.5f;
    }

    public float GetPlatformSpeed()
    {
        return _speed;
    }
}
