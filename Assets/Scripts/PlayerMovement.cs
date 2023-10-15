using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private bool isJumping;
    private Rigidbody rb;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float fullHopFastFallMultiplier;
    [SerializeField] private float shortHopFastFallMultiplier;
    private bool jumpRequest;
    private bool isHoldingJump;

    //private float playerMaxJumpHeight;
    //private RaycastHit info;
    // Start is called before the first frame update
    void Start()
    {
        isHoldingJump = false;
        jumpRequest = false;
        isJumping = false;
        rb = GetComponent<Rigidbody>();
        //playerMaxJumpHeight = transform.position.y + maxJumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        JumpRequest();
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            HandleJump();
        }
        ScaleGravity();
    }

    private void JumpRequest()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            jumpRequest = true;
            isHoldingJump = true;
        }
        else
        {
            jumpRequest = false;
            isHoldingJump = false;
        }
    }

    private void HandleJump()
    {
        if (!isJumping && IsOnGround()) //Being OnGround implies that you canJump
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
            rb.AddForce(Vector3.up * maxJumpHeight,ForceMode.Impulse);
            isHoldingJump = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);
            ///throw new Exception("Unimplemented");
        }
    }

    private void ScaleGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += new Vector3(0, Physics.gravity.y * fullHopFastFallMultiplier * Time.deltaTime, 0);
        } 
        else if (rb.velocity.y > 0 && !isHoldingJump)
        {
            rb.velocity += new Vector3(0, Physics.gravity.y * shortHopFastFallMultiplier * Time.deltaTime, 0);
        }
    }
    private bool IsOnGround()
    {
        //playerMaxJumpHeight = transform.position.y + maxJumpHeight;
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
