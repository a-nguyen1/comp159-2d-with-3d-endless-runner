using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 10.0f;
    [SerializeField] private float yOffset = 2.0f;
    //[SerializeField] private float xOffset = 5.0f;
    [SerializeField] private float fixedXPosition = 0.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y = player.position.y + yOffset; // Only follow the y-position
        targetPosition.x = fixedXPosition; // Keep the x-position fixed

        // Interpolate smoothly towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition,
            followSpeed * Time.deltaTime);
    }
}
