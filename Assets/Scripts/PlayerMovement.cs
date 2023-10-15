using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isJumping;
    private Rigidbody rb;
    [SerializeField] private LayerMask whatIsGround;
    //[SerializeField] private float maxJumpHeight;
    //private RaycastHit info;
    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        if (!isJumping && OnGround()) //Being OnGround implies that you canJump
        {
            isJumping = true;
            Jump();
        }
        else
        {
            isJumping = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            //Add Force to RB
            //Ideally, this will be a multi-stage jump depending on how long you hold down your jump key.
            //Thus a parabola with differing jump heights
            //But how easy is it to manipulate force and gravity to achieve this?
            rb.AddForce(Vector3.up * 3,ForceMode.Impulse);
            ///throw new Exception("Unimplemented");
        }
    }
    private bool OnGround()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        return Physics.SphereCast(ray, 0.1f, 1f, whatIsGround);
    }
    
    
    /*
    private bool IsPlatformBeneath()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        bool temp = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, whatIsGround);
        info = hit;
        return temp;
    }

    private void SnapCharacterToPlatform()
    {
        if (IsPlatformBeneath() && OnGround())
        {
            transform.position = new Vector3(transform.position.x, info.transform.position.y + 0.1f, transform.position.z);
            //Set Gravity to 0 as to make sure the Player won't fall and repeatedly cause snapping
        }
    }
    */
    
}
